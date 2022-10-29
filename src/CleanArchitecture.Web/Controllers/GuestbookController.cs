using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Web.Api;
using CleanArchitecture.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    [VerifyGuestbookExistsAttribute]
    public class GuestbookController : BaseApiController
    {
        private readonly IRepository _repository;

        public GuestbookController(IRepository repository)
        {
            _repository = repository;
        }

        //GET: api/Guestbook/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");
            //if (guestbook is null)
            //{
            //    return NotFound(id);
            //}

            return Ok(guestbook);
        }

        //POST: api/Guestbook/1
        [HttpPost("{id:int}/NewEntry")]
        public IActionResult NewEntry(int id, [FromBody]GuestbookEntry newEntry)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");
            //if (guestbook is null)
            //{
            //    return NotFound(id);
            //}

            guestbook.AddEntry(newEntry);
            _repository.Update(guestbook);
            return Ok(guestbook);
        }
    }
}
