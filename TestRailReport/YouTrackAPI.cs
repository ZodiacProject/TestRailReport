using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gurock.TestRail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Validation;

namespace TestRailReport
{
    class YouTrackAPI
    {
        private string _youTrackBaseUrl = "https://propellerads.myjetbrains.com/youtrack/rest/";
        private const string _loginYouTrack = "a.stepanov@propellerads.net";
        private const string _passwordYouTrack = "propeller";
        private const string _logIN = "user/login";
        private const string _attachToTask = "issue/ITDQA-471/attachment";
        private string _comment = null;
        private string _pathFile = null;
        private RestClient _client;
        private RestRequest _request;
        private IRestResponse _loginResponse;

        public YouTrackAPI(string pathFile)
        {
            this._pathFile = pathFile;
            this._client = new RestClient(_youTrackBaseUrl);
            this._request = new RestRequest(_logIN, Method.POST);
            this._client.Authenticator = new SimpleAuthenticator("login", _loginYouTrack, "password", _passwordYouTrack);
            this._loginResponse = _client.Execute(_request);

        }
        public void AttachFileToTask()
        {
            RestRequest request = new RestRequest(_attachToTask, Method.POST);
            for (int i = 0; i < _loginResponse.Cookies.Count; i++)
            {
                request.AddCookie(_loginResponse.Cookies[i].Name, _loginResponse.Cookies[i].Value);
            }        
            request.AddFile("Report", _pathFile);
            IRestResponse response = _client.Execute(request);            
            Console.WriteLine("Attach file: " + response.StatusCode);
        }
        public void AddComments()
        {
            DateTime date = DateTime.Now;
            switch (date.DayOfWeek.ToString())
            {
                case "Monday": _comment = "Onclick and Interstitial";
                    break;
                case "Tuesday": _comment = "Pushup and Interstitial";
                    break;
                case "Wednesday": _comment = "Mobile and Mac desktop Onclick";
                    break;
                case "Friday": _comment = "Pushup and Interstitial";
                    break;
                default: break;
            }
            RestRequest request = new RestRequest("issue/ITDQA-471/execute?comment=" + _comment, Method.POST);
            for (int i = 0; i < _loginResponse.Cookies.Count; i++)
            {
                request.AddCookie(_loginResponse.Cookies[i].Name, _loginResponse.Cookies[i].Value);
            }            
            IRestResponse response = _client.Execute(request);
            Console.WriteLine("Add comments: " + response.StatusCode);
        }
    }
   

}
