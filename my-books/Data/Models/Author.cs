namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Naviagation Properties
        public List<BookAuthor> Book_Authors { get; set; }

    }
}
