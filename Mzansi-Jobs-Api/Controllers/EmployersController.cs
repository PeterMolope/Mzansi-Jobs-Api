using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MzansiJobs.Application.Common.DTOs.Employers;
using MzansiJobs.Application.Features.Employers.Commands;
using MzansiJobs.Application.Features.Employers.Queries;

namespace Mzansi_Jobs_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get employer profile by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(EmployerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployerDTO>> GetEmployerById(Guid id)
        {
            var result = await _mediator.Send(new GetEmployerProfileQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Get all employers belonging to a specific company.
        /// </summary>
        [HttpGet("company/{companyId:guid}")]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(IEnumerable<EmployerDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployerDTO>>> GetEmployersByCompany(Guid companyId)
        {
            var result = await _mediator.Send(new GetEmployersByCompanyQuery(companyId));
            return Ok(result);
        }

        /// <summary>
        /// Create an employer profile.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> CreateEmployer([FromBody] CreateEmployerCommand command)
        {
            var employerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployerById), new { id = employerId }, employerId);
        }
    }
}
