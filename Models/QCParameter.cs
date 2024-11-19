namespace SAPPortal.Models
{
    public class QCParameter
    {
        public int Id { get; set; }
        public string ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string Type { get; set; }
        public string Type1 { get; set; }
        
        public double? NumericValue { get; set; }
        public double? Numeric { get; set; }
        public bool? LogicalValue { get; set; }
        public string? Logical { get; set; }
        public string Remarks { get; set; }
       public string ProcessStage {  get; set; }
        public string ProcessStageName {get;set;}

        public int? Sequence { get; set; }
        public double? StandValue { get; set; }
        public double? AcceptMaxValue { get; set; }
        public double? AcceptMinValue { get; set; }
        public double? HoldMaxValue { get; set; }
        public double? HoldMinValue { get; set; }
        public double? RejMaxValue { get; set; }
        public double? RejMinValue { get; set; }
  
     
        //public string ParmCode { get; set; }
        //public string ParmName { get; set; }
        //public string ParmType { get; set; }

    }
}