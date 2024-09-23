using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaSystem_HE161378.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int ShowId { get; set; }
        public string? Name { get; set; }
        public string? SeatStatus { get; set; }
        public decimal? Amount { get; set; }
        [JsonIgnore]
        public virtual Show? Show { get; set; } = null!;
    }
}
