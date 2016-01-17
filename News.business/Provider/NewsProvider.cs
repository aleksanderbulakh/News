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
        public List<NewsViewModel> Deserialize_All()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<NewsViewModel>));

            List<NewsViewModel> All_News = new List<NewsViewModel>();

            using (FileStream fs = new FileStream("D://News.json", FileMode.OpenOrCreate))
            {
                All_News = (List<NewsViewModel>)jsonFormatter.ReadObject(fs);
                fs.Close();
            }

            return All_News;
        }

        public void Serialize_All(List<NewsViewModel> ListNews)
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
