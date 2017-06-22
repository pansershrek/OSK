using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;
using Newtonsoft.Json;
namespace OSK
{
    public class PersonalServiceAccountCred
    {
        public string type { get; set; }
        public string project_id { get; set; }
        public string private_key_id { get; set; }
        public string private_key { get; set; }
        public string client_email { get; set; }
        public string client_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_x509_cert_url { get; set; }
    }
    public class Authoriz
    {
        private static string[] scopes = { CalendarService.Scope.Calendar };
        private static string ApplicationName = "GoogleCalendarAPIStart";
        private static string res_way = "";
        private static ServiceAccountCredential GetUserCredential()
        {
            var json = File.ReadAllText("client_secret.json");
            var cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json); // "personal" service account credential

            // Create an explicit ServiceAccountCredential credential
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.client_email)
            {
                Scopes = scopes
            }.FromPrivateKey(cr.private_key));
            return xCred;
        }
        /*static Event newEvent = new Event()
        {
            //Summary = "Google I/O 2015",
            //Location = "800 Howard St., San Francisco, CA 94103",
            //Description = "A chance to hear more about Google's developer products.",
            

            Start = new EventDateTime()
            {
                //DateTime = DateTime.Parse("2017-06-22T09:00:00-07:00"),
                DateTime = new DateTime(2017, 6, 21, 7, 0, 0),
                TimeZone = "Europe/Moscow",
            },
            End = new EventDateTime()
            {
                //DateTime = DateTime.Parse("2017-06-22T17:00:00-07:00"),
                DateTime = new DateTime(2017, 06, 21, 21, 0, 0),
                TimeZone = "Europe/Moscow",
            }
        };*/
        public int cal_event(Statement St)
        {
            ServiceAccountCredential credential = GetUserCredential();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Event newEvent = new Event();
            newEvent.Location = St.Location;
            try
            {
                newEvent.Start = new EventDateTime() { DateTime = DateTime.Parse(St.Day + "T" + St.Event_Begin), TimeZone = "Europe/Moscow" };
                newEvent.End = new EventDateTime() { DateTime = DateTime.Parse(St.Day + "T" + St.Event_End), TimeZone = "Europe/Moscow" };
            }
            catch(Exception)
            {
                return 1;
            }
            newEvent.Summary = "На рассмотрении " + St.Name + " " + St.Phone + " " + St.Organization + " " + St.Summary;
            String calendarId = "osk.msu@gmail.com";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();

            return 0;
        }

    }
}