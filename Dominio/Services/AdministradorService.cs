using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos;

public class AdministradorService : IAdministrador
{
    private readonly DbContexto _contexto;
    private readonly string _jwtKey;

    public AdministradorService(DbContexto contexto, IConfiguration config)
    {
        _contexto = contexto;
        _jwtKey = config["JWT:Key"] ?? "MinhaChaveSuperSecreta123456789!@#MinhaChaveSuperSecreta123456789";
    }


    public AdministradorEntity? Login(LoginDTO loginDto)
    {
        var userAdmin = _contexto.Administradores
            .FirstOrDefault(a => a.Email == loginDto.Email && a.Senha == loginDto.Senha);
        return userAdmin;
    }

    public List<AdministradorEntity> All(int? page, int? pageSize)
    {
        var query = _contexto.Administradores.AsQueryable();
        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? 10;
        query = query.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize);
        return query.ToList();
    }

    public AdministradorEntity? FindById(int id)
    {
        return _contexto.Administradores.Find(id);
    }

    public void Include(AdministradorEntity administradorEntity)
    {
        _contexto.Administradores.Add(administradorEntity);
        _contexto.SaveChanges();
    }

    public void Update(AdministradorEntity administradorEntity)
    {
        _contexto.Administradores.Update(administradorEntity);
        _contexto.SaveChanges();
    }

    public void Delete(AdministradorEntity administradorEntity)
    {
        _contexto.Administradores.Remove(administradorEntity);
        _contexto.SaveChanges();
    }

    public  string GenerateToken(AdministradorEntity administradorEntity)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim("Email", administradorEntity.Email),
            new Claim("Perfil", administradorEntity.Perfil),
            new Claim(ClaimTypes.Role, administradorEntity.Perfil)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}