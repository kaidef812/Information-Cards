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
        public async Task Delete(BookCard bookCard)
        {
            await _cardsContext.Remove(bookCard);
        }

        [HttpPut]
        public async Task Put(BookCard bookCard)
        {
            await _cardsContext.Edit(bookCard);
        }

        //// GET: api/<BookCardsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<BookCardsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<BookCardsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<BookCardsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BookCardsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
