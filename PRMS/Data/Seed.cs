using Microsoft.EntityFrameworkCore;
using PRMS.DTOs;
using PRMS.Entities;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace PRMS.Data
{
    public class Seed
    {
        public static async Task SeedGroup(DataContext context)
        {
            if (await context.Groups.AnyAsync()) return;

            var groupSeed = await System.IO.File.ReadAllTextAsync("Data/SeedData/GroupSeedData.json");
            var groups = JsonSerializer.Deserialize<List<Group>>(groupSeed);

            context.Groups.AddRange(groups);
            await context.SaveChangesAsync();
        }

        public static async Task SeedAppointed(DataContext context)
        {
            if (await context.Appointeds.AnyAsync()) return;

            var appointedSeed = await System.IO.File.ReadAllTextAsync("Data/SeedData/AppointedSeedData.json");
            var appointeds = JsonSerializer.Deserialize<List<Appointed>>(appointedSeed);

            context.Appointeds.AddRange(appointeds);
            await context.SaveChangesAsync();
        }

        public static async Task SeedPublisher(DataContext context)
        {
            if (await context.Publishers.AnyAsync()) return;

            var publisherSeed = await System.IO.File.ReadAllTextAsync("Data/SeedData/PublisherSeedData.json");
            var publishers = JsonSerializer.Deserialize<List<Publisher>>(publisherSeed);
            var appointeds = await context.Appointeds.ToListAsync();

            foreach (var publisher in publishers)
            {
                publisher.Appointeds = new List<Appointed>();
                foreach (var appointedId in publisher.AppointedIds)
                {
                    var appointed = appointeds.Find(x => x.Id == appointedId);

                    publisher.Appointeds.Add(appointed);
                }
            }

            context.Publishers.AddRange(publishers);
            await context.SaveChangesAsync();
        }
    }
}
