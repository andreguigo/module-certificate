namespace Certificate.Models
{
    public class Attested
    {
        public int Id { get; set; }
        public string Licensed { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TestimonialId { get; set; }
        public DateTime DateSign { get; set; }
    }
}