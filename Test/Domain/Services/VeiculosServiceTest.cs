using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Services;

[TestClass]
public class VeiculosServiceTest
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
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");
        var veiculoService = new VeiculoService(context);

        var veiculo = new VeiculoEntity
        {
            Id = 23,
            Nome = "Sandeiro",
            Marca = "Renault",
            Ano = 2013
        };

        veiculoService.Include(veiculo);
        var AddAdmin = veiculoService.FindById(veiculo.Id);

        Assert.AreEqual(23, AddAdmin.Id);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");
        var veiculoService = new VeiculoService(context);

        var veiculo = new VeiculoEntity();
        veiculo.Nome = "Sandeiro";
        veiculo.Marca = "Renault";
        veiculo.Ano = 2013;

        // Act
        veiculoService.Include(veiculo);
        var veiculoDoBanco = veiculoService.FindById(veiculo.Id);

        // Assert
        Assert.IsNotNull(veiculoDoBanco);
        Assert.AreEqual(veiculo.Id, veiculoDoBanco.Id);
    }
}