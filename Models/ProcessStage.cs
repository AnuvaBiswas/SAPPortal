using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class ProcessStage
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }

        public int U_Sequence { get; set; }
    }
}
