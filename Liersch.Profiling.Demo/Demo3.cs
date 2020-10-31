/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Diagnostics;

namespace Liersch.Profiling.Demo
{
  sealed class Demo3
  {
    public Func<Action[], MeasuringData[]> MeasureExternal;

    MeasuringData[] Measure(params Action[] actions) { return MeasureExternal(actions); }


    [Measuring(100, "Empty function vs. incrementing an integer")]
    public MeasuringData[] TestSimple1()
    {
      Action a1=Functions.DoNothing;
      Action a2=Functions.IncrementInteger;
      return Measure(a1, a2);
    }

    [Measuring(100, "Empty function vs. incrementing an integer (lambda)")]
    public MeasuringData[] TestSimple2()
    {
      Action a1=Functions.DoNothing;
      Action a2=() => Functions.IncrementInteger();
      return Measure(a1, a2);
    }

    [Measuring(100, "Normal code vs. code in try-catch")]
    public MeasuringData[] TestTryCatch()
    {
      Action a1=Functions.IncrementInteger;

      Action a2=() =>
      {
        try
        {
          Functions.IncrementInteger();
        }
        catch
        {
          // Do nothing here!
        }
      };

      return Measure(a1, a2);
    }

    [Measuring(100, "Unsynchronized vs. synchronized member access")]
    public MeasuringData[] TestLock()
    {
      object syncRoot=new object();

      Action a1=Functions.IncrementInteger;

      Action a2=() =>
      {
        lock(syncRoot)
          Functions.IncrementInteger();
      };

      return Measure(a1, a2);
    }


    [Measuring(200, "GetDigitCount - Optimized vs. Log10-based version")]
    public MeasuringData[] TestGetDigitCount_Log10()
    {
      int c1=0;
      int c2=0;

      Action a1=() => Formatter.GetDigitCount(unchecked(c1++));
      Action a2=() => Functions.GetDigitCount_Log10(unchecked(c2++));

      return Measure(a1, a2);
    }

    [Measuring(200, "GetDigitCount - Optimized vs. simple version")]
    public MeasuringData[] TestGetDigitCount_Simple()
    {
      int c1=0;
      int c2=0;

      Action a1=() => Formatter.GetDigitCount(unchecked(c1++));
      Action a2=() => Functions.GetDigitCount_Simple(unchecked(c2++));

      return Measure(a1, a2);
    }


    [Measuring(300, "Environment.TickCount vs. StopWatch.ElapsedTicks")]
    public MeasuringData[] TestTimers()
    {
      var sw=new Stopwatch();
      sw.Start();

      Action a1=() => Convert.ToInt32(Environment.TickCount);
      Action a2=() => Convert.ToInt64(sw.ElapsedTicks);

      return Measure(a1, a2);
    }

    [Measuring(300, "DateTime.UtcNow vs. DateTime.Now")]
    public MeasuringData[] TestDateTimeNow()
    {
      Action a1=() => Convert.ToInt64(DateTime.UtcNow.Ticks);
      Action a2=() => Convert.ToInt64(DateTime.Now.Ticks);

      return Measure(a1, a2);
    }


    [Measuring(400, "SleepMilliseconds(1) vs. SleepMilliseconds(2)")]
    public MeasuringData[] TestSleepMilliseconds1()
    {
      Action a1=() => Functions.SleepMilliseconds(1);
      Action a2=() => Functions.SleepMilliseconds(2);
      return Measure(a1, a2);
    }

    [Measuring(400, "SleepMilliseconds(10) vs. SleepMilliseconds(20)")]
    public MeasuringData[] TestSleepMilliseconds10()
    {
      Action a1=() => Functions.SleepMilliseconds(10);
      Action a2=() => Functions.SleepMilliseconds(20);
      return Measure(a1, a2);
    }

    [Measuring(400, "SleepMilliseconds(16) vs. SleepMilliseconds(32)")]
    public MeasuringData[] TestSleepMilliseconds16()
    {
      Action a1=() => Functions.SleepMilliseconds(16);
      Action a2=() => Functions.SleepMilliseconds(32);
      return Measure(a1, a2);
    }
  }
}