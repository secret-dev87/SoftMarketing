using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.DAL.Helper
{
    public class HMACHasher
    {
        public string otpHash { get; set; }
        public bool hasVerified { get; set; }

        byte[] otpSalt = new byte[] { 0x13, 0x27, 0x11, 0x1, 0x10, 0x74, 0x39 };
        public HMACHasher(string _otp)
        {
            otpHash = CreateHashPass(_otp);
            
        }
        public HMACHasher(string _otp, string _otpHash)
        {
            byte[] otpHash = Encoding.ASCII.GetBytes(_otpHash);
            hasVerified = VerifyHashPass(_otp, otpHash);
        }
        private string CreateHashPass(string otp)
        {
            using (var hmac = new HMACSHA512())
            {
                return Encoding.Default.GetString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(otp)));
                //return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(otp)).ToString();
            }
        }
        private bool VerifyHashPass(string otp, byte[] otpHash)
        {
            using (var hmac = new HMACSHA512())
            {
                try
                {
                    if (otpHash.Length > 0)
                    {
                        var newHashed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(otp));
                        return newHashed.SequenceEqual(otpHash);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}
