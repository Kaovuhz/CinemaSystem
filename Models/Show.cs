using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaSystem_HE161378.Models
{
    public partial class Show
    {
        public Show()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ShowId { get; set; } = default(int);
        public int RoomId { get; set; } 
        public int FilmId { get; set; }
        public DateTime ShowDate { get; set; }
        public decimal Price { get; set; }
        public bool? Status { get; set; }
        public int Slot { get; set; }
        [JsonIgnore] 
        public virtual Film? Film { get; set; } = null!;
        [JsonIgnore]
        public virtual Room? Room { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
