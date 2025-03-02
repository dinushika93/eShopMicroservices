using Discount.Api.Models;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Discount.Api.Services;
public class DiscountService1(DiscountDbContext dbcontext, ILogger<DiscountService1> logger) : DiscountService.DiscountServiceBase
{

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon == null)
        {
            coupon = new Coupon { Id = 0, ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
        }
        logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount : {coupon.Amount}");
        return coupon.Adapt<CouponModel>();
    }


    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        if (request.Coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is missing"));
        }
        var coupon = request.Coupon.Adapt<Coupon>();
        await dbcontext.AddAsync(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation($"Discount is succefully created. ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");
        return request.Coupon;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        if (request.Coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is missing"));
        }
        var coupon = request.Coupon.Adapt<Coupon>();
        dbcontext.Update(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation($"Discount is succefully updated. ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");
        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        if(request.Id == null){
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Id cannot be empty"));
        }
        var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id);
        if(coupon == null){
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon is not found for the product Id: {request.Id}"));
        }
        dbcontext.Remove(coupon);
        await dbcontext.SaveChangesAsync();
        logger.LogInformation($"Discount is  deleted. ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

        return new DeleteDiscountResponse {IsSuccess = true};
    }


}
