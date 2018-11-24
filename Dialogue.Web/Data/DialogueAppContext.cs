using Dialogue.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Dialogue.Web.Data
{
    public class DialogueAppContext : DbContext
    {
        // Add DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
      

        public DialogueAppContext(DbContextOptions<DialogueAppContext> options) : base(options) { }

        public DialogueAppContext() { }
    }
}
