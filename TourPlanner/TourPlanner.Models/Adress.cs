using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Adress
    {
        public String Street { get; set; }
        public String Number { get; set; }
        public int ZibCode { get; set; }
        public String City { get; set; }
        public String Country { get; set; }

        public Adress(String street, String number, int zibCode, String city, String country)
        {
            this.Street = street;
            this.Number = number;
            this.ZibCode = zibCode;
            this.City = city;
            this.Country = country;
        }
        public Adress()
        {
        }
        public Adress(String s)
        {
            this.Street = "test";
            this.Number = "numb";
            this.ZibCode = 1230;
            this.City = "Wien";
            this.Country = "AT";
            /*
            string[] substrings = s.Split(' ', ',', StringSplitOptions.RemoveEmptyEntries);
            this.Street = substrings[0];
            this.Number = substrings[1];
            this.ZibCode = Int32.Parse(substrings[2]);
            this.City = substrings[3];
            */
        }

        public override String ToString()
        {
            return this.Street + " " + this.Number + ", " + this.ZibCode + " " + this.City; 
        }
    }
}
