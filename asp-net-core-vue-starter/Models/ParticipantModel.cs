using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AspNetCoreVueStarter.Models
{
    public class ParticipantModel
    {
        [Key]
        public int Participantid { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string Firstname { get; set; }
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string? Familyname { get; set; }
        [Required]
        // Save ID code / company registration code as string. Different countries might use 
        // letters in the ID code field and no arithmatic will be done with. 
        [Column("code")]
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string Idcode { get; set; }
        public int? NumParticipants { get; set; }
        [Required]
        public string Paymentmethod { get; set; }
        [StringLength(5000, ErrorMessage = "Maksimaalselt 5000 tähemärki")]
        public string Details { get; set; }
        [Required]
        public string ParticipantType { get; set; }
        // JsonIgnore to avoid infinite recursive loop
        [JsonIgnore]
        public EventModel EventModel { get; set; }
    }
}
