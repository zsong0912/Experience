using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Experience.Framework
{
    internal abstract class AuthorizeBase
    {
        private string RandomKey { get; set; }
        private ITokenCrypto TokenCrypto { get { return ServiceConfiguration.Configuration.TokenCrypto; } }
        protected AuthorizeBase(string randomKey)
        {
            RandomKey = randomKey;
        }
        protected string CreateToken<T>(T identity) where T : IIdentity
        {
            string identityValue = ConvertJson(identity);
            string randomKey = TokenCrypto.GetRandomKey();
            var token = new EncryptedToken()
            {
                RandomKey = Encoding.UTF8.GetBytes(randomKey),
                Token = Encoding.UTF8.GetBytes(Encrypt(identityValue, randomKey))
            };
            string tokenValue = ConvertJson(token);
            return Base64Encrypt(tokenValue, RandomKey);
        }
        protected T GetToken<T>(string token) where T : IIdentity
        {
            string urlDecodeToken = Base64Decrypt(token, RandomKey);
            var tokenObject = JsonConvert.DeserializeObject<EncryptedToken>(urlDecodeToken);
            string randomKey = Encoding.UTF8.GetString(tokenObject.RandomKey);
            string tookenValue = Encoding.UTF8.GetString(tokenObject.Token);
            string identityValue = Decrypt(tookenValue, randomKey);
            return ConvertObjectFromJson<T>(identityValue);
        }

        #region private function

        private string ConvertJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private T ConvertObjectFromJson<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        private string Encrypt(string value, string randomKey)
        {
            return TokenCrypto.Encrypt(value, randomKey);
        }
        private string Decrypt(string value, string randomKey)
        {
            return TokenCrypto.Decrypt(value, randomKey);
        }
        private string UrlEncodeEncrypt(string value, string randomKey)
        {
            string encryptValue = TokenCrypto.Encrypt(value, randomKey);
            if (encryptValue.Contains("=="))
            {
                encryptValue = encryptValue.Replace("==", "2");
            }
            else if (encryptValue.Contains("="))
            {
                encryptValue = encryptValue.Replace("=", "1");
            }
            else
            {
                encryptValue = encryptValue += "0";
            }

            return HttpUtility.UrlEncode(encryptValue);
        }
        private string UrlDecodeDecrypt(string value, string randomKey)
        {
            var urlDecodeValue = HttpUtility.UrlDecode(value);
            var encryptValue = urlDecodeValue.Remove(urlDecodeValue.Length - 1);
            if (urlDecodeValue.EndsWith("1"))
            {
                encryptValue += "=";
            }
            else if (urlDecodeValue.EndsWith("2"))
            {
                encryptValue += "==";
            }
            else
            {

            }
            return TokenCrypto.Decrypt(encryptValue, randomKey);
        }
        private string Base64Encrypt(string value, string randomKey)
        {
            string encryptValue = Base64Crypto.EncodeBase64(Encoding.Unicode, TokenCrypto.Encrypt(value, randomKey));
            if (encryptValue.Contains("=="))
            {
                encryptValue = encryptValue.Replace("==", "2");
            }
            else if (encryptValue.Contains("="))
            {
                encryptValue = encryptValue.Replace("=", "1");
            }
            else
            {
                encryptValue = encryptValue += "0";
            }

            return encryptValue;
        }
        private string Base64Decrypt(string value, string randomKey)
        {
            var encryptValue = value.Remove(value.Length - 1);
            if (value.EndsWith("1"))
            {
                encryptValue += "=";
            }
            else if (value.EndsWith("2"))
            {
                encryptValue += "==";
            }
            else
            {

            }
            var urlDecodeValue = Base64Crypto.DecodeBase64(Encoding.Unicode, encryptValue);
            return TokenCrypto.Decrypt(urlDecodeValue, randomKey);
        }

        #endregion
    }
}
