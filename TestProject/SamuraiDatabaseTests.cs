using System;
using System.Diagnostics;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace TestProject
{
    public class SamuraiDatabaseTests
    {

        private void CreateDb(Action<SamuraiContext> action)
        {
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("InsertSamurai");

            using var context = new SamuraiContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            action(context);
        }
        
        [Fact]
        public void CanInsertSamuraiIntoDatabase()
        {
            Action<SamuraiContext> action = (context) =>
            {
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                context.SaveChanges();
                Assert.NotEqual(0, samurai.Id);
            };
           
            CreateDb(action);
           
        }
        
        [Fact]
        public void CanDeleteSamuraiIntoDatabase()
        {
            Action<SamuraiContext> action = (context) =>
            {
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                context.SaveChanges();
                Assert.NotEqual(0, samurai.Id);

                var id = samurai.Id;
                context.Samurais.Remove(samurai);
                context.SaveChanges();

                var deletedSamurai = context.Samurais.Find(id);
                Assert.Null(deletedSamurai);
            };
           
            CreateDb(action);
           
        }
    }
}