using System;
using System.Threading;
using System.Threading.Tasks;
using Blazor.WASM.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.WASM.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;

        public PeopleController(PeopleService peopleService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException(nameof(peopleService));
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleAsync([FromQuery] int skip = 0, [FromQuery] int take = 100,
            CancellationToken cancellationToken = default)
        {
            var people = await _peopleService.GetPeopleAsync(skip, take, cancellationToken);
            return Ok(people);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPeopleAsync([FromRoute] string name,
            CancellationToken cancellationToken = default)
        {
            var person = await _peopleService.GetPersonAsync(name, cancellationToken);
            if (person == null)
            {
                return new NotFoundResult();
            }

            return Ok(person);
        }
    }
}