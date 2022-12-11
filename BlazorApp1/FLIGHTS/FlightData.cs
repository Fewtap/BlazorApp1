using Newtonsoft.Json;
using RestSharp;
using System;

namespace BlazorApp1
{

    public struct Room
    {
        public string RoomNumber { get; set; }
        public string FlightHash { get; set; }

        public Room(string _rn, string _fh)
        {
            RoomNumber = _rn;
            FlightHash = _fh;
        }
    }
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
        
        public static bool IsGettingData { get; set;} = false;
        
        public static List<Flight> Flights { get; set; }

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
                flight.Planned = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(flight.Planned, "Greenland Standard Time");
                if(flight.Estimated != null)
                {
                    flight.Estimated = TimeZoneInfo.ConvertTimeBySystemTimeZoneId((DateTime)flight.Estimated, "Greenland Standard Time");
                }

                Console.WriteLine(flight.Planned.ToShortTimeString());
                
            }
            
            IsGettingData = false;
            return Flights.ToList();

            
           
        }


        public static async Task PostRoom(Room data)
        {
            // Set the URL of the API
            var url = "http://localhost:5000/api/data";

            // Create a new RestClient
            var client = new RestClient();

            // Create a new RestRequest
            var request = new RestRequest(url, Method.Post);

            // Set the request content type to JSON
            request.AddHeader("Content-Type", "application/json");

            // Serialize the struct to a JSON string
            var json = JsonConvert.SerializeObject(data);

            // Add the JSON string to the request body
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // Send the request and get the response
            var response = await client.ExecuteAsync(request);

            // Print the response
            Console.WriteLine(response.Content);
        }


    }
}
