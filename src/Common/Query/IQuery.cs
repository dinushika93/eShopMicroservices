
using MediatR;

namespace Common.Query
{
public interface IQuery<TResponse> : IRequest<TResponse> {

}

public interface IQuery : IRequest{}

}