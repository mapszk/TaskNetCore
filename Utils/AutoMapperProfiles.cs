using AutoMapper;
using TaskApp.DTOs;
using TaskApp.Models;

namespace TaskApp.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ToDo, ToDoDTO>();
            CreateMap<CreateToDoDTO, ToDo>();
            CreateMap<UpdateToDoDTO, ToDo>();
        }
    }
}