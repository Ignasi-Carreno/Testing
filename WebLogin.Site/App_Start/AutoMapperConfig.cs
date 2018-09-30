using AutoMapper;
using WebLogin.Site.MapperProfile;

namespace WebLogin.Site
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Register automapper profiles
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<UserProfile>());
        }
    }
}