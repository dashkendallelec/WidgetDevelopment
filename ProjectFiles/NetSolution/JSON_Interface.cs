#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.NetLogic;
using FTOptix.SerialPort;
using FTOptix.Core;
using FTOptix.WebUI;
#endregion

public class JSON_Interface : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
namespace NSTomorrow
{
    public class TomorrowObj
    {
        public Data data { get; set; }
        public Location location { get; set; }
    }

    public class Data
    {
        public string time { get; set; }
        public Values values { get; set; }
    }

    public class Values
    {
        public float cloudBase { get; set; }
        public float cloudCeiling { get; set; }
        public float cloudCover { get; set; }
        public float dewPoint { get; set; }
        public float freezingRainIntensity { get; set; }
        public float humidity { get; set; }
        public float precipitationProbability { get; set; }
        public float pressureSeaLevel { get; set; }
        public float pressureSurfaceLevel { get; set; }
        public float rainIntensity { get; set; }
        public float sleetIntensity { get; set; }
        public float snowIntensity { get; set; }
        public float temperature { get; set; }
        public float temperatureApparent { get; set; }
        public int uvHealthConcern { get; set; }
        public int uvIndex { get; set; }
        public float visibility { get; set; }
        public int weatherCode { get; set; }
        public float windDirection { get; set; }
        public float windGust { get; set; }
        public float windSpeed { get; set; }
    }

    public class Location
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string name { get; set; }
        public string type { get; set; }

    }
}
