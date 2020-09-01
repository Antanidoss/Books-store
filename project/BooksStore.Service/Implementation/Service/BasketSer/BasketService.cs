using AutoMapper;
using BooksStore.Core.BasketModel;
using BooksStore.Core.BookBasketJunctionModel;
using BooksStore.Core.BookModel;
using BooksStore.Infastructure.Interfaces;
using BooksStore.Service.DTO;
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
        IMapper Mapper { get; set; }
        public BasketService(IBasketRepository basketRepository, IBookRepository bookRepository, IMapper mapper)
        {
            BasketRepository = basketRepository;
            BookRepository = bookRepository;
            Mapper = mapper;
        }

        public async Task AddBasketAsync(BasketDTO basketDTO)
        {
            if(basketDTO != null && basketDTO != default)
            {
                await BasketRepository.AddBasketAsync(Mapper.Map<Basket>(basketDTO));
            }
        }

        public async Task<BasketDTO> GetBasketByIdAsync(int basketId)
        {
            if (basketId >= 1)
            {
                return Mapper.Map<BasketDTO>(await BasketRepository.GetBasketById(basketId));
            }
            return null;
        }

        public async Task<IEnumerable<BasketDTO>> GetBaskets(int skip, int take)
        {
            if (skip >= 0 && take >= 1)
            {
                return Mapper.Map<IEnumerable<BasketDTO>>((await BasketRepository.GetBaskets(skip, take) ?? new List<Basket>()));
            }
            return new List<BasketDTO>();
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

        public async Task UpdateBasketAsync(BasketDTO basketDTO)
        {
            if (basketDTO != null && basketDTO != default)
            {
                await BasketRepository.UpdateBasketAsync(Mapper.Map<Basket>(basketDTO));
            }
        }

        public async Task AddBasketBookAsync(int basketId, int bookId)
        {
            Basket basket = new Basket();
            Book book = new Book();

            if ((basket = await BasketRepository.GetBasketById(basketId)) != null && 
                (book = await BookRepository.GetBookByIdAsync(bookId)) != null) 
            {               
                var bookBasket = basket.BasketBooks.ToList();
                bookBasket.Add(new BookBasketJunction() { BasketId = basketId, BookId = bookId });
                basket.BasketBooks = bookBasket;

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
                var bookBasket = basket.BasketBooks.ToList();
                BookBasketJunction bookBasketJunction = bookBasket.FirstOrDefault(p => p.BookId == bookId);

                if(bookBasketJunction != default)
                {
                    bookBasket.Remove(bookBasketJunction);
                    basket.BasketBooks = bookBasket;

                    await BasketRepository.UpdateBasketAsync(basket);
                }
            }
        }

        public async Task RemoveAllBasketBooksAsync(int basketId)
        {
            Basket basket = new Basket();

            if ((basket = await BasketRepository.GetBasketById(basketId)) != null)
            {
                var bookBasket = basket.BasketBooks.ToList();
                bookBasket.Clear();
                basket.BasketBooks = bookBasket;

                await BasketRepository.UpdateBasketAsync(basket);
            }
        }
    }
}
