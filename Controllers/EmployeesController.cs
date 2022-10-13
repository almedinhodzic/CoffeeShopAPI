using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
            {
                throw new NotFoundException(nameof(GetEmployees), "");
            }

            var records = _mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
            return Ok(records);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(string id)
        {
            if (id == null)
            {
                throw new BadRequestException(nameof(GetEmployee));
            }

            var employee = await _employeeRepository.GetEmployee(id);
            if (employee == null)
            {
                throw new NotFoundException(nameof(GetEmployee), id);
            }

            var record = _mapper.Map<Employee, EmployeeDto>(employee);
            return record;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (!await _employeeRepository.Exists(id))
            {
                throw new BadRequestException(nameof(DeleteEmployee));
            }

            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto.Id != id)
            {
                throw new BadRequestException(nameof(UpdateEmployee));
            }

            if (!await _employeeRepository.Exists(id))
            {
                throw new NotFoundException(nameof(UpdateEmployee), id);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
            await _employeeRepository.UpdateAsync(employee);
            return NoContent();
        }

    }
}
