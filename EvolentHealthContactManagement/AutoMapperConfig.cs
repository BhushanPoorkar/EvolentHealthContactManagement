using AutoMapper;
using EvolentHealthContactManagement.Models;

namespace EvolentHealthContactManagement
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<Contact, ContactDTO>();//.ReverseMap();//ForMember(contactDTO => contactDTO.Status, opt => opt.MapFrom<string>(item => item.Status ? "Active" : "Inactive"));//ResolveUsing<ContactDTOStatusResolver, bool>(str => str.Status));//.ReverseMap();
                config.CreateMap<ContactDTO, Contact>();
            });
        }
    }

    //public class ContactStatusResolver : IValueResolver<Contact, ContactDTO, string>
    //{
    //    public string Resolve(Contact source, ContactDTO destination, string member, ResolutionContext context)
    //    {
    //        return source.Status ? "Active" : "Inactive";
    //    }
    //}

    //public class ContactStatusResolver : ITypeConverter<bool, string>
    //{
    //    public string Convert(ResolutionContext context)
    //    {
    //        return DateTime.Parse(((object)context.SourceValue).ToString()).ToString("dd/MMM/yyyy");
    //    }

    //    public string Convert(bool source, string destination, ResolutionContext context)
    //    {
    //        return source ? 
    //    }
    //}

    //public class ContactDTOStatusResolver : IMemberValueResolver<ContactDTO, Contact, string, bool>
    //{
    //    public bool Resolve(ContactDTO source, Contact destination, string sourceMember, bool destMember, ResolutionContext context)
    //    {
    //        return source.Status == "Active" ? true : false;
    //    }
    //}


}