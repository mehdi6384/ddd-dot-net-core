using CleanArchitecture.Core.Entities;
using System.Collections.Generic;

namespace CleanArchitecture.Web.ViewModels
{
    public class HomePageViewModel
    {
        public string GuestbookName { get; set; }
        public List<GuestbookEntry> PreviouseEntries { get; set; }
        public GuestbookEntry NewEntry { get; set; }
    }
}
