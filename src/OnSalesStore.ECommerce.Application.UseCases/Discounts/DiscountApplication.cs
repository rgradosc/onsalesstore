using AutoMapper;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Infrastructure;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Domain.Entities;
using OnSalesStore.ECommerce.Domain.Events;
using OnSalesStore.ECommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Discounts
{
    public class DiscountApplication : IDiscountApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly INotification _notification;

        public DiscountApplication(
            IUnitOfWork unitOfWork, IMapper mapper, 
            IEventBus eventBus, INotification notification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventBus = eventBus;
            _notification = notification;
        }

        public async Task<Response<bool>> Add(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var discount = _mapper.Map<Discount>(discountDTO);
                await _unitOfWork.Discounts.InsertAsync(discount);
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro correcto";

                    // Publicamos el evento
                    var discountCreatedEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                    _eventBus.Publish(discountCreatedEvent);

                    // Enviamos un correo
                    await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(discount), cancellationToken);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(response), cancellationToken);
            }
            return response;
        }

        public async Task<Response<bool>> Edit(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var discount = _mapper.Map<Discount>(discountDTO);
                await _unitOfWork.Discounts.UpdateAsync(discount);
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización correcta";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Remove(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                await _unitOfWork.Discounts.DeleteAsync(id.ToString());
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación correcta";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDTO>();

            try
            {
                var discount = await _unitOfWork.Discounts.SelectAsync(id, cancellationToken);
                if (discount == null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento no encontrado";
                    return response;
                }
                response.Data = _mapper.Map<DiscountDTO>(discount);
                response.IsSuccess = true;
                response.Message = "Descuento encontrado";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<IEnumerable<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<DiscountDTO>>();

            try
            {
                var discounts = await _unitOfWork.Discounts.SelectAllAsync(cancellationToken);
                response.Data = _mapper.Map<List<DiscountDTO>>(discounts);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Registros encontrados";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponsePagination<IEnumerable<DiscountDTO>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<DiscountDTO>>();
            try
            {
                var count = await _unitOfWork.Discounts.CountAsync();
                var discounts = await _unitOfWork.Discounts.SelectAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<DiscountDTO>>(discounts);
                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta paginada correctamente!";
                }
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }
            return response;
        }
    }
}
