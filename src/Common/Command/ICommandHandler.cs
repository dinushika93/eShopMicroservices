using Common.Command;
using MediatR;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TCommand : ICommand<TResponse>
{


}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand,Unit>
where TCommand : Common.Command.ICommand
{

}
