using EvolentHealthContactManagement.Models;
using System.Collections.Generic;

namespace EvolentHealthContactManagement.DAL
{
    public class ContactInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ContactContext>
    {
        protected override void Seed(ContactContext context)
        {
            var contact = new List<Contact>
            {
            new Contact{FirstName="Bhushan",LastName="Poorkar",Email="test@gmail.com",PhoneNumber="9999999999",Status=true},
            new Contact{FirstName="Amit",LastName="Poorkar",Email="test@gmail.com",PhoneNumber="9999999999",Status=true},
            new Contact{FirstName="Ram",LastName="Poorkar",Email="test@gmail.com",PhoneNumber="9999999999",Status=true}

            };

            contact.ForEach(s => context.Contacts.Add(s));
            context.SaveChanges();

        }
    }
}