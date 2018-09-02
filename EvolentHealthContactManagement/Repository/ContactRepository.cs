using AutoMapper;
using EvolentHealthContactManagement.DAL;
using EvolentHealthContactManagement.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EvolentHealthContactManagement.Repository
{
    public class ContactRepository : IRepository<ContactDTO>
    {
        protected readonly ContactContext Context;

        public ContactRepository(ContactContext context)
        {
            Context = context;
        }

        public bool Update(ContactDTO entity)
        {
            UpdateContactDTOStatusToTrueOrFalse(entity);
            Contact contact = MapContactDTOToContact(entity);
            Context.Contacts.Add(contact);
            Context.Entry(contact).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(entity.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public bool Add(ContactDTO entity, out int id)
        {
            UpdateContactDTOStatusToTrueOrFalse(entity);
            Contact con = MapContactDTOToContact(entity);
            Context.Set<Contact>().Add(con);
            int count = Context.SaveChanges();
            id = con.ID;
            return count == 1 ? true : false;
        }

        public bool AddRange(List<ContactDTO> entities)
        {
            entities.ForEach(data => UpdateContactDTOStatusToTrueOrFalse(data));
            Context.Set<Contact>().AddRange(MapContactDTOLstToContactLst(entities));
            return Context.SaveChanges() == entities.Count ? true : false;
        }

        public ContactDTO Get(int id)
        {
            ContactDTO dto = MapContactToContactDTO(GetContact(id));
            if (dto != null)
                UpdateContactDTOStatusToActiveOrInactive(dto);
            return dto;
            //return Mapper.Map<Contact, ContactDTO>(Context.Set<Contact>().Find(id),opt => opt.ConfigureMap().ForMember(dest => dest.Status,opt1 => opt1.ResolveUsing<ContactDTOStatusResolver,bool>(b => b.Status)));
        }

        public IEnumerable<ContactDTO> GetAll()
        {
            List<ContactDTO> dtolst = MapContactLstToContactDTOLst(GetContacts());
            dtolst.ForEach(item => UpdateContactDTOStatusToActiveOrInactive(item));
            return dtolst;
        }

        public bool Remove(ContactDTO contact)
        {
            Contact con = Context.Contacts.Find(contact.ID);
            if (con != null && con.Status != false)
            {
                con.Status = false;
                Context.Entry(con).Property("Status").IsModified = true;
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        private List<Contact> GetContacts()
        {
            return Context.Set<Contact>().Where(data => data.Status == true).ToList();
        }

        private Contact GetContact(int id)
        {
            return Context.Set<Contact>().Where(data => data.ID == id && data.Status == true).SingleOrDefault();
        }

        private void UpdateContactDTOStatusToTrueOrFalse(ContactDTO contact)
        {
            contact.Status = contact.Status.ToLower() == "active" ? "true" : "false";
        }

        private void UpdateContactDTOStatusToActiveOrInactive(ContactDTO contact)
        {
            contact.Status = contact.Status.ToLower() == "true" ? "Active" : "Inactive";
        }

        private Contact MapContactDTOToContact(ContactDTO entity)
        {
            return Mapper.Map<ContactDTO, Contact>(entity);
        }

        private ContactDTO MapContactToContactDTO(Contact contact)
        {
            return Mapper.Map<Contact, ContactDTO>(contact);
        }

        private List<ContactDTO> MapContactLstToContactDTOLst(List<Contact> lst)
        {
            return Mapper.Map<List<Contact>, List<ContactDTO>>(lst);
        }

        private List<Contact> MapContactDTOLstToContactLst(List<ContactDTO> lst)
        {
            return Mapper.Map<List<ContactDTO>, List<Contact>>(lst);
        }

        private bool ContactExists(int id)
        {
            return Context.Contacts.Find(id) != null ? true : false;
        }


    }
}