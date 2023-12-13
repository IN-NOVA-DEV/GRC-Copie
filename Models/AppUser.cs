namespace grc_copie.Models
{
    [Serializable]
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

   
    }
}
