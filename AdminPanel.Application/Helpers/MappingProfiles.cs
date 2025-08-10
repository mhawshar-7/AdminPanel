using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AutoMapper;

namespace AdminPanel.Application.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Project, ProjectDto>();
		}
	}
}