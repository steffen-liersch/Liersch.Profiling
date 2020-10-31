/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Globalization;

namespace Liersch.Profiling
{
  public static partial class Formatter
  {
    public static string FormatValue(double value, int precision, params FormatterUnit[] units)
    {
      return FormatValue(value, precision, c_DefaultExtraFactor, null, units);
    }

    public static string FormatValue(double value, int precision, double extraFactor, params FormatterUnit[] units)
    {
      return FormatValue(value, precision, extraFactor, null, units);
    }

    public static string FormatValue(double value, int precision, double extraFactor, string baseUnit, params FormatterUnit[] units)
    {
      FormatterUnit best=GetBestUnit(value, extraFactor, units);
      double v=value/best.Factor;

      long z=(long)v;
      if(z!=0)
      {
        int preDecimalDigits=GetDigitCount(z);
        precision+=preDecimalDigits;
      }

      string s=v.ToString("G"+precision, CultureInfo.InvariantCulture);

      if(string.IsNullOrEmpty(baseUnit))
      {
        if(best.Unit.Length>0)
          s+=" "+best.Unit;
      }
      else
      {
        s+=" ";
        if(best.Unit.Length>0)
          s+=best.Unit;
        s+=baseUnit;
      }

      return s;
    }

    public static FormatterUnit GetBestUnit(double value, double extraFactor, FormatterUnit[] units)
    {
      int best=-1;
      double min=double.MaxValue;
      for(int i = 0; i<units.Length; i++)
      {
        FormatterUnit unit=units[i];

        if(best<0 && Math.Abs(1-unit.Factor)<=1e-7)
          best=i;

        double delta=value-unit.Factor*extraFactor;
        if(delta>=0 && delta<min)
        {
          min=delta;
          best=i;
        }
      }

      return units[best<0 ? 0 : best];
    }

    public static int GetDigitCount(long value)
    {
      // The negative value is used to avoid an overflow with long.MinValue.
      long v=value>0 ? -value : value;

      if(v>-1E10)
      {
        if(v>-1E5)
        {
          if(v>-1E1) return 1;
          if(v>-1E2) return 2;
          if(v>-1E3) return 3;
          if(v>-1E4) return 4;
          return 5;
        }
        else
        {
          if(v>-1E6) return 6;
          if(v>-1E7) return 7;
          if(v>-1E8) return 8;
          if(v>-1E9) return 9;
          return 10;
        }
      }
      else
      {
        if(v>-1E14)
        {
          if(v>-1E10) return 10;
          if(v>-1E11) return 11;
          if(v>-1E12) return 12;
          if(v>-1E13) return 13;
          return 14;
        }
        else
        {
          if(v>-1E15) return 15;
          if(v>-1E16) return 16;
          if(v>-1E17) return 17;
          if(v>-1E18) return 18;
          return 19;
        }
      }
    }

    const double c_DefaultExtraFactor=1.1;
  }
}