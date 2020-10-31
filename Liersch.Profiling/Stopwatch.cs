/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

namespace Liersch.Profiling
{
  sealed class Stopwatch : System.Diagnostics.Stopwatch, IStopwatch
  {
#if NET35
    public void Restart()
    {
      Reset();
      Start();
    }
#endif
  }
}