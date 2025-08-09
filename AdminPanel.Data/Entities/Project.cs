using AdminPanel.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Data.Entities
{
    public class Project: BaseEntity, ISoftDeletable
    {
        public Project(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Project name cannot be null or empty.", nameof(name));
            }
            Name = name;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
