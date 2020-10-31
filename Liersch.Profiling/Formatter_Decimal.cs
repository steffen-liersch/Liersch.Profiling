/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

namespace Liersch.Profiling
{
  partial class Formatter
  {
    public static string FormatDecimal(double value, int precision)
    {
      return FormatValue(value, precision, c_DefaultExtraFactor, null, m_DecimalUnits);
    }

    public static string FormatDecimal(double value, int precision, double extraFactor)
    {
      return FormatValue(value, precision, extraFactor, null, m_DecimalUnits);
    }

    public static string FormatDecimal(double value, int precision, double extraFactor, string baseUnit)
    {
      return FormatValue(value, precision, extraFactor, baseUnit, m_DecimalUnits);
    }

    public static string FormatDecimal(double value, int precision, string baseUnit)
    {
      return FormatValue(value, precision, c_DefaultExtraFactor, baseUnit, m_DecimalUnits);
    }

    static readonly FormatterUnit[] m_DecimalUnits=new[]
    {
      new FormatterUnit(1e-24, "y", "Yocto"),
      new FormatterUnit(1e-21, "z", "Zepto"),
      new FormatterUnit(1e-18, "a", "Atto"),
      new FormatterUnit(1e-15, "f", "Femto"),
      new FormatterUnit(1e-12, "p", "Pico"),
      new FormatterUnit(1e-9, "n", "Nano"),
      new FormatterUnit(1e-6, "µ", "Micro"),
      new FormatterUnit(1e-3, "m", "Milli"),
      new FormatterUnit(1e-2, "c", "Centi"),
      new FormatterUnit(1e-1, "d", "Deci"),
      new FormatterUnit(1, "", ""),
      new FormatterUnit(1e+3, "k", "Kilo"),
      new FormatterUnit(1e+6, "M", "Mega"),
      new FormatterUnit(1e+9, "G", "Giga"),
      new FormatterUnit(1e+12, "T", "Tera"),
      new FormatterUnit(1e+15, "P", "Peta"),
      new FormatterUnit(1e+18, "E", "Exa"),
      new FormatterUnit(1e+21, "Z", "Zetta"),
      new FormatterUnit(1e+24, "Y", "Yotta"),
    };
  }
}