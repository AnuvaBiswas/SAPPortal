namespace SAPPortal.Models
{
    public class IncomingQC
    {
        public int Id {  get; set; }
        public string QCNo { get; set; }
        public DateTime QCDate { get; set; }
        public string QCDate1 { get; set; }
        public string Type { get; set; }
        public string? DocEntry { get; set; }
        public string GatepassNo { get; set; } 
        public string VendorCode { get; set; }
        public string Linenum { get; set; }
        public string VisOrder {  get; set; }
        public string Object { get; set; }
        public int LogInst {  get; set; }
        public string GoodsReceiptPO { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string VendorName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName{ get; set; }
        public string BatchNo { get; set; }
        public string ProcessStage { get; set; }

        public string Project { get; set; }
        public string ProjectName { get; set; }
        public string Warehouse { get; set; }
        public string WarehouseCode { get; set; }
        


        public string Supervisor { get; set; }
        public string Location { get; set; } 
        public string Result { get; set; } 
       
      
        public string Remarks { get; set; }


    

        public List<QCParameter> lstQCParameters { get; set; } = new List<QCParameter>();
      
    }
}