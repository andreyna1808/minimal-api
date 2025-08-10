using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Services;

[TestClass]
public class AdministradorServiceTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..", "..", "..", "..", "Api"
        );

        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }


    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
        var administradorService = new AdministradorService(context);
        
        var adm = new AdministradorEntity
        {
            Id = 23,
            Email = "teste@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        };

        administradorService.Include(adm);
        var AddAdmin = administradorService.FindById(adm.Id);
        
        Assert.AreEqual(23, AddAdmin.Id);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");
        var administradorService = new AdministradorService(context);

        var adm = new AdministradorEntity();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        // Act
        administradorService.Include(adm);
        var admDoBanco = administradorService.FindById(adm.Id);

        // Assert
        Assert.IsNotNull(admDoBanco);
        Assert.AreEqual(adm.Id, admDoBanco.Id);
    }
}