using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Options;
using Shifts.Application.Models;

namespace Shifts.Infrastructure.Calender.Services;

public class GoogleCalendarService : IGoogleCalendarService
{
    private GoogleSheetsConfig _googleSheetsConfig;

    public GoogleCalendarService(IOptions<GoogleSheetsConfig> googleSheetsConfig)
    {
        _googleSheetsConfig = googleSheetsConfig.Value;
    }

    public async void CreateEvents(List<Event> shifts)
    {
        string[] scopes = { CalendarService.Scope.Calendar };

        UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = _googleSheetsConfig.client_id,
                        ClientSecret = _googleSheetsConfig.client_secret
                    },
                    scopes,
                    "user",
                    CancellationToken.None);

        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Vagter",
        });

        foreach (var shift in shifts)
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMinDateTimeOffset = shift.Start.DateTimeDateTimeOffset;
            request.TimeMinDateTimeOffset = shift.End.DateTimeDateTimeOffset;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events existingEvents = request.Execute();

            if (existingEvents.Items.Count == 0)
            {
                EventsResource.InsertRequest insert = service.Events.Insert(shift, "primary");
                insert.Execute();
            }
        }
    }


    public List<Event> events(List<Shift> shifts)
    {
        List<Event> events = new List<Event>();
        // Parse the date
        foreach (var shift in shifts)
        {
            if (shift.Dato != "")
            {
                DateTime date = Convert.ToDateTime(shift.Dato);

                // Parse the start and end times
                string[] timeParts = shift.Hours.Split('-');
                string startTimeString = timeParts[0];
                string endTimeString = timeParts[1];
                if (startTimeString.Contains("."))
                {
                    startTimeString = startTimeString.Replace(".", ":");
                }

                if (endTimeString.Contains("."))
                {
                    endTimeString = endTimeString.Replace(".", ":");
                }


                // Parse the start and end times as TimeSpan
                TimeSpan startTime = TimeSpan.Parse(startTimeString);
                TimeSpan endTime = TimeSpan.Parse(endTimeString);

                // Combine date and start time to create the start DateTime
                DateTime startDateTime = date.Add(startTime);

                // Combine date and end time to create the end DateTime
                DateTime endDateTime = date.Add(endTime);

                // Print the results
                Console.WriteLine("Start Date and Time: " + startDateTime);
                Console.WriteLine("End Date and Time: " + endDateTime);

                events.Add(new Event
                {
                    Summary = "Livredder ved Aarhus Svømmestadion",
                    Location = "F. Vestergaards Gade 5, 8000 Aarhus C",
                    Description = "Redde liv",
                    Start = new EventDateTime
                    {
                        DateTimeDateTimeOffset = startDateTime,
                        TimeZone = "Europe/Copenhagen"
                    },
                    End = new EventDateTime
                    {
                        DateTimeDateTimeOffset = endDateTime,
                        TimeZone = "Europe/Copenhagen"
                    },
                    Recurrence = /*new List<string> { "RRULE:FREQ=DAILY;COUNT=2" }*/ null,
                    Attendees = new List<EventAttendee>
                    {
                        //new EventAttendee { Email = "lpage@example.com" },
                        //new EventAttendee { Email = "sbrin@example.com" }
                    },
                    Reminders = new Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new List<EventReminder>
                    {
                        new EventReminder { Method = "email", Minutes = 24 * 60 },
                        new EventReminder { Method = "popup", Minutes = 10 }
                    }
                    }
                });
            }

        }
        return events;
    }
}

