using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLogin.Site.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Objects.User, Models.User>();
            CreateMap<Models.User, Objects.User>();
        }
    }
}