using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Service
{

    public class LoginService
    {
        public Task<Response<User>> LoginAsync(Login loginModel)
        {
            return Task.FromResult(
                new Response<User>()
                {
                    Item = new User
                    {
                        id = 1,
                        name = "Mohammad Mheidat",
                        //CountryId = loginModel.CountryCode,
                        phone = loginModel.PhoneNumber,
                        //Email = "MohammadMheidat@SoftSolutions.com",
                        token = Guid.Empty.ToString(),
                        //languageId = loginModel.LanguageId
                    },
                    IsSuccess = true,
                    ErrorMessage=null,
                    
                }
                );
        } 
        public Task<bool> RequestOTPAsync(string countryCode, string phoneNumber)
        {
            if(String.IsNullOrEmpty(countryCode)|| String.IsNullOrEmpty(phoneNumber))
            {
                return Task.FromResult(false);
            }
            else
            {
                return Task.FromResult(true);
            }
        }
    }
}
