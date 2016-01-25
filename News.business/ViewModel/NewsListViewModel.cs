using News.business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.business.ViewModel
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsOfListViewModel> NewsPerPages { get; set; }
        public PageInfo PageData { get; set; }
        public string SortOrder { get; set; }
    }
}
