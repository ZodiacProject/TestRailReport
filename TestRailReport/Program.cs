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
            Driver Driver = new Driver();
            pdf_converter_properties PDFReport = new pdf_converter_properties();
            Console.Write("Please enter report ID include '#' project ID for example 111#1: ");
            string str = Console.ReadLine();
            string[] RepID_ProjID = str.Split('#');
            string reportID = RepID_ProjID[0];
            string idProject = RepID_ProjID[1];
            Driver.NavigateToTestRail(idProject);
            Driver.FindReport(reportID);           
            Driver.ExtractFileToDirectory(reportID);
            PDFReport.BtnCreatePdf_Click(reportID);
            Driver.CloseDriver();
        }
    }
}
