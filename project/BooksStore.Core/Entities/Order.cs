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
        private Order() : base()
        {
            OrderBooks = new List<BookOrderJunction>();
            TimeOfDelivery = new DateTime();
        }

        public Order(IEnumerable<BookOrderJunction> orderBooks, string appUserId, DateTime timeOfDelivery) : base()
        {
            ValidateArgumentConstructor(orderBooks, appUserId, timeOfDelivery);

            OrderBooks = orderBooks;
            AppUserId = appUserId;
            TimeOfDelivery = timeOfDelivery;
        }

        private void ValidateArgumentConstructor(IEnumerable<BookOrderJunction> orderBooks, string appUserId, DateTime timeOfDelivery)
        {
            if (orderBooks == null)
                throw new ArgumentException("Обьект представляющий книги заказа не может быть равен null", nameof(orderBooks));

            if (string.IsNullOrEmpty(appUserId))
                throw new ArgumentException("Id пользователя не может быть пустым либо равным null", nameof(appUserId));

            if (timeOfDelivery == null || timeOfDelivery < DateTime.Now)
                throw new ArgumentException("Время доставки не может быть равным null либо иметь прошлую форму времени", nameof(timeOfDelivery));
        }
    }
}