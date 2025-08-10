using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;

namespace minimal_api.Dominio.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministradorController : ControllerBase
{
    private readonly IAdministrador _administrador;
    private readonly IValidationAdministrador _validator;

    public AdministradorController(IAdministrador administrador, IValidationAdministrador validator)
    {
        _administrador = administrador;
        _validator = validator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Post([FromBody] LoginDTO loginDTO)
    {
        var resultado = _administrador.Login(loginDTO);

        if (resultado != null)
        {
            var token = _administrador.GenerateToken(resultado);
            return Ok(
                new
                {
                    resultado.Email,
                    resultado.Perfil,
                    token,
                }
            );
        }

        return Unauthorized();
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public IResult Get([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        var administrador = _administrador.All(page, pageSize);
        return Results.Ok(administrador);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public IResult Get([FromRoute] int id)
    {
        var administrador = _administrador.FindById(id);
        if (administrador == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(administrador);
    }

    [HttpPost()]
    [Authorize(Roles = "admin")]
    public IResult Post([FromBody] AdministradorDTO administradorDTO)
    {
        var validation = _validator.ValidateDTO(administradorDTO);
        if (validation.Mensagens.Count > 0)
        {
            return Results.BadRequest(validation.Mensagens);
        }

        var newVeiculo = new AdministradorEntity
        {
            Email = administradorDTO.Email,
            Senha = administradorDTO.Senha,
            Perfil = administradorDTO.Perfil,
        };
        _administrador.Include(newVeiculo);

        return Results.Created("administrador", newVeiculo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IResult Put([FromRoute] int id, [FromBody] AdministradorDTO administradorDTO)
    {
        var validation = _validator.ValidateDTO(administradorDTO);
        if (validation.Mensagens.Count > 0)
        {
            return Results.BadRequest(validation.Mensagens);
        }

        var administrador = _administrador.FindById(id);
        if (administrador == null)
            return Results.NotFound();

        administrador.Email = administradorDTO.Email;
        administrador.Senha = administradorDTO.Senha;
        administrador.Perfil = administradorDTO.Perfil;

        _administrador.Update(administrador);
        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IResult Delete([FromRoute] int id)
    {
        var administrador = _administrador.FindById(id);
        if (administrador == null)
            return Results.NotFound();

        _administrador.Delete(administrador);
        return Results.NoContent();
    }
}
