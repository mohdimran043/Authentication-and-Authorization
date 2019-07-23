using Microsoft.EntityFrameworkCore;
using MOI.IdentityServer.DataAccess.Extensions;

namespace MOI.IdentityServer.DataAccess.DbContexts
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.NamesToSnakeCase();
        }
    }
}
