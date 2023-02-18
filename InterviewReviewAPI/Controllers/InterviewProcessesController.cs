using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterviewReviewAPI.Models;

namespace InterviewReviewAPI.Controllers
{
    [Route("api/InterviewProcesses")]
    [ApiController]
    public class InterviewProcessesController : ControllerBase
    {

        // https://learn.microsoft.com/en-gb/aspnet/core/tutorials/first-web-api?WT.mc_id=dotnet-35129-website&view=aspnetcore-7.0&tabs=visual-studio#routing-and-url-paths

        private readonly InterviewContext _context;

        public InterviewProcessesController(InterviewContext context)
        {
            _context = context;
        }

        // GET: api/InterviewProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewProcessDTO>>> GetinterviewProcesses()
        {
            if (_context.interviewProcesses == null)
            {
                return NotFound();
            }
            return await _context.interviewProcesses
                .Select(x => InterviewProcessToDTO(x))
                .ToListAsync();
        }

        // GET: api/InterviewProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewProcessDTO>> GetInterviewProcess(long id)
        {
            if (_context.interviewProcesses == null)
            {
                return NotFound();
            }
            var interviewProcess = await _context.interviewProcesses.FindAsync(id);

            if (interviewProcess == null)
            {
                return NotFound();
            }

            return InterviewProcessToDTO(interviewProcess);
        }

        // id has to be greater than 0
        // PUT: api/InterviewProcesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterviewProcess(long id, InterviewProcessDTO interviewProcessDTO)
        {
            if (id != interviewProcessDTO.Id)
            {
                return BadRequest();
            }

            var interviewProcess = await _context.interviewProcesses.FindAsync(id);
            if (interviewProcess == null)
            {
                return NotFound();
            }
            interviewProcess = InterviewProcessFromDTO(interviewProcessDTO);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!InterviewProcessExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/InterviewProcesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InterviewProcessDTO>> PostInterviewProcess(InterviewProcessDTO interviewProcessDTO)
        {
            var interviewProcess = InterviewProcessFromDTO(interviewProcessDTO);
            if (_context.interviewProcesses == null)
            {
                return Problem("Entity set 'InterviewContext.interviewProcesses'  is null.");
            }
            _context.interviewProcesses.Add(interviewProcess);
            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetInterviewProcess", new { id = interviewProcess.Id }, interviewProcess);
            return CreatedAtAction(
                nameof(GetInterviewProcess),
                new { id = interviewProcess.Id },
                InterviewProcessToDTO(interviewProcess));
        }

        // DELETE: api/InterviewProcesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterviewProcess(long id)
        {
            if (_context.interviewProcesses == null)
            {
                return NotFound();
            }
            var interviewProcess = await _context.interviewProcesses.FindAsync(id);
            if (interviewProcess == null)
            {
                return NotFound();
            }

            _context.interviewProcesses.Remove(interviewProcess);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewProcessExists(long id)
        {
            return (_context.interviewProcesses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static InterviewProcessDTO InterviewProcessToDTO(InterviewProcess interviewProcess) =>
           new InterviewProcessDTO
           {
               Id = interviewProcess.Id,
               companyName = interviewProcess.companyName,
               JobName = interviewProcess.JobName,
               Description = interviewProcess.Description,
               WhereDiscovered = interviewProcess.WhereDiscovered,
               Review = interviewProcess.Review,
               IsComplete = interviewProcess.IsComplete,
               ApplyDate = interviewProcess.ApplyDate,
               FirstContact = interviewProcess.FirstContact,
               EndDate = interviewProcess.EndDate,
               InterviewCount = interviewProcess.InterviewCount,
               OnlineAssesment = interviewProcess.OnlineAssesment,
               VideoInterview = interviewProcess.VideoInterview,
               JobOffered = interviewProcess.JobOffered,
               OfferAccepted = interviewProcess.OfferAccepted,
           };

        private static InterviewProcess InterviewProcessFromDTO(InterviewProcessDTO interviewProcessDTO) =>
           new InterviewProcess
           {
               Id = interviewProcessDTO.Id,
               companyName = interviewProcessDTO.companyName,
               JobName = interviewProcessDTO.JobName,
               Description = interviewProcessDTO.Description,
               WhereDiscovered = interviewProcessDTO.WhereDiscovered,
               Review = interviewProcessDTO.Review,
               IsComplete = interviewProcessDTO.IsComplete,
               ApplyDate = interviewProcessDTO.ApplyDate,
               FirstContact = interviewProcessDTO.FirstContact,
               EndDate = interviewProcessDTO.EndDate,
               InterviewCount = interviewProcessDTO.InterviewCount,
               OnlineAssesment = interviewProcessDTO.OnlineAssesment,
               VideoInterview = interviewProcessDTO.VideoInterview,
               JobOffered = interviewProcessDTO.JobOffered,
               OfferAccepted = interviewProcessDTO.OfferAccepted
           };
    }
}
