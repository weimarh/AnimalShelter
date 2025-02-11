using Application.Adopters.Create;
using Application.Adopters.GetAll;
using Application.Adopters.GetById;
using Application.Adopters.Update;
using Application.Animals.Delete;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("adopters")]
public class AdoptersController : ApiController
{
    private readonly IMediator _mediator;
    public AdoptersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var adopters = await _mediator.Send(new GetAllAdoptersQuery());

        return adopters.Match(
            adopters => Ok(adopters),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var adopter = await _mediator.Send(new GetAdopterByIdQuery(id));

        return adopter.Match(
            adopter => Ok(adopter),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdopter([FromBody] CreateAdopterCommand command)
    {
        var result = await _mediator.Send(command);
        
        return result.Match(
            adopter => Ok(adopter),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdopter(Guid id, [FromBody] UpdateAdopterCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Errors.Adopter.AdopterNotFound
            };

            return Problem(errors);
        }
        
        var result = await _mediator.Send(command);

        return result.Match(
            adopter => Ok(adopter),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdopter(Guid id)
    {
        var result = await _mediator.Send(new DeleteAnimalCommand(id));

        return result.Match(
            adopter => Ok(adopter),
            errors => Problem(errors)
        );
    }
}
