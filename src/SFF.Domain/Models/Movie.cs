using System.Collections.Generic;

namespace SFF.Domain.Models {
    public class Movie {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool Borrowed { get; set; } = false;
    }
}