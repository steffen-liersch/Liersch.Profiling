/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;

namespace Liersch.Profiling
{
  public struct FormatterUnit : IEquatable<FormatterUnit>
  {
    public double Factor { get; private set; }

    public string Unit { get; private set; }
    
    public string Name { get; private set; }

    public FormatterUnit(double factor, string unit, string name)
    {
      Factor=factor;
      Unit=unit;
      Name=name;
    }

    public override string ToString() { return Unit+", "+Name+", "+Factor; }

    public override int GetHashCode()
    {
      int res=Factor.GetHashCode();

      if(Unit!=null)
        res^=Unit.GetHashCode();

      if(Name!=null)
        res^=Name.GetHashCode();

      return res;
    }

    public bool Equals(FormatterUnit other) { return Equals(this, other); }

    public override bool Equals(object obj)
    {
      if(obj is FormatterUnit)
        return Equals(this, (FormatterUnit)obj);
      return false;
    }

    public static bool Equals(FormatterUnit x, FormatterUnit y)
    {
      return
        x.Factor==y.Factor &&
        x.Unit==y.Unit &&
        x.Name==y.Name;
    }

    public static bool operator ==(FormatterUnit x, FormatterUnit y) { return Equals(x, y); }

    public static bool operator !=(FormatterUnit x, FormatterUnit y) { return !Equals(x, y); }
  }
}
