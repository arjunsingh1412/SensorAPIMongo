namespace SensorsAPI.Models
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string SensorCollectionName { get; set; }
        public string ProjectionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string SensorCollectionName { get; set; }
        string ProjectionCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
