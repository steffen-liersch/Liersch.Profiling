/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Liersch.Profiling
{
  /// <summary> Various functions for measuring the performance of functions </summary>
  public static class MeasuringTools
  {
    /// <summary> Calls every function marked with an MeasuringAttribute and outputs the measurement result on the console </summary>
    /// <param name="instancesOrTypes"> Array of instances or types that should be searched for corresponding functions </param>
    public static void RunTests(params object[] instancesOrTypes)
    {
      var list=EnumerateTestFunctions(instancesOrTypes).OrderBy(x => x.SortIndex).ThenBy(x => x.Description).ToList();
      int c=list.Count;
      int dc=Formatter.GetDigitCount(c);

      const string sep=": ";
      int descLen=dc+sep.Length+list.Max(x => x.Description!=null ? x.Description.Length : 0);

      string f="d"+dc.ToString(CultureInfo.InvariantCulture);
      int i=0;
      while(i<c)
      {
        MeasuringAttribute ma=list[i++];
        MeasuringData[] md=ma.TestFunction();
        string s=i.ToString(f, CultureInfo.InvariantCulture)+sep+ma.Description;
        var mr=md.ToResult(s);
        mr.Print("{0,-"+descLen.ToString(CultureInfo.InvariantCulture)+"}  ", "{0,-17}  ");
      }
    }

    /// <summary> Returns an instance of MeasuringAttribute for each function marked with MeasuringAttribute </summary>
    /// <param name="instancesOrTypes"> Array of instances or types that should be searched for corresponding functions </param>
    /// <returns> Enumerable result </returns>
    public static IEnumerable<MeasuringAttribute> EnumerateTestFunctions(params object[] instancesOrTypes)
    {
      foreach(object z in instancesOrTypes)
        foreach(MeasuringAttribute ma in EnumerateTestFunctions(z))
          yield return ma;
    }

    /// <summary> Returns an instance of MeasuringAttribute for each function marked with MeasuringAttribute </summary>
    /// <param name="instanceOrType"> Instance or type that should be searched for corresponding functions </param>
    /// <returns> Enumerable result </returns>
    public static IEnumerable<MeasuringAttribute> EnumerateTestFunctions(object instanceOrType)
    {
      object inst;
      var type=instanceOrType as Type;
      if(type!=null)
        inst=null;
      else
      {
        inst=instanceOrType;
        type=instanceOrType.GetType();
      }

      MethodInfo[] mis=type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
      foreach(MethodInfo mi in mis)
      {
        object[] mas=mi.GetCustomAttributes(typeof(MeasuringAttribute), false);
        if(mas!=null && mas.Length>0)
        {
          if(mi.GetParameters().Length>0 || mi.ReturnType!=typeof(MeasuringData[]))
            throw new InvalidOperationException("Function has an unexpected signature ("+mi.DeclaringType.FullName+"."+mi.Name+")");

          Delegate d;
          if(mi.IsStatic)
            d=Delegate.CreateDelegate(typeof(Func<MeasuringData[]>), mi, true);
          else
          {
            if(inst==null)
              inst=Activator.CreateInstance(type);
            d=Delegate.CreateDelegate(typeof(Func<MeasuringData[]>), inst, mi, true);
          }

          var ma=(MeasuringAttribute)mas[0];
          ma.TestFunction=(Func<MeasuringData[]>)d;
          yield return ma;
        }
      }
    }


    /// <summary> This function calculates the performance on the basis of class Stopwatch. </summary>
    /// <param name="actions"> Array of all functions to be measured </param>
    /// <returns> Array of the measurement results corresponding to the given functions </returns>
    public static MeasuringData[] MeasurePerformance(params Action[] actions)
    {
      ThreadPriority last=Thread.CurrentThread.Priority;
      Thread.CurrentThread.Priority=ThreadPriority.Highest;
      try
      {
        int c=actions.Length;
        var res=new MeasuringData[c];
        var sw=new Stopwatch();
        for(int i = 0; i<c; i++)
          res[i]=MeasureAction1(actions[i], sw);

        return res;
      }
      finally
      {
        Thread.CurrentThread.Priority=last;
      }
    }

    /// <summary>
    /// This function calculates the performance on the basis of the
    /// used processor time considering all threads of the current process.
    /// The result is influenced by background actions of other threads.
    /// </summary>
    /// <param name="actions"> Array of all functions to be measured </param>
    /// <returns> Array of the measurement results corresponding to the given functions </returns>
    public static MeasuringData[] MeasureProcessorTime(params Action[] actions)
    {
      int c=actions.Length;
      var res=new MeasuringData[c];
      using(var sw = new ProcessorTimeWatch())
      {
        for(int i = 0; i<c; i++)
          res[i]=MeasureAction1(actions[i], sw);
      }

      return res;
    }

    static MeasuringData MeasureAction1(Action action, IStopwatch stopwatch)
    {
      long repCount=1;
      while(true)
      {
        long t=MeasureAction2(action, repCount, stopwatch) ;
        if(t>0)
          return new MeasuringData(TimeSpan.FromTicks(t), repCount);

        repCount=checked(repCount*2);
      }
    }

    static long MeasureAction2(Action action, long repetitionCount, IStopwatch stopwatch)
    {
      long best=long.MaxValue;
      for(int i = 0; i<c_LoopCount; i++)
      {
        long t=MeasureAction3(action, repetitionCount, stopwatch);

        if(t<c_MinimumDurationTicks)
          return -1;

        if(t<best)
          best=t;
      }

      return best;
    }

    static long MeasureAction3(Action action, long repetitionCount, IStopwatch stopwatch)
    {
      PerformGarbageCollection();
      action();

      stopwatch.Restart();

      for(long z = 0; z<repetitionCount; z++)
        action();

      return stopwatch.ElapsedTicks;
    }


    /// <summary> Used to ensure the same start conditions before a measurement </summary>
    public static void PerformGarbageCollection()
    {
      for(int i = 0; i<2; i++)
      {
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }
    }


    const long c_LoopCount=3;
    const long c_MinimumDurationTicks=10*TimeSpan.TicksPerMillisecond;
  }
}