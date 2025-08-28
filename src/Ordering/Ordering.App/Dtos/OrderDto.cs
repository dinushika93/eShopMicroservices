using Ordering.Domain.Enums;

namespace Ordering.App.Dtos;
public record OrderDto(
  Guid Id,
  Guid CustomerId,
  AddressDto ShippingAddress,
  AddressDto BillingAddress,
  PaymentDto1 Payment,
  List<OrderItemDto> OrderItems,
  OrderStatus Status
);