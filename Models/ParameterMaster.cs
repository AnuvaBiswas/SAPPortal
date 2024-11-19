using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class ParameterMaster
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public string ParameterType { get; set; }
        public string ParameterType1 { get; set; }
        public string UpDown {  get; set; }
        public string UpDown1 { get; set; }
        public string ShowOnPrint { get; set; }
        public string ShowOnPrint1 { get; set; }
        public string GRN { get; set; }
        public string GRN1 { get; set; }
        public string ViewOnPo { get; set; }
        public string ViewOnPo1 {  get; set; }
        public string? StdText { get; set; }
        public bool Active { get; set; }

        public string strActive {  get; set; }
    }
}
