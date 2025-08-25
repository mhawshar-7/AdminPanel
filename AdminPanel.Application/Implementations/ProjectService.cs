using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using AdminPanel.Persistence.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Application.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.Repository<Project>().Count();
            return count;
        }
        public async Task<int> CountDeleted()
        {
            var count = await _unitOfWork.Repository<Project>().CountDeleted();
            return count;
        }

        public async Task<int> CountWithSpecAsync(ISpecification<Project> spec)
        {
            return await _unitOfWork.Repository<Project>().CountAsync(spec);
        }

        public async Task<IReadOnlyList<ProjectDto>> GetAll()
        {
            var list = await _unitOfWork.Repository<Project>().ListAllAsync();
            return _mapper.Map<IReadOnlyList<ProjectDto>>(list);
        }

        public async Task<IReadOnlyList<ProjectDto>> GetAllWithSpec(ISpecification<Project> spec)
        {
            var list = await _unitOfWork.Repository<Project>().ListWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<ProjectDto>>(list);
        }

        public async Task<ProjectDto> GetById(int id)
        {
            var project = await _unitOfWork.Repository<Project>().GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task Remove(int id)
        {
            var project = await _unitOfWork.Repository<Project>().GetByIdAsync(id);
            _unitOfWork.Repository<Project>().Delete(project);
            await _unitOfWork.Complete();
        }

        public async Task Save(ProjectDto dto)
        {
            Project project;
            Client client = null;
            if (dto.ClientId != 0)
            {
                client = await _unitOfWork.Repository<Client>().GetByIdAsync(dto.ClientId) ??
                         throw new ArgumentNullException(nameof(client), "Client not found"); ;
            }

            if (dto.Id == 0)
            {
                project = new Project(dto.Name)
                {
                    Description = dto.Description,
                    StartDate = dto.StartDate == default ? DateTime.UtcNow : dto.StartDate,
                    EndDate = dto.EndDate,
                    Status = dto.Status,
                    Budget = dto.Budget,
                    Client = client
                };
                _unitOfWork.Repository<Project>().Create(project);
            }
            else
            {
                project = await _unitOfWork.Repository<Project>().GetByIdAsync(dto.Id);
                if (project == null)
                {
                    throw new ArgumentNullException(nameof(project), "Project not found");
                }
                project.Name = dto.Name;
                project.Description = dto.Description;
                project.StartDate = dto.StartDate == default ? project.StartDate : dto.StartDate;
                project.EndDate = dto.EndDate;
                project.Status = dto.Status;
                project.Budget = dto.Budget;
                project.Client = client;
                _unitOfWork.Repository<Project>().Update(project);
            }
            await _unitOfWork.Complete();
        }
    }
}
