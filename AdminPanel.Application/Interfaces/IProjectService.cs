using AdminPanel.Application.Dtos;
namespace AdminPanel.Data.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetById(int id);
        Task<IReadOnlyList<ProjectDto>> GetAll();
        Task Save(ProjectDto dto);
        Task Remove(int id);
        Task<int> Count();

    }
}
