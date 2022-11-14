using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Taller.Identity.Api.Data
{
    public class TallerDBContext : IdentityDbContext
    {
        public TallerDBContext(DbContextOptions<TallerDBContext> options) : base(options) { }
    }
}
