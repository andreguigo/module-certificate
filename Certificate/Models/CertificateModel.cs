namespace Certificate.Models
{
    public class CertificateModel
    {
        public string Name { get; set; }
        public string Licensed { get; set; }
        public string Event { get; set; }
        public string TextEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public int Workload { get; set; }
        public DateTime DateSign { get; set; }
    }
}