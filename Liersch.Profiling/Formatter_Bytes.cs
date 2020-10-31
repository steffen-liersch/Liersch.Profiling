/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;

namespace Liersch.Profiling
{
  partial class Formatter
  {
    public static string FormatBytes(double value, int precision)
    {
      return FormatValue(value, precision, c_DefaultExtraFactor, null, m_SizeUnits);
    }

    public static string FormatBytes(double value, int precision, double extraFactor)
    {
      return FormatValue(value, precision, extraFactor, null, m_SizeUnits);
    }

    static readonly FormatterUnit[] m_SizeUnits=new[]
    {
      new FormatterUnit(1d/8, "Bit", "Bit"),
      new FormatterUnit(1, "B", "Byte"),
      new FormatterUnit(1<<10, "kB", "Kilobyte"),
      new FormatterUnit(1<<20, "MB", "Megabyte"),
      new FormatterUnit(Math.Pow(2, 30), "GB", "Gigabyte"),
      new FormatterUnit(Math.Pow(2, 40), "TB", "Terabyte"),
      new FormatterUnit(Math.Pow(2, 50), "PB", "Petabyte"),
      new FormatterUnit(Math.Pow(2, 60), "EB", "Exabyte"),
      new FormatterUnit(Math.Pow(2, 70), "ZB", "Zettabyte"),
      new FormatterUnit(Math.Pow(2, 80), "YB", "Yottabyte"),
    };
  }
}