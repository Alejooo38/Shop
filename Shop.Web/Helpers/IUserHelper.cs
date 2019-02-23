using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Web.Models;

namespace Shop.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
