using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationCards_Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InformationCards_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCardsController : ControllerBase
    {
        private BookCardsContext _cardsContext;

        public BookCardsController()
        {
            _cardsContext = new BookCardsContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCard>>> Get()
        {
            return await _cardsContext.Get();
        }

        [HttpPost]
        public async Task Post(BookCard bookCard)
        {
            await _cardsContext.Add(bookCard);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _cardsContext.Remove(id);
        }

        [HttpPut]
        public async Task Put(BookCard bookCard)
        {
            await _cardsContext.Edit(bookCard);
        }
    }
}
