using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.ModelViews;

namespace minimal_api.Dominio.Interfaces;

public interface IValidationAdministrador
{
    ErrorsValidation ValidateDTO(AdministradorDTO administradorDto);
}