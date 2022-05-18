﻿using my_books.Data.Models;

namespace my_books.Data
{
    public class AppDBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceSope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceSope.ServiceProvider.GetService<ApplicationDbContext>();
                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st Book Title",
                        Description = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-11),
                        Rate = 4,
                        Genre = "Biography",
                        //Author = "First Author",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "2nd Book Title",
                        Description = "2nd Book Description",
                        IsRead = false,
                        Genre = "Biography",
                        //Author = "First Author",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}