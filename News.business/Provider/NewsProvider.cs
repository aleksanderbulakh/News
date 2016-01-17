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
        public List<NewsViewModel> DeserializeAll()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            List<NewsViewModel> AllNews = new List<NewsViewModel>();

            using (FileStream fs = new FileStream("D://News.json", FileMode.OpenOrCreate))
            {
                AllNews = (List<NewsViewModel>)jsonFormatter.ReadObject(fs);
                fs.Close();
            }

            return AllNews;
        }

        public void SerializeAll(List<NewsViewModel> ListNews)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            using (FileStream fs = new FileStream("D://News.json", FileMode.Create, FileAccess.Write))
            {

                jsonFormatter.WriteObject(fs, ListNews);
                fs.Close();
            }
        }
    }
}
