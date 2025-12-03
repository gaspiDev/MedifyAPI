using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MedifyDbContext context) : base(context) { }

        public async Task<User?> ReadUserByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email && u.IsActive == true)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User?>> ReadUsers()
        {
            return await _context.Users.Where(u => u.IsActive == true).AsNoTracking().ToListAsync();
        }

        public async Task<User?> ReadByAuth0IdAsync(string auth0Id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Auth0Id == auth0Id);
        }


        public async Task<int> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return 0;
            }
            user.IsActive = false;
            return await _context.SaveChangesAsync();
        }

    }
}
