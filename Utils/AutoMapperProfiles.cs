using AutoMapper;
using TaskApp.DTOs;
using TaskApp.Models;

namespace TaskApp.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Inicia los DTO para usarlos: source => dest

            // users
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<User, UserInfoDTO>()
                .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id))
                .ForMember(x => x.ToDosAmmount, options => options.MapFrom(x => x.ToDos.Count))
                .ForMember(x => x.UserRoles, options => options.MapFrom(MapUserRoles));

            // roles
            CreateMap<CreateRoleDTO, Role>();

            // todos
            CreateMap<ToDo, ToDoDTO>();
            CreateMap<CreateToDoDTO, ToDo>();
            CreateMap<UpdateToDoDTO, ToDo>();
            CreateMap<ToDo, ToDoShortDTO>()
                .ForMember(x => x.CommentsAmount, options => options.MapFrom(x => x.Comments.Count));

            // comments
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();

        }

        private List<string> MapUserRoles(User user, UserInfoDTO userInfoDTO)
        {
            var result = new List<string>();
            if (user.UserRoles == null || user.UserRoles.Count == 0) { return result; }
            foreach (var role in user.UserRoles)
            {
                result.Add(role.Role.Name);
            }
            return result;
        }
    }
}