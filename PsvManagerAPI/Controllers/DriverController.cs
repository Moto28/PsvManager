using Microsoft.AspNetCore.Mvc;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Shared.DTO.Address;
using PsvManager.Shared.DTO.Driver;
using PsvManagerAPI.Core.Interfaces;

namespace PsvManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;

        public DriverController(ILogger<DriverController> logger, IDriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }

        [HttpGet(Name = "GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            IEnumerable<Driver> drivers = await _driverService.GetAllDrivers();
            IEnumerable<DriverDto> driverDtos = drivers.Select(e => new DriverDto
            {
                Forename = e.Forename,
                Surname = e.Surname,
                LicenseNumber = e.LicenseNumber
            });

            return Ok(driverDtos);
        }

        [HttpGet("WithAddress", Name = "GetAllDriversWithAddress")]
        public async Task<IActionResult> GetAllDriversWithAddress()
        {
            IEnumerable<Driver> drivers = await _driverService.GetAllDriversWithAddress();
            IEnumerable<DriverWithAddressDto> driverDtos = drivers.Select(e => new DriverWithAddressDto
            {
                Driver = new DriverDto
                {
                    Forename = e.Forename,
                    Surname = e.Surname,
                    LicenseNumber = e.LicenseNumber
                },
                Address = new AddressDto
                {
                    HouseNumber = e.Address.HouseNumber,
                    StreetName = e.Address.StreetName,
                    TownOrCity = e.Address.TownOrCity,
                    County = e.Address.County,
                    Postcode = e.Address.Postcode
                }
            });

            return Ok(driverDtos);
        }
    }
}