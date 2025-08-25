using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Application.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.Repository<Client>().Count();
            return count;
        }
        public async Task<int> CountDeleted()
        {
            var count = await _unitOfWork.Repository<Client>().CountDeleted();
            return count;
        }
        public async Task<IReadOnlyList<ClientDto>> GetAll()
        {
            var list = await _unitOfWork.Repository<Client>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<ClientDto>>(list);
        }

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<IReadOnlyList<IdNameDto>> GetIdNameClients()
        {
            var list = await _unitOfWork.Repository<Client>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<IdNameDto>>(list);
        }

        public async Task Remove(int id)
        {
            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(id);
            _unitOfWork.Repository<Client>().Delete(client);
            await _unitOfWork.Complete();
        }

        public async Task Save(ClientDto dto)
        {
            Client client;
            if (dto.Id == 0)
            {
                client = new Client(dto.Name, dto.Email)
                {
                    Phone = dto.Phone,
                    Address = dto.Address
                };
                _unitOfWork.Repository<Client>().Create(client);
            }
            else
            {
                client = await _unitOfWork.Repository<Client>().GetByIdAsync(dto.Id);
                if (client != null)
                {
                    client.Name = dto.Name;
                    client.Email = dto.Email;
                    client.Phone = dto.Phone;
                    client.Address = dto.Address;
                    _unitOfWork.Repository<Client>().Update(client);
                }
            }
            await _unitOfWork.Complete();
        }

        public async Task<IReadOnlyList<ClientDto>> GetAllWithSpec(ISpecification<Client> spec)
        {
            var list = await _unitOfWork.Repository<Client>().ListWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<ClientDto>>(list);
        }
        public async Task<int> CountWithSpecAsync(ISpecification<Client> spec)
        {
            return await _unitOfWork.Repository<Client>().CountAsync(spec);
        }
    }
}
