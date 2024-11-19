using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class PO_Details_Model
    {       
        public string? DocEntry { get; set; }
        public string? DocNumer { get; set; }
        public string? PurchaseOrderNumer { get; set; }
        public string? VendorCode { get; set; }        
        public string? VendorName { get; set; }
        public string? ItemCode { get; set; }
        public string? WhsName { get; set; }
        public string? WhsCode { get; set; }
        public string? Project { get; set; }
        public string? ProjectName { get; set; }
        
        public DateTime? PostingDate { get; set; } 
        public string? TotalAmount { get; set; }
        public string? ContactPerson { get; set; }
        public string? VendorRefNo { get; set; }
        public string? Currency { get; set; }
        public string? ParameterTaged { get; set; }       
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DocumentDate { get; set; }
    }
}
