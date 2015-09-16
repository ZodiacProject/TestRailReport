using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;

namespace TestRailReport
{
    public partial class PDFReport : System.Web.UI.Page
    {
        private string _nameReport = null;      
        public void CreatePDF(string nameReport)
        {
            // name of report in folder
            this._nameReport = nameReport;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url
            string url = @"C:\selenium_report\test_rail_report_" + _nameReport + "\\index.html";
            PdfDocument doc = converter.ConvertUrl(url);
                
            // get conversion result (contains document info from the web page)
            HtmlToPdfResult result = converter.ConversionResult;
            string savePath = @"C:\selenium_report\" + _nameReport + "_report.pdf";
            // set the document properties
            doc.DocumentInformation.Title = result.WebPageInformation.Title;
            doc.DocumentInformation.Subject = result.WebPageInformation.Description;
            doc.DocumentInformation.Keywords = result.WebPageInformation.Keywords;

            doc.DocumentInformation.Author = "Select.Pdf Samples";
            doc.DocumentInformation.CreationDate = DateTime.Now;
            // save pdf document                  
            doc.Save(savePath);
            //doc.Save(Response, false, "Yes.pdf");
            Console.WriteLine("Report is create");
            // close pdf document
            doc.Close();

//Attach file & add comment to task ITDQA-471            
            YouTrackAPI YouTrackAPI = new YouTrackAPI(savePath);
            YouTrackAPI.AttachFileToTask();
            YouTrackAPI.AddComments();
        }
     
    }
}