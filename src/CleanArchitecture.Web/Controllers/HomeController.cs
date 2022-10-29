using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            if (!_repository.List<Guestbook>().Any())
            {
                var newGuestbook = new Guestbook() { Name = "Mehdi Guestbook", Entries = new List<GuestbookEntry>() };
                newGuestbook.Entries.Add(new GuestbookEntry
                {
                    EmailAddress = "test@test.com",
                    Message = "Test Message",
                    DateTimeCreated = DateTime.UtcNow.AddHours(-1)
                });
                newGuestbook.Entries.Add(new GuestbookEntry
                {
                    EmailAddress = "test2@test.com",
                    Message = "Test2 Message",
                    DateTimeCreated = DateTime.UtcNow.AddHours(-2)
                });
                newGuestbook.Entries.Add(new GuestbookEntry
                {
                    EmailAddress = "test3@test.com",
                    Message = "Test3 Message",
                    DateTimeCreated = DateTime.UtcNow.AddHours(-3)
                });
                _repository.Add(newGuestbook);
            }

            var guestbook = _repository.GetById<Guestbook>(1, "Entries");

            var viewModel = new HomePageViewModel
            {
                GuestbookName = guestbook.Name,
                PreviouseEntries = guestbook.Entries
            };


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(HomePageViewModel model)
        {
            if(ModelState.IsValid)
            {
                var guestbook = _repository.GetById<Guestbook>(1, "Entries");
                guestbook.AddEntry(model.NewEntry);
                _repository.Update(guestbook);

                model = new HomePageViewModel()
                {
                    GuestbookName = guestbook.Name,
                    PreviouseEntries = guestbook.Entries
                };
            }

            return View(model);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
