using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using HotChocolate.Authorization;
using Shifts.Application.Interfaces;
using Shifts.Application.Models;

namespace Shifts.Presentation.GraphQL.Queries;

[ExtendObjectType("Query")]
public class EventsQuery
{
    List<KeyValuePair<string, string>> namesAndLinks = new List<KeyValuePair<string, string>>
    {
    new KeyValuePair<string, string>("Mathilde", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/292515208_10223129544846727_5607384605376488947_n.jpg?stp=dst-jpg_p480x480&_nc_cat=107&ccb=1-7&_nc_sid=5740b7&_nc_ohc=yORKL2M9lTIAX_Upez_&_nc_ht=scontent.fcph3-1.fna&oh=00_AfBr1Z00jXd0HiRy2ZfBX9LTbcmbOv1plKKKc2SXxNu-2A&oe=6589F4A6"),
    new KeyValuePair<string, string>("Rasmus", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/339419396_533390675658516_6795437543526765466_n.jpg?stp=c0.272.480.480a_dst-jpg_p480x480&_nc_cat=107&ccb=1-7&_nc_sid=5740b7&_nc_ohc=ifkALCFg5qEAX8UaaJ0&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCuqK4kMYPn5ucoukDkiL4Zpfp5hLuHJaeDCn7F-8lrqg&oe=6588C99E"),
    new KeyValuePair<string, string>("Bo", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/150785943_10222171376587003_1412743611811163383_n.jpg?stp=c0.302.480.480a_dst-jpg_p480x480&_nc_cat=102&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=HOA4-ZwOIlQAX-miFo1&_nc_ht=scontent.fcph3-1.fna&oh=00_AfBk6lhbFSxOQeS4CfLgrG5N0WyA6bPj_fJxWhKuefEC2g&oe=65ABB753"),
    new KeyValuePair<string, string>("Camilla", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/308987139_5614774565249776_7081493100820153418_n.jpg?stp=dst-jpg_p480x480&_nc_cat=100&ccb=1-7&_nc_sid=5740b7&_nc_ohc=i2PDwNDacZ4AX88kkRp&_nc_ht=scontent.fcph3-1.fna&oh=00_AfDuCcxCTF1GSMqw3qCs3BbFyvCxJ7waOPJHX2b873s3aQ&oe=6588F825"),
    new KeyValuePair<string, string>("Caroline", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/300072066_5251929628252800_5933945373370555276_n.jpg?stp=c0.61.480.480a_dst-jpg_p480x480&_nc_cat=104&ccb=1-7&_nc_sid=5740b7&_nc_ohc=g7UQ8UYGSnYAX_bzCrO&_nc_ht=scontent.fcph3-1.fna&oh=00_AfAMC5W8DqA_D9R5qKJajBnGW95Tx_wbENBkodCKo5zO5g&oe=658956A8"),
    new KeyValuePair<string, string>("Clara", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/370225115_10229679050903334_5551913660429535678_n.jpg?stp=c0.142.480.480a_dst-jpg_p480x480&_nc_cat=100&ccb=1-7&_nc_sid=5740b7&_nc_ohc=gyXS8XZdDJoAX-TODk-&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCtsTDCEXaicT6Wb3oLH89JKoXprNm_wxX6_hhCnG6Kvg&oe=658A1189"),
    new KeyValuePair<string, string>("Ehab", "https://imgur.com/a/n1372Xt"),
    new KeyValuePair<string, string>("Jonas", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/306948626_10228741018011377_9122074964398791215_n.jpg?stp=c62.0.480.480a_dst-jpg_p480x480&_nc_cat=101&ccb=1-7&_nc_sid=5740b7&_nc_ohc=qPdY7OVbC2QAX8KdrZH&_nc_ht=scontent.fcph3-1.fna&oh=00_AfA9_qYmlsEW7Hg6IiXr_03gJyXZgQxCwERfxsR7dM3Cdw&oe=6588ED11"),
    new KeyValuePair<string, string>("Linea", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.18169-1/16939511_724733204355579_4616552802240393110_n.jpg?stp=dst-jpg_s480x480&_nc_cat=104&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=qsgdwtqLkX8AX9825Gd&_nc_ht=scontent.fcph3-1.fna&oh=00_AfBlIMF7tOhAPQgqOUj7KATuBBPngVFGHjUQ48CgKkpT6Q&oe=65AB9A39"),
    new KeyValuePair<string, string>("Mads", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/312328338_10229610182623155_8598964037897311795_n.jpg?stp=cp6_dst-jpg_s480x480&_nc_cat=111&ccb=1-7&_nc_sid=5740b7&_nc_ohc=7saHtv57XTcAX9NsXo2&_nc_ht=scontent.fcph3-1.fna&oh=00_AfAkTB7fXCxW6jO7ZU-CPrw5wk6vqK2fkbzuTGeilUBqfA&oe=65894F27"),
    new KeyValuePair<string, string>("Magnus T", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/367442083_10231940838803248_4980376132006683418_n.jpg?stp=dst-jpg_p480x480&_nc_cat=110&ccb=1-7&_nc_sid=5740b7&_nc_ohc=lhLC1Q9llW4AX8afStW&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCuIRrbtGw0ySf8r-ywXYPhTMbGLyS_N3B_yjPvqVz8tg&oe=65883F9F"),
    new KeyValuePair<string, string>("Rudi", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/295064636_10160732732557240_4370230157502362225_n.jpg?stp=dst-jpg_s480x480&_nc_cat=105&ccb=1-7&_nc_sid=5740b7&_nc_ohc=wZX0SDU4wdYAX9ULigS&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCi1PbOLSvf_3qvayBFqTVyzLBuXX_oKBk36pGRcg2Uyw&oe=6589422E"),
    new KeyValuePair<string, string>("Cecilie", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/119005906_3257813424336847_4756496435014489747_n.jpg?stp=dst-jpg_p480x480&_nc_cat=109&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=nHmYjPPj-EUAX8Uq5SL&_nc_ht=scontent.fcph3-1.fna&oh=00_AfBw1IPrTZadYxBafCLGP8C83EOfai6m5hhSBn0obC4I5A&oe=65ABAC0A"),
    new KeyValuePair<string, string>("Jakob M", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/117716925_321246135589700_746707016022653725_n.jpg?stp=c80.0.480.480a_dst-jpg_p480x480&_nc_cat=110&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=iohNOuhxYMAAX-1pmnE&_nc_ht=scontent.fcph3-1.fna&oh=00_AfAgDj684QWNuD6SC_hGQurwKfFDZkQ7TwYyJjPIC5fENg&oe=65ABA454"),
    new KeyValuePair<string, string>("Jakob B", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/189727528_10224920315026097_2117271144480069954_n.jpg?stp=dst-jpg_p480x480&_nc_cat=102&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=HGqoBiRY3u0AX-5cKz0&_nc_ht=scontent.fcph3-1.fna&oh=00_AfDAJDDGeXGHiob-bqL2J5a8Yr5x8ICnrDWvXcEVrRy0ZA&oe=65ABAD04"),
    new KeyValuePair<string, string>("Signe", "https://scontent-cph2-1.xx.fbcdn.net/v/t39.30808-1/364132950_10232071583002903_1355842068762094104_n.jpg?stp=cp6_dst-jpg_p200x200&_nc_cat=102&ccb=1-7&_nc_sid=5740b7&_nc_ohc=8UWwo1zqhFUAX9LMbNP&_nc_ht=scontent-cph2-1.xx&oh=00_AfDr9DeshCA7nWO-dlKEEsyebfZf329Dknna4xeGYDoz_Q&oe=658FAABC"),
    new KeyValuePair<string, string>("Christina", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/404298883_10160886240176285_6123582980323298599_n.jpg?stp=cp6_dst-jpg_p480x480&_nc_cat=104&ccb=1-7&_nc_sid=5740b7&_nc_ohc=zuxmx66BD-0AX9LshD6&_nc_ht=scontent.fcph3-1.fna&oh=00_AfDLaC4LkYcGClpD3YvKIgt4uZ3YEgFmX2DaYp-15jVPzw&oe=65898478"),
    new KeyValuePair<string, string>("Halfdan", "https://scontent.fcph3-1.fna.fbcdn.net/v/t31.18172-1/21316500_1933607786914734_3127496160315459309_o.jpg?stp=dst-jpg_p480x480&_nc_cat=102&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=m3DaSqytZkoAX-ok48b&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCQZFprpKODKUHAGW3F0h86Y8_FHUMN_wuRdAI-6jSCug&oe=65ABC0E7"),
    new KeyValuePair<string, string>("Kira", "https://scontent.fcph3-1.fna.fbcdn.net/v/t31.18172-1/456336_293819454046515_191372112_o.jpg?stp=c113.33.414.414a_dst-jpg_p480x480&_nc_cat=110&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=2kdJqjFS9kcAX8w_Tuo&_nc_ht=scontent.fcph3-1.fna&oh=00_AfDh8j7CVPZE1kjNRo8uyxRy_1Qpa0lQMbldPJd9AA-WEw&oe=65AB9CD5"),
    new KeyValuePair<string, string>("Torben", ""),
    new KeyValuePair<string, string>("Annette", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/131939048_3608973082494464_5562417553446027487_n.jpg?stp=dst-jpg_p480x480&_nc_cat=100&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=9DCIA6jEtVIAX95U_Bm&_nc_ht=scontent.fcph3-1.fna&oh=00_AfAbdU3fogRIwwIOWE7g7Cy4StQ0cQncQZfhTgP3cpbAEw&oe=65AB8D55"),
    new KeyValuePair<string, string>("Ümmühan", ""),
    new KeyValuePair<string, string>("Gülseren", ""),
    new KeyValuePair<string, string>("Sascha", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/362278372_10224024441103698_2613124409591985727_n.jpg?stp=dst-jpg_s480x480&_nc_cat=107&ccb=1-7&_nc_sid=5740b7&_nc_ohc=J4I--TzHjAgAX_s5_rm&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCzkxncsdv2Yu-bEgCnogS8IzEKT-djsdGvRNBgXkDGFg&oe=65892179"),
    new KeyValuePair<string, string>("Mikkel", "https://scontent.fcph3-1.fna.fbcdn.net/v/t1.6435-1/150927889_3289334431172156_2902948046668812404_n.jpg?stp=dst-jpg_p480x480&_nc_cat=105&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=p4T6bdVBmkYAX8UXyOU&_nc_ht=scontent.fcph3-1.fna&oh=00_AfCKwyBReQ9mqTxL5GvPCr6Zy7QQn94ONqARXgFrYWQifg&oe=65ABAE93"),
    new KeyValuePair<string, string>("Kille", "https://scontent.fcph3-1.fna.fbcdn.net/v/t39.30808-1/306362677_2261103850720953_4306115329199921799_n.jpg?stp=dst-jpg_p480x480&_nc_cat=110&ccb=1-7&_nc_sid=5740b7&_nc_ohc=IAFM_voIk9gAX-3uwX8&_nc_ht=scontent.fcph3-1.fna&oh=00_AfAjHqkEN1cBmvyHU1PO4HI50iBWC_3r9vEp_LZyJ-MVXA&oe=6588C459"),
    new KeyValuePair<string, string>("Marianne","https://scontent-cph2-1.xx.fbcdn.net/v/t1.18169-1/1656123_10202134359295399_2020078094_n.jpg?stp=c0.42.200.200a_dst-jpg&_nc_cat=100&ccb=1-7&_nc_sid=2b6aad&_nc_ohc=k1JGqaC9YngAX_xa9jk&_nc_ht=scontent-cph2-1.xx&oh=00_AfCAo0q0E-6xcH4gyFQGNi24dg9xarQesta705H-x1IpJw&oe=65ABB829"),
    };
    public async Task<List<Event>> Get(IGoogleSheetsService _googleSheetsService, IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService, string employeeName)
    {
        if (employeeName != null)
        {
            UserCredential credential = await _googleAuthService.AuthorizeAsync();
            IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
            List<Shift> shifts = _googleSheetsService.FindShifts(objects, employeeName);
            var nameAndLink = namesAndLinks.FirstOrDefault(n => n.Key == employeeName);
            List<Event> events = _googleCalendarService.events(shifts, nameAndLink.Value);
            return events;
        }
        else
        {
            throw new ArgumentNullException("name can not be null");
        }
    }

    public async Task<List<Event>> GetAllShifts(IGoogleSheetsService _googleSheetsService, IGoogleCalendarService _googleCalendarService, IGoogleAuthService _googleAuthService)
    {
        UserCredential credential = await _googleAuthService.AuthorizeAsync();
        IList<IList<object>> objects = _googleSheetsService.ReadDataFromGoogleSheet(credential);
        List<Shift> shifts = new List<Shift>();
        List<Event> events = new List<Event>();
        foreach (var name in namesAndLinks)
        {
            shifts = _googleSheetsService.FindShifts(objects, name.Key);
            events.AddRange(_googleCalendarService.events(shifts, name.Value));
        }
        return events;
    }

}