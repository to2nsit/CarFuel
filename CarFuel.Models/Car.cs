using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarFuel.Models {
    public class Car {

        // Contructor

        public Car() : this("Car") {
            //
        }

        public Car(string name) {
            FillUps = new HashSet<FillUp>();
            Name = name;
        }

        // Property

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // run id
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        public virtual ICollection<FillUp> FillUps { get; set; } //= new HashSet<FillUp>();

        public double? AverageKmL {
            get {
                if (FillUps.Count() <= 1) {
                    return null;
                }
                if (FillUps.Count() == 2) {
                    return FillUps.First().KmL;
                }

                var minKm = FillUps.Min(c => c.Odometer);
                var totalKm = FillUps.Max(c => c.Odometer) - minKm;
                var totalLiter = FillUps.Where(c => c.Odometer > minKm).Sum(c => c.Liters);

                return Math.Round((totalKm / totalLiter), 2, MidpointRounding.AwayFromZero);
            }
        }

        // Method

        public FillUp AddFillUp(int odometer, double liters) {
            FillUp f = new FillUp(odometer, liters);
            FillUp last = FillUps.LastOrDefault();

            if (last != null) {
                last.NextFillUp = f;
            }

            FillUps.Add(f);

            return f;
        }
    }
}