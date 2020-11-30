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

        [Fact]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("InsertSamurai");

            using var context = new SamuraiContext(builder.Options);
            
            var samurai = new Samurai();
            context.Samurais.Add(samurai);
            context.SaveChanges();
            Assert.NotEqual(0, samurai.Id);
        }
    }
}