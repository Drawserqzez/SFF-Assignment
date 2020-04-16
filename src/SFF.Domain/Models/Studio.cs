using System.Collections.Generic;

namespace SFF.Domain.Models {
    public class Studio {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Movie> BorrowedMovies { get; set; }
    }
}