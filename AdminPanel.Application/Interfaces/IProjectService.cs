using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
namespace AdminPanel.Data.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetById(int id);
        Task<IReadOnlyList<ProjectDto>> GetAll();
        Task Save(ProjectDto dto);

    }
}
