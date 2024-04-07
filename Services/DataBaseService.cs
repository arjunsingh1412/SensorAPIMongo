using MongoDB.Driver;
using SensorsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SensorsAPI.Services
{
    public class DataBaseService
    {
        private readonly IMongoCollection<Plants> _sensors;
        private readonly IMongoCollection<Sensor> _projections;

        public DataBaseService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sensors = database.GetCollection<Plants>(settings.SensorCollectionName);
        }

        public List<Plants> Get() => _sensors.Find(sensor => true).ToList();

        public Plants Get(string id) => _sensors.Find(sensor => sensor.Id.ToString() == id).FirstOrDefault();

        public Plants Create(Plants sensor)
        {
            _sensors.InsertOne(sensor);
            return sensor;
        }

        public void Update(string id, Plants updatedsensor) => _sensors.ReplaceOne(sensor => sensor.Id.ToString() == id, updatedsensor);

        public void Delete(Plants sensorForDeletion) => _sensors.DeleteOne(sensor => sensor.Id == sensorForDeletion.Id);

        public void Delete(string id) => _sensors.DeleteOne(sensor => sensor.Id.ToString() == id);




        #region custom Crud operation
        public Plants CreateOrUpdate(Plants sensor)
        {
            var sensorInfo = GetByLocation(sensor.Location);
            if(sensorInfo != null)
            {
                var existingSensorEntries = sensorInfo.Sensors;
                if(existingSensorEntries?.Count > 0)
                {
                    var matchingSensor = existingSensorEntries.Find(item=>item.Name.Equals(sensor.Sensors.First().Name));
                    if (matchingSensor != null && sensor.Sensors.First().LastUpdated.Date.Equals(matchingSensor.LastUpdated.Date)) {
                        matchingSensor.ActualCount = matchingSensor.ActualCount + sensor.Sensors.First().ActualCount;
                        matchingSensor.LastUpdated = sensor.Sensors.First().LastUpdated;
                    }
                    else
                    {
                       sensorInfo.Sensors = existingSensorEntries = sensor.Sensors.Concat(existingSensorEntries).ToList();
                    }

                }
                Update(sensorInfo.Id.ToString(), sensorInfo);
                return sensorInfo;
            }
            else
            {
                _sensors.InsertOne(sensor);
            }
            return sensor;
            
        }
        public Plants GetByLocation(string Location) => _sensors.Find(sensor => sensor.Location == Location).FirstOrDefault();

        #endregion










        #region projection operation
        public List<Sensor> GetProjection() => _projections.Find(projection => true).ToList();
        public Sensor GetProjection(string id) => _projections.Find(projection => projection.Id == id).FirstOrDefault();

        public Sensor CreateProjection(Sensor projection)
        {
            _projections.InsertOne(projection);
            return projection;
        }

        public void UpdateProjection(string id, Sensor updatedprojection) => _projections.ReplaceOne(projection => projection.Id == id, updatedprojection);

        public void DeleteProjection(Sensor projectionForDeletion) => _projections.DeleteOne(projection => projection.Id == projectionForDeletion.Id);

        public void DeleteProjection(string id) => _projections.DeleteOne(projection => projection.Id == id);
        #endregion

    }
}
