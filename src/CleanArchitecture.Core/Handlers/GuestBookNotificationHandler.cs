using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Linq;

namespace CleanArchitecture.Core.Handlers
{
    internal class GuestBookNotificationHandler : IHandle<EventAddedEntry>
    {
        private readonly IRepository _repository;
        private readonly IMessageSender _messageSender;

        public GuestBookNotificationHandler(IRepository repository,
            IMessageSender messageSender)
        {
            _repository = repository;
            _messageSender = messageSender;
        }
        public void Handle(EventAddedEntry domainEvent)
        {
            var guestbook = _repository.GetById<Guestbook>(1, "Entries");

            var emailsToNotify = guestbook.Entries
                .Where(e => e.DateTimeCreated > DateTimeOffset.UtcNow.AddDays(-1) &&
                e.Id != domainEvent.NewEntry.Id)
                .Select(e => e.EmailAddress);

            foreach (var emailAddress in emailsToNotify)
            {
                var message = $"{domainEvent.NewEntry.EmailAddress} left the new message: {domainEvent.NewEntry.Message}";
                _messageSender.SendGuestBookNotificationEmail(emailAddress, message);
            }
        }
    }
}
