
using MediatR;

namespace Common.Query{
  
  public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
  where TRequest : IQuery<TResponse>{}

  public interface IQueryHandler<TRequest> : IRequestHandler<TRequest>
  where TRequest : IQuery
  {}

}