using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaSystem_HE161378.Models
{
    public partial class Film
    {
        public Film()
        {
            Shows = new HashSet<Show>();
        }

        public int FilmId { get; set; } = default(int);
        public int GenreId { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string CountryCode { get; set; } = null!;
        public string? FilmUrl { get; set; }

        [JsonIgnore]
        public virtual Country? CountryCodeNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual Genre? Genre { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Show> Shows { get; set; }
    }
}
