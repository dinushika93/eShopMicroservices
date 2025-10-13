using Ordering.Domain.Enums;

namespace Ordering.App.Dtos;
public record OrderDto(
  Guid Id,
  Guid CustomerId,
  AddressDto ShippingAddress,
  AddressDto BillingAddress,
  PaymentDto Payment,
  List<OrderItemDto> OrderItems,
  OrderStatus Status
);