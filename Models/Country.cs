using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaSystem_HE161378.Models
{
    public partial class Country
    {
        public Country()
        {
            Films = new HashSet<Film>();
        }

        public string CountryCode { get; set; } = null!;
        public string? CountryName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Film>? Films { get; set; }
    }
}
