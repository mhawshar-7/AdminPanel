namespace AdminPanel.Data.Entities
{
    public class Client: BaseEntity
    {
        public Client(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Client name cannot be null or empty.", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Client email cannot be null or empty.", nameof(email));
            }
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public virtual ISet<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
