using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace SensorsAPI.Models
{
    public class Plants
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Location { get; set; }
        public List<Sensor> Sensors { get; set; }

        public Plants() {
            Id = ObjectId.GenerateNewId();
            Sensors = new List<Sensor>();
        }
    }
}
