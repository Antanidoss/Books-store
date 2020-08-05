using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.BookModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service.BasketSer
{
    public class BasketService : IBasketService
    {
        IBasketRepository BasketRepository { get; set; }
        IBookRepository BookRepository { get; set; }
        public BasketService(IBasketRepository basketRepository, IBookRepository bookRepository)
        {
            BasketRepository = basketRepository;
            BookRepository = bookRepository;
        }

        public async Task AddBasketAsync(Basket basket)
        {
            if(basket != null && basket != default)
            {
                await BasketRepository.AddBasketAsync(basket);
            }
        }

        public async Task<Basket> GetBasketByIdAsync(int basketId)
        {
            if (basketId >= 1)
            {
                return await BasketRepository.GetBasketById(basketId);
            }
            return null;
        }

        public async Task<IEnumerable<Basket>> GetBaskets(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return (await BasketRepository.GetBaskets(skip, take) ?? new List<Basket>());
            }
            return new List<Basket>();
        }

        public async Task RemoveBasketAsync(int basketId)
        {
            if (basketId >= 1)
            {
                var basket = await BasketRepository.GetBasketById(basketId);

                if (basket != default)
                {
                    await BasketRepository.RemoveBasketAsync(basket);
                }
            }
        }

        public async Task UpdateBasketAsync(Basket basket)
        {
            if (basket != null && basket != default)
            {
                await BasketRepository.UpdateBasketAsync(basket);
            }
        }

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = new Basket();
            Book book = new Book();

            if ((basket = await BasketRepository.GetBasketById(basketId)) != null && 
                (book = await BookRepository.GetBookByIdAsync(bookId)) != null) 
            {               
                var bookBasket = basket.BookBaskets.ToList();
                bookBasket.Add(new BookBasketJunction() { BasketId = basketId, BookId = bookId });
                basket.BookBaskets = bookBasket;

                await BasketRepository.UpdateBasketAsync(basket);                
            }
        }

        public async Task RemoveBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = new Basket();
            Book book = new Book();

            if ((basket = await BasketRepository.GetBasketById(basketId)) != null &&
                (book = await BookRepository.GetBookByIdAsync(bookId)) != null)
            {
                var bookBasket = basket.BookBaskets.ToList();
                BookBasketJunction bookBasketJunction = bookBasket.FirstOrDefault(p => p.BookId == bookId);

                if(bookBasketJunction != default)
                {
                    bookBasket.Remove(bookBasketJunction);
                    basket.BookBaskets = bookBasket;

                    await BasketRepository.UpdateBasketAsync(basket);
                }
            }
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            Basket basket = new Basket();

            if ((basket = await BasketRepository.GetBasketById(basketId)) != null)
            {
                var bookBasket = basket.BookBaskets.ToList();
                bookBasket.Clear();
                basket.BookBaskets = bookBasket;

                await BasketRepository.UpdateBasketAsync(basket);
            }
        }
    }
}
