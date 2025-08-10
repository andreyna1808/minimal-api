using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.ModelViews;

namespace minimal_api.Dominio.DTOs;

public class ValidationAdministradorDTO : IValidationAdministrador
{
    public ErrorsValidation ValidateDTO(AdministradorDTO administradorDto)
    {
        var validation = new ErrorsValidation();

        if (
            string.IsNullOrEmpty(administradorDto.Email)
            || string.IsNullOrEmpty(administradorDto.Perfil)
            || string.IsNullOrEmpty(administradorDto.Senha)
        )
        {
            validation.Mensagens.Add("O Email/Senha/Perfil não pode ser vazio");
        }

        return validation;
    }
}
