using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private ApplicationDbContext _context;
        public PublishersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if(StringStartsWithNumber(publisher.Name))
            {
                throw new PublisherNameException("Name starts with Number", publisher.Name);
            }
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            return _publisher;
        }
        public List<Publisher> GetAllPublishers() => _context.Publishers.ToList();
        public Publisher GetPublisherById(int publisherId) => _context.Publishers.FirstOrDefault(x => x.Id == publisherId);
        public PublisherWithBookAndAuthorVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(p => p.Id == publisherId)
                .Select(x => new PublisherWithBookAndAuthorVM()
            {
                Name = x.Name,
                BookAuthors = x.Books.Select(b => new BookAuthorVM()
                {
                    BookName = b.Title,
                    BookAuthors = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                }).ToList()

            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int publisherId)
        {
            var _publisher = _context.Publishers.FirstOrDefault(b => b.Id == publisherId);
            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }

        private bool StringStartsWithNumber(string name) =>  Regex.IsMatch(name, @"^\d");
        
    }
}
