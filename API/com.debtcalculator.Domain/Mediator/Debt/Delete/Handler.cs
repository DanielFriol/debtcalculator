using System;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Enums;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.Delete
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IDebtWriteRepository _debtWriteRepository;
        private readonly IDebtReadRepository _debtReadRepository;

        public readonly IUnitOfWork _uow;

        public Handler(IDebtWriteRepository debtWriteRepository, IDebtReadRepository debtReadRepository, IUnitOfWork uow)
        {
            _debtWriteRepository = debtWriteRepository;
            _debtReadRepository = debtReadRepository;
            _uow = uow;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var debt = await _debtReadRepository.GetAsync(request.Id);
            debt.FinalizeDebt();

            var value = CalculatePaschoalottoValue(debt);

            debt.UpdatePaschoalottoValue(value);

            _debtWriteRepository.Update(debt);

            await _uow.CommitAsync();

            return new Response(debt);
        }


        public float CalculatePaschoalottoValue(Domain.Entities.Debt debt)
        {
            var dateFin = debt.FinalizedDate.Value;
            var dateDiff = dateFin.Date - debt.DueDate.Date;
            float paschoallotoValue = 0;
            if (debt.InterestType.Equals((int)InterestType.Simple))
            {
                var m = debt.Value;
                var perc = debt.Interest / 100;
                var j = m * perc;
                j *= dateDiff.Days;
                var pascPerc = debt.PaschoalottoPercentage / 100;
                paschoallotoValue = (m + j) * pascPerc;
            }
            else
            {
                var m = debt.Value;
                var perc = debt.Interest / 100;
                var j = 1 + perc;
                var x = Math.Pow((double)j, (double)dateDiff.Days);
                paschoallotoValue = (float)(m * x);
            }
            return paschoallotoValue;
        }
    }
}