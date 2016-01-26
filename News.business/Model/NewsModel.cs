using News.business.Provider;
using News.business.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace News.business.Model
{
    public class NewsModel
    {
        private static IKernel appKernel;
        public static IKernel AppKernelProperty
        {
            get
            {
                if (appKernel == null)
                    appKernel = new StandardKernel(new NewsProviderNinjectModule());
                return appKernel;
            }
        }

        private static NewsProvider newsProvider;
        public static NewsProvider NewsProviderProperty
        {
            get
            {
                if (newsProvider == null)
                    newsProvider = AppKernelProperty.Get<NewsProvider>();
                return newsProvider;
            }
        }
        public List<NewsViewModel> NewsOnScreen()
        {
            var NewsList = new List<NewsViewModel>();

            var allNews = NewsProviderProperty.GetAllNews();

            return allNews;
        }

        public void AddNew(NewsViewModel new_add)
        {
            new_add.Date = DateTime.Now;

            new_add.Id = new Guid();
            new_add.Id = Guid.NewGuid();

            var AllNews = NewsProviderProperty.GetAllNews();

            AllNews.Add(new_add);

            NewsProviderProperty.SetAllNews(AllNews);
        }

        public NewsViewModel MoreInfo(Guid id)
        {
            NewsProviderProperty.GetAllNews();

            var selectedNew = new NewsViewModel();

            var AllNews = NewsProviderProperty.GetAllNews();

            foreach (var n in AllNews)
            {
                if (n.Id == id)
                {
                    selectedNew = new NewsViewModel(n);
                }
            }

            return selectedNew;
        }

        public void Edit(NewsViewModel editedData)
        { 
            var AllNews = NewsProviderProperty.GetAllNews();

            foreach (var n in AllNews)
            {
                if (n.Id == editedData.Id)
                {
                    n.Header = editedData.Header;
                    n.Content = editedData.Content;
                    n.IsVisible = editedData.IsVisible;
                }
            }

            NewsProviderProperty.SetAllNews(AllNews);
        }

        public NewsViewModel Edit(Guid id)
        {
            
            var SelectedNew = new NewsViewModel();

            var AllNews = NewsProviderProperty.GetAllNews();

            foreach (var n in AllNews)
            {
                if (n.Id == id)
                {
                    SelectedNew = new NewsViewModel(n);
                }
            }

            return SelectedNew;            
        }

        public void DeleteNews(Guid id)
        {
            var AllNews = NewsProviderProperty.GetAllNews();

            AllNews.RemoveAll(m => m.Id == id);

            NewsProviderProperty.SetAllNews(AllNews);
        }
        public List<NewsViewModel> SortNewsBy(string sortOrder, List<NewsViewModel> AllNews)
        {
            switch (sortOrder)
            {
                case "ByAuthor":
                    AllNews = AllNews.OrderByDescending(m=>m.Author).ToList();
                    break;
                case "ByDate":
                    AllNews = AllNews.OrderBy(m => m.Date).ToList();
                    break;
            }

            return AllNews;
        }
    }
}
