using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.SharedKernel;

namespace CleanArchitecture.Core.Events
{
    public class EventAddedEntry : BaseDomainEvent
    {
        public int GuestBookId { get; }
        public GuestbookEntry NewEntry { get; }

        public EventAddedEntry(int guestBookId, GuestbookEntry entry)
        {
            GuestBookId = guestBookId;
            NewEntry = entry;
        }
    }
}
