using minimal_api.Dominio.Entidades;

namespace Test.Domain;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestGetSetProperties()
    {
        // Arrange - Todas as variaveis que vamos criar
        var admin = new AdministradorEntity();
        
        // Act - Todas as ações que vamos executar
        admin.Id = 1;
        admin.Email = "admin@teste.com";
        admin.Senha = "123456";

        // Assert - Tudo que nós esperamos/validação
        Assert.AreEqual(1, admin.Id);
        Assert.AreEqual("admin@teste.com", admin.Email);
        Assert.AreEqual("123456", admin.Senha);
    }
}