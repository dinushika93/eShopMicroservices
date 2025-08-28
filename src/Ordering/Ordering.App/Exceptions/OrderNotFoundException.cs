using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;
using Common.Exceptions;

namespace Ordering.App.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(string id) : base("Order", id)
    {
    }
}