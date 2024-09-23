using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CinemaSystem_HE161378.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }
        public int GenreId { get; set; } = default(int);
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Film>? Films { get; set; }
    }
}
