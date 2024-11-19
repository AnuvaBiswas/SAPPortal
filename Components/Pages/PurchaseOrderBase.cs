using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using SAPPortal.Components.Services;
using Microsoft.IdentityModel.Tokens;
using SAPbobsCOM;
using SAPPortal.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.JSInterop;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;



namespace SAPPortal.Components.Pages
{
    public class PurchaseOrderBase : ComponentBase
    {

        [Inject]
        public AuthenticationService? authenticationService { get; set; }

        protected bool isLoading = false;
        [Inject]
        public IJSRuntime? JsRuntime { get; set; }
        [Inject]
        public SAPConnection? AppConfig { get; set; }

        [Inject]
        NavigationManager? NavigationManager { get; set; }

        protected bool isModalVisible = false;
        protected bool IsModelOpenVisible_ForPOItemDetails = false;
        protected VendorModel selectedVendor = new VendorModel();  // Initialize selectedValue to avoid null reference
        public string searchTerm = string.Empty;

        protected List<VendorModel> filteredVendor = new List<VendorModel>();
        protected VendorModel? highlightedItem;

        public List<VendorModel>? VendorList = new List<VendorModel>();
        public List<PO_Details_Model>? Open_PO_details = new List<PO_Details_Model>();
        public PO_Details_Model? Selected_PO_ItemDetails = new PO_Details_Model();
        public List<Item_Parameter_Details_Model>? POItemParameterDetailsList = new List<Item_Parameter_Details_Model>();
        //private List<Item_Parameter_Details_Model> TempEditedItemsList = new List<Item_Parameter_Details_Model>();

        public List<POItemDetailsModel> POItemDetailsList = new List<POItemDetailsModel>();
        public POItemDetailsModel SelectedItemDetails = new POItemDetailsModel();
        public string? ReloadOpenVendor { get; set; }
        public string? AutoCode { get; set; }
        public string selectedDocEntry { get; set; } // Store the selected document entry

        // public AuthenticationModel AuthencicationPermiction = new AuthenticationModel();



        protected override async Task OnInitializedAsync()
        {


            //await GetVendor();
            //filteredVendor = VendorList;
            if ((CommonSessionClass.UName) == String.Empty || CommonSessionClass.UName == null)
            {
                NavigationManager.NavigateTo("/loginPage", true);
            }
            else
            {
                isLoading = true;
                //await Task.Delay(1000);
                await GetVendor();
                isLoading = false;

                //await GetPermission();
                base.OnInitializedAsync();

            }
        }

        protected void FilterItems(ChangeEventArgs e)
        {
            searchTerm = e.Value?.ToString() ?? string.Empty;

            filteredVendor = VendorList!
                .Where(v =>
                v.CardCode!.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                v.CardName!.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Auto-select the first matching item
            if (filteredVendor.Count > 0)
            {
                highlightedItem = filteredVendor[0];
            }
        }

        protected async Task OpenModal()
        {

            isModalVisible = true;

        }
        protected async void SelectItem(VendorModel item)
        {
            //isLoading = true;
            //await Task.Delay(2000);
            CloseModal();
            selectedVendor = item;
            ReloadOpenVendor = item.CardCode;
            await Get_Open_PO(item.CardCode!);
            //isLoading = false;
        }

        protected async Task Select_PO_Details(PO_Details_Model POItemDetails)
        {
            //isLoading = true;
            //await Task.Delay(1000);
            Selected_PO_ItemDetails = POItemDetails;
            selectedDocEntry = POItemDetails.DocEntry!; // Set selected document entry
            await Get_POItem_Details(POItemDetails.DocEntry!);
            //await Get_POItem_ParameterDetails(POItemDetails.DocEntry!);
            //isLoading = false;
        }

        protected async Task Select_POItem_Details(POItemDetailsModel PoItemDetails)
        {
            IsModelOpenVisible_ForPOItemDetails = true;
            SelectedItemDetails = PoItemDetails;
            await Get_POItem_ParameterDetails(PoItemDetails.DocEntry!, PoItemDetails.ItemCode!, PoItemDetails.LineNum!, PoItemDetails.PostingDate.ToString("yyyyMMdd"));
        }

        protected void CloseModal()
        {
            isModalVisible = false;
            IsModelOpenVisible_ForPOItemDetails = false;
        }

        protected void ClearSearchTerm()
        {
            searchTerm = string.Empty;
            filteredVendor = VendorList!; // Reset the filtered list to all items
        }

        protected void HandleKeyDown(KeyboardEventArgs e)
        {
            // Check if Enter or Tab is pressed
            if (e.Key == "Enter" || e.Key == "Tab")
            {
                // Select the highlighted item
                SelectItem(highlightedItem!);
            }
        }
        public async Task GetVendor()
        {
            if (AppConfig.diCompany == null)
            {
                try
                {
                    AppConfig.diCompany = AppConfig.SAPDIConnection();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (AppConfig.diCompany != null)
            {
                try
                {
                    SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    string strSql = "Select CardCode,CardName From OCRD where CardType='S' and validFor='Y'";
                    oRs.DoQuery(strSql);
                    if (oRs.RecordCount > 0)
                    {
                        VendorList!.Clear();

                        while (!oRs.EoF)
                        {

                            var option = new VendorModel
                            {
                                CardCode = oRs.Fields.Item("CardCode").Value.ToString(),
                                CardName = oRs.Fields.Item("CardName").Value.ToString()

                            };

                            VendorList.Add(option);
                            oRs.MoveNext();
                        }
                    }
                    filteredVendor = VendorList.ToList();


                }
                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    AppConfig.SAPDIDisconnect();
                    isLoading = false;

                }


            }

        }
        public async Task Get_Open_PO(string CardCode)
        {

            if (AppConfig.diCompany == null)
            {
                try
                {
                    AppConfig.diCompany = AppConfig.SAPDIConnection();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (AppConfig.diCompany != null)
            {
                try
                {
                    SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    string procedute = "exec TRIZ_QC_GetOpenPO @CardCode='" + CardCode + "'";

                    oRs.DoQuery(procedute);
                    if (oRs.RecordCount > 0)
                    {
                        Open_PO_details!.Clear();

                        while (!oRs.EoF)
                        {
                            var option = new PO_Details_Model
                            {
                                DocEntry = oRs.Fields.Item("DocEntry").Value.ToString(),
                                DocNumer = oRs.Fields.Item("DocNumer").Value.ToString(),
                                VendorCode = oRs.Fields.Item("VendorCode").Value.ToString(),
                                VendorName = oRs.Fields.Item("VendorName").Value.ToString(),
                                PostingDate = DateTime.Parse(oRs.Fields.Item("PostingDate").Value.ToString()),
                                TotalAmount = oRs.Fields.Item("TotalAmount").Value.ToString(),
                                ContactPerson = oRs.Fields.Item("ContactPerson").Value.ToString(),
                                VendorRefNo = oRs.Fields.Item("VendorRefNo").Value.ToString(),
                                Currency = oRs.Fields.Item("Currency").Value.ToString(),
                                ParameterTaged = oRs.Fields.Item("ParameterTaged").Value.ToString(),
                                DeliveryDate = DateTime.Parse(oRs.Fields.Item("DeliveryDate").Value.ToString()),
                                DocumentDate = DateTime.Parse(oRs.Fields.Item("DocumentDate").Value.ToString())
                            };

                            Open_PO_details!.Add(option);
                            oRs.MoveNext();

                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
                finally
                {
                    AppConfig.SAPDIDisconnect();


                }


            }

        }

        public async Task Get_POItem_Details(string DocEntry)
        {

            if (AppConfig!.diCompany == null)
            {
                try
                {
                    AppConfig.diCompany = AppConfig.SAPDIConnection();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (AppConfig.diCompany != null)
            {
                try
                {
                    SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    string procedute = "exec Triz_QC_GetPOItemDetails @DocEntry='" + DocEntry + "'";

                    oRs.DoQuery(procedute);
                    if (oRs.RecordCount > 0)
                    {
                        POItemDetailsList!.Clear();

                        while (!oRs.EoF)
                        {

                            var option = new POItemDetailsModel
                            {
                                DocEntry = oRs.Fields.Item("DocEntry").Value.ToString(),
                                LineNum = oRs.Fields.Item("LineNum").Value.ToString(),
                                DocNum = oRs.Fields.Item("DocNum").Value.ToString(),
                                ItemCode = oRs.Fields.Item("ItemCode").Value.ToString(),
                                ItemDescription = oRs.Fields.Item("ItemDescription").Value.ToString(),
                                Quantity = oRs.Fields.Item("Quantity").Value.ToString(),
                                Price = oRs.Fields.Item("Price").Value.ToString(),
                                TaxCode = oRs.Fields.Item("TaxCode").Value.ToString(),
                                WarehouseCode = oRs.Fields.Item("WarehouseCode").Value.ToString(),
                                PostingDate = (DateTime)oRs.Fields.Item("PostingDate").Value
                            };

                            POItemDetailsList!.Add(option);
                            oRs.MoveNext();

                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
                finally
                {
                    AppConfig.SAPDIDisconnect();


                }


            }

        }

        public async Task Get_POItem_ParameterDetails(string DocEntry, string ItemCode, string LineNum, string postingDate)
        {

            if (AppConfig.diCompany == null)
            {
                try
                {
                    AppConfig.diCompany = AppConfig.SAPDIConnection();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (AppConfig.diCompany != null)
            {
                try
                {
                    SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                    string procedute = "exec Triz_QC_GetPOItemParam @DocEntry='" + DocEntry + "',@ItemCode='" + ItemCode + "',@Linenum='" + LineNum + "',@PODate='" + postingDate + "' ";

                    oRs.DoQuery(procedute);
                    POItemParameterDetailsList!.Clear();
                    if (oRs.RecordCount > 0)
                    {
                        //POItemParameterDetailsList!.Clear();

                        while (!oRs.EoF)
                        {

                            var option = new Item_Parameter_Details_Model
                            {
                                DocEntry = oRs.Fields.Item("DocEntry").Value.ToString(),
                                DocNum = oRs.Fields.Item("DocNum").Value.ToString(),
                                LineNum = oRs.Fields.Item("LineNum").Value.ToString(),
                                ItemCode = oRs.Fields.Item("ItemCode").Value.ToString(),
                                ItemDescription = oRs.Fields.Item("ItemDescription").Value.ToString(),
                                ProcessStage = oRs.Fields.Item("ProcessStage").Value.ToString(),
                                ProcessStageName = oRs.Fields.Item("ProcessStageName").Value.ToString(),
                                ParmCode = oRs.Fields.Item("ParmCode").Value.ToString(),
                                ParmDesc = oRs.Fields.Item("ParmDesc").Value.ToString(),
                                ParmType = oRs.Fields.Item("ParmType").Value.ToString(),
                                Sequence = oRs.Fields.Item("Sequence").Value.ToString(),
                                Mandatory = oRs.Fields.Item("Mandatory").Value.ToString(),
                                StandValue = oRs.Fields.Item("StandValue").Value.ToString(),
                                AcceptMaxValue = oRs.Fields.Item("AcceptMaxValue").Value.ToString(),
                                AcceptMinValue = oRs.Fields.Item("AcceptMinValue").Value.ToString(),
                                HoldMaxValue = oRs.Fields.Item("HoldMaxValue").Value.ToString(),
                                HoldMinValue = oRs.Fields.Item("HoldMinValue").Value.ToString(),
                                RejMaxValue = oRs.Fields.Item("RejMaxValue").Value.ToString(),
                                RejMinValue = oRs.Fields.Item("RejMinValue").Value.ToString(),
                                ShowOnPrint = oRs.Fields.Item("ShowOnPrint").Value.ToString(),
                                //AfterBeforeGRN = oRs.Fields.Item("AfterBeforeGRN").Value.ToString(),
                                //ShowOnPO = oRs.Fields.Item("ShowOnPO").Value.ToString(),
                            };

                            POItemParameterDetailsList!.Add(option);
                            oRs.MoveNext();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    AppConfig.SAPDIDisconnect();

                }
            }
        }
        // Edit a row
        protected void EditRow(Item_Parameter_Details_Model item)
        {
            //item.IsEdit = true;
            // Backup original values
            item.OriginalValues = new Item_Parameter_Details_Model
            {
                ItemCode = item.ItemCode,
                ItemDescription = item.ItemDescription,
                ProcessStage = item.ProcessStage,
                ParmCode = item.ParmCode,
                ParmDesc = item.ParmDesc,
                ParmType = item.ParmType,
                Sequence = item.Sequence,
                Mandatory = item.Mandatory,
                StandValue = item.StandValue,
                AcceptMaxValue = item.AcceptMaxValue,
                AcceptMinValue = item.AcceptMinValue,
                HoldMaxValue = item.HoldMaxValue,
                HoldMinValue = item.HoldMinValue,
                RejMaxValue = item.RejMaxValue,
                RejMinValue = item.RejMinValue
            };

            // Enable edit mode

            //OriginalValues = item;
            item.IsEdit = true;
        }
        // Save edited row
        protected void SaveEdit(Item_Parameter_Details_Model item)
        {
            // Find the original item in the POItemParameterDetailsList
            //var originalItem = POItemParameterDetailsList.FirstOrDefault(x =>
            //    x.ItemCode == item.ItemCode &&
            //    x.ParmCode == item.ParmCode &&
            //    x.ProcessStage == item.ProcessStage &&
            //    x.ItemDescription == item.ItemDescription);

            //// If the item is found, update its properties
            //if (originalItem != null)
            //{
            //    originalItem.Sequence = item.Sequence; // Update Sequence
            //    originalItem.Required = item.Required; // Update Required
            //    originalItem.StandValue = item.StandValue; // Update StandValue
            //    originalItem.AcceptMaxValue = item.AcceptMaxValue; // Update AcceptMaxValue
            //    originalItem.AcceptMinValue = item.AcceptMinValue; // Update AcceptMinValue
            //    originalItem.HoldMaxValue = item.HoldMaxValue; // Update HoldMaxValue
            //    originalItem.HoldMinValue = item.HoldMinValue; // Update HoldMinValue
            //    originalItem.RejMaxValue = item.RejMaxValue; // Update RejMaxValue
            //    originalItem.RejMinValue = item.RejMinValue; // Update RejMinValue
            //}

            // Exit edit mode
            item.IsEdit = false;

            //Console.WriteLine($"Temporary saved changes for Item Code: {item.ItemCode}");
        }

        // Cancel edit
        protected void CancelEdit(Item_Parameter_Details_Model item)
        {
            if (item.OriginalValues != null)
            {
                // Restore original values
                item.ItemCode = item.OriginalValues.ItemCode;
                item.ItemDescription = item.OriginalValues.ItemDescription;
                item.ProcessStage = item.OriginalValues.ProcessStage;
                item.ParmCode = item.OriginalValues.ParmCode;
                item.ParmDesc = item.OriginalValues.ParmDesc;
                item.ParmType = item.OriginalValues.ParmType;
                item.Sequence = item.OriginalValues.Sequence;
                item.Mandatory = item.OriginalValues.Mandatory;
                item.StandValue = item.OriginalValues.StandValue;
                item.AcceptMaxValue = item.OriginalValues.AcceptMaxValue;
                item.AcceptMinValue = item.OriginalValues.AcceptMinValue;
                item.HoldMaxValue = item.OriginalValues.HoldMaxValue;
                item.HoldMinValue = item.OriginalValues.HoldMinValue;
                item.RejMaxValue = item.OriginalValues.RejMaxValue;
                item.RejMinValue = item.OriginalValues.RejMinValue;
            }

            // Exit edit mode
            item.IsEdit = false;

            Console.WriteLine($"Edit canceled and original values restored for Item Code: {item.ItemCode}");
        }
        protected void OnRequiredEditChange(ChangeEventArgs e, Item_Parameter_Details_Model item)
        {
            // Convert the checkbox value to 'Y' or 'N'
            item.Mandatory = (bool)e.Value ? "Y" : "N";

        }



        protected async Task Save_PO_Item(string ReloadOpenVendor, string DocEntry)
        {
            isLoading = true;
            await Task.Delay(2000);

            var SavePermesion = authenticationService!.AuthenticationList.Where(x => x.MenuId == 6 && x.ADD == "Y").ToList();
            //AuthencicationPermiction=itemToSave

            if (SavePermesion.Any())
            {
                if (AppConfig!.diCompany == null)
                {
                    try
                    {
                        AppConfig.diCompany = AppConfig.SAPDIConnection();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }

                if (AppConfig.diCompany != null)
                {
                    try
                    {
                        SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                        string DeleteQuery = "Delete from [@TRIZ_QC_POR2] where U_PODocEntry= '" + DocEntry + "'";

                        oRs.DoQuery(DeleteQuery);


                        string procedute = "exec Triz_QC_SavePOItemParam1 @DocEntry='" + DocEntry + "'";

                        oRs.DoQuery(procedute);
                        await JsRuntime!.InvokeVoidAsync("alert", "Data Saved successfully");
                        Selected_PO_ItemDetails = new PO_Details_Model();
                        POItemParameterDetailsList!.Clear();
                        POItemDetailsList!.Clear();
                        selectedDocEntry = null;
                        await Get_Open_PO(ReloadOpenVendor!);
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception();
                        string sapError = AppConfig.diCompany.GetLastErrorDescription();

                        await JsRuntime!.InvokeVoidAsync("alert", "Error Occured" + ex.Message + sapError);
                    }
                    finally
                    {
                        isLoading = false;
                        AppConfig.SAPDIDisconnect();


                    }


                }
            }
            else
            {
                isLoading = false;
                await JsRuntime!.InvokeVoidAsync("alert", "Data Save Not Allowed");

            }

        }

        protected async Task Save_PO_ItemDetailsByItemCode(POItemDetailsModel POItemDetail)
        {
            var SavePermesion = authenticationService!.AuthenticationList.Where(x => x.MenuId == 6 && x.ADD == "Y").ToList();

            if (SavePermesion.Any())
            {
                if (AppConfig.diCompany == null)
                {
                    try
                    {
                        AppConfig.diCompany = AppConfig.SAPDIConnection();
                        if (AppConfig.diCompany.Connected == false)
                        {
                            int connectionResult = AppConfig.diCompany.Connect();
                            if (connectionResult != 0)
                            {
                                string errMsg = AppConfig.diCompany.GetLastErrorDescription();
                                throw new Exception("Failed to connect to SAP: " + errMsg);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //errorMessage = ex.Message;
                        return;
                    }
                }
                if (AppConfig.diCompany != null)
                {
                    string AddUpdateFlag = string.Empty;
                    SAPbobsCOM.GeneralService oGeneralService = null;
                    SAPbobsCOM.GeneralData oGeneralData = null;
                    SAPbobsCOM.GeneralData oChild = null;
                    SAPbobsCOM.GeneralDataCollection oChildren = null;
                    SAPbobsCOM.GeneralDataParams oGeneralParams = null;
                    SAPbobsCOM.CompanyService oCompanyService = null;

                    try
                    {

                        SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                        string DeleteQuery = "Delete from [@TRIZ_QC_POR1] where U_PODocEntry= '" + POItemDetail.DocEntry + "' and U_ItemCode='" + POItemDetail.ItemCode + "' and U_LineNum='" + POItemDetail.LineNum + "'";

                        oRs.DoQuery(DeleteQuery);

                        oCompanyService = AppConfig.diCompany.GetCompanyService();
                        oGeneralService = oCompanyService.GetGeneralService("TRIZ_QC_POR1");

                        //oGeneralData = ((SAPbobsCOM.GeneralData)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));

                        foreach (var Item in POItemParameterDetailsList!)
                        {
                            oGeneralData = ((SAPbobsCOM.GeneralData)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));
                            await GetMaxCode();
                            oGeneralData.SetProperty("Code", AutoCode);
                            oGeneralData.SetProperty("U_PODocEntry", Item.DocEntry);
                            oGeneralData.SetProperty("U_PODocNum", Item.DocNum);
                            oGeneralData.SetProperty("U_ProcessStage", Item.ProcessStage);
                            oGeneralData.SetProperty("U_ItemCode", Item.ItemCode);
                            oGeneralData.SetProperty("U_ItemName", Item.ItemDescription);
                            oGeneralData.SetProperty("U_ParmCode", Item.ParmCode);
                            oGeneralData.SetProperty("U_ParmDesc", Item.ParmDesc);
                            if (Item.ParmType == "Logical")
                            {
                                oGeneralData.SetProperty("U_ParmType", "L");
                            }
                            else if (Item.ParmType == "Numeric")
                            {
                                oGeneralData.SetProperty("U_ParmType", "N");
                            }

                            oGeneralData.SetProperty("U_StandValue", Item.StandValue);
                            oGeneralData.SetProperty("U_AcceptMaxValue", Item.AcceptMaxValue);
                            oGeneralData.SetProperty("U_AcceptMinValue", Item.AcceptMinValue);
                            oGeneralData.SetProperty("U_RejMinValue", Item.RejMinValue);
                            oGeneralData.SetProperty("U_RejMaxValue", Item.RejMaxValue);
                            oGeneralData.SetProperty("U_HoldMaxValue", Item.HoldMaxValue);
                            oGeneralData.SetProperty("U_HoldMinValue", Item.HoldMinValue);
                            oGeneralData.SetProperty("U_Mandatory", Item.Mandatory);
                            oGeneralData.SetProperty("U_Sequence", Item.Sequence);
                            oGeneralData.SetProperty("U_ShowOnPrint", Item.ShowOnPrint);
                            //oGeneralData.SetProperty("U_ABGRN", Item.AfterBeforeGRN);
                            //oGeneralData.SetProperty("U_ShowOnPO", Item.ShowOnPO);
                            oGeneralData.SetProperty("U_LineNum", Item.LineNum);

                            oGeneralParams = oGeneralService.Add(oGeneralData);


                        }
                        string Result = System.Convert.ToString(oGeneralParams!.GetProperty("Code"));
                        //await JsRuntime!.InvokeVoidAsync("alert", "Data Saved successfully");
                        //Selected_PO_ItemDetails = new PO_Details_Model();
                        POItemParameterDetailsList.Clear();
                        //await Get_Open_PO(ReloadOpenVendor);
                        await JsRuntime!.InvokeVoidAsync("alert", "Data Update successfully");

                        CloseModal();

                    }
                    catch (Exception ex)
                    {
                        string sapError = AppConfig.diCompany.GetLastErrorDescription();

                        await JsRuntime!.InvokeVoidAsync("alert", "Error Occured" + ex.Message + sapError);
                    }
                    finally
                    {
                        isLoading = false;
                        AppConfig.SAPDIDisconnect();

                    }

                }
            }

        }

        protected async Task GetMaxCode()
        {
            if (AppConfig.diCompany == null)
            {
                try
                {
                    AppConfig.diCompany = AppConfig.SAPDIConnection();
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (AppConfig.diCompany != null)
            {
                SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)AppConfig.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string strSql = "Select Isnull(Max(CAST(Code as INT)),0)+1 as Code From [@TRIZ_QC_POR1]";
                oRs.DoQuery(strSql);
                if (oRs.RecordCount > 0)
                {
                    AutoCode = oRs.Fields.Item("Code").Value.ToString();
                }

                AppConfig.SAPDIDisconnect();
            }
        }


    }
}
