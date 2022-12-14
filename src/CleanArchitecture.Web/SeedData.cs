using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Web
{
    public static class SeedData
    {
        public static readonly ToDoItem ToDoItem1 = new ToDoItem
        {
            Title = "Get Sample Working",
            Description = "Try to get the sample to build."
        };
        public static readonly ToDoItem ToDoItem2 = new ToDoItem
        {
            Title = "Review Solution",
            Description = "Review the different projects in the solution and how they relate to one another."
        };
        public static readonly ToDoItem ToDoItem3 = new ToDoItem
        {
            Title = "Run and Review Tests",
            Description = "Make sure all the tests run and review what they are doing."
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
            {
                // Look for any TODO items.
                if (dbContext.ToDoItems.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);


            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.ToDoItems)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(ToDoItem1);
            dbContext.ToDoItems.Add(ToDoItem2);
            dbContext.ToDoItems.Add(ToDoItem3);

            dbContext.SaveChanges();

            foreach (var item in dbContext.Guestbooks)
            {
                dbContext.Remove(item);
            }

            var guestbook = new Guestbook { 
                Id = 1,
                Name = "Test",
                Entries = new List<GuestbookEntry>()
            };
            guestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "test@test.com",
                Message = "Test"
            });
            dbContext.Guestbooks.Add(guestbook);
            dbContext.SaveChanges();
        }
    }
}
