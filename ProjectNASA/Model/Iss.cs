namespace ProjectNASA.Model
{
    public class Iss
    {
        public Iss()
        {
            Id = 25544; // ISS NORAD catalog id = 25544
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public string Visibility { get; set; }
        public double Footprint { get; set; }
        public int Timestamp { get; set; }
        public double Daynum { get; set; }
        public double SolarLat { get; set; }
        public double SolarLon { get; set; }
        public string Units { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
