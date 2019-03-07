using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace WebApi1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BooksController : Controller
    {
        [HttpGet,Authorize]
        public IEnumerable<Book> Get()
        {
            var currentUser = HttpContext.User;
            
            var resultBookList = new[]
            {
                new Book() {Author = "ねの1",Title = "写真集1",AgeRestriction = false},
                new Book() {Author = "ねの2",Title = "写真集2",AgeRestriction = false},
                new Book() {Author = "ねの3",Title = "写真集3",AgeRestriction = true},
            };
            return resultBookList;
        }
        
        [HttpGet]
        public IEnumerable<Book> Get2()
        {
            var currentUser = HttpContext.User;
            var resultBookList = new[]
            {
                new Book() {Author = "ねの10",Title = "写真集1",AgeRestriction = false},
                new Book() {Author = "ねの20",Title = "写真集2",AgeRestriction = false},
                new Book() {Author = "ねの30",Title = "写真集3",AgeRestriction = true},
            };
            return resultBookList;
        }
    }

    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public bool AgeRestriction { get; set; }
    }
}