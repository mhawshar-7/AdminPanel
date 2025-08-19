using AdminPanel.Application.Dtos;
namespace AdminPanel.Data.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> GetById(int id);
        Task<IReadOnlyList<ClientDto>> GetAll();
        Task Save(ClientDto dto);
        Task Remove(int id);
        Task<int> Count();

    }
}
