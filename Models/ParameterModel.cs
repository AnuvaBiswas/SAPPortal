namespace SAPPortal.Models
{
    public class ParameterModel
    {
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }

        public int? LineNum { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ProcessStage { get; set; }
        public string? ProcessStageName { get; set; }
        public string? ParmCode { get; set; }
        public string? ParmName { get; set; }
        public string? ParmType {  get; set; }
        public string? ParmType1 { get; set; }
 
        public int? Sequence { get; set; }
        public double? StandValue { get; set; }
        public double? AcceptMaxValue { get; set; }
        public double? AcceptMinValue { get; set; }
        public double? HoldMaxValue { get; set; }
        public double? HoldMinValue { get; set; }
        public double? RejMaxValue { get; set; }
        public double? RejMinValue { get; set; }
        public string? Remarks { get; set; }
        public double? Numeric { get; set; }
        public string? Logical { get; set; }

     
    }
}
