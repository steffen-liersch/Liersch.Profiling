/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Globalization;

namespace Liersch.Profiling
{
  /// <summary> Contains the result of a measurement in the form of duration and number of calls </summary>
  public sealed class MeasuringData
  {
    /// <summary> Measured duration for the iterations </summary>
    public TimeSpan Duration { get; private set; }

    /// <summary> Number of calls in the period </summary>
    public long Iterations { get; private set; }

    /// <summary> Relationship between the number of calls and the duration on a second basis </summary>
    public double Score { get { return Iterations/Duration.TotalSeconds; } }

    /// <summary> Generates a measured value </summary>
    /// <param name="duration"> Measured duration for the iterations </param>
    /// <param name="iterations"> Number of calls in the period </param>
    public MeasuringData(TimeSpan duration, long iterations)
    {
      Duration=duration;
      Iterations=iterations;
    }

    public override string ToString()
    {
      return
        Iterations.ToString(CultureInfo.InvariantCulture)+"@"+
        Duration.ToString()+
        " with score "+
        Score.ToString("0.#", CultureInfo.InvariantCulture);
    }
  }
}