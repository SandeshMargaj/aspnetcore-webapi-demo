using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books_test
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;
        ApplicationDbContext context;
        PublishersService publishersService;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new ApplicationDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            publishersService = new PublishersService(context);
        }


        [Test, Order(1)]
        public void GetAllPublishers_ReturnsPublisherList()
        {
            var result = publishersService.GetAllPublishers();

            //Assert.AreEqual(3, result.Count);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test, Order(2)]
        public void GetPublisherById_ValidIdPassed_ReturnsPublisher()
        {
            var publisher = publishersService.GetPublisherById(2);

            Assert.That(publisher, Is.Not.Null);
            Assert.That(publisher.Name, Is.EqualTo("Publisher 2"));
        }

        [Test, Order(3)]
        public void GetPublisherById_InValidIdPassed_ReturnsNull()
        {
            var publisher = publishersService.GetPublisherById(0);

            Assert.That(publisher, Is.Null);
        }

        [Test, Order(4)]
        public void AddPublisher_ValidPublisherPassed_AddsPublisherToDB()
        {

            var newPublisher = new PublisherVM()
            {
                Name = "Publisher 4"
            };

            var result = publishersService.AddPublisher(newPublisher);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Does.StartWith("Publisher"));

        }

        [Test, Order(5)]
        public void AddPublisher_InValidPublisherPassed_RaisesException()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "2 Publisher"
            };
            
            Assert.That(() => publishersService.AddPublisher(newPublisher),
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with Number"));

        }

        [OneTimeTearDown]
        public void CleanUp ()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher(){ Id=1, Name="Publisher 1"},
                new Publisher(){ Id=2, Name="Publisher 2"},
                new Publisher(){ Id=3, Name="Publisher 3"},
            };
            context.Publishers.AddRange(publishers);
            var authors = new List<Author>
            {
                new Author(){Id =1, FullName="Author 1"},
                new Author(){Id =2, FullName="Author 2"},
                new Author(){Id =3, FullName="Author 3"},
            };
            context.Authors.AddRange(authors);
            var books = new List<Book>
            {
                new Book()
                {
                    Id=1,
                    Title="Book Title 1",
                    Description="Book 1 Description",
                    IsRead= false,
                    Genre= "Genre",
                    CoverUrl ="http...",
                    DateAdded = DateTime.Now.AddDays(-3),
                    publisherId = 1
                },
                new Book()
                {
                    Id=2,
                    Title="Book Title 2",
                    Description="Book 2 Description",
                    IsRead= false,
                    Genre= "Genre",
                    CoverUrl ="http...",
                    DateAdded = DateTime.Now.AddDays(-3),
                    publisherId = 1
                }
            };
            context.Books.AddRange(books);
            var book_authors = new List<BookAuthor>
            {
                new BookAuthor()
                {
                    Id = 1,
                    AuthorId = 1,
                    BookId = 1
                },
                new BookAuthor()
                {
                    Id = 2,
                    AuthorId = 1,
                    BookId = 2
                },
                new BookAuthor()
                {
                    Id = 3,
                    AuthorId = 2,
                    BookId = 2
                }
            };
            context.Books_Authors.AddRange(book_authors);

            context.SaveChanges();
        }
    }
}