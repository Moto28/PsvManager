using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsvManager.Infrastructure.Data.Entities;
using PsvManager.Shared.DTO.Address;
using PsvManager.Shared.DTO.Driver;
using PsvManagerAPI.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PsvManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IDriverService _driverService;

        public DriverController(ILogger<DriverController> logger, IDriverService driverService)
        {
            _logger = logger;
            _driverService = driverService;
        }

        /// <summary>
        /// Gets all drivers without address.
        /// </summary>
        /// <returns>A list of drivers.</returns>
        [HttpGet("drivers", Name = "GetAllDrivers")]
        [SwaggerOperation(Summary = "Gets all drivers without address", Description = "Retrieves a list of all drivers.")]
        [SwaggerResponse(200, "Returns the list of drivers", typeof(IEnumerable<DriverDto>))]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var result = await _driverService.GetAllDriversAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            IEnumerable<DriverDto> driverDtos = result.Value.Select(e => new DriverDto
            {
                Forename = e.Forename,
                Surname = e.Surname,
                LicenseNumber = e.LicenseNumber
            });

            return Ok(driverDtos);
        }

        /// <summary>
        /// Gets all drivers with address.
        /// </summary>
        /// <returns>A list of drivers with their addresses.</returns>
        [HttpGet("drivers/with-address", Name = "GetAllDriversWithAddress")]
        [SwaggerOperation(Summary = "Gets all drivers with address", Description = "Retrieves a list of all drivers with their addresses.")]
        [SwaggerResponse(200, "Returns the list of drivers with addresses", typeof(IEnumerable<DriverWithAddressDto>))]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> GetAllDriversWithAddress()
        {
            var result = await _driverService.GetAllDriversWithAddressAsync();
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            IEnumerable<DriverWithAddressDto> driverDtos = result.Value.Select(e => new DriverWithAddressDto
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

        /// <summary>
        /// Gets a driver by id.
        /// </summary>
        /// <param name="id">The id of the driver.</param>
        /// <returns>The driver.</returns>
        [HttpGet("driver/{id}", Name = "GetDriverById")]
        [SwaggerOperation(Summary = "Gets a driver by id", Description = "Retrieves a driver by their unique ID.")]
        [SwaggerResponse(200, "Returns the driver", typeof(DriverDto))]
        [SwaggerResponse(404, "If the driver is not found")]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> GetDriverById(Guid id)
        {
            var result = await _driverService.GetDriverByIdAsync(id);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            var driverDto = new DriverDto
            {
                Forename = result.Value.Forename,
                Surname = result.Value.Surname,
                LicenseNumber = result.Value.LicenseNumber
            };

            return Ok(driverDto);
        }

        /// <summary>
        /// Adds a new driver.
        /// </summary>
        /// <param name="driverDto">The driver to add.</param>
        /// <returns>The added driver.</returns>
        [HttpPost("driver", Name = "AddDriver")]
        [SwaggerOperation(Summary = "Adds a new driver", Description = "Adds a new driver to the system.")]
        [SwaggerResponse(201, "Returns the added driver", typeof(DriverDto))]
        [SwaggerResponse(409, "If the driver already exists")]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> AddDriver([FromBody] DriverDto driverDto)
        {
            var driver = new Driver
            {
                Forename = driverDto.Forename,
                Surname = driverDto.Surname,
                LicenseNumber = driverDto.LicenseNumber
            };

            var result = await _driverService.AddDriverAsync(driver);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            var addedDriverDto = new DriverDto
            {
                Forename = result.Value.Forename,
                Surname = result.Value.Surname,
                LicenseNumber = result.Value.LicenseNumber
            };

            return CreatedAtRoute("GetDriverById", new { id = result.Value.Id }, addedDriverDto);
        }

        /// <summary>
        /// Updates an existing driver.
        /// </summary>
        /// <param name="id">The id of the driver to update.</param>
        /// <param name="driverDto">The updated driver.</param>
        /// <returns>The updated driver.</returns>
        [HttpPut("driver/{id}", Name = "UpdateDriver")]
        [SwaggerOperation(Summary = "Updates an existing driver", Description = "Updates the details of an existing driver.")]
        [SwaggerResponse(200, "Returns the updated driver", typeof(DriverDto))]
        [SwaggerResponse(404, "If the driver is not found")]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> UpdateDriver(Guid id, [FromBody] DriverDto driverDto)
        {
            var driver = new Driver
            {
                Forename = driverDto.Forename,
                Surname = driverDto.Surname,
                LicenseNumber = driverDto.LicenseNumber
            };

            var result = await _driverService.UpdateDriverAsync(driver);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            var updatedDriverDto = new DriverDto
            {
                Forename = result.Value.Forename,
                Surname = result.Value.Surname,
                LicenseNumber = result.Value.LicenseNumber
            };

            return Ok(updatedDriverDto);
        }

        /// <summary>
        /// Deletes a driver by id.
        /// </summary>
        /// <param name="id">The id of the driver to delete.</param>
        /// <returns>A boolean indicating success.</returns>
        [HttpDelete("driver/{id}", Name = "DeleteDriver")]
        [SwaggerOperation(Summary = "Deletes a driver by id", Description = "Deletes a driver from the system by their unique ID.")]
        [SwaggerResponse(200, "Returns true if the driver was deleted", typeof(bool))]
        [SwaggerResponse(404, "If the driver is not found")]
        [SwaggerResponse(500, "If there is a server error")]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            var result = await _driverService.DeleteDriverAsync(id);
            if (!result.IsSuccess)
            {
                return StatusCode(result.ProblemDetails.Status ?? 500, result.ProblemDetails);
            }

            return Ok(result.Value);
        }
    }
}

