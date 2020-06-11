using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository
    {
        private EfDbContext _context;
        public UserRepository(EfDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> ListUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
