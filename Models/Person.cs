using System.ComponentModel.DataAnnotations;

namespace grc_copie.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DayOfBirth { get; set; }
        public int? SexeId { get; set; }
        public Sexe? Sexe { get; set; }

        public string? Pays { get; set; }

        public string? Ville { get; set; }

        public string? BP { get; set; }
        public string? PhoneNumber { get; set; }

        public string? PhoneNumberPro { get; set; }
        public string? Email { get; set; }

        public bool? IsPending { get; set; }
        public byte[]? Photo { get; set; }


        public ICollection<Job>? Jobs { get; set; } = new List<Job>();

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
        public bool? Statut { get; set; }

        public string? Comment { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }

        public string? PendAcceptedBy { get; set; }
        //public ICollection<DynamicColumnValue> DynamicColumnValues { get; set; }
        //public ICollection<TextStandartModel> TextStandartModels { get; set; } = new List<TextStandartModel>();
    }
}
