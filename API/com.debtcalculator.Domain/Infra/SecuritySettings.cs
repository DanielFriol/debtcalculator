namespace com.debtcalculator.Domain.Infra
{
    public class SecuritySettings
    {
        // Obrigatório ou não o uso em https
        public bool RequireHttpsMetadata { get; set; }

        // Chave p/ o Token
        public string SigningKey { get; set; }

        // Tempo em horas que o Token é válido
        public int Expires { get; set; }

        // Tempo em horas do RefreshToken
        public int RefreshTokenExpires { get; set; }

        // Emissor Válido do Token
        public string ValidIssuer { get; set; }

        // Quem pode receber o token
        public string ValidAudience { get; set; }
    }
}