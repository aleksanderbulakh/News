﻿using News.business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.business.Interfaces
{
    interface INewsProvider
    {
        void SetAllNews(List<NewsViewModel> ListNews);

        List<NewsViewModel> GetAllNews();
    }
}
