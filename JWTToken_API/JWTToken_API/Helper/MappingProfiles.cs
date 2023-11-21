using AutoMapper;
using JWTToken_API.Model;

namespace JWTToken_API.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<TodoInputModel, TodoModel>();
        }
    }
}
