using Application.Adoptions.Create;
using Application.Adoptions.Delete;
using Application.Adoptions.GetAll;
using Application.Adoptions.GetById;
using Application.Adoptions.Update;
using Domain.DomainErrors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("adoptions")]
public class AdoptionsController : ApiController
{
    private readonly ISender _sender;

    public AdoptionsController(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var adoptions = await _sender.Send(new GetAllAdoptionsQuery());

        return adoptions.Match(
            adoptions => Ok(adoptions),
            error => Problem(error));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var adoption = await _sender.Send(new GetAdoptionByIdQuery(id));

        return adoption.Match(
            adoption => Ok(adoption),
            error => Problem(error));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdoption([FromBody] CreateAdoptionCommand command)
    {
        var result = await _sender.Send(command);

        return result.Match(
            adoption => Ok(adoption),
            error => Problem(error));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdoption(Guid id, [FromBody] UpdateAdoptionCommand command)
    {
        if (id != command.Id)
        {
            List<Error> errors = new()
            {
                Errors.Adoption.AdoptionNotFound
            };

            return Problem(errors);
        }

        var result = await _sender.Send(command);

        return result.Match(
            adoption => Ok(adoption),
            error => Problem(error));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdoption(Guid id)
    {
        var result = await _sender.Send(new DeleteAdoptionCommand(id));

        return result.Match(
            adoption => Ok(adoption),
            error => Problem(error));
    }
}
