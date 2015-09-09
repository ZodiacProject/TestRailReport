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
        private string _url = "https://propeller.testrail.net/index.php?/reports/overview/";        
        private List<string> TopSitesOnClick = new List<string>();
        private Dictionary<string, List<string>> _sectionCaseToRun = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _testCase = new Dictionary<string, string>();
        private IWebDriver _driver;
        private ZipFile _zipFile;
        //Constuctor
        //public Driver(TestRail test)
        //{
        //    _testRun = test;
        //    _testRun.StartTestRail();
        //    foreach (KeyValuePair<string, List<string>> case_id_section in _testRun.GetIDCaseInSection())
        //        _sectionCaseToRun.Add(case_id_section.Key, case_id_section.Value);
        //    foreach (KeyValuePair<string, string> test_case_name in _testRun.GetTestCaseName)
        //        _testCase.Add(test_case_name.Key, test_case_name.Value);
        //}
        //methods
        public void NavigateToTestRail(string proj_ID)
        {
            FirefoxProfile profile = new FirefoxProfile();

            profile.SetPreference("browser.download.dir", @"C:\selenium_report\");
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/zip");
            _driver = new FirefoxDriver(profile);            
            _driver.Navigate().GoToUrl(_url + proj_ID);
            Thread.Sleep(2000);
            IWebElement elementLogin = _driver.FindElement(By.XPath("//*[@id='name']"));
            elementLogin.Click();
            elementLogin.SendKeys(_login);
            IWebElement elementPass = _driver.FindElement(By.XPath("//*[@id='password']"));
            elementPass.Click();
            elementPass.SendKeys(_password);
            elementPass.Submit();
        }
        public void FindReport(string reportID)
        {            
            IWebElement elementFindRep, elementDwLoadRep;
            DateTime date = DateTime.Today;
            //string linkReport = //date.DayOfWeek + ": " + _getСonversionDate(date);//date.Day.ToString() + date.Month.ToString() + date.Year.ToString();
       
                if (_driver.PageSource.Contains(reportID))
                {                                       //(By.XPath("//*[@id='report-234']/td[2]/a"));
                    elementFindRep = _driver.FindElement(By.XPath("//*[@id='report-" + reportID + "']/td[2]/a"));
                    elementFindRep.Click();
                    elementDwLoadRep = _driver.FindElement(By.XPath("//*[@id='content-header']/div/span[1]/a/img"));
                    elementDwLoadRep.Click();
                    Console.WriteLine("Report is download");
                    Thread.Sleep(2000);
                    }
                else                
                    Console.WriteLine(reportID + " is not undefined at this project");                                                                              
        }

        public void ExtractFileToDirectory(string rep_ID)
        {
            IReadOnlyCollection<string> zipFilesName = Directory.GetFiles(@"C:\selenium_report\", "testrail-report-" + rep_ID + "-standalone.zip");
            string outputDirectory = @"C:\selenium_report\test_rail_report_" + rep_ID;
            _zipFile = ZipFile.Read(zipFilesName.ElementAt(0));
                foreach (ZipEntry e in _zipFile)
                {
                // check if you want to extract e or not
                 //   if (e.FileName.Contains(rep_ID))
                        e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
                }        

            Console.WriteLine("Arhive is extract");
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
            _driver.Quit();            
        }
    
    }
}
