namespace AdminPanel.Web.Models
{
    public class EntityStatsModel
    {
        public string EntityName { get; set; } = string.Empty;
        public int ActiveCount { get; set; }
        public int DeletedCount { get; set; }
        public int TotalCount => ActiveCount + DeletedCount;
        public double DeletedPercentage => TotalCount == 0 ? 0 : Math.Round((double)DeletedCount / TotalCount * 100, 1);
        
        public string DeletedPercentageFormatted => $"{DeletedPercentage}%";
        
        public string ActiveEntityLabel => $"Active {EntityName}";
        
        public string DeletedSummary => $"{DeletedCount} Deleted";
    }
}