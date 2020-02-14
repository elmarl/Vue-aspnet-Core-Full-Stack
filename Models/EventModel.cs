using AspNetCoreVueStarter.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVueStarter.Models
{
    public class EventModel
    {
        [Key]
        public int Id { get; set; }
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
        [StringLength(50, ErrorMessage = "Maksimaalselt 50 tähemärki")]
        public string Details { get; set; }
    }
}
