using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            //define the characters by witch to seperate
            char[] seperators = new char[] { ' ', ',' };

            string[] substrings = s.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            
            //Check if the splitted array contains item at that point in case the string was not as long as expected
            this.Street = substrings.ElementAtOrDefault(0) != null ? substrings[0] : "";
            this.Number = substrings.ElementAtOrDefault(1) != null ? substrings[1] : "";
            this.ZibCode = substrings.ElementAtOrDefault(2) != null ? Int32.Parse(substrings[2]) : 0000; 
            this.City = substrings.ElementAtOrDefault(3) != null ? substrings[3] : "";
            this.Country = substrings.ElementAtOrDefault(4) != null ? substrings[4] : "";
        }

        public override String ToString()
        {
            return this.Street + " " + this.Number + " " + this.ZibCode + " " + this.City + " " + this.Country; 
        }
    }
}
