using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class NewsOfListViewModel
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public bool IsView { get; set; }
    }
}