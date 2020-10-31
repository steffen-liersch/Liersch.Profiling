/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

namespace Liersch.Profiling
{
  interface IStopwatch
  {
    long ElapsedTicks { get; }

    void Restart();
  }
}