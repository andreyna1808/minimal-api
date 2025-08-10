using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;

namespace Test.Mocks;

public class AdministradorServicoMock : IAdministrador
{
    private static List<AdministradorEntity> administradores = new List<AdministradorEntity>()
    {
        new AdministradorEntity { Id = 1, Email = "adm@teste.com", Senha = "123456", Perfil = "Adm" },
        new AdministradorEntity { Id = 2, Email = "editor@teste.com", Senha = "123456", Perfil = "Editor" }
    };

    public AdministradorEntity? BuscaPorId(int id) => administradores.Find(a => a.Id == id);

    public AdministradorEntity Incluir(AdministradorEntity administrador)
    {
        administrador.Id = administradores.Count + 1;
        administradores.Add(administrador);
        return administrador;
    }

    public AdministradorEntity? Login(LoginDTO loginDTO) =>
        administradores.Find(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);

    public string GenerateToken(AdministradorEntity administradorEntity)
    {
        return "token-fake-para-teste";
    }

    public List<AdministradorEntity> All(int? page, int? pageSize) => administradores;

    public AdministradorEntity? FindById(int id) => administradores.Find(a => a.Id == id);

    public void Include(AdministradorEntity administradorEntity)
    {
        administradorEntity.Id = administradores.Count + 1;
        administradores.Add(administradorEntity);
    }

    public void Update(AdministradorEntity administradorEntity)
    {
        var adm = administradores.Find(a => a.Id == administradorEntity.Id);
        if (adm != null)
        {
            adm.Email = administradorEntity.Email;
            adm.Senha = administradorEntity.Senha;
            adm.Perfil = administradorEntity.Perfil;
        }
    }

    public void Delete(AdministradorEntity administradorEntity)
    {
        administradores.RemoveAll(a => a.Id == administradorEntity.Id);
    }

    public List<AdministradorEntity> Todos(int? pagina) => administradores;
}