using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PativerMVC.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public string DonorName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public int? CampaignId { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign? Campaign { get; set; }
    }
}