using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces;

public interface IVeiculo
{
    List<VeiculoEntity> All(
        int? page = 1,
        int? pageSize = 10,
        string? nome = null,
        string? marca = null
    );
    VeiculoEntity? FindById(int id);
    void Include(VeiculoEntity veiculoEntity);
    void Update(VeiculoEntity veiculoEntity);
    void Delete(VeiculoEntity veiculoEntity);
}
