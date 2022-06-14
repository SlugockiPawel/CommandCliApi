using AutoMapper;
using CommandCliApi.DTOs;
using CommandCliApi.Models;

namespace CommandCliApi.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // source => target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>(); // used for PATCH verb
        }
    }
}
