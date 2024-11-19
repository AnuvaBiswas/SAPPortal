namespace SAPPortal.Models
{
    public class IOQ1Model
    {
        public string? ProcessStage { get; set; }
        public string? ProcessStageName { get; set; }
        public string? U_ParmCode { get; set; }
        public string? U_ParmDesc { get; set; }
        public string? U_ParmType { get; set; }
        public string? U_Remarks { get; set; }
        public string? ParmType1 { get; set; }

        public int? Sequence { get; set; }
        public double? U_StandValue { get; set; }
        public double? U_AcceptMaxValue { get; set; }
        public double? U_AcceptMinValue { get; set; }
        public double? U_HoldMaxValue { get; set; }
        public double? U_HoldMinValue { get; set; }
        public double? U_RejMaxValue { get; set; }
        public double? U_RejMinValue { get; set; }
        
    }
}
