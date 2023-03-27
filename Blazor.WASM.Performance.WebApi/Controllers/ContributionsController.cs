using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Performance.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.WASM.Performance.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContributionsController : Controller
    {
        private readonly ContributionsService _contributionService;

        public ContributionsController(ContributionsService contributionService)
        {
            _contributionService = contributionService ?? throw new ArgumentNullException(nameof(contributionService));
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetContributionsAsync([FromQuery] int skip = 0, [FromQuery] int take = 100,
            CancellationToken cancellationToken = default)
        {
            var contributions = await _contributionService.GetContributionsAsync();
            var result = contributions.Skip(skip).Take(take);
            Response.Headers["X-Contribution-Count"] = $"{contributions.Count}";
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetContribution([FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var contribution = _contributionService.GetContribution(id, cancellationToken);
            if (contribution == null)
            {
                return new NotFoundResult();
            }

            return Ok(contribution);
        }
    }
}