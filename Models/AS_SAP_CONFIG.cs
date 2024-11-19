namespace SAPPortal.Models
{
	public class AS_SAP_CONFIG
	{

		public int Id {  get; set; }
		public string? SAPDataSource { get; set; }
		public string? SAPCompanyDB { get; set; }

		public string? SAPUserName { get; set; }
		public string? SAPPassword { get; set; }

		public string? SAPDBUserID { get; set; }
		public string? SAPDBPassword { get; set; }

		public string? SAPLicenseServer { get; set; }
		public string? DBVersion { get; set; }
		public string? IsActive {  get; set; }

	}
}
