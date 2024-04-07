using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace SensorsAPI.Models
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
        public int EstimatedCount { get; set; }
        public int ActualCount { get; set; }
        public string OperatorName { get; set; }
        public double Percentage { get; set; }
        public bool IsSensorEnabled { get; set;}
        


        public Sensor()
        {
            Id = ObjectId.GenerateNewId().ToString();
            LastUpdated = DateTime.UtcNow;
            IsSensorEnabled = true;
        }

    }
}
