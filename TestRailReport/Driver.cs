using System;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Internal;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Ionic.Zip;

namespace TestRailReport
{
    class Driver
    {
        private const string _login = "stepanov.guap@gmail.com";
        private const string _password = "302bis";     
        private string _url = "https://propeller.testrail.net/index.php?/reports/overview/3"; // the url only for Automatically tests         
        private IWebDriver _driver;
        private ZipFile _zipFile;
        private string _nameReport = null;
        public Driver()
        {
            FirefoxProfile profile = new FirefoxProfile();

            profile.SetPreference("browser.download.dir", @"C:\selenium_report\");
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/zip");
            _driver = new FirefoxDriver(profile);            
        }
        public void LogINToTestRail()
        {            
            _driver.Navigate().GoToUrl(_url);
            Thread.Sleep(2000);
            IWebElement elementLogin = _driver.FindElement(By.XPath("//*[@id='name']"));
            elementLogin.Click();
            elementLogin.SendKeys(_login);
            IWebElement elementPass = _driver.FindElement(By.XPath("//*[@id='password']"));
            elementPass.Click();
            elementPass.SendKeys(_password);
            elementPass.Submit();
        }
        public void FindReport()
        {                 
            IWebElement elementFindRep, elementDwLoadRep;
            DateTime date = DateTime.Today;
            string linkReport = date.DayOfWeek + ": " + _getСonversionDate(date);
                if (_driver.PageSource.Contains(linkReport))
                {                                       
                    elementFindRep = _driver.FindElement(By.LinkText(linkReport));
                    elementFindRep.Click();
                    elementDwLoadRep = _driver.FindElement(By.XPath("//*[@id='content-header']/div/span[1]/a/img"));
                    elementDwLoadRep.Click();
                    Console.WriteLine("Report is download");
                    Thread.Sleep(2000);
                    }
                else
                    Console.WriteLine(linkReport + " not found");                                                                              
        }
        public void ExtractFileToDirectory()
        {

            this._nameReport = _driver.Url.Substring(_driver.Url.LastIndexOf("/")).Remove(0, 1); //обрезка символа '/'
            IReadOnlyCollection<string> zipFilesName = Directory.GetFiles(@"C:\selenium_report\", "testrail-report-" + this._nameReport + "-standalone.zip");
            string outputDirectory = @"C:\selenium_report\test_rail_report_" + this._nameReport;
            _zipFile = ZipFile.Read(zipFilesName.ElementAt(0));
                foreach (ZipEntry e in _zipFile)
                {
                        e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
                }        

            Console.WriteLine("Arhive is extract");
//create report to PDF
            PDFReport PDFReport = new PDFReport();
            PDFReport.CreatePDF(_nameReport);

        }
        private string _getСonversionDate(DateTime date)
        {
            string month = null;
            if (date.Day < 10)
                month = "0" + date.Day + ".";
            else
                month = date.Day + ".";

            if (date.Month < 10)
                month += "0" + date.Month + "." + date.Year;
            else
                month += date.Month + "." + date.Year;
            return month;
        }
        public void CloseDriver()
        {
            _driver.Close();        
        }
    }
}
