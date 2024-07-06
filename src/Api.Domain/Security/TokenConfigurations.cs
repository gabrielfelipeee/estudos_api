namespace Api.Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; } // público
        public string Issuer { get; set; } // Emissor
        public int Seconds { get; set; } // Validade do token
    }
}
