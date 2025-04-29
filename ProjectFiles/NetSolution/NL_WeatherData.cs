#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.DataLogger;
using FTOptix.Store;
using FTOptix.WebUI;
using FTOptix.System;
using RestSharp;
using System.Text.Json;
#endregion

public class NL_WeatherData : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public async void UpdateWeather()
    {
        //Set Alias to Weather Data Object
        var WD = (objWeatherData)Owner.GetAlias("WD");
        //Get location and developer key for API call
        var UALocation = (UAVariable)Owner.Get("SetLocation");
        var UADevKey = (UAVariable)Owner.Get("DevKey");
        FTOptix.System.DateAndTime UAtime;

        //Setup REST request
        var options = new RestClientOptions($"https://api.tomorrow.io/v4/weather/realtime?location={UALocation.Value.Value}&apikey={UADevKey.Value.Value}");
        var client = new RestClient(options);
        var request = new RestRequest("");

        request.AddHeader("accept", "application/json");
        request.AddHeader("accept-encoding", "deflate, gzip, br");
        try
        {
            var response = await client.GetAsync(request);

            // Deserialize JSON into a interface object 
            NSTomorrow.TomorrowObj Tobj = JsonSerializer.Deserialize<NSTomorrow.TomorrowObj>(response.Content);

            WD.dtNew = Convert.ToDateTime(Tobj.data.time);
            //WD.time = DateTime.SpecifyKind(Tobj.data.time, DateTimeKind.Utc);
            WD.dewPoint = Tobj.data.values.dewPoint;
            WD.humidity = (UAValue)Tobj.data.values.humidity;
            WD.pressureSurfaceLevel = (UAValue)Tobj.data.values.pressureSurfaceLevel;
            WD.temperature = (UAValue)Tobj.data.values.temperature;
            WD.windDirection = (UAValue)Tobj.data.values.windDirection;
            WD.windGust = (UAValue)Tobj.data.values.windGust;
            WD.windSpeed = (UAValue)Tobj.data.values.windSpeed;
            WD.lat = (UAValue)Tobj.location.lat;
            WD.lon = (UAValue)Tobj.location.lon;
            WD.dtString = Tobj.data.time.ToString();
            

            if (Tobj.location.name is null)
            {
                WD.name = "Lat/Lon";
            }
            else
            {
                WD.name = (UAValue)Tobj.location.name;
            }

            if (Tobj.location.type is null)
            {
                WD.type = "Lat/Lon";
            }
            else
            {
                WD.type = (UAValue)Tobj.location.type;
            }

        }
        catch (Exception ex)
        {
            Log.Info($"Error processing tomorrow: {ex.Message}");
        }
    }


}
