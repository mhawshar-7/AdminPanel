using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
namespace AdminPanel.Data.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetById(Guid id);
        Task<IReadOnlyList<ProjectDto>> GetAll();
        Task<IReadOnlyList<ProjectDto>> GetAllWithSpec(ISpecification<Project> spec);
        Task Save(ProjectDto dto);
        Task Remove(Guid id);
        Task<int> Count();
        Task<int> CountDeleted();
        Task<int> CountWithSpecAsync(ISpecification<Project> spec);
    }
}
