using System.Globalization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Shifts.Application.Models;

namespace Shifts.Infrastructure.Calender.Services;

public class GoogleCalendarService : IGoogleCalendarService
{
    private List<string> months = new List<string>()
    {
    "jan",
    "feb",
    "mar",
    "apr",
    "may",
    "jun",
    "jul",
    "aug",
    "sep",
    "oct",
    "nov",
    "dec"
    };

    public async Task CreateEvents(List<Event> shifts, UserCredential credential)
    {
        var service = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Vagter",
        });

        foreach (var shift in shifts)
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMinDateTimeOffset = shift.Start.DateTimeDateTimeOffset;
            request.TimeMaxDateTimeOffset = shift.End.DateTimeDateTimeOffset;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events existingEvents = await request.ExecuteAsync();

            if (existingEvents.Items.Count == 0)
            {
                EventsResource.InsertRequest insert = service.Events.Insert(shift, "primary");
                await insert.ExecuteAsync();
            }
        }
    }


    public List<Event> events(List<Shift> shifts, string imgUrl)
    {
        List<Event> events = new List<Event>();
        int year = DateTime.Now.Year;
        bool isFirstJan = true;
        foreach (var shift in shifts)
        {
            if (shift.Dato != null)
            {
                DateTime currentDate = DateTime.Now;
                string[] dateParts;
                if (shift.Dato.Contains("-"))
                {
                    dateParts = shift.Dato.Split('-');
                }
                else
                {
                    dateParts = shift.Dato.Split('.');
                }
                int month = FindMonth(dateParts[1]);
                int day = int.Parse(dateParts[0]);

                if (month == 1 && isFirstJan)
                {
                    year++;
                    isFirstJan = false;
                }


                DateTime date = new DateTime(year, month, day);



                // Parse the start and end times
                string[] timeParts = shift.Hours.Split('-');
                string startTimeString = timeParts[0];
                string endTimeString = timeParts[1];
                if (startTimeString.Contains("."))
                {
                    startTimeString = startTimeString.TrimEnd('.').Replace(".", ":");
                }
                else
                {
                    startTimeString = startTimeString + ":00";
                }

                if (endTimeString.Contains("."))
                {
                    endTimeString = endTimeString.Replace(".", ":");
                }
                else
                {
                    endTimeString = endTimeString + ":00";
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
                    Creator = new Event.CreatorData
                    {
                        DisplayName = shift.Name,
                    },
                    Gadget = new Event.GadgetData
                    {
                        IconLink = imgUrl,
                    },
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
    public int FindMonth(string monthstring)
    {
        int monthInt;
        if (int.TryParse(monthstring, out monthInt))
        {
            // If the month is a number, just return it.
            return monthInt;
        }
        else
        {
            foreach (var month in months)
            {
                if (monthstring.Contains(month))
                {
                    monthInt = DateTime.ParseExact(month, "MMM", new CultureInfo("da-DK")).Month;
                    return monthInt;
                }
            }
        }
        return monthInt;
    }

}