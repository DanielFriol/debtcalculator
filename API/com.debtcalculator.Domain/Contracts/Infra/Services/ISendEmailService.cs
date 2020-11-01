using System.Threading.Tasks;
using com.debtcalculator.Domain.DTOs.Infra.Serivces;

namespace com.debtcalculator.Domain.Contracts.Infra.Services
{
    public interface ISendEmailService
    {
        Task SendAsync(EmailMessage data);
    }
}