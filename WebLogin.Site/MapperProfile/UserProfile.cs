using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLogin.Site.MapperProfile
{
    public class UserProfile : Profile
    {
        /// <summary>
        /// Set user mappings between business layer objects and view objects
        /// </summary>
        public UserProfile()
        {
            CreateMap<Objects.User, Models.User>();
            CreateMap<Models.User, Objects.User>();
            CreateMap<Objects.Role, Models.Role>().ConvertUsing(value =>
            {
                switch (value)
                {
                    case Objects.Role.ADMIN:
                        return Models.Role.ADMIN;
                    case Objects.Role.PAGE_1:
                        return Models.Role.PAGE_1;
                    case Objects.Role.PAGE_2:
                        return Models.Role.PAGE_2;
                    case Objects.Role.PAGE_3:
                        return Models.Role.PAGE_3;
                    default:
                        return Models.Role.NOTHING;
                }
            });
            CreateMap<Models.Role, Objects.Role>().ConvertUsing(value =>
            {
                switch (value)
                {
                    case Models.Role.ADMIN:
                        return Objects.Role.ADMIN;
                    case Models.Role.PAGE_1:
                        return Objects.Role.PAGE_1;
                    case Models.Role.PAGE_2:
                        return Objects.Role.PAGE_2;
                    case Models.Role.PAGE_3:
                        return Objects.Role.PAGE_3;
                    default:
                        return Objects.Role.NOTHING;
                }
            });
        }
    }
}