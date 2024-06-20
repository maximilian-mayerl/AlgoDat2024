using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MockExam {
    class City : IComparable<City> {
        public string CountryName { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public City(string countrName, string name, int population) { 
            this.CountryName = countrName;
            this.Name = name;
            this.Population = population;
        }

        public int CompareTo(City? other) {
            if (other == null) {
                return 1;
            }

            // Sort by country name first, ascending.
            int countryCompare = this.CountryName.CompareTo(other.CountryName);
            if (countryCompare != 0) {
                return countryCompare;
            }
            
            // If country is the same, sort by population, descending.
            return other.Population.CompareTo(this.Population);
        }

        public override string ToString() {
            return $"{this.CountryName} - {this.Name} ({this.Population})";
        }
    }
}
