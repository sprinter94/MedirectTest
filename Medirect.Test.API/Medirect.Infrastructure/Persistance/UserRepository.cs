using Medirect.Application.Contracts;
using Medirect.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.Persistance
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            return _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username));
        }
    }
}