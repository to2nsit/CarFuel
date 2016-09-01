using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFuel.Models {
  [Table("tblFillUp")]
  public class FillUp {

    public FillUp() {
    }

    public FillUp(int odometer, double liters, bool isFull = true) {
      Odometer = odometer;
      Liters = liters;
      IsFull = isFull;
    }

    [Key]
    public int Id { get; set; }

    [Column("IS_FULL")]
    public bool IsFull { get; set; }

    [Range(0.0, 100.0)] // using by MVC
    public double Liters { get; set; }

    // Navigation Properties
    // make it "virtual" to enable lazy-loading this property
    public virtual FillUp NextFillUp { get; set; }

    [Range(0, 999999)]
    public int Odometer { get; set; }

    public double? KmL { // not built if no set method
      get {
        if (NextFillUp == null) {
          return null;
        }
        if (Odometer > NextFillUp.Odometer) {
          throw new Exception("Odometer Error");
        }
        return (NextFillUp.Odometer - Odometer) / NextFillUp.Liters;
      }
    } 
      

  }
}