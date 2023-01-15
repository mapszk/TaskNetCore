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

            CreateMap<UserRegistrationDTO, User>();
            CreateMap<User, UserInfoDTO>()
                .ForMember(x => x.ToDosAmmount, x => x.MapFrom(x => x.ToDos.Count));

            CreateMap<ToDo, ToDoDTO>();
            CreateMap<CreateToDoDTO, ToDo>();
            CreateMap<UpdateToDoDTO, ToDo>();
            CreateMap<ToDo, ToDoShortDTO>()
                .ForMember(x => x.CommentsAmount, x => x.MapFrom(x => x.Comments.Count));

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}