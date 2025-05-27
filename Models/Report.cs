using System;
using System.ComponentModel.DataAnnotations;

namespace PativerMVC.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public string AnimalType { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImagePath { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.Now;

        public bool IsAdopted { get; set; } = false;
    }
}