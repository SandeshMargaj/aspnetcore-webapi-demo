using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_books.Controllers;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books_test
{
    public class PublishersControllerTest
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;


        ApplicationDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new ApplicationDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService);
        }

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_RetunsOkResult()
        {
            var result = publishersController.GetAllPublishers();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var actionResultData = (result as OkObjectResult).Value as List<Publisher> ;
            Assert.That(actionResultData.Count, Is.EqualTo(6));
        }
        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_RetunsOkResult()
        {
            var result = publishersController.GetPublisherById(1);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var actionResultData = (result as OkObjectResult).Value as Publisher;
            Assert.That(actionResultData.Name, Is.EqualTo("Publisher 1"));
        }
        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_RetunsNotFoundResult()
        {
            var result = publishersController.GetPublisherById(11);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreatedResult()
        {
            var newpublisher = new PublisherVM()
            {
                Name = "New Publisher"
            };
            var result = publishersController.AddPublisher(newpublisher);

            Assert.That(result, Is.TypeOf<CreatedResult>());
            var actionResultData = (result as CreatedResult).Value as Publisher;
            Assert.That(actionResultData.Name, Is.EqualTo("New Publisher"));
        }
        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequestdResult()
        {
            var newpublisher = new PublisherVM()
            {
                Name = "12New Publisher"
            };
            var result = publishersController.AddPublisher(newpublisher);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_ReturnsOkResult()
        {
            int publisherId = 6;
            var result = publishersController.DeletePublisherById(publisherId);

            Assert.That(result, Is.TypeOf<OkResult>());
        }
        [OneTimeTearDown]
        public void CleanUp()
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
                new Publisher(){ Id=4, Name="Publisher 4"},
                new Publisher(){ Id=5, Name="Publisher 5"},
                new Publisher(){ Id=6, Name="Publisher 6"}
            };
            context.Publishers.AddRange(publishers);

            context.SaveChanges();
        }
    }
}
