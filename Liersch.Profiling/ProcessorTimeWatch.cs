/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Linq;

namespace Liersch.Profiling
{
  sealed class ProcessorTimeWatch : IStopwatch, IDisposable
  {
    public long TotalProcessorTime
    {
      get
      {
        return m_Process.Threads.Cast<ProcessThread>().Sum(x => x.TotalProcessorTime.Ticks);
      }
    }

    public long ElapsedTicks
    {
      get
      {
        return unchecked(TotalProcessorTime-m_InitialTicks);
      }
    }

    public ProcessorTimeWatch()
    {
      m_Process=Process.GetCurrentProcess();
      Restart();
    }

    public void Restart()
    {
      m_InitialTicks=TotalProcessorTime;
    }

    public void Dispose()
    {
      if(m_Process!=null)
      {
        m_Process.Dispose();
        m_Process=null;
      }
    }

    Process m_Process;
    long m_InitialTicks;
  }
}