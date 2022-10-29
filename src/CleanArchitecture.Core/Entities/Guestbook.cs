﻿using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.SharedKernel;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Guestbook : BaseEntity
    {
        public string Name { get; set; }

        public List<GuestbookEntry> Entries { get; set; }

        public void AddEntry(GuestbookEntry entry)
        {
            Entries.Add(entry);
            Events.Add(new EventAddedEntry(Id, entry));
        }
    }
}
