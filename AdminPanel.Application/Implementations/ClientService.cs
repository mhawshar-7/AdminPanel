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

        public async Task Remove(int id)
        {
            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(id);
            _unitOfWork.Repository<Client>().Delete(client);
            await _unitOfWork.Complete();
        }

        public async Task Save(ClientDto dto)
        {
            //Project project;
            //if (dto.Id == 0)
            //{
            //    project = new Client(dto.Name);
            //    _unitOfWork.Repository<Project>().Create(project);
            //}
            //else
            //{
            //    project = await _unitOfWork.Repository<Project>().GetByIdAsync(dto.Id);
            //    if (project == null)
            //    {
            //        throw new ArgumentNullException(nameof(project), "Project not found");
            //    }
            //    project.Name = dto.Name;
            //    project.Description = dto.Description;
            //    _unitOfWork.Repository<Project>().Update(project);
            //}
        }
    }
}
