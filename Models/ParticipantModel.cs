using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVueStarter.Models
{
    public class ParticipantModel
    {
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Firstname { get; set; }
        public string? Familyname { get; set; }
        [Column("code")]
        public string Idcode { get; set; }
        public int? NumParticipants { get; set; }
        public string Paymentmethod { get; set; }
        public string Details { get; set; }
        public string ParticipantType { get; set; }
        [ForeignKey("EventId")]
        public int EventModel { get; set; }
    }
}
