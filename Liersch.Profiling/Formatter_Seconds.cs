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
    public static string FormatSeconds(double value, int precision)
    {
      return FormatValue(value, precision, c_DefaultExtraFactor, null, m_TimeUnits);
    }

    public static string FormatSeconds(double value, int precision, double extraFactor)
    {
      return FormatValue(value, precision, extraFactor, null, m_TimeUnits);
    }

    static readonly FormatterUnit[] m_TimeUnits=new[]
    {
      new FormatterUnit(1e-9, "ns", "Nanosecond"),
      new FormatterUnit(1e-6, "µs", "Microsecond"),
      new FormatterUnit(1e-3, "ms", "Millisecond"),
      new FormatterUnit(1, "s", "Second"),
      new FormatterUnit(60, "min", "Minute"),
      new FormatterUnit(60*60, "h", "Hour"),
      new FormatterUnit(60*60*24, "d", "Day"),
      new FormatterUnit(60*60*24*7, "w", "Week"),
      new FormatterUnit(60*60*24*30, "M", "Month"),
      new FormatterUnit(60*60*24*365.25, "a", "Year"),
    };
  }
}