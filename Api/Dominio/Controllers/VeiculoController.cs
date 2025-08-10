using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.ModelViews;
using minimal_api.Dominio.Servicos;

namespace minimal_api.Dominio.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculo _veiculo;
    private readonly IValidationVeiculo _validator;

    public VeiculoController(IVeiculo veiculo, IValidationVeiculo validator)
    {
        _veiculo = veiculo;
        _validator = validator;
    }

    [HttpGet]
    [Authorize]
    public IResult Get(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] string? nome,
        [FromQuery] string? marca
    )
    {
        var veiculos = _veiculo.All(page, pageSize, nome, marca);
        return Results.Ok(veiculos);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IResult Get([FromRoute] int id)
    {
        var veiculo = _veiculo.FindById(id);
        if (veiculo == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(veiculo);
    }

    [HttpPost()]
    [Authorize(Roles = "admin,editor")]
    public IResult Post([FromBody] VeiculoDTO veiculoDTO)
    {
        var validation = _validator.ValidateDTO(veiculoDTO);
        if (validation.Mensagens.Count > 0)
        {
            return Results.BadRequest(validation.Mensagens);
        }

        var newVeiculo = new VeiculoEntity
        {
            Nome = veiculoDTO.Nome,
            Marca = veiculoDTO.Marca,
            Ano = veiculoDTO.Ano,
        };
        _veiculo.Include(newVeiculo);

        return Results.Created("veiculo", newVeiculo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IResult Put([FromRoute] int id, [FromBody] VeiculoDTO veiculoDTO)
    {
        var validation = _validator.ValidateDTO(veiculoDTO);
        if (validation.Mensagens.Count > 0)
        {
            return Results.BadRequest(validation.Mensagens);
        }

        var veiculo = _veiculo.FindById(id);
        if (veiculo == null)
            return Results.NotFound();

        veiculo.Nome = veiculoDTO.Nome;
        veiculo.Marca = veiculoDTO.Marca;
        veiculo.Ano = veiculoDTO.Ano;

        _veiculo.Update(veiculo);
        return Results.NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IResult Delete([FromRoute] int id)
    {
        var veiculo = _veiculo.FindById(id);
        if (veiculo == null)
            return Results.NotFound();

        _veiculo.Delete(veiculo);
        return Results.NoContent();
    }
}
