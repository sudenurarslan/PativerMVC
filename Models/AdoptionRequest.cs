using System;
using System.ComponentModel.DataAnnotations;

namespace PativerMVC.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        public int AnimalId { get; set; }
        public Animal? Animal { get; set; } 

        public DateTime RequestDate { get; set; }
        public bool IsApproved { get; set; } 
    }
}