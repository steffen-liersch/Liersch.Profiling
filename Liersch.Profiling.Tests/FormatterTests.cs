/*--------------------------------------------------------------------------*\
::
::  Copyright © 2009-2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Profiling.Tests
{
  [TestClass]
  public sealed class FormatterTests
  {
    [TestMethod]
    public void TestGetDigitCount()
    {
      Assert.AreEqual(1, Formatter.GetDigitCount(0));
      Assert.AreEqual(1, Formatter.GetDigitCount(1));
      Assert.AreEqual(1, Formatter.GetDigitCount(9));
      Assert.AreEqual(2, Formatter.GetDigitCount(10));
      Assert.AreEqual(2, Formatter.GetDigitCount(19));
      Assert.AreEqual(2, Formatter.GetDigitCount(99));
      Assert.AreEqual(3, Formatter.GetDigitCount(100));
      Assert.AreEqual(3, Formatter.GetDigitCount(999));
      Assert.AreEqual(4, Formatter.GetDigitCount(1000));
      Assert.AreEqual(5, Formatter.GetDigitCount(12345));
      Assert.AreEqual(6, Formatter.GetDigitCount(123456));
      Assert.AreEqual(10, Formatter.GetDigitCount(1234567890));
      Assert.AreEqual(3, Formatter.GetDigitCount(-123));
      Assert.AreEqual(19, Formatter.GetDigitCount(long.MinValue+1));
      Assert.AreEqual(19, Formatter.GetDigitCount(long.MaxValue-1));
      Assert.AreEqual(19, Formatter.GetDigitCount(long.MinValue));
      Assert.AreEqual(19, Formatter.GetDigitCount(long.MaxValue));
    }

    [TestMethod]
    public void TestFormatBytes()
    {
      Assert.AreEqual("1 Bit", Formatter.FormatBytes(0.125, 2, 1));
      Assert.AreEqual("4 Bit", Formatter.FormatBytes(0.5, 2, 1));
      Assert.AreEqual("7.5 Bit", Formatter.FormatBytes(7d/8+1d/16, 2, 1));

      Assert.AreEqual("0 B", Formatter.FormatBytes(0, 2, 1));
      Assert.AreEqual("1 B", Formatter.FormatBytes(1, 2, 1));
      Assert.AreEqual("99 B", Formatter.FormatBytes(99, 2, 1));
      Assert.AreEqual("999 B", Formatter.FormatBytes(999, 2, 1));

      Assert.AreEqual("9.9 kB", Formatter.FormatBytes(9.9*1024, 2, 1));
      Assert.AreEqual("9.99 kB", Formatter.FormatBytes(9.99*1024, 2, 1));
      Assert.AreEqual("9.99 kB", Formatter.FormatBytes(9.991*1024, 2, 1));

      Assert.AreEqual("10 kB", Formatter.FormatBytes(10*1024, 2, 1));
      Assert.AreEqual("10.1 kB", Formatter.FormatBytes(10.1*1024, 2, 1));
      Assert.AreEqual("10.12 kB", Formatter.FormatBytes(10.12*1024, 2, 1));
      Assert.AreEqual("10.12 kB", Formatter.FormatBytes(10.123*1024, 2, 1));
    }

    [TestMethod]
    public void TestFormatDecimal()
    {
      Assert.AreEqual("1000", Formatter.FormatDecimal(1000, 2, 1.1));
      Assert.AreEqual("1099", Formatter.FormatDecimal(1099, 2, 1.1));
      Assert.AreEqual("1.1 k", Formatter.FormatDecimal(1100, 2, 1.1));
      Assert.AreEqual("1.23 k", Formatter.FormatDecimal(1234, 2, 1.1));
      Assert.AreEqual("1.24 k", Formatter.FormatDecimal(1236, 2, 1.1));
    }
  }
}