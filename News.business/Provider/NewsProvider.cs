using System;
using News.business.Interfaces;
using News.business.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace News.business.Provider
{
    [DataContract]
    public class NewsProvider : INewsProvider
    {
        public string path = AppDomain.CurrentDomain.BaseDirectory + "/News.json";

        public List<NewsViewModel> GetAllNews()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (List<NewsViewModel>)jsonFormatter.ReadObject(fs);
            }
        }

        public void SetAllNews(List<NewsViewModel> ListNews)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                jsonFormatter.WriteObject(fs, ListNews);
            }
        }

        public NewsViewModel GetById(Guid id)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            List<NewsViewModel> newsList;
            //NewsViewModel selectedNewsArticle;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                newsList = (List<NewsViewModel>)jsonFormatter.ReadObject(fs);
            }

            return newsList.Find(m => m.Id == id);
        }
    }
}
