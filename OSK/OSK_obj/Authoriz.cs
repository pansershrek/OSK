using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Calendar.v3.Data;

namespace OSK
{
    public class Authoriz
    {
        private static string[] Scopes = { CalendarService.Scope.Calendar };
        private static string ApplicationName = "GoogleCalendarAPIStart";
        private static string res_way = "";
        private static UserCredential GetUserCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                //string[] Scopes = { CalendarService.Scope.Calendar };
                //string res_way = "C:\\Users\\Petrel\\Documents\\Visual Studio 2017\\Projects\\ConsoleApp3\\ConsoleApp3\\auto_res.json";
                return GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None).Result;
            }
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
            UserCredential credential = GetUserCredential();
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
            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();

            return 0;
        }

    }
}