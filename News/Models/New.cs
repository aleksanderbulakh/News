using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace News.Models
{
    [DataContract]
    public class New
    {
        [DataMember]
        [Required]
        [Display(Name = "Заголовок")]
        [StringLength(15, ErrorMessage = "Довжина заголовку повинна бути не більше 15 символів.")]
        public string Header { get; set; }

        [DataMember]
        [Required]
        [StringLength(6000, ErrorMessage = "Довжина заголовку повинна бути не більше 6000 символів.")]
        [Display(Name = "Зміст статті")]
        public string Content { get; set; }

        [DataMember]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата публікації")]
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Видимість")]
        public bool IsView { get; set; }

        public static List<New> All_News = new List<New>();

        //ЗБерігаємо поточну новину у файл *.json
        public static void Serialize_New()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<New>));

            using (FileStream fs = new FileStream("D://News.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, All_News);
            }
        }


        //Зчитуємо усі об'єкти з файлу *.json
        public static void Deserialize_All()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<New>));

            using (FileStream fs = new FileStream("D://News.json", FileMode.OpenOrCreate))
            {
               //All_News = (List<New>)jsonFormatter.ReadObject(fs);

            }


        }
    }
}