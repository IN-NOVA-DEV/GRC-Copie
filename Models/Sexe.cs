using System.ComponentModel.DataAnnotations;
using System.Security.Principal;


namespace grc_copie.Models
{
    public class Sexe
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
