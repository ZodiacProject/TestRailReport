using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;

namespace TestRailReport
{
    public partial class pdf_converter_properties : System.Web.UI.Page
    {
        public void BtnCreatePdf_Click(string r_ID)
        {
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url
            string url = @"C:\selenium_report\test_rail_report_" + r_ID + "\\index.html";
            PdfDocument doc = converter.ConvertUrl(url);//("http://192.168.44.82/index.php");
                
            // get conversion result (contains document info from the web page)
            HtmlToPdfResult result = converter.ConversionResult;

            // set the document properties
            doc.DocumentInformation.Title = result.WebPageInformation.Title;
            doc.DocumentInformation.Subject = result.WebPageInformation.Description;
            doc.DocumentInformation.Keywords = result.WebPageInformation.Keywords;

            doc.DocumentInformation.Author = "Select.Pdf Samples";
            doc.DocumentInformation.CreationDate = DateTime.Now;
            // save pdf document                  
            doc.Save(@"C:\selenium_report\report.pdf");
            //doc.Save(Response, false, "Yes.pdf");
            Console.WriteLine("Report is create");
            // close pdf document
            doc.Close();
        }
     
    }
}