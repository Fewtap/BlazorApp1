using Newtonsoft.Json;
using RestSharp;

namespace BlazorApp1
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Flight
    {
        public string Rute { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime Planned { get; set; }
        public DateTime? Estimated { get; set; }
        public object Actual { get; set; }
        public Status Status { get; set; }
        public string FlightHash { get; set; }
        public string ArrivalICAO { get; set; }
        public string DepartureICAO { get; set; }

        public List<string> DeparturingRooms { get; set; }

        public Flight()
        {
            DeparturingRooms = new List<string>();
        }
    }

    public class Status
    {
        public string kl { get; set; }
        public string en { get; set; }
        public string da { get; set; }
    }

    public class FlightData
    {
        public struct DepartureInfo
        {
            public string Rute { get; set; }
            public string ArrivalAirport { get; set; }
            public DateTime Planned { get; set; }
            public List<string> Rooms { get; set; } = new List<string>();

            public DepartureInfo(Flight flight)
            {
                Rute = flight.Rute;
                ArrivalAirport= flight.ArrivalAirport;
                Planned = flight.Planned;

            }
        }
        public static bool IsGettingData { get; set;} = false;
        public static List<DepartureInfo> InfoList { get; set; } = new List<DepartureInfo>();
        public static List<Flight> Flights { get; set; } = new List<Flight>();

        public static async Task<List<Flight>> GetFlights()
        {
            if (FlightData.IsGettingData)
            {
                return null;
            }
            else
            {
                FlightData.IsGettingData = true;
            }
            string url = "https://www.mit.gl/wp-content/themes/mitgl/webservice.php?type=Departures&icao=BGJN";
            RestClient client = new RestClient();

            RestRequest request = new RestRequest(url, Method.Get);

            RestResponse response = await client.ExecuteAsync(request);



            Flights = JsonConvert.DeserializeObject<List<Flight>>(response.Content);
            foreach (var flight in Flights)
            {
                InfoList.Add(new DepartureInfo(flight));
            }
            IsGettingData = false;
            return Flights.ToList();

            
           
        }

        

        

    }
}
