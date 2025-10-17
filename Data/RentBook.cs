using System.ComponentModel.DataAnnotations;

namespace Blazor_lab1.Data
{
    public class RentBook
    {

        public int RentBookId { get; set; }

        public int? BookId { get; set; }
        public int? ReaderId { get; set; }

        private DateTime _rentDate;
        public DateTime RentDate
        {
            get => _rentDate;
            set => _rentDate = value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
        }

        private DateTime? _returnDate;
        public DateTime? ReturnDate
        {
            get => _returnDate;
            set => _returnDate = value.HasValue
                ? (value.Value.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                    : value.Value.ToUniversalTime())
                : null;
        }

        public Book? Book { get; set; }
        public Reader? Reader { get; set; }
    }
}
