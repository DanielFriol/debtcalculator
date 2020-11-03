using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.UpdateConfig
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IDebtReadRepository _debtReadRepository;
        private readonly IDebtWriteRepository _debtWriteRepository;
        private readonly IUnitOfWork _uow;

        public Handler(IDebtReadRepository debtReadRepository, IDebtWriteRepository debtWriteRepository, IUnitOfWork uow)
        {
            _debtReadRepository = debtReadRepository;
            _debtWriteRepository = debtWriteRepository;
            _uow = uow;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var debt = await _debtReadRepository.GetAsync(request.Id);
            if (debt == null)
                return new Response().AddError("Debt not found");

            debt.UpdateConfig(request.MaxSplit, request.InterestType, request.Interest, request.PaschoalottoPercentage);

            _debtWriteRepository.Update(debt);

            await _uow.CommitAsync();

            return new Response(debt);
        }
    }
}