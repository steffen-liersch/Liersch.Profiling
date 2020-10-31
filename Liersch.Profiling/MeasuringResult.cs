/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Liersch.Profiling
{
  public sealed class MeasuringResult
  {
    public string Description { get; private set; }

    public IList<MeasuringData> Items { get; private set; }

    public MeasuringResult(string description, IEnumerable<MeasuringData> items)
    {
      Description=description;

      if(items!=null)
        Items=new ReadOnlyCollection<MeasuringData>(items.ToArray());
    }

    public override string ToString() { return Description; }
  }
}