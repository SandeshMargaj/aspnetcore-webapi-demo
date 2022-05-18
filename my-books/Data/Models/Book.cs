﻿namespace my_books.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //Naviagation Properties
        public int publisherId { get; set; }
        public Publisher publisher { get; set; }
        public List<BookAuthor> Book_Authors { get; set; }
    }
}