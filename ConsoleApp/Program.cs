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
        private static SamuraiContext context = new SamuraiContext();
        
        private static Faker faker = new Faker();
        private static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            GetSamurais("Before Add:");
            AddSamurai(20);
            GetSamurais("After Add: ");
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void AddSamurai(int num)
        {
            for (var i = 0; i < num; i++)
            {
                var fName = faker.Name.FirstName(Name.Gender.Male);
                var samurai = new Samurai
                {
                    Name = fName
                };

                context.Samurais.Add(samurai);
            }
            
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text} Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}