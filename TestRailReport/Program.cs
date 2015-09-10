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
            Console.Write("Please enter report ID include '#' project ID for example 111#1: ");
            string str = Console.ReadLine();
            string[] RepID_ProjID = str.Split('#');
            string reportID = RepID_ProjID[0];
            string idProject = RepID_ProjID[1];
            Driver Driver = new Driver();
            PDFReport PDFReport = new PDFReport();
            Driver.NavigateToTestRail(idProject);
            Driver.FindReport(reportID);
            Driver.ExtractFileToDirectory(reportID);
            PDFReport.BtnCreatePdf(reportID);
            Driver.SendReport();
            Driver.CloseDriver();
        }
    }
}
