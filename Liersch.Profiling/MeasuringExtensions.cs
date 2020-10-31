/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Liersch.Profiling
{
  public static class MeasuringExtensions
  {
    public static MeasuringResult ToResult(this IEnumerable<MeasuringData> items, string description)
    {
      return new MeasuringResult(description, items);
    }

    public static void Print(this IEnumerable<MeasuringResult> results, string descriptionFormat, string valueFormat)
    {
      foreach(MeasuringResult r in results)
        Print(r, descriptionFormat, valueFormat);
    }

    public static void Print(this MeasuringResult result, string descriptionFormat, string valueFormat)
    {
      ConsoleColor defaultFC=Console.ForegroundColor;
      double max=result.Items.Max(x => x.Score);
      Console.Write(descriptionFormat, result.Description);
      //double[] orderedScores=result.Items.Select(x => x.Score).OrderByDescending(x => x).ToArray();
      foreach(MeasuringData d in result.Items)
      {
        double score=d.Score;
        double percent=(score/max)*100;
        bool best=Math.Abs(score-max)<=1e-7;
        if(best)
          Console.ForegroundColor=ConsoleColor.Green;
        //int rank=Array.IndexOf(orderedScores, score)+1;
        Console.Write(valueFormat,
          percent.ToString("0", CultureInfo.InvariantCulture)+"% @ "+
          Formatter.FormatDecimal(score, 1, "Hz"));
        if(best)
          Console.ForegroundColor=defaultFC;
      }
      Console.WriteLine();
    }

    public static void Print(this MeasuringData data) { Console.WriteLine(Format(data)); }

    public static string Format(this MeasuringData data)
    {
      return string.Format(CultureInfo.InvariantCulture,
        "{0} iteration(s) in {1} with score {2}",
        data.Iterations,
        Formatter.FormatSeconds(data.Duration.TotalSeconds, 1),
        Formatter.FormatDecimal(data.Score, 1, "Hz"));
    }
  }
}