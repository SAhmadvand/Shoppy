using MediatR;
using Shoppy.Application.Requests;
using Shoppy.Domain.Abstractions;

namespace Shoppy.Application.Behaviors;

public class SaveChangesBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveChangesBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var isTransactional = request is ITransactionalRequest<TResponse>;
        try
        {
            if (isTransactional)
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (isTransactional)
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch
        {
            if (isTransactional)
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            throw;
        }
    }
}