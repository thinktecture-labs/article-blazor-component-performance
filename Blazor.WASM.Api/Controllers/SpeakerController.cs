using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.WASM.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeakerController : Controller
    {
        private readonly SpeakerService _speakerService;

        public SpeakerController(SpeakerService speakerService)
        {
            _speakerService = speakerService ?? throw new ArgumentNullException(nameof(speakerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSpeakersAsync([FromQuery] int skip = 0, [FromQuery] int take = 100,
            CancellationToken cancellationToken = default)
        {
            var speakers = await _speakerService.GetSpeakersAsync();
            var result = speakers.Skip(skip).Take(take);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeakerAsync([FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var speaker = await _speakerService.GetSpeakerAsync(id, cancellationToken);
            if (speaker == null)
            {
                return new NotFoundResult();
            }

            return Ok(speaker);
        }
    }
}