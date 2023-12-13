using System.ComponentModel.DataAnnotations;

namespace grc_copie.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string? EmailOwner { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{H:mm}")]
        public DateTime StartAt { get; set; }

        [DisplayFormat(DataFormatString = "{H:mm}")]
        public DateTime EndAt { get; set; }

        public string TaskTitle { get; set; }
    }
}
