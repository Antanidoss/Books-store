using AutoMapper;
using BooksStore.Services.DTO.Order;
using BooksStore.Web.Сommon.ViewModel.ReadModel;

namespace BooksStore.Web.Сommon.Profiles
{
    public class OrderVMProfile : Profile
    {
        public OrderVMProfile()
        {
            CreateMap<OrderDTO, OrderViewModel>().ForMember(p => p.BooksViewModel, conf => conf.MapFrom(o => o.OrderBooks));
        }
    }
}
