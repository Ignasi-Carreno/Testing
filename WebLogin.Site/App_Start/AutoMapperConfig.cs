using AutoMapper;
using WebLogin.Site.MapperProfile;

namespace WebLogin.Site
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<UserProfile>());
        }
    }
}