using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Taller.Identity.Api.Data
{
    public class TallerDBIdentityContext : IdentityDbContext
    {
        public TallerDBIdentityContext(DbContextOptions<TallerDBIdentityContext> options) : base(options) { }
    }
}
