using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AutoMapper;

namespace AdminPanel.Application.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Project, ProjectDto>();
			CreateMap<Project, IdNameDto>();
			CreateMap<Client, ClientDto>();
			CreateMap<Client, IdNameDto>();
			CreateMap<User, UserDto>();
        }
	}
}