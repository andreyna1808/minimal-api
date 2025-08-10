using minimal_api.Dominio.Entidades;

namespace Test.Domain;

[TestClass]
public class VeiculosTest
{
    [TestMethod]
    public void TestGetSetProperties()
    {
        // Arrange - Todas as variaveis que vamos criar
        var veiculo = new VeiculoEntity();

        // Act - Todas as ações que vamos executar
        veiculo.Id = 1;
        veiculo.Nome = "Fiesta";
        veiculo.Marca = "Ford";

        // Assert - Tudo que nós esperamos/validação
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Fiesta", veiculo.Nome);
        Assert.AreEqual("Ford", veiculo.Marca);
    }
}