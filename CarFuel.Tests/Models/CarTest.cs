﻿using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Should;

namespace CarFuel.Tests.Models {
  public class CarTest {

    public class General {

      [Fact]
      public void InitialValues() {
        var c = new Car(name: "My Jazz");

        //c.Name.ShouldBe("My Jazz");

        Assert.Equal("My Jazz", c.Name);
        Assert.Empty(c.FillUps);
      }
      

    }

    public class AddFillUpMethod {

      private readonly ITestOutputHelper _output; // read only define in only contructor

      public AddFillUpMethod(ITestOutputHelper output) {
        _output = output;
      }

      [Fact]
      public void AddFirstFillUp() {
        var c = new Car(name: "My Jazz");
        FillUp f = c.AddFillUp(odometer: 1000, liters:20.0);

        Assert.NotNull(f);
        Assert.Equal(1, c.FillUps.Count());
        Assert.Equal(1000, f.Odometer);
        Assert.Equal(20.0, f.Liters);
      }

      [Fact]
      public void AddTwoFillUpsAndThemShouldBeChainedCorrectly() {
        var c = new Car(name: "My Jazz");
        FillUp f1 = c.AddFillUp(1000, 50);
        FillUp f2 = c.AddFillUp(2000, 60);
        FillUp f3 = c.AddFillUp(2500, 20);

        Dump(c);

        f1.NextFillUp.ShouldBeSameAs(f2);
        f2.NextFillUp.ShouldBeSameAs(f3);

        //Assert.Same(f2, f1.NextFillUp);
        //Assert.Same(f3, f2.NextFillUp);
      }

      //[Theory]
      //[MemberData("RandomFillUpData", 50)]
      //public void AddSeveralFillUps(int odometer, double liters) {
      //  var c = new Car("Vios");

      //  c.AddFillUp(odometer, liters);

      //  c.FillUps.Count().ShouldEqual(1);
      //}

      //public static IEnumerable<object[]> RandomFillUpData(int count) {
      //  var r = new Random();
      //  for (int i = 0; i < count; i++) {
      //    var odo = r.Next(0, 999999 + 1);
      //    var liters = r.Next(0, 9999 + 1) / 100.0;

      //    yield return new object[] { odo, liters };
      //  }
      //}

      private void Dump(Car c) {
        _output.WriteLine("Car: {0}", c.Name);
        foreach (var f in c.FillUps) {
          _output.WriteLine($"{f.Odometer:000000} {f.Liters:n2} L. {f.KmL:n2} Km/L.");
        }
      }
    }

        public class AverageKmL {
            [Fact]
            public void NoFillUp_NoValue() {
                var c = new Car(name: "My Jazz");

                //Assert.Null(c.AverageKmL);

                double? kml = c.AverageKmL;
                kml.ShouldBeNull();
            }

            [Fact]
            public void OneFillUp_NoValue() {
                var c = new Car(name: "My Jazz");
                c.AddFillUp(1000, 40);

                //Assert.Null(c.AverageKmL);

                double? kml = c.AverageKmL;
                kml.ShouldBeNull();
            }

            [Fact]
            public void TwoFillUp_SameAsKmLOffFirstFillUp() {
                var c = new Car(name: "My Jazz");
                c.AddFillUp(1000, 40);
                c.AddFillUp(2000, 50);

                //Assert.Equal(20, c.AverageKmL);

                double? kml = c.AverageKmL;
                kml.ShouldEqual(20);
            }

            [Fact]
            public void ThreeFillUp_TrueAverageKmL() {
                var c = new Car(name: "My Jazz");
                c.AddFillUp(1000, 40);
                c.AddFillUp(2000, 50);
                c.AddFillUp(2500, 20);

                //Assert.Equal(21.43, c.AverageKmL);

                double? kml = c.AverageKmL;
                kml.ShouldEqual(21.43);
            }


        }
  }
}
