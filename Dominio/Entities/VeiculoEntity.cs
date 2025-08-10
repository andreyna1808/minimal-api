using System.ComponentModel.DataAnnotations;

namespace minimal_api.Dominio.Entidades;

using System.ComponentModel.DataAnnotations.Schema;

public class VeiculoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default!;

    [Required] [StringLength(150)] public string Nome { get; set; } = default!;

    [Required] [StringLength(100)] public string Marca { get; set; } = default!;

    [Required] [StringLength(10)] public int Ano { get; set; } = default!;
}