/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Profiling.Tests
{
  [TestClass]
  public sealed class StandardTests
  {
    [TestMethod]
    public void TestDoubleFormat()
    {
      Assert.AreEqual("0", FormatDouble(0.0, "G0"));
      Assert.AreEqual("0", FormatDouble(0.0, "G1"));
      Assert.AreEqual("0", FormatDouble(0.0, "G2"));
      Assert.AreEqual("0.13", FormatDouble(0.1251, "G2"));
      Assert.AreEqual("0.125", FormatDouble(0.1251, "G3"));
      Assert.AreEqual("1.13", FormatDouble(1.1251, "G3"));
      Assert.AreEqual("1.125", FormatDouble(1.1251, "G4"));
    }

    static string FormatDouble(double value, string format) { return value.ToString(format, CultureInfo.InvariantCulture); }
  }
}