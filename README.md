[![NuGet](https://img.shields.io/nuget/v/Liersch.Profiling.svg)](https://www.nuget.org/packages/Liersch.Profiling)

# Liersch.Profiling

The .NET library `Liersch.Profiling` includes functions for measuring the execution time of functions. All major .NET platforms are supported (from .NET Framework 4.5, from .NET Core 2.0 and from .NET Standard 2.0). All major changes are logged in the [CHANGELOG.md](https://github.com/steffen-liersch/Liersch.Profiling/blob/main/CHANGELOG.md) file.

## Time Measurement

The `MeasurePerformance` function of the `MeasuringTools` class determines the performance of the specified functions. A `MeasuringData` object is supplied for each function. The number of calls and the time required for them are stored in it. The time measurement is based on the `Stopwatch` class. The accuracy depends on the used hardware. With this type of measurement, the result is always influenced by the processes running at the same time and their threads.

```cs
Action a1=() => Thread.Sleep(10);
Action a2=() => Thread.Sleep(20);
MeasuringData[] md=MeasuringTools.MeasurePerformance(a1, a2);
Console.WriteLine("Thread.Sleep(10) => "+md[0].Format());
Console.WriteLine("Thread.Sleep(20) => "+md[1].Format());
```

## Measurement of the CPU Time

Alternatively, the `MeasureProcessorTime` function can be used to determine the processor time required by the process during the measurement. The influence of other processes running at the time of the measurement should be significantly less than with the `Stopwatch`-based approach.

`MeasureProcessorTime` cannot measure the time that is required to wait for certain events (e.g. `EventWaitHandle.Wait` or `Thread.Sleep`), as this type of waiting does not require CPU time.

## Measurement Automation

If several test functions are available, these can be called automatically. Test functions must be marked with the attribute `MeasuringAttribute` and return` MeasuringData` objects.

```cs
static class Tests
{
  [Measuring(1, "SleepTicks(1) vs. SleepTicks(2)")]
  public static MeasuringData[] TestSleepTicks()
  {
    Action a1=() => Functions.SleepTicks(1);
    Action a2=() => Functions.SleepTicks(2);
    return MeasuringTools.MeasurePerformance(a1, a2);
  }

  [Measuring(2, "SleepMilliseconds(1) vs. SleepMilliseconds(2)")]
  public static MeasuringData[] TestSleepMilliseconds()
  {
    Action a1=() => Functions.SleepMilliseconds(1);
    Action a2=() => Functions.SleepMilliseconds(2);
    return MeasuringTools.MeasurePerformance(a1, a2);
  }

  [Measuring(3, "Thread.Sleep(10) vs. Thread.Sleep(20)")]
  public static MeasuringData[] TestThreadSleep()
  {
    Action a1=() => Thread.Sleep(10);
    Action a2=() => Thread.Sleep(20);
    return MeasuringTools.MeasurePerformance(a1, a2);
  }
}
```

The `RunTests` function of the` MeasuringTools` class is used to automatically call test functions. The description texts and measurement results are printed on the console. If a class only contains static test functions or has a standard constructor, it is sufficient to pass the type instead of an instance.

```cs
MeasuringTools.RunTests(typeof(Tests));
```

## Measurement Accuracy

Regardless of the measurement method, there are various factors that affect a measurement. Functions that generate many objects are influenced, for example, by a garbage collection that may take place. For this reason, a garbage collection is forced before each measurement in order to create a reasonably comparable start situation. Other threads that are active on the same processor core can also falsify the measurement result.

## Value Formatting

The `Formatter` class contains functions for formatting unit-related values. The functions can also be used independently of the measuring functions. The current implementation of the formatting functions is generally culture-independent (see `CultureInfo.InvariantCulture`).

```cs
Console.WriteLine("Duration: "+Formatter.FormatSeconds(md.Duration.TotalSeconds, 1));
Console.WriteLine("Score: "+Formatter.FormatDecimal(md.Score, 1, "Hz"));
```

## Copyright

Copyright © 2020-2021 Steffen Liersch  
https://www.steffen-liersch.de/

## Links

The source code is maintained on GitHub:  
https://github.com/steffen-liersch/Liersch.Profiling

Packages can be downloaded through NuGet:  
https://www.nuget.org/packages/Liersch.Profiling
