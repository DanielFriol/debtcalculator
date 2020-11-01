using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Delete
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;

        public Handler(IUnitOfWork uow, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _uow = uow;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(request.Id);
            if (user == null)
                return new Response().AddError("User does not exists");

            _userWriteRepository.Delete(user);
            await _uow.CommitAsync();

            return new Response();
        }
    }
}