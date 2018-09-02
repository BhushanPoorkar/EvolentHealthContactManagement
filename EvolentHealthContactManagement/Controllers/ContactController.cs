using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using EvolentHealthContactManagement.Models;
using EvolentHealthContactManagement.Repository;
using EvolentHealthContactManagement.CustomAttribute;

namespace EvolentHealthContactManagement.Controllers
{
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        IRepository<ContactDTO> contactrepository;

        public ContactController(IRepository<ContactDTO> repository)
        {
            contactrepository = repository;
        }

        [HttpGet]
        [Route("GetAll")]
        [ResponseType(typeof(IEnumerable<ContactDTO>))]
        public IHttpActionResult GetContacts()
        {
            IEnumerable<ContactDTO> dto = contactrepository.GetAll();
            if (dto.Count() == 0)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("GetContact/{id:int}",Name ="Get")]
        [ResponseType(typeof(ContactDTO))]
        public IHttpActionResult GetContact(int id)
        {
            ContactDTO dto = contactrepository.Get(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost]
        [ActionName("AddContact")]
        [Route("AddContact",Name ="Add")]
        [ValidateModelState]
        public IHttpActionResult AddContact([FromBody]ContactDTO contact)
        {
            if(contact.ID != 0)
                return BadRequest("ID should be 0 for new record!!");
            
            if (contactrepository.Add(contact,out int Id))
            {
                return CreatedAtRoute("Get", new { id = Id }, contact);
                //return Ok();
            }
            else
            {
                 return BadRequest("Some error occoured while saving record!!");
            }
        }

        [HttpPost]
        [ActionName("AddMultipleContacts")]
        [Route("AddMultipleContacts")]
        [ValidateModelState]
        public IHttpActionResult AddContacts([FromBody]List<ContactDTO> contacts)
        {
            if (contacts.Any(data => data.ID != 0))
                return BadRequest("ID should be 0 for new record!!");

            if (contacts.Any(data => data.Status.ToLower() == "inactive"))
                return BadRequest("Status value of a new record should not be inactive!!");

            if (contactrepository.AddRange(contacts))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Some error occoured while saving record!!");
            }
        }

        [HttpPut]
        [Route("UpdateContact")]
        [ValidateModelState]
        public IHttpActionResult UpdateContact(int ID, ContactDTO contact)
        {
            if (ID != contact.ID)
                return BadRequest("ID parameter value and contact object ID does not match!!");

            if(contact.Status.ToLower() == "inactive")
                return BadRequest("Status cannot be updated to inactive using update mathod use delete for this!!");

            try
            {
                if (contactrepository.Update(contact))
                {
                    return Ok("Record Updated successfully!!");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest("Exception occurred while updating Contact!!");
            }

        }
        
        [HttpDelete]
        [Route("DeleteContact")]
        [ValidateModelState]
        public IHttpActionResult DeleteContact(ContactDTO contact)
        {
            if (contactrepository.Remove(contact))
            {
                return Ok();
            }
            
            return NotFound();
        }
    }
}
