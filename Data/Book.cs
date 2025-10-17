using System.ComponentModel.DataAnnotations;

namespace Blazor_lab1.Data
{
    public class Book
    {
        public int BookId { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Автор")]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Год публикации")]
        public int? PublicationYear { get; set; }

        [Display(Name = "Издатель")]
        public string Publisher { get; set; } = string.Empty;

        [Display(Name = "Категория")]
        public string CategoryCode { get; set; } = string.Empty; 

        public ICollection<RentBook> rentBooks { get; set; }
    }
}
