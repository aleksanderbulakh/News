using System;

namespace News.business.ViewModel
{
    public class NewsOfListViewModel
    {
        public Guid Id { get; set; }

        public string Header { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        public bool IsVisible { get; set; }

        public string AuthorId { get; set; }
    }
}
