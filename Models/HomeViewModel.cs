// Models/HomeViewModel.cs
using System.Collections.Generic;

namespace PativerMVC.Models
{
    public class HomeViewModel
    {
        public List<Animal> RecentAnimals { get; set; }
        public List<Campaign> ActiveCampaigns { get; set; }
    }
}