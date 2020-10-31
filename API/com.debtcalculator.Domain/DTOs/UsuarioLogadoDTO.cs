using System;

namespace com.debtcalculator.Domain.DTOs
{
    public class UsuarioLogadoDTO
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public long IdProfile { get; set; }
        public DateTime DataToken { get; set; }
    }
}