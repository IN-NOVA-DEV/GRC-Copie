namespace grc_copie.Models
{
    public class Job
    {
        public int JobId { get; set; }

        public string JobName { get; set; }
        public int OrganisationId { get; set; }
        public virtual Organisation? Organisation { get; set; }
        
        //public virtual ICollection<Critere> Criteres { get; set; } = new List<Critere>();

        public int PersonId { get; set; }
        public virtual Person? Person { get; set; }
    }
}
