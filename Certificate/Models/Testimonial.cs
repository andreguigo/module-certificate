namespace Certificate.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public string Text { get; set; }
        public DateTime DateEvent { get; set; }
        public int Workload { get; set; }
    }
}