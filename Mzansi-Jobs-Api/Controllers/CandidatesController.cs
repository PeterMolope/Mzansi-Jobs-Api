using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mzansi_Jobs_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get candidate profile by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Candidate,Employer,Admin")]
        [ProducesResponseType(typeof(CandidateProfileDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CandidateProfileDTO>> GetProfile(Guid id)
        {
            var result = await _mediator.Send(new GetCandidateProfileQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Create or update candidate profile details.
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Candidate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateCandidateProfileCommand command)
        {
            if (id != command.CandidateId)
            {
                return BadRequest("ID mismatch between route parameter and request body.");
            }

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
