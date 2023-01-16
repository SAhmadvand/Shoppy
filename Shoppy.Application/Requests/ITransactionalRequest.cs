using MediatR;

namespace Shoppy.Application.Requests;

public interface ITransactionalRequest<TResponse> : IRequest<TResponse>
{
}