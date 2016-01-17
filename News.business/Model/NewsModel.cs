using News.business.Provider;
using News.business.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;

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
            var All_News = NewsProviderProperty.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.IsView)
                {
                    NewInList = new NewsOfListViewModel
                    {
                        Author = n.Author,
                        Date = n.Date,
                        Header = n.Header,
                        IsView = n.IsView,
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
                        IsView = n.IsView,
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
                        IsView = n.IsView,
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
            var AllNews = NewsProviderProperty.Deserialize_All();
            AllNews.Add(new_add);
            NewsProviderProperty.Serialize_All(AllNews);
        }

        public NewsViewModel MoreInfo(Guid id)
        {
            NewsProviderProperty.Deserialize_All();
            var selectedNew = new NewsViewModel();
            var All_News = NewsProviderProperty.Deserialize_All();
            foreach (var n in All_News)
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
            NewsProviderProperty.Deserialize_All();
            var All_News = NewsProviderProperty.Deserialize_All();
            foreach (var n in All_News)
            {
                if (n.Id == editedData.Id)
                {
                    n.Header = editedData.Header;
                    n.Content = editedData.Content;
                    n.IsView = editedData.IsView;
                }
            }
            NewsProviderProperty.Serialize_All(All_News);
        }

        public NewsViewModel Edit(Guid id)
        {
            
            var SelectedNew = new NewsViewModel();
            var All_News = NewsProviderProperty.Deserialize_All();
            foreach (var n in All_News)
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
            var AllNews = NewsProviderProperty.Deserialize_All();
            AllNews.RemoveAll(m => m.Id == id);
            NewsProviderProperty.Serialize_All(AllNews);
        }
    }
}
