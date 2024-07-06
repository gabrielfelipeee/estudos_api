using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Api.Domain.Security
{
    public class SigningConfigurations
    {
        public SigningConfigurations()
        {
            //Depois de sair do bloco de using, a var provider é descartada da memória
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                // Gera uma nova chave RSA de 2048 bits
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            // Gera as credenciais de assinatura a partir da key gerada, usando o algoritmo RSA-SHA256
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public SecurityKey Key { get; set; } // Armazena a key gerada
        public SigningCredentials SigningCredentials { get; set; } // armazena as credenciais gerada
    }
}
