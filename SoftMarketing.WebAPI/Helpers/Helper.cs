using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Crypto.Tls;
using SoftMarketing.Model;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace SoftMarketing.WebAPI.Helpers
{
    public static class Helper
    {
        public static String Compress(string json)
        {
            var bytes = Encoding.Unicode.GetBytes(json);
            using (var MemoryStream = new MemoryStream(bytes))
            using (var MemoryStream2 = new MemoryStream())
            {
                using (var gipstream = new GZipStream(MemoryStream2, CompressionMode.Compress))
                {
                    MemoryStream.CopyTo(gipstream);
                }
                return Convert.ToBase64String(MemoryStream2.ToArray());
            }
        }

        public static string Decompress(String s)
        {
            try
            {
                var bytes = Convert.FromBase64String(s);
                using (var MemoryStream = new MemoryStream(bytes))
                using (var MemoryStream2 = new MemoryStream())
                {
                    using (var gipstream = new GZipStream(MemoryStream, CompressionMode.Decompress))
                    {
                        gipstream.CopyTo(MemoryStream2);
                    }
                    return Encoding.Unicode.GetString(MemoryStream2.ToArray());
                }
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.ToString());
            }
            return ("Invalid string");
        }

        public static string GetProperMessage(string exMessage,string controllerName = null)
        {
            string errorMessage = "";
            if (controllerName != null)
                errorMessage = controllerName + " ";
            if (exMessage.ToLower().Contains("duplicate"))
            {
                errorMessage += "already exist!";
            }
            else
            {
                return exMessage;
            }
            return errorMessage;
        }

        public static string GitUserImagePath(string wwwroot, string name)
        {
            var folder = Directory.GetParent(Directory.GetParent(wwwroot).FullName).FullName;
            return Path.Combine(folder, "data.business1.app", "template", "user", $"{name}.png");
        }

        public static string EncodeTo64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        const int keySize = 64;
        const int iterations = 350000;
        public static string HashPasword(string password, out byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
