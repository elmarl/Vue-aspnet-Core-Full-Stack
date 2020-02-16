using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVueStarter.Models
{
    public class ParticipantModelignores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Firstname { get; set; }
        public string? Familyname { get; set; }
        [Required]
        [Column("code")]
        public string Idcode { get; set; }
        public int? NumParticipants { get; set; }
        [Required]
        public string Paymentmethod { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string ParticipantType { get; set; }
        [Required]
        [ForeignKey("EventId")]
        public int EventModel { get; set; }
    }
}
