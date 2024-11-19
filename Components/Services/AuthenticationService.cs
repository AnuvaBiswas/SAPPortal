using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SAPPortal.Models;

namespace SAPPortal.Components.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _appDbContext;
        public AuthenticationService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public  List<AuthenticationModel> AuthenticationList = new List<AuthenticationModel>();

        public async Task<List<AuthenticationModel>> GetPermission(string customerId )
        {
             //customerId = "3";
            AuthenticationList = await _appDbContext.Authentication.FromSqlRaw("exec GlobalAuthenticationMatrix @UserId={0}", customerId).ToListAsync();
            return AuthenticationList.ToList();
        }
    }
}
