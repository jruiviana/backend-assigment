using System;

namespace Movies.Core.Models
{
    public class SearchDto
    {
        public string Id { get; set; }
        public string Search_Token { get; set; }
        public string ImdbID { get; set; }
        public long Processing_Time_Ms { get; set; }
        public DateTime Timestamp { get; set; }
        public string Ip_Address { get; set; }
    }
}
