/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;

namespace Liersch.Profiling
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public sealed class MeasuringAttribute : Attribute
  {
    public int SortIndex { get; set; }

    public string Description { get; set; }

    public Func<MeasuringData[]> TestFunction { get; set; }

    public MeasuringAttribute(string description) : this(0, description) { }

    public MeasuringAttribute(int sortIndex, string description)
    {
      SortIndex=sortIndex;
      Description=description;
    }
  }
}