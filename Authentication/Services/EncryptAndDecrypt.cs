using Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class EncryptAndDecrypt
    {
        private readonly string _privatekey = "<RSAKeyValue><Modulus>yNBXJQrsXX/I+UdfcpRUAveukqTDoRVJunf76TNU1RdDeTczAn9BpkKMLfS5TMzHCXPjdg7L2fllZtUY/Ol9aCtRpN9dgoLxkD10cNfZiw3H7Scf6AwGWyEysVCAZr3p90c07ieYfVxpDr96HYaGGniJnApnMjyAm5BhGRScKAk=</Modulus><Exponent>AQAB</Exponent><P>6HZIQkSLI4md4Fvajm3CNMctbq+feRb9o2PIDrh6E1ImmRaqvOOXOolwMdYgxarCYK4d0dg15UZ3Z0cChMNEpw==</P><Q>3SWyChMQJjij4wHEmB04bAZK29tgnxApGpfnm9l6nb8Vr8JQYvzIEV0HkpgDnmB5IBkOurgD2ngPxM8buA7Tzw==</Q><DP>h6KUUM4rnSWrz3/oyxfxq9fXg3DHjODER3RuA2DSIbnaOZLHNoVY9NfCdeGpp4wV9FFDpvAPqmJuQv1k09AXmw==</DP><DQ>zj72saCfwhW2+uLON9OgqFaiADO0BATtYMjlD5ufWHk6v4VotTjtWgw6IMS3M0DkFkRoUUmHBnxMsI87WcgyTw==</DQ><InverseQ>rygwCm9UaK0LB5vIZ6r3jNZ5mnjViVomA0qsprWQDpsiUfHDiitXs8NBBXlxQnypcauur6T89upE3GGk6RzatA==</InverseQ><D>euiJbfKxmMiNiYVVthtzEB3oi3itA/qzlZ26YZE5avNCKP7QCcc5tzkj8zzF7WuopvB3V9rWiiNLHRVwpduDFRlLh4tVIaCug6nWLaIII/2Pjdc0jIOHKdlP4YJd3xBd1pW4UZd36W7RfvdI7MNd0Ppz1yl+WbiCUVjRS5an5cE=</D></RSAKeyValue>";
        private readonly string _publickey = "<RSAKeyValue><Modulus>yNBXJQrsXX/I+UdfcpRUAveukqTDoRVJunf76TNU1RdDeTczAn9BpkKMLfS5TMzHCXPjdg7L2fllZtUY/Ol9aCtRpN9dgoLxkD10cNfZiw3H7Scf6AwGWyEysVCAZr3p90c07ieYfVxpDr96HYaGGniJnApnMjyAm5BhGRScKAk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }
            UnicodeEncoding _encoder = new();
            rsa.FromXmlString(_privatekey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return _encoder.GetString(decryptedByte);
        }

        public string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            //string privateKey = rsa.ToXmlString(true);
            //string publicKey = rsa.ToXmlString(false);
            rsa.FromXmlString(_publickey);
            UnicodeEncoding _encoder = new();
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }
            return sb.ToString();
        }
    }
}