using Certificate.Models;

namespace Certificate.Repositories
{
    public class AttestedRepository
    {
        public AttestedRepository(bool data)
        {
            if(data)
            {
                CreateMemoryData();
            } 
            else
            {
                AttestedList = new List<Attested>();
            }
        }

        private void CreateMemoryData()
        {
            this.AttestedList = new List<Attested>()
            {
                new Attested() { Id = 1, Licensed = "123abc", Name = "Bruce Wayne", Email = "bruce.wayne@gothan.org", TestimonialId = 1, DateSign = new DateTime(2023, 12, 12) },
                new Attested() { Id = 2, Licensed = "abc123", Name = "Robin", Email = "robin@gothan.org", TestimonialId = 2, DateSign = new DateTime(2023, 12, 12) }
            };
        }
        public List<Attested> AttestedList { get; private set; }

        public List<Attested> SelectAllAttested()
        {
            return AttestedList;
        }
        
        public Attested SelectAttested(string licensed)
        {
            var temp = (from attested in AttestedList where attested.Licensed == licensed select attested).SingleOrDefault();
            if(temp != null )
                return temp;
            return new Attested();
        }
    }
}