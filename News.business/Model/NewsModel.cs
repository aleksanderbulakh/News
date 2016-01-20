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
        public List<NewsOfListViewModel> NewsOnScreen(bool adminRole, bool editorRole, bool journalistRole, string userName)
        {
            var NewsList = new List<NewsOfListViewModel>();
            NewsOfListViewModel NewInList;
            var AllNews = NewsProviderProperty.DeserializeAll();
            foreach (var n in AllNews)
            {
                if (n.IsVisible)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsVisible = n.IsVisible,
                        Id = n.Id
                    };
                    NewsList.Add(NewInList);
                }
                else if (adminRole || editorRole)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsVisible = n.IsVisible,
                        Id = n.Id
                    };

                    NewsList.Add(NewInList);
                }
                else if (journalistRole && userName == n.Author)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsVisible = n.IsVisible,
                        Id = n.Id
                    };
                    NewsList.Add(NewInList);
                }
            }
            return NewsList;
        }

        public void AddNew(string userName, NewsViewModel new_add)
        {
            new_add.Date = DateTime.Now;
            new_add.Author = userName;
            new_add.Id = new Guid();
            new_add.Id = Guid.NewGuid();
            var AllNews = NewsProviderProperty.DeserializeAll();
            AllNews.Add(new_add);
            NewsProviderProperty.SerializeAll(AllNews);
        }

        public NewsViewModel MoreInfo(Guid id)
        {
            NewsProviderProperty.DeserializeAll();
            var selectedNew = new NewsViewModel();
            var AllNews = NewsProviderProperty.DeserializeAll();
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
            NewsProviderProperty.DeserializeAll();
            var AllNews = NewsProviderProperty.DeserializeAll();
            foreach (var n in AllNews)
            {
                if (n.Id == editedData.Id)
                {
                    n.Header = editedData.Header;
                    n.Content = editedData.Content;
                    n.IsVisible = editedData.IsVisible;
                }
            }
            NewsProviderProperty.SerializeAll(AllNews);
        }

        public NewsViewModel Edit(Guid id)
        {
            
            var SelectedNew = new NewsViewModel();
            var AllNews = NewsProviderProperty.DeserializeAll();
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
            var AllNews = NewsProviderProperty.DeserializeAll();
            AllNews.RemoveAll(m => m.Id == id);
            NewsProviderProperty.SerializeAll(AllNews);
        }
        public void SortNewsBy(string sortOrder)
        {
            var AllNews = NewsProviderProperty.DeserializeAll();
            switch (sortOrder)
            {
                case "ByAuthor":
                    AllNews = (List<NewsViewModel>)AllNews.OrderByDescending(m => m.Author);
                    break;
                case "ByDate":
                    AllNews = (List<NewsViewModel>)AllNews.OrderByDescending(m => m.Date);
                    break;
            }
            NewsProviderProperty.SerializeAll(AllNews);
        }
    }
}
