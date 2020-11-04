/*--------------------------------------------------------------------------*\
::
::  Copyright © 2020 Steffen Liersch
::  https://www.steffen-liersch.de/
::
\*--------------------------------------------------------------------------*/

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Liersch.Profiling.Tests
{
  [TestClass]
  public sealed class AttributeTests
  {
    [TestMethod]
    public void TestNonStaticClassFormInstance()
    {
      var x=MeasuringTools.EnumerateTestFunctions(new NonStaticClass()).ToList();
      Assert.AreEqual(2, x.Count);
      CheckAttribute(x[0], 1, "Static function");
      CheckAttribute(x[1], 2, "Non-static function");
    }

    [TestMethod]
    public void TestNonStaticClassFromType()
    {
      var x=MeasuringTools.EnumerateTestFunctions(typeof(NonStaticClass)).ToList();
      Assert.AreEqual(2, x.Count);
      CheckAttribute(x[0], 1, "Static function");
      CheckAttribute(x[1], 2, "Non-static function");
    }

    [TestMethod]
    public void TestStaticClass()
    {
      var x=MeasuringTools.EnumerateTestFunctions(typeof(StaticClass)).ToList();
      Assert.AreEqual(1, x.Count);
      CheckAttribute(x[0], 0, "Static function");
    }

    static void CheckAttribute(MeasuringAttribute attribute, int sortIndex, string description)
    {
      Assert.AreEqual(sortIndex, attribute.SortIndex);
      Assert.AreEqual(description, attribute.Description);
      Assert.IsNull(attribute.TestFunction());
    }

    static class StaticClass
    {
      [Measuring("Static function")]
      public static MeasuringData[] Test() { return null; }
    }

    class NonStaticClass
    {
      [Measuring(1, "Static function")]
      public static MeasuringData[] Test1() { return null; }

      [Measuring(2, "Non-static function")]
      public MeasuringData[] Test2()
      {
        Assert.IsNotNull(this);
        return null;
      }
    }
  }
}