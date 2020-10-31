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
  static class Demo2
  {
    [Measuring(1, "SleepTicks(1) vs. SleepTicks(2)")]
    public static MeasuringData[] TestSleepTicks()
    {
      Action a1=() => Functions.SleepTicks(1);
      Action a2=() => Functions.SleepTicks(2);
      return MeasuringTools.MeasurePerformance(a1, a2);
    }

    [Measuring(2, "SleepMilliseconds(1) vs. SleepMilliseconds(2)")]
    public static MeasuringData[] TestSleepMilliseconds()
    {
      Action a1=() => Functions.SleepMilliseconds(1);
      Action a2=() => Functions.SleepMilliseconds(2);
      return MeasuringTools.MeasurePerformance(a1, a2);
    }

    [Measuring(3, "Thread.Sleep(10) vs. Thread.Sleep(20)")]
    public static MeasuringData[] TestThreadSleep()
    {
      Action a1=() => Thread.Sleep(10);
      Action a2=() => Thread.Sleep(20);
      return MeasuringTools.MeasurePerformance(a1, a2);
    }
  }
}