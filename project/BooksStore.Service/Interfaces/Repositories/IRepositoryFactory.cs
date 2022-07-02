namespace BooksStore.Services.Interfaces.Repositories
{
    public interface IRepositoryFactory
    {
        public IBookRepository CreateBookRepository();
        public IBasketRepository CreateBasketRepository();
        public IAuthorRepository CreateAuthorRepository();
        public ICategoryRepository CreateCategoryRepository();
        public ICommentRepository CreateCommentRepository();
        public IOrderRepository CreateOrderRepository();
    }
}
