using System;
using System.Collections.Generic;
using System.Text;

namespace BooksStore.Core.Entities
{
    public class Discount : BaseEntity
    {
        public int Percentage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        private Discount() { }

        public Discount(int percentage, DateTime expirationDate)
        {
            ValidateArgumentConstructor(percentage, expirationDate);

            Percentage = percentage;
            ExpirationDate = expirationDate;
        }

        private void ValidateArgumentConstructor(int percentage, DateTime expirationDate)
        {
            if (percentage <= 0)
            {
                throw new ArgumentException("Процент скидки не может быть меньше либо равен 0", nameof(Percentage));
            }
            if (expirationDate <= DateTime.Now)
            {
                throw new ArgumentException("Дата окончания скидки не может быть прошлой либо текущей 0", nameof(expirationDate));
            }
        }
    }
}
