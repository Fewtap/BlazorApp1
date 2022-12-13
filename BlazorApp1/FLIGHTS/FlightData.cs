using Newtonsoft.Json;
using RestSharp;

namespace FlightData
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
        [JsonProperty("Rute")]
        public string Rute { get; set; }

        [JsonProperty("DepartureAirport")]
        public string DepartureAirport { get; set; }

        [JsonProperty("ArrivalAirport")]
        public string ArrivalAirport { get; set; }

        [JsonProperty("Planned")]
        public DateTime Planned { get; set; }

        [JsonProperty("Estimated")]
        public DateTime? Estimated { get; set; }

        [JsonProperty("status_kl")]
        public string StatusKl { get; set; }

        [JsonProperty("status_en")]
        public string StatusEn { get; set; }

        [JsonProperty("status_da")]
        public string StatusDa { get; set; }

        [JsonProperty("FlightHash")]
        public string FlightHash { get; set; }

        [JsonProperty("ArrivalICAO")]
        public string ArrivalICAO { get; set; }

        [JsonProperty("DepartureICAO")]
        public string DepartureICAO { get; set; }
    }



    

    public class FlightDB
    {
        
        public static bool IsGettingData { get; set;} = false;
        
        

        public static async Task<List<Flight>> GetFlights(DateTime date)
        {
            if (IsGettingData)
            {
                return null;
            }
            else
            {
                IsGettingData = true;
            }
            string url = $"http://localhost:5018/api/flights/{date:yyyy-MM-dd}";
            RestClient client = new RestClient();

            RestRequest request = new RestRequest(url, Method.Get);

            RestResponse response = await client.ExecuteAsync(request);

            // Create a JsonSerializerSettings object.
            JsonSerializerSettings settings = new JsonSerializerSettings();

// Specify that null values should be ignored during serialization.
            settings.NullValueHandling = NullValueHandling.Include;


            var Flights = JsonConvert.DeserializeObject<List<Flight>>(response.Content, settings);

            
            
            
            IsGettingData = false;
            return Flights.ToList();

            
           
        }


        
        
        public static async Task PostRoom(List<Room> data)
        {
            // Set the URL of the API
            var url = "http://localhost:5018/api/rooms/add";

            // Create a new RestClient
            var client = new RestClient();

            // Create a new RestRequest
            var request = new RestRequest(url, Method.Post);

            // Set the request content type to JSON
            request.AddHeader("Content-Type", "application/json");

            // Serialize the struct to a JSON string
            var json = JsonConvert.SerializeObject(data,Formatting.None);

            // Add the JSON string to the request body
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // Send the request and get the response
            var response = await client.ExecuteAsync(request);

            // Print the response
            Console.WriteLine(json);
        }

        public async Task<List<Flight>> GetRooms(DateTime date)
        {
            // Set the URL of the API
            var url = $"http://localhost:5000/flights?date={date.ToString("d/MM/yyyy")}";

            // Create a new RestClient
            var client = new RestClient();

            // Create a new RestRequest
            var request = new RestRequest(url, Method.Post);

            // Set the request content type to JSON
            request.AddHeader("Content-Type", "application/json");

            
            

            

            // Send the request and get the response
            var response = await client.ExecuteAsync(request);

            // Print the response
            return JsonConvert.DeserializeObject<List<Flight>>(response.Content); 
        }


    }
}