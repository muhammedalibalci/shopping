using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Controllers
{
    [ApiController]
    public class HomeController
    {
        private readonly EfDbContext db;

        public HomeController(EfDbContext db)
        {
            this.db = db;
        }
        [HttpGet("/hello")]
        public async Task<ActionResult<IEnumerable>> GetProducts()
        {
            return await db.Users.ToListAsync();
        }

    }
}
