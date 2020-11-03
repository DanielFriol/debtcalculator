using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.Add
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IDebtWriteRepository _debtWriteRepository;
        public readonly IUnitOfWork _uow;

        public Handler(IDebtWriteRepository debtWriteRepository, IUnitOfWork uow)
        {
            _debtWriteRepository = debtWriteRepository;
            _uow = uow;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var debt = new Domain.Entities.Debt(request.ClientCPF, request.Value, request.DueDate, request.ContactPhone, request.MaxSplit, request.InterestType, request.Interest, request.PaschoalottoPercentage);

            _debtWriteRepository.Add(debt);

            await _uow.CommitAsync();

            return new Response(debt);

        }
    }
}