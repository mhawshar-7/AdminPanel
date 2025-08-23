namespace AdminPanel.Data.Entities
{
    public class Project: BaseEntity
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
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public decimal? Budget { get; set; }
        public int? ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
