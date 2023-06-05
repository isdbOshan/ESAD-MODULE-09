using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreAppOne.ViewModels
{
    public class PartsDataInputViewModel
    {
        [Required, StringLength(50)]
        public string PartName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal PartsPrice { get; set; }
    }
}
