using AuthNotes.Domain.Entities;
using AuthNotes.Domain.Enums;
using AuthNotes.Infrastructure.Security;

namespace AuthNotes.Infrastructure.Persistence
{
    public static class ApplicationDbInitializer
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User
                    {
                        Username = "guest",
                        PasswordHash = PasswordHasher.Hash("123"),
                        Role = Role.User
                    },
                    new User
                    {
                        Username = "admin",
                        PasswordHash = PasswordHasher.Hash("password"),
                        Role = Role.Admin
                    }
                );

                db.SaveChanges();
            }
        }
    }
}
