using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MzansiJobs.Application.Common.DTOs.Companies;
using MzansiJobs.Application.Features.Companies.Commands;
using MzansiJobs.Application.Features.Companies.Queries;

namespace Mzansi_Jobs_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get company details by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CompanyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDTO>> GetCompanyById(Guid id)
        {
            var result = await _mediator.Send(new GetCompanyByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Register a new employer company profile.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreateCompany([FromBody] CreateCompanyCommand command)
        {
            var companyId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCompanyById), new { id = companyId }, companyId);
        }

        /// <summary>
        /// Update an existing company profile.
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.CompanyId)
            {
                return BadRequest("Mismatched company ID in route and request body.");
            }

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
