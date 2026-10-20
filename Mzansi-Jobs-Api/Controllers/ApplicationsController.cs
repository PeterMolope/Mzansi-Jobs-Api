using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MzansiJobs.Application.Common.DTOs.Applying;
using MzansiJobs.Application.Features.Applying.Commands;
using MzansiJobs.Application.Features.Applying.Queries;

namespace Mzansi_Jobs_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Submit an application for a job post (Candidates only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Candidate")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ApplyForJob([FromBody] ApplyForJobCommand command)
        {
            var applicationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCandidateApplications), new { candidateId = command.CandidateId }, applicationId);
        }

        /// <summary>
        /// Get all applications submitted by a specific candidate.
        /// </summary>
        [HttpGet("candidate/{candidateId:guid}")]
        [Authorize(Roles = "Candidate,Admin")]
        [ProducesResponseType(typeof(IEnumerable<CandidateApplicationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CandidateApplicationDTO>>> GetCandidateApplications(Guid candidateId)
        {
            var result = await _mediator.Send(new GetCandidateApplicationsQuery(candidateId));
            return Ok(result);
        }

        /// <summary>
        /// Get all applications received for a specific job posting (Employers & Admins).
        /// </summary>
        [HttpGet("job/{jobId:guid}")]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(typeof(IEnumerable<JobApplicationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<JobApplicationDTO>>> GetApplicationsByJob(Guid jobId)
        {
            var result = await _mediator.Send(new GetApplicationsByJobIdQuery(jobId));
            return Ok(result);
        }

        /// <summary>
        /// Update the recruitment status of a submitted application (Employers & Admins).
        /// </summary>
        [HttpPatch("{id:guid}/status")]
        [Authorize(Roles = "Employer,Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplicationStatus(Guid id, [FromBody] UpdateApplicationStatusCommand command)
        {
            if (id != command.ApplicationId)
            {
                return BadRequest("Mismatched application ID in route and request body.");
            }

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
