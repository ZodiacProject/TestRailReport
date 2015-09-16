using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestRailReport
{
    class Program
    {
        static void Main(string[] args)
        {             
            DateTime date = DateTime.Now;           
            if (date.DayOfWeek.ToString() == "Monday" || date.DayOfWeek.ToString() == "Wednesday")
            {
                Console.Write("Today is " + date.DayOfWeek + " report wiil be create...");      
                Driver Driver = new Driver();
               
                Driver.LogINToTestRail();
                Driver.FindReport();
                Driver.ExtractFileToDirectory();                           
                Driver.CloseDriver();
            }                
            else
                Console.Write("Today is " + date.DayOfWeek + " report already exist!");
            
        }
    }
}
