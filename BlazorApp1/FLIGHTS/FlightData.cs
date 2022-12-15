using FlightData;
using Newtonsoft.Json;
using RestSharp;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Lists.Internal;
using System.ComponentModel;
using System.Security.Cryptography;

namespace FlightData
{
    public class Room
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

        public static bool IsGettingData { get; set; } = false;



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
            string url = $"http://85.159.211.191/api/Flights/{date:yyyy-MM-dd}";
            RestClient client = new RestClient();

            RestRequest request = new RestRequest(url, Method.Get);

            RestResponse response = await client.ExecuteAsync(request);

            Console.WriteLine("GetFlights: " + response.Content);
            Console.WriteLine("GetFlights: " + response.StatusCode);

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
            var url = "http://85.159.211.191/api/flights";

            // Create a new RestClient
            var client = new RestClient();

            // Create a new RestRequest
            var request = new RestRequest(url, Method.Post);


            // Set the request content type to JSON
            request.AddHeader("Content-Type", "application/json");

            // Serialize the struct to a JSON string
            var json = JsonConvert.SerializeObject(data, Formatting.None);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            string payload = System.Text.Encoding.UTF8.GetString(bytes);
            //payload = "\"" + payload + "\"";

            payload = JsonConvert.ToString(json);
            // Add the JSON string to the request body
            request.AddParameter("application/json", payload, ParameterType.RequestBody);

            // Send the request and get the response
            var response = await client.ExecuteAsync(request);

            Console.WriteLine("PostRoom: " + response.Content);
            Console.WriteLine("PostRoom: " + response.StatusCode);

            // Print the response
            Console.WriteLine(response.StatusCode.ToString());
            Console.WriteLine(response.Content.ToString());
        }

        public static async Task<List<Room>> GetRooms(string hash)
        {
            // Set the URL of the API
            var url = $"http://85.159.211.191/api/Flights/rooms/{hash}";

            // Create a new RestClient
            var client = new RestClient();

            // Create a new RestRequest
            var request = new RestRequest(url, Method.Get);

            // Set the request content type to JSON
            request.AddHeader("Content-Type", "application/json");
            





            // Send the request and get the response
            var response = await client.ExecuteAsync(request);

            Console.WriteLine("GetRooms: " + response.Content);
            Console.WriteLine("GetRooms: " + response.StatusCode);

            // Print the response

            List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(response.Content);
            return rooms;
        }

        public static async void DeleteRooms(Room room)
        {
            var client = new RestClient();

            var request = new RestRequest($"http://85.159.211.191/api/flights/{room.FlightHash}/room/{room.RoomNumber}", Method.Delete);
            Console.WriteLine("Room to be deleted: ");
            Console.WriteLine(room.RoomNumber);
            Console.WriteLine(room.FlightHash);

            request.AddParameter("hash", room.FlightHash);
            request.AddParameter("roomnumber", room.RoomNumber);

            var response = await client.ExecuteAsync(request);

            Console.WriteLine("DeleteRooms: " + response.Content);
            Console.WriteLine("DeleteRooms: " + response.StatusCode);

            Console.WriteLine(response.Content);


        }
    }
}



