using AspNetCoreVueStarter.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVueStarter.Models
{
    public class EventsModel
    {
        [Key]
        public int Eventid { get; set; }
        [Display(Name = "Ürituse nimi")]
        [Required(ErrorMessage = "Palun sisestage ürituse nimi")]
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string EventName { get; set; }
        [Display(Name = "Toimumisaeg")]
        [Required(ErrorMessage = "Palun sisestage toimumisaeg")]
        [MyDate]
        public DateTime EventDate { get; set; }
        [Display(Name = "Koht")]
        [Required(ErrorMessage = "Palun sisestage koht")]
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string Location { get; set; }
        [Display(Name = "Lisainfo")]
        [StringLength(1500, ErrorMessage = "Maksimaalselt 1500 tähemärki")]
        public string Details { get; set; }
        public ICollection<ParticipantModel> Participants { get; set; }
    }
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
        [Column("code")]
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string Idcode { get; set; }
        public int? NumParticipants { get; set; }
        [Required]
        public string Paymentmethod { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "Maksimaalselt 5000 tähemärki")]
        public string Details { get; set; }
        [Required]
        public string ParticipantType { get; set; }
        [Required]
        [ForeignKey("EventId")]
        public int Eventid { get; set; }
        public EventsModel EventModel { get; set; }
    }
}
