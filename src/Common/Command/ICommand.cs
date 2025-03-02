using System.Net;
using MediatR;

namespace Common.Command
{
    public interface ICommand<TResponse> :IRequest<TResponse>
    {   
        
    }    

    public interface ICommand :  ICommand<Unit> 
    {
        
    }


}