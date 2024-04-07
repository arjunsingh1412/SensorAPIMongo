using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SensorsAPI.Models;
using SensorsAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SensorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SensorsController : Controller
    {
        private readonly DataBaseService _dbService;

        public SensorsController(DataBaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public ActionResult<List<Plants>> Get() =>  _dbService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSensor")]
        public ActionResult<Plants> Get(string id)
        {
            var sensor = _dbService.Get(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        [HttpGet]
        [Route("GetByLocation")]
        public ActionResult<Plants> GetByLocation([Required] string Location)
        {
            var sensor = _dbService.Get(Location);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        [HttpPost]
        public ActionResult<Plants> CreateOrUpdate(string MachineName,int count ,string Location)
        {

            Plants plantInfo = new Plants();
            plantInfo.Location = Location;
            plantInfo.Sensors.Add(new Sensor
            {
                Name = MachineName,LastUpdated = DateTime.Now,ActualCount = count,EstimatedCount = 100,Location = Location
            });

            _dbService.CreateOrUpdate(plantInfo);

            return CreatedAtRoute("GetSensor", new { id = plantInfo.Id.ToString() }, plantInfo);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Plants sensorIn)
        {
            var sensor = _dbService.Get(id);

            if (sensor == null)
            {
                return NotFound();
            }

            _dbService.Update(id, sensorIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var sensor = _dbService.Get(id);

            if (sensor == null)
            {
                return NotFound();
            }

            _dbService.Delete(sensor.Id.ToString());

            return NoContent();
        }
    }
}
