namespace SAPPortal.Models
{
    public class ParameterRow
    {
        public int Id { get; set; }
        public int ItemParameterId { get; set; } 
        public string Code { get; set; } 
        public string Name { get; set; } 
        public string Type { get; set; }
        public string Type1 { get; set; }
        public decimal Standard { get; set; } 
        public decimal MinValue { get; set; } 
        public decimal MaxValue { get; set; } 
        public decimal HoldMinValue { get; set; } 
        public decimal HoldMaxValue { get; set; } 
        public decimal RejMinValue { get; set; } 
        public decimal RejMaxValue { get; set; }
        
        public bool Mandatory { get; set; }
        public string strMandatory { get; set; }
        public int Sequence { get; set; }
        public bool ShowOnPrint { get; set; }
        public string strShowOnPrint {  get; set; }
        public string AfterBeforeGRN { get; set; }
        public string AfterBeforeGRN1 { get; set; }

        public bool ViewOnPO { get; set; } 
        public string strViewOnPO {  get; set; }

        public int MyProperty { get; set; }


    }
}
