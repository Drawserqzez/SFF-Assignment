namespace SFF.Domain.Models {
    public enum StarAmount { One = 1, Two, Three, Four, Five };
    public class Grade {
        public int ID { get; set; }
        public Movie Movie { get; set; }
        public Studio Studio { get; set; }
        public StarAmount Rating { get; set; }
    }
}