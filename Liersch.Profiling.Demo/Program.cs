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
  static class Program
  {
    static void Main()
    {
      try
      {
        Console.WriteLine("Stopwatch based on high-resolution performance counter: "+(Stopwatch.IsHighResolution ? "yes" : "no"));
        Console.WriteLine("Stopwatch timer frequency: "+Formatter.FormatDecimal(Stopwatch.Frequency, 1, "Hz"));
        Console.WriteLine();

        Console.WriteLine("Demo 1");
        Demo1.Run();
        Console.WriteLine();

        Console.WriteLine("Demo 2");
        MeasuringTools.RunTests(typeof(Demo2));
        Console.WriteLine();

        var tests=new Demo3();

        Console.WriteLine("Demo 3 - MeasuringTools.MeasurePerformance");
        tests.MeasureExternal=MeasuringTools.MeasurePerformance;
        MeasuringTools.RunTests(tests);
        Console.WriteLine();

        Console.WriteLine("Demo 3 - MeasuringTools.MeasureProcessorTime");
        tests.MeasureExternal=MeasuringTools.MeasureProcessorTime;
        MeasuringTools.RunTests(tests);
      }
      catch(Exception e)
      {
        Console.WriteLine(e.ToString());
      }

      Console.WriteLine();
      Console.WriteLine("[Press any key!]");
      Console.ReadKey(true);
    }
  }
}