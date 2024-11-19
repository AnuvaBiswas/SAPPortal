using System.ComponentModel.DataAnnotations;
namespace SAPPortal.Models
{
    public class ParameterStatus
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
