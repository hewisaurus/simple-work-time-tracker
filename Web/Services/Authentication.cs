using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Database.Interfaces.Repositories;

namespace SimpleWorkTimeTracker.Services
{
    public class Authentication : IAuthentication
    {
        private readonly IPersonQueryRepository _dbPersonQuery;

        public Authentication(IPersonQueryRepository dbPersonQuery)
        {
            _dbPersonQuery = dbPersonQuery;
        }

        public async Task<ReturnValue> AuthenticateAsync(string emailAddress, string password)
        {
            var returnValue = new ReturnValue();

            try
            {
                var dbUser = await _dbPersonQuery.GetByEmailAddressAsync(emailAddress);
                var success = Common.BCrypt.BCrypt.Validate(password, dbUser.Password);
                if (success)
                {
                    returnValue.Success = true;
                }
                else
                {
                    returnValue.Message = $"Login failed for {emailAddress}";
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                return new ReturnValue(false, ex.Message);
            }
        }
    }
}
