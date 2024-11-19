namespace SAPPortal.Models
{
    public class ItemParameter
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ItemCode { get; set; } 
        public string ItemDescription { get; set; }
        public DateTime EffectiveFrom { get; set; } 
        public DateTime EffectiveTo { get; set; }  
        public int NumberOfSample { get; set; }
        public string strProduction { get; set; }
        public bool Production { get; set; }
        public string ProcessStage { get; set; }

        // List to hold multiple parameter rows
        public List<ParameterRow> Parameters { get; set; } = new List<ParameterRow>();
    }
  
}


