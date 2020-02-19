using AspNetCoreVueStarter.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AspNetCoreVueStarter.Models
{
    public class EventModel
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
        [StringLength(1000, ErrorMessage = "Maksimaalselt 1000 tähemärki")]
        public string Details { get; set; }
        public ICollection<ParticipantModel> Participants { get; set; } = new HashSet<ParticipantModel>();
    }
    
}
