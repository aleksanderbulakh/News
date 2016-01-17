using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace News.business.ViewModel
{
    [DataContract]
    public class NewsViewModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Зміст статті")]
        public string Content { get; set; }

        [DataMember]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Дата публікації")]
        public DateTime Date { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Видимість")]
        public bool IsView { get; set; }

        public NewsViewModel(NewsViewModel CopyNew)
        {
            Id = CopyNew.Id;
            Header = CopyNew.Header;
            Content = CopyNew.Content;
            Date = CopyNew.Date;
            Author = CopyNew.Author;
            IsView = CopyNew.IsView;
        }

        public NewsViewModel() { }
    }
}
