using Common.Exceptions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Basket.Api.Exceptions;

public class BasketNotFoundException : NotFoundException{

    public BasketNotFoundException(string userName) : base("Cart", userName) { }
}