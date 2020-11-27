using System;
using System.Collections.Generic;
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
            
           // AddSamuraisToBattles();

            var samuraiBattleStats = _context.SamuraiBattleStats.OrderByDescending(x => x.NoOfBattles).ToList();

            foreach (var samuraiBattleStat in samuraiBattleStats)
            {
               Console.WriteLine($"{samuraiBattleStat.Id}, {samuraiBattleStat.Name}, {samuraiBattleStat.NoOfBattles}");     
            }
            
            // Console.WriteLine("Do you want to add Samurais?");
            // var yes = Console.ReadKey();
            // if(yes.Key == ConsoleKey.Y)
            // {
            //     AddSamurai(20);
            // } else
            // {
            //     Console.WriteLine("Ok, enough of those Samurais!");
            // }
            //
            // GetSamurais("After Add: ");
            Console.Write("Press any key...");
            Console.ReadKey();
        }


        private static void AddSamuraisToBattles()
        {
            var samurais = _context.Samurais.Skip(0).Take(20).ToList();
            var index = 0;
            
            foreach (var samurai in samurais)
            {
                samurai.SamuraiBattles.AddRange(createSamuraiBattles(index, samurai));
                index += 1;
            }

            _context.SaveChanges();
        }

        private static List<SamuraiBattle> createSamuraiBattles(int index, Samurai samurai)
        {
            var even = index % 2 == 0;
            var fifth = index % 5 == 0;
            var samuraiBattles = new List<SamuraiBattle>();
            var num = 1;

            if (even)
            {
                num = 2;
            }
            else if (fifth)
            {
                num = 5;
            }

            for (var i = 0; i < num; i++)
            {
                var b = createBattle();
                var sb = new SamuraiBattle
                {
                    Battle = b,
                    Samurai = samurai
                };
                
                samuraiBattles.Add(sb);
            }

            return samuraiBattles;
        }

        private static Battle createBattle()
        {
            return new Battle
            {
                Name = $"Battle {_faker.Address.City()}",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(14)
            };
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