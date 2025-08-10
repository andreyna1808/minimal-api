using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces;

public interface IAdministrador
{
    AdministradorEntity? Login(LoginDTO loginDto);
    string GenerateToken(AdministradorEntity administradorEntity);
    List<AdministradorEntity> All(int? page = 1, int? pageSize = 10);
    AdministradorEntity? FindById(int id);
    void Include(AdministradorEntity administradorEntity);
    void Update(AdministradorEntity administradorEntity);
    void Delete(AdministradorEntity administradorEntity);
}