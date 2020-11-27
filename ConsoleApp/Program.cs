using System;
using System.Linq;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using Bogus;
using Bogus.DataSets;

namespace ConsoleApp
{
     internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        
        private static Faker _faker = new Faker();
        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            GetSamurais("Before Add:");
            
            Console.WriteLine("Do you want to add Samurais?");
            var yes = Console.ReadKey();
            if(yes.Key == ConsoleKey.Y)
            {
                AddSamurai(20);
            } else
            {
                Console.WriteLine("Ok, enough of those Samurais!");
            }
            
            GetSamurais("After Add: ");
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void AddSamurai(int num)
        {
            for (var i = 0; i < num; i++)
            {
                var fName = _faker.Name.FirstName(Name.Gender.Male);
                var samurai = new Samurai
                {
                    Name = fName
                };

                if (i % 5 == 0)
                {
                    var clan = new Clan
                    {
                        ClanName = _faker.Company.CompanyName()
                    };
                    
                    _context.AddRange(samurai, clan);
                    
                }
                else
                {
                    _context.Samurais.Add(samurai);
                }

                
            }
            
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.OrderBy(x => x.Name).ToList();
            Console.WriteLine($"{text} Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine($"id: {samurai.Id}, name: {samurai.Name}");
            }
        }
    }
}