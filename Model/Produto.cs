using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_test.Model
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "Decimal(6,2)")]
        public decimal Preco { get; set; }
    }
}
