using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.ModelViews;

namespace minimal_api.Dominio.DTOs;

public class ValidationVeiculoDTO : IValidationVeiculo
{
    public ErrorsValidation ValidateDTO(VeiculoDTO veiculoDTO)
    {
        var validation = new ErrorsValidation();

        if (string.IsNullOrEmpty(veiculoDTO.Nome) || string.IsNullOrEmpty(veiculoDTO.Marca))
        {
            validation.Mensagens.Add("O nome/marca não pode ser vazio");
        }
        else if (veiculoDTO.Ano < 1950)
        {
            validation.Mensagens.Add("Veículo deveria estar no museu ou sucateado, apenas superiores que 1950");
        }

        return validation;
    }
}