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
    }

    public class Status
    {
        public string kl { get; set; }
        public string en { get; set; }
        public string da { get; set; }
    }

    public class FlightData
    {
        static List<Flight> Flights;
        public FlightData()
        {
            
        }
    }


}
