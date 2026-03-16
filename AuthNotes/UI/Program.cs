using AuthNotes.Application.Interfaces;
using AuthNotes.Application.Services;
using AuthNotes.Infrastructure.Persistence;
using AuthNotes.UI.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthNotes.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=auth.db"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<NoteService>();

            services.AddTransient<LoginForm>();
            services.AddTransient<MainForm>();
            services.AddTransient<NotesForm>();
            services.AddTransient<ManageUsersForm>();

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();
                ApplicationDbInitializer.Seed(db);
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(provider.GetRequiredService<LoginForm>());
        }
    }
}