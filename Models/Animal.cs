// Animal.cs
using System.ComponentModel.DataAnnotations;

namespace PativerMVC.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }
        public string Breed { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }

        public bool IsAdopted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}


