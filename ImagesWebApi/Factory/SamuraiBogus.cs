using Bogus;
using SamuraiApp.Domain;

namespace ImagesWebApi.Factory
{
    public class SamuraiBogus
    {


        public static Samurai Create()
        {
            var faker = new Faker();

            return new Samurai
            {
                Name = faker.Name.FirstName()
            };
        }
    }
}