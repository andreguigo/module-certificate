using Certificate.Models;

namespace Certificate.Repositories
{
    public class TestimonialRepository
    {
        public TestimonialRepository(bool data)
        {
            if(data)
            {
                CreateMemoryData();
            } 
            else
            {
                TestimonialList = new List<Testimonial>();
            }
        }

        private void CreateMemoryData()
        {
            this.TestimonialList = new List<Testimonial>()
            {
                new Testimonial() { Id = 1, Event = "How to be a Super Hero?", Text = "how to be a super hero and save people in a violent city.", DateEvent = new DateTime(2023, 12, 10), Workload = 32 },
                new Testimonial() { Id = 2, Event = "How to be a Superhero's assistant?", Text = "how to be a superhero's assistant and confuse a villain.", DateEvent = new DateTime(2023, 12, 10), Workload = 16 }
            };
        }
        public List<Testimonial> TestimonialList { get; private set; }

        public List<Testimonial> SelectAllTestimonial()
        {
            return TestimonialList;
        }

        public Testimonial SelectTestimonial(int id)
        {
            var temp = (from testimonial in TestimonialList where testimonial.Id == id select testimonial).SingleOrDefault();
            if(temp != null )
                return temp;
            return new Testimonial();
        }
    }
}