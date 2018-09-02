using EvolentHealthContactManagement.Models;
using System.Data.Entity;

namespace EvolentHealthContactManagement.DAL
{
    public class ContactContext : DbContext
    {
        public ContactContext()
            : base("name=ContactDB")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
    }
}