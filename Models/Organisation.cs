namespace grc_copie.Models
{
    public class Organisation
    {
        public int OrganisationId { get; set; }

        public string OrganisationName { get; set; }

        public ICollection<AppUser>? AppUserList { get; set; }
    }
}
