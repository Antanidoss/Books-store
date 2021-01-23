using System;
using System.Collections.Generic;

namespace BooksStore.Core.Entities
{
    public class Order : BaseEntity
    {
        public IEnumerable<BookOrderJunction> OrderBooks { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Order() : base()
        {
            OrderBooks = new List<BookOrderJunction>();
            TimeOfDelivery = DateTime.Today.AddDays(3);
        }

        public Order(IEnumerable<BookOrderJunction> orderBooks, string appUserId) : base()
        {
            ValidateArgumentConstructor(orderBooks, appUserId);

            OrderBooks = orderBooks;
            AppUserId = appUserId;
        }

        private void ValidateArgumentConstructor(IEnumerable<BookOrderJunction> orderBooks, string appUserId)
        {
            if (orderBooks == null)
            {
                throw new ArgumentException("Обьект представляющий книги заказа не может быть равен null", nameof(orderBooks));
            }
            if (string.IsNullOrEmpty(AppUserId))
            {
                throw new ArgumentException("Id пользователя не может быть пустым либо равным null", nameof(appUserId));
            }
        }
    }
}
