namespace SFF.Domain.Models {
    public class Movie {
        public int ID { get; internal set; }
        public string Title { get; internal set; }

        public Movie(int id = 1, string title = "Hej") {
            ID = id;
            Title = title;
        }
    }
}