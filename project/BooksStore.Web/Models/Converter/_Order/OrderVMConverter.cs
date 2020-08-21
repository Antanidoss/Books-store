using AutoMapper;
using BooksStore.Service.DTO;
using BooksStore.Web.Converter._Book;
using BooksStore.Web.Models.ViewModels.Order;
using System.Collections.Generic;
using System.Linq;

namespace BooksStore.Web.Converter._Order
{
    public static class OrderVMConverter
    {
        public static OrderViewModel ConvertToOrderViewModel(OrderDTO orderDTO)
        {
            if (orderDTO != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()
                    .ForMember(p => p.BooksViewModel, conf => conf.MapFrom(o => BookVMConverter.ConvertToBookViewModel(o.BooksOrder))))
                    .CreateMapper();
                
                return mapper.Map<OrderDTO, OrderViewModel>(orderDTO);
            }
            return new OrderViewModel();
        }

        public static IEnumerable<OrderViewModel> ConvertToOrderViewModel(IEnumerable<OrderDTO> ordersDTO)
        {
            if (ordersDTO != null && ordersDTO.Count() != 0)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()
                    .ForMember(p => p.BooksViewModel, conf => conf.MapFrom(o => BookVMConverter.ConvertToBookViewModel(o.BooksOrder))))
                    .CreateMapper();

                return mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderViewModel>>(ordersDTO);
            }
            return new List<OrderViewModel>();
        }
    }
}
