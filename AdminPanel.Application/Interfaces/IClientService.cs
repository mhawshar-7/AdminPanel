using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
namespace AdminPanel.Data.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> GetById(Guid id);
        Task<IReadOnlyList<ClientDto>> GetAll();
        Task<IReadOnlyList<ClientDto>> GetAllWithSpec(ISpecification<Client> spec);
        Task<IReadOnlyList<IdNameDto>> GetIdNameClients();
        Task Save(ClientDto dto);
        Task Remove(Guid id);
        Task<int> Count();
        Task<int> CountDeleted();
        Task<int> CountWithSpecAsync(ISpecification<Client> spec);
    }
}
