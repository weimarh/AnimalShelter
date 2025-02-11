using Application.Animals.Create;
using Application.Animals.Delete;
using Application.Animals.GetAll;
using Application.Animals.GetById;
using Application.Animals.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("animals")]
public class AnimalsController : ApiController
{
    private readonly ISender _mediator;

    public AnimalsController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _mediator.Send(new GetAllAnimalsQuery());

        return animals.Match(
            animals => Ok(animals),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var animal = await _mediator.Send(new GetAnimalByIdQuery(id));

        return animal.Match(
            animal => Ok(animal),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnimal([FromBody]CreateAnimalCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            animal => Ok(animal),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(Guid id, [FromBody] UpdateAnimalCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Errors.Animal.AnimalNotFound
            };

            return Problem(errors);
        }

        var result = await _mediator.Send(command);

        return result.Match(
            animal => Ok(animal),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(Guid id)
    {
        var result = await _mediator.Send(new DeleteAnimalCommand(id));

        return result.Match(
            animal => Ok(animal),
            errors => Problem(errors)
        );
    }
}
