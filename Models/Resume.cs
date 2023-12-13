namespace grc_copie.Models
{
    public class Resume
    {
        public int ResumeId { get; set; }

        public Person Person { get; set; }
        public byte[] ResumeFile { get; set; }
    }
}
