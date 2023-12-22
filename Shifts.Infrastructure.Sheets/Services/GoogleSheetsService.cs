using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Infrastructure.Sheets.Services;

public class GoogleSheetsService : IGoogleSheetsService
{
    private List<string> weekDays = new List<string>()
    {
        "Mandag",
        "Tirsdag",
        "Onsdag",
        "Torsdag",
        "Fredag",
        "Lørdag",
        "Søndag"
    };
    public IList<IList<object>> ReadDataFromGoogleSheet(UserCredential credential)
    {
        var service = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Vagter"
        });
        // The "spreadsheetId" is the ID of your Google Sheet, and "range" is the range you want to read.
        SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get("15SUZ197w4tZRtcyOqf9H3koRuGyUC0G3iFq3xTmkHkQ", "A8:I700");
        ValueRange response = request.Execute();

        IList<IList<object>> values = response.Values;

        return values;
    }

    public List<Shift> FindShifts(IList<IList<object>> values, string nameToSearch)
    {
        List<Shift> shifts = new List<Shift>();

        if (values != null)
        {
            for (int col = 0; col < values.Count; col++)
            {
                var row = values[col];
                for (int colIndex = 0; colIndex < row.Count; colIndex++)
                {
                    if (row[colIndex] is string stringValue && stringValue == nameToSearch)
                    {
                        Shift shift = new Shift();
                        shift.Name = nameToSearch;
                        var hoursColumn = values[col - 1];
                        int i = 0;
                        shift.Hours = Convert.ToString(hoursColumn[colIndex]);
                        var aboveColumn = values[col - i];
                        while (col > 0)
                        {
                            if (aboveColumn.Count > colIndex)
                            {
                                if (weekDays.Contains(aboveColumn[colIndex]))
                                {
                                    shift.Day = Convert.ToString(aboveColumn[colIndex]);
                                    i++;
                                    var aboveAboveColumn = values[col - i];
                                    shift.Dato = Convert.ToString(aboveAboveColumn[colIndex]);
                                    shifts.Add(shift);
                                    i = 0;
                                    break;
                                }
                            }
                            i++;
                            aboveColumn = values[col - i];
                        }


                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No data found in the specified range.");
        }
        return shifts;
    }

}
