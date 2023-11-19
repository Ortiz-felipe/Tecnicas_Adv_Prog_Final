using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VTVApp.Api.Data;
using VTVApp.Api.Models.Entities;

namespace VTVApp.SpecflowTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(VTVDataContext db)
        {
            db.Provinces.AddRange(GetSeedingProvinces());
            db.Cities.AddRange(GetSeedingCities());
            db.SaveChanges();
        }

        private static IEnumerable<City> GetSeedingCities()
        {
            return new List<City>()
            {
                new() { Name = "La Plata", ProvinceId = 1, PostalCode = "1425"},
                new() { Name = "Berisso", ProvinceId = 1, PostalCode = "2536"},
                new() { Name = "Ensenada", ProvinceId = 1, PostalCode = "4251"},
                new() { Name = "Caballito", ProvinceId = 2, PostalCode = "3514"},
            };
        }

        public static IEnumerable<Province> GetSeedingProvinces()
        {
            return new List<Province>()
            {
                new() { Name = "Buenos Aires" },
                new() { Name = "Ciudad Autonoma de Buenos Aires" },
            };
        }
    }
}
