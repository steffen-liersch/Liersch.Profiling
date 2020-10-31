/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Threading;

namespace Liersch.Profiling.Demo
{
  static class Demo1
  {
    public static void Run()
    {
      Action a1=() => Functions.SleepTicks(1000);
      Action a2=() => Thread.Sleep(10);
      Action a3=() => Thread.Sleep(20);

      MeasuringData[] md=MeasuringTools.MeasurePerformance(a1, a2, a3);

      Console.WriteLine("SleepTicks(1000) => "+md[0].Format());
      Console.WriteLine("Thread.Sleep(10) => "+md[1].Format());
      Console.WriteLine("Thread.Sleep(20) => "+md[2].Format());
    }
  }
}