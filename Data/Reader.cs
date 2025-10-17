using System.ComponentModel.DataAnnotations;

namespace Blazor_lab1.Data
{
    public class Reader
    {
        public int ReaderId { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; } = string.Empty;


        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
        }



        [Display(Name = "Адрес")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; } = string.Empty;

        public ICollection<RentBook> rentBooks { get; set; }
    }
}
