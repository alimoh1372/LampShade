using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class IncreaseInventory
    {
        
        public  long InventoryId { get; set; }
        [DisplayName("تعداد")]
        [Range(1, long.MaxValue, ErrorMessage = ValidatingMessage.IsRequired)]
        public long Count { get; set; }
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Description { get; set; }
    }
}