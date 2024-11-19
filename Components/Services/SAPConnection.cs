using SAPPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using SAPbobsCOM;

namespace SAPPortal.Components.Services
{
    public class SAPConnection
    {
        private readonly ApplicationDbContext applicationDbContext;
        public SAPConnection(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        string SAPDataSource = string.Empty;
        string SAPCompanyDB = string.Empty;
        string SAPUserName = string.Empty;
        string SAPPassword = string.Empty;
        string SAPDBUserID = string.Empty;
        string SAPDBPassword = string.Empty;
        string SAPLicenseServer = string.Empty;
        string DBVersion = string.Empty;
        List<AS_SAP_CONFIG> lstSAPConfig = new List<AS_SAP_CONFIG>();


        public SAPbobsCOM.Company? diCompany
        {
            get;
            set;
        }
        public bool SAPDIDisconnect()
        {
            bool ConnectionClosed = false;
            if (diCompany != null)
            {
                if (diCompany.Connected)
                {
                    diCompany.Disconnect();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(diCompany);
                    diCompany = null;
                    GC.Collect();
                    ConnectionClosed = true;
                }
            }
            return ConnectionClosed;
        }
        private Company GetSAPConnectionDtl()
        {
            List<AS_SAP_CONFIG> lstSAPConfig = applicationDbContext.AS_SAP_CONFIGs.ToList();

            // List<AS_SAP_CONFIG> lstSAPConfig1 = GetSAPData();
            if (lstSAPConfig == null || lstSAPConfig.Count() == 0)
            {
                throw new Exception("No SAP configuration found.");
            }
            else
            {
                AS_SAP_CONFIG sapConfig = lstSAPConfig[0];


                SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company
                {
                    DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019,
                    Server = sapConfig.SAPDataSource,
                    language = SAPbobsCOM.BoSuppLangs.ln_English,
                    UseTrusted = false,
                    DbUserName = sapConfig.SAPDBUserID,
                    DbPassword = sapConfig.SAPDBPassword,
                    UserName = sapConfig.SAPUserName,
                    Password = sapConfig.SAPPassword,
                    CompanyDB = sapConfig.SAPCompanyDB,
                    LicenseServer = sapConfig.SAPLicenseServer,
                };
                return oCompany;
            }
        }
        public Company SAPDIConnection()
        {
            diCompany = GetSAPConnectionDtl();
            // Try to connect
            int lRetCode = 0;
            int temp_int = 100;
            string temp_string = "Error";

            if (diCompany.Connected)
            {
                diCompany.Disconnect();

            }
            try
            {
                lRetCode = diCompany.Connect();
                if (lRetCode != 0) // if the connection failed
                {
                    temp_int = -1;
                    temp_string = string.Empty;
                    diCompany.GetLastError(out temp_int, out temp_string);

                }
                else
                {
                    if (diCompany.Connected) // if connected
                    {
                        return diCompany;
                    }
                }
            }
            catch (Exception ex1)
            {
                // Handle exception (logging, rethrowing, etc.)

            }
            return diCompany;
        }
    }
}
