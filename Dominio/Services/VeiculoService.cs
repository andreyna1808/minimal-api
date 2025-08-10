using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos;

public class VeiculoService : IVeiculo
{
    private readonly DbContexto _contexto;

    public VeiculoService(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public void Include(VeiculoEntity veiculoEntity)
    {
        _contexto.Veiculos.Add(veiculoEntity);
        _contexto.SaveChanges();
    }

    public List<VeiculoEntity> All(int? page = 1, int? pageSize = 10, string? nome = null, string? marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(c => c.Nome.Contains(nome));
        }

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(c => c.Nome.Contains(marca));
        }

        var currentPage = page ?? 1;
        var currentPageSize = pageSize ?? 10;
        query = query.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize);

        return query.ToList();
    }

    public VeiculoEntity? FindById(int id)
    {
        return _contexto.Veiculos.Find(id);
    }

    public void Update(VeiculoEntity veiculoEntity)
    {
        _contexto.Veiculos.Update(veiculoEntity);
        _contexto.SaveChanges();
    }

    public void Delete(VeiculoEntity veiculoEntity)
    {
        _contexto.Veiculos.Remove(veiculoEntity);
        _contexto.SaveChanges();
    }
}