using PativerMVC.Models;

namespace PativerMVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            
            context.Database.EnsureCreated();

            
            context.Animals.RemoveRange(context.Animals);
            context.Campaigns.RemoveRange(context.Campaigns);
            context.SaveChanges();

            
            var animals = new Animal[]
            {
                new Animal { Name = "Bici", Age = 5, Breed = "Tekir", Description = "Sevecen bir kedi", ImagePath = "/images/cat1.jpg", CreatedDate = DateTime.Now.AddDays(-5), IsAdopted = false },
                new Animal { Name = "Elmas", Age = 2, Breed = "Tekir", Description = "Sadık ve koruyucu bir köpek", ImagePath = "/images/cat2.jpg", CreatedDate = DateTime.Now.AddDays(-3), IsAdopted = false },
                new Animal { Name = "Duman", Age = 2, Breed = "British", Description = "Oyuncu ve enerjik bir kedi", ImagePath = "/images/cat3.jpg", CreatedDate = DateTime.Now.AddDays(-1), IsAdopted = false }
            };

            foreach (var a in animals)
            {
                context.Animals.Add(a);
            }

            
            var campaigns = new Campaign[]
            {
                new Campaign { Title = "Kış Yardım Kampanyası", Description = "Soğuk kış günlerinde sokak hayvanlarına barınak ve yiyecek sağlıyoruz", TargetAmount = 10000, CollectedAmount = 4500, ImagePath = "/images/campaign1.jpg", StartDate = DateTime.Now.AddMonths(-1) },
                new Campaign { Title = "Aşı Kampanyası", Description = "Sokak hayvanlarını hastalıklardan korumak için aşılıyoruz", TargetAmount = 5000, CollectedAmount = 3200, ImagePath = "/images/campaign2.jpg", StartDate = DateTime.Now.AddDays(-15) }
            };

            foreach (var c in campaigns)
            {
                context.Campaigns.Add(c);
            }

            context.SaveChanges();
        }
    }
}
