using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Enums;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.ResfreshData
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
            var dateFin = debt.Finalized ? debt.FinalizedDate.Value : DateTime.UtcNow;
            var dateDiff = dateFin.Date - debt.DueDate.Date;
            float interestValue = 0;
            float paschoallotoValue = 0;
            float finalValue = 0;
            if (debt.InterestType.Equals((int)InterestType.Simple))
            {
                var m = debt.Value;
                var perc = debt.Interest / 100;
                var j = m * perc;
                j *= dateDiff.Days;
                finalValue = (m + j);
                var pascPerc = debt.PaschoalottoPercentage / 100;
                paschoallotoValue = finalValue * pascPerc;
                interestValue = finalValue - m;
            }
            else
            {
                var m = debt.Value;
                var perc = debt.Interest / 100;
                var j = 1 + perc;
                var x = Math.Pow((double)j, (double)dateDiff.Days);
                finalValue = (float)(m * x);
                var pascPerc = debt.PaschoalottoPercentage / 100;
                paschoallotoValue = finalValue * pascPerc;
                interestValue = finalValue - m;
            }
            var splits = new List<dynamic>();
            var splitValue = finalValue / debt.MaxSplit;
            for (var x = 1; x <= debt.MaxSplit; x++)
            {
                splits.Add(new
                {
                    Value = splitValue,
                    DueDate = DateTime.UtcNow.AddMonths(x).Date
                });
            }
            if (!debt.Finalized)
            {

                debt.PaschoalottoValue = paschoallotoValue;
                _debtWriteRepository.Update(debt);
                await _uow.CommitAsync();

            }

            return new Response(new
            {
                DueDate = debt.DueDate,
                DaysLate = dateDiff.Days,
                OriginalValue = debt.Value,
                InterestValue = interestValue,
                FinalValue = finalValue,
                PlotsValue = splits,
                ContactPhone = debt.ContactPhone
            });
        }
    }
}