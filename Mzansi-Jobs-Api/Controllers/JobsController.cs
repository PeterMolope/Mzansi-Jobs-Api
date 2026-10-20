using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MzansiJobs.Application.Common.DTOs.Jobs;
using MzansiJobs.Application.Common.DTOs.Jobs.Pagination;
using MzansiJobs.Application.Features.Jobs.Commands;
using MzansiJobs.Application.Features.Jobs.Queries;

namespace Mzansi_Jobs_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Search and filter jobs with pagination.
        /// </summary>
        [HttpGet("search")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedListDTO<JobDetailsDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedListDTO<JobDetailsDTO>>> SearchJobs([FromQuery] SearchJobsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get detailed information for a specific job posting.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JobDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobDetailsDTO>> GetJobById(Guid id)
        {
            var result = await _mediator.Send(new GetJobByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Post a new job opportunity (Employers and Admins only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateJob([FromBody] CreateJobCommand command)
        {
            var jobId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetJobById), new { id = jobId }, jobId);
        }
    }
}
