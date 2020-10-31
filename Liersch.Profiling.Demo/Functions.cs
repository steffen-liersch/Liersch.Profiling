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
  static class Functions
  {
    public static int GetDigitCount_Simple(long value)
    {
      // The negative value is used to avoid an overflow with long.MinValue.
      long v=value>0 ? -value : value;

      if(v>-1E1) return 1;
      if(v>-1E2) return 2;
      if(v>-1E3) return 3;
      if(v>-1E4) return 4;
      if(v>-1E5) return 5;
      if(v>-1E6) return 6;
      if(v>-1E7) return 7;
      if(v>-1E8) return 8;
      if(v>-1E9) return 9;
      if(v>-1E10) return 10;
      if(v>-1E11) return 11;
      if(v>-1E12) return 12;
      if(v>-1E13) return 13;
      if(v>-1E14) return 14;
      if(v>-1E15) return 15;
      if(v>-1E16) return 16;
      if(v>-1E17) return 17;
      if(v>-1E18) return 18;
      return 19;
    }

    public static int GetDigitCount_Log10(long value)
    {
      switch(value)
      {
        case 0: return 1;
        case long.MinValue: return 19;
        default: return (int)Math.Log10(unchecked(value<0 ? -value : value))+1;
      }
    }


    public static void SleepMilliseconds(int milliseconds)
    {
      SleepTicks(TimeSpan.FromMilliseconds(milliseconds).Ticks);
    }

    public static void SleepTicks(long ticks)
    {
      var sw=Stopwatch.StartNew();
      while(true)
        if(sw.ElapsedTicks>=ticks)
          return;
    }


    public static void DoNothing()
    {
      // Empty function w/o code
    }

    public static void IncrementInteger()
    {
      unchecked
      {
        ++m_IntegerValue;
      }
    }

    public static int IncrementInteger2()
    {
      unchecked
      {
        return ++m_IntegerValue;
      }
    }

    static int m_IntegerValue;
  }
}