using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Utilities {
    public static class SystemTime {

        public static Func<DateTime> Now = () => DateTime.Now;

        public static void SetDateTime(DateTime now) {
            Now = () => now;
        }

        public static void ResetDateTime() {
            Now = () => DateTime.Now;
        }

    }
}
