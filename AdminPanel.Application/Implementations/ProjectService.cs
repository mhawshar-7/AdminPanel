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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProjectDto>> GetAll()
        {
            var list = await _unitOfWork.Repository<Project>().ListAllAsync();
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
            if (dto.Id == 0)
            {
                project = new Project(dto.Name);
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
                _unitOfWork.Repository<Project>().Update(project);
            }
        }
    }
}
