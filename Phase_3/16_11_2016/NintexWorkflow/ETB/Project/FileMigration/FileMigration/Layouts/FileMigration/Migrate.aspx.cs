using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net;
using System.Data;
using System.Data.OleDb;
//using FileMigration.XLService;
//using SP = Microsoft.SharePoint.Client;
//using Microsoft.Office.Interop.Excel;
//using System.Reflection;
//using System.Runtime.InteropServices;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace FileMigration.Layouts.FileMigration
{
    public partial class Migrate : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SPWeb currentWeb = SPContext.Current.Web;
                lblMsg.Text ="";
                //this.PageTitleLabel.Text = currentWeb.Title + " Contact Properties";

                //this.CreateWebPropertyIfNotExists(currentWeb, "client_name");
                //this.CreateWebPropertyIfNotExists(currentWeb, "client_address");
                //this.CreateWebPropertyIfNotExists(currentWeb, "client_email");
                //this.CreateWebPropertyIfNotExists(currentWeb, "client_phone");
                //this.CreateWebPropertyIfNotExists(currentWeb, "client_mobile");

                //this.PopulateContactValues(currentWeb);
            }

        }
        public Results GetItemInfo(string baseStockCode, string stockCode)
        {
            List<SharePointListItem> posts = new List<SharePointListItem>();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SPContext.Current.Web.Url.ToString()+"/ETBAPI/ETBDataService.svc/Product?$select=BasicSKU,Brand,ProductManager,Segment&$filter=startswith(BasicSKU,'" + baseStockCode + "') eq true and startswith(StockCode,'" + stockCode + "') eq true& distinct=true");
            request.Method = "GET";
            request.Accept = "application/json;odata=verbose";
            request.ContentType = "application/json;odata=verbose";
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Data data = null;
            object obj = null;
            Results result = null;

            // Read the returned posts into an object that can be consumed by the calling application
            using (response)
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    try
                    {
                        string jSON = reader.ReadToEnd();
                        result = serializer.Deserialize<Results>(jSON);
                        //Dictionary<string, object> result = (serializer.DeserializeObject(jSON) as Dictionary<string, object>);
                        //var jsonResult = result["d"];

                        //var Brand = jsonResult["value"] as Dictionary<string,object>;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("An error occurred when reading the list items from SharePoint: {0}; {1}", ex.Message, ex.StackTrace));
                    }
                }
            }
            //foreach (SharePointListItem post in data.d.results)
            //foreach (var post in (Data)obj)
            {
                //posts.Add(post);
            }
            return result;
        }



        protected void UpdateButton_Click(object sender, EventArgs e)
        {}
        private void MoveFileInArtwork()
        {

           // string siteUrl = "https://stg-sp-04.etbrowne.com/";
            string siteUrl = SPContext.Current.Web.Url.ToString();
            //string fileName = "F1515.txt";
            string sourceDirectory = "/Artwork Document";
            string destinationDirectory = "/TestArtworkUpload";
            using (SPSite currSite = new SPSite(siteUrl))
            {
                using (SPWeb currWeb = currSite.OpenWeb())
                {

                    SPList oDocumentLibrary = currWeb.Lists["Artwork Excel"];

                    SPListItemCollection collListItems = oDocumentLibrary.Items;

                    foreach (SPListItem oListItem in collListItems)
                    {
                        if (Convert.ToBoolean(oListItem["IsUpload"].ToString()) == false)
                        {
                            string fileName = oListItem["FileName"].ToString();
                            string stockCode = oListItem["StockCode"].ToString();
                            string baseStockCode = oListItem["BaseStockCode"].ToString();
                            Results loResult = GetItemInfo(baseStockCode, stockCode);
                            if (MoveFile(currWeb, sourceDirectory, destinationDirectory, fileName, loResult, oListItem))
                            {
                                currWeb.AllowUnsafeUpdates = true;
                                oListItem["IsUpload"] = true;
                                oListItem.Update();
                                currWeb.AllowUnsafeUpdates = false;
                            }
                        }

                    }


                }
            }


        }
        public void FileMove()
        {
            string sourceDirectory = "Artwork Document";
           // string destinationDirectory = "/TestArtworkUpload";
            using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url.ToString()))
            {
                using (SPWeb oSPWeb = oSPsite.OpenWeb())
                {
                    oSPWeb.AllowUnsafeUpdates = true;

                    // Fetch the List
                    SPDocumentLibrary spDocArtworkLib =(SPDocumentLibrary)oSPWeb.Lists[sourceDirectory];
                    SPListItemCollection collListItems = spDocArtworkLib.Items;

                    foreach (SPListItem oListItem in collListItems)
                    {
                        SPFile spFile = oListItem.File;
                        string fileName = spFile.Name; //"B3238-A106 COF FaceOil ClearLabel OL.ai";
                        string artBoardNumber = fileName.Substring(0, fileName.IndexOf(' '));
                        string artworkName = artBoardNumber.Substring(0, artBoardNumber.IndexOf('-'));
                        string baseStockCode = artworkName.Substring(artworkName.IndexOfAny("0123456789".ToCharArray()));
                        string stockCode  = "1515";
                        Results loResult = GetItemInfo(baseStockCode, stockCode);

                        //object modifiedOn = spFile.Item["Modified"];
                        //object modifiedBy = spFile.Item["Modified By"];
                        ////true - replace if file exists
                        //spFile.MoveTo(destinationDirectory + "/" + fileName, true);
                        //SPFile dstFile = sourceWeb.GetFile(destinationDirectory + "/" + fileName);
                        //SPListItem dstItem = (SPListItem)dstFile.Item;
                        //dstItem["Name"] = oListItem["FileName"];
                        //dstItem["Base Stock Code"] = oListItem["BaseStockCode"];
                        //dstItem["Stock Code"] = oListItem["StockCode"];
                        //dstItem["Artboard Number"] = oListItem["ArtboardNumber"];
                        //dstItem["Buyer"] = oListItem["Buyer"];
                        //dstItem["Brand"] = loResult.d[0].Brand;
                        //dstItem["Product Manager"] = loResult.d[0].ProductManager;
                        //dstItem["Segment"] = loResult.d[0].Segment;
                        //dstItem["Description"] = "Through Migration";
                        //// dstItem["Approval Status"] = "Approved";
                        //dstItem.Update();
                        //dstFile.Publish("Through Migration");
                        //dstFile.Approve("Through Migration");

                    }

                    oSPWeb.AllowUnsafeUpdates =false;
                }
            }
        }
        public bool MoveFile(SPWeb sourceWeb, string sourceDirectory, string destinationDirectory, string fileName, Results foResult, SPListItem foListItem)
        {
            bool isUpload = false;
            SPFile sourcefile = sourceWeb.GetFile(sourceDirectory + "/" + fileName);
            if (sourcefile.Exists)
            {
                object modifiedOn = sourcefile.Item["Modified"];
                object modifiedBy = sourcefile.Item["Modified By"];
                //true - replace if file exists
                sourcefile.MoveTo(destinationDirectory + "/" + fileName, true);
                SPFile dstFile = sourceWeb.GetFile(destinationDirectory + "/" + fileName);
                SPListItem dstItem = (SPListItem)dstFile.Item;
                dstItem["Name"] = foListItem["FileName"];
                dstItem["Base Stock Code"] = foListItem["BaseStockCode"];
                dstItem["Stock Code"] = foListItem["StockCode"];
                dstItem["Artboard Number"] = foListItem["ArtboardNumber"];
                dstItem["Buyer"] = foListItem["Buyer"];
                dstItem["Brand"] = foResult.d[0].Brand;
                dstItem["Product Manager"] = foResult.d[0].ProductManager;
                dstItem["Segment"] = foResult.d[0].Segment;
                dstItem["Description"] = "Through Migration";
               // dstItem["Approval Status"] = "Approved";
                dstItem.Update();
                dstFile.Publish("Through Migration");
                dstFile.Approve("Through Migration");
                //dstItem.ParentList.Fields["Modified"].ReadOnlyField = false;
                //dstItem.ParentList.Fields["Modified By"].ReadOnlyField = false;
               // dstItem["Modified"] = modifiedOn;
               // dstItem["Modified By"] = modifiedBy;
                //updates the item without creating another version of the item
               // dstItem.UpdateOverwriteVersion();
                //dstItem.ParentList.Fields["Modified"].ReadOnlyField = true;
                //dstItem.ParentList.Fields["Modified By"].ReadOnlyField = true;
                
                isUpload = true;
            }
            return isUpload;
        }

        protected void UploadLocalFile_Click(object sender, EventArgs e)
        {}
        private void UploadFromLocal()
        {
           // string sharePointSite = "https://stg-sp-04.etbrowne.com/";
            string sharePointSite = SPContext.Current.Web.Url.ToString();
            string documentLibraryName = "TestArtwork";
            using (SPSite oSite = new SPSite(sharePointSite))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    string[] filePaths = Directory.GetFiles(@"\\stg-sql-01\ArtworkFile");
                   // string[] filePaths = Directory.GetFiles(LocationTextBox.Text);
                    foreach (string lstFilPath in filePaths)
                    {
                        if (!System.IO.File.Exists(lstFilPath))
                            throw new FileNotFoundException("File not found.", lstFilPath);

                        SPFolder myLibrary = oWeb.Folders[documentLibraryName];

                        // Prepare to upload
                        Boolean replaceExistingFiles = true;
                        String fileName = System.IO.Path.GetFileName(lstFilPath);
                        FileStream fileStream = File.OpenRead(lstFilPath);

                        // Upload document
                        SPFile spfile = myLibrary.Files.Add(fileName, fileStream, replaceExistingFiles);

                        // Commit 
                        myLibrary.Update();
                    }
                }
            }
        }

        //protected void UpdateButton_Click(object sender, EventArgs e)
        //{
        //    SPWeb currentWeb = SPContext.Current.Web;
        //    String fileToUpload = @"D:\test\F1515.txt";
        //    String sharePointSite = "https://stg-sp-04.etbrowne.com/";
        //    String documentLibraryName = "test1";

        //    using (SPSite oSite = new SPSite(sharePointSite))
        //    {
        //        using (SPWeb oWeb = oSite.OpenWeb())
        //        {
        //            if (MetaFile.HasFile)
        //            {
        //                try
        //                {
        //                    string FileName = DateTime.Now.Ticks+"_"+ Path.GetFileName(MetaFile.PostedFile.FileName);

        //                    string Extension = Path.GetExtension(MetaFile.PostedFile.FileName);

        //                    string FolderPath = Server.MapPath("~/File/");

        //                    string FilePath = Server.MapPath(FolderPath + FileName);

        //                    MetaFile.SaveAs(FilePath);

        //                   // Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            }

        //            //if (!System.IO.File.Exists(fileToUpload))
        //            //    throw new FileNotFoundException("File not found.", fileToUpload);

        //            //SPFolder myLibrary = oWeb.Folders[documentLibraryName];

        //            //// Prepare to upload
        //            //Boolean replaceExistingFiles = true;
        //            //String fileName = System.IO.Path.GetFileName(fileToUpload);
        //            //FileStream fileStream = File.OpenRead(fileToUpload);

        //            //// Upload document
        //            //SPFile spfile = myLibrary.Files.Add(fileName, fileStream, replaceExistingFiles);

        //            //// Commit 
        //            //myLibrary.Update();
        //        }
        //    }
        //}

        //private static void ReadExcelFile(string strFileURL, string strSheetName, string strRange)
        //{

        //    //excel.Visible = false;
        //    //string workbookPath = url;
        //    //Workbook excelWorkbook = excel.Workbooks.Open(workbookPath, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
        //    //Sheets sheets = excelWorkbook.Worksheets;
        //    //Worksheet worksheet = (Worksheet)sheets.get_Item(1);
        //    //bool flag = false; ;
        //    //for (int i = 1; i <= worksheet.Rows.Count; i++)
        //    //{
        //    //    Range range = worksheet.get_Range("A" + i.ToString(), "I" + i.ToString());
        //    //    Array myvalues = (System.Array)range.Cells.Value2;
        //    //    string[] strArray = ConvertToStringArray(myvalues);
        //    //}



        //    Microsoft.Office.Interop.Excel._Worksheet excelSheet = null;
        //    Microsoft.Office.Interop.Excel.Range range;
        //    Microsoft.Office.Interop.Excel.Application excelApp;
        //    Microsoft.Office.Interop.Excel.Workbook excelBook;
        //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = new Worksheet();
        //    try
        //    {
        //        excelApp = new Microsoft.Office.Interop.Excel.Application();

        //        //Set Excel file Path from local machine
        //        excelBook = excelApp.Workbooks.Open(@"D:\test\MetaFile.xlsx", true, true, true, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, false, true, false);

        //        excelSheet = (Microsoft.Office.Interop.Excel._Worksheet)excelBook.Worksheets.get_Item(1);
        //        excelApp.Visible = false;
        //        excelApp.UserControl = false;

        //        range = excelSheet.UsedRange;
        //        Object[,] saRet = (System.Object[,])range.get_Value(Missing.Value);
        //        long iRows = saRet.GetUpperBound(0);
        //        long iCols = saRet.GetUpperBound(1);
        //        for (long rowCounter = 2; rowCounter <= iRows; rowCounter++)
        //        {

        //            //InsertToSPList(string.IsNullOrEmpty(Convert.ToString(saRet[rowCounter, 1])) ? "" : saRet[rowCounter, 1].ToString(),
        //            //string.IsNullOrEmpty(Convert.ToString(saRet[rowCounter, 2])) ? "" : saRet[rowCounter, 2].ToString(),
        //            //string.IsNullOrEmpty(Convert.ToString(saRet[rowCounter, 3])) ? "" : saRet[rowCounter, 3].ToString());
        //        }

        //        excelBook.Close(false, "MetaFile.xlsx", null);
        //        Marshal.ReleaseComObject(excelBook);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadLine();
        //    }
        //    //try
        //    //{
        //    //    ExcelService objXL = new ExcelService();
        //    //    objXL.Url = "https://stg-sp-04.etbrowne.com/_vti_bin/excelservice.asmx";
        //    //    objXL.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        //    //    XLService.Status[] stati;
        //    //    string sessionId = objXL.OpenWorkbook(strFileURL,
        //    //      String.Empty, String.Empty, out stati);
        //    //    object[] rangeResults = objXL.GetRangeA1(sessionId, strSheetName, strRange, true, out stati);
        //    //    foreach (object[] rangeResult in rangeResults)
        //    //    {
        //    //        for (int idx = 0; idx < rangeResult.Length; idx++)
        //    //            Console.Write(Convert.ToString(rangeResult[idx]));
        //    //    }
        //    //    objXL.CloseWorkbook(sessionId);
        //    //    Console.Read();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //}

        protected DataTable ImportExcel(string strFileURL)
        {
            //Save the uploaded Excel file.
            string filePath = strFileURL;
           // FileUpload1.SaveAs(filePath);
            
            //Open the Excel file in Read Mode using OpenXml.
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
            {
                //Read the first Sheet from Excel file.
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                //Get the Worksheet instance.
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                foreach (Row row in rows)
                {
                    //Use the first row to add columns to DataTable.
                    if (row.RowIndex.Value == 1)
                    {
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dt.Columns.Add(GetValue(doc, cell));
                        }
                    }
                    else
                    {
                        //Add rows to DataTable.
                        //dt.Rows.Add();
                        int i = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                       // for (int index = 0; index < 5; index++)
                        {
                          //  IEnumerable<Cell> cells = row.ChildElements[index].Descendants<Cell>();
                          // Cell cell = cells.;
                            //Cell cell = row .c.c row.Descendants. row.Descendants<Cell>();
                            if (cell.CellValue != null)
                            {
                                if (i == 0)
                                {
                                    dt.Rows.Add();
                                }
                                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                            }
                            i++;
                        }
                    }
                }

                //DataTable dt2 = dt;
                return dt;
               // GridView1.DataSource = dt;
              //  GridView1.DataBind();
            }
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
        protected void MetaFileUploadButton_Click(object sender, EventArgs e)
        {
            //FileMove();

            string FolderPath = "D:/Row Data/MetaFile/";
            lblMsg.Text = "File is processing ...";
            MetaFileUpload.Visible = false;
            //UploadFromLocal();
            if (MetaFile.HasFile)
            {
                try
                {
                    string FileName = DateTime.Now.Ticks + "_" + Path.GetFileName(MetaFile.PostedFile.FileName);

                    string Extension = Path.GetExtension(MetaFile.PostedFile.FileName);

                    bool folderExists = Directory.Exists(FolderPath);
                    if (!folderExists)
                        Directory.CreateDirectory(FolderPath);



                    string FilePath = FolderPath + FileName;

                    MetaFile.SaveAs(FilePath);

                    // Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
                    //DataTable dtMetadata= ReadExcel(FilePath, Extension);

                  //  string filePath = "https://stg-sp-04.etbrowne.com/TestArtwork/MetaFile.xlsx";
                  //  ReadExcelFile(filePath, "Sheet1", "A1:D3");
                   DataTable dtMetadata = ImportExcel(FilePath);
                    
                    // Insert Recode in list.
                   using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url.ToString()))
                   {
                       using (SPWeb oSPWeb = oSPsite.OpenWeb())
                       {
                           oSPWeb.AllowUnsafeUpdates = true;

                           // Fetch the List
                           SPList list = oSPWeb.Lists["Artwork Excel"];


                          // string title = properties.AfterProperties["Title"].ToString();
                           


                           //Add a new item in the List
                           foreach (DataRow dr in dtMetadata.Rows)
                           {
                               string strQuery = "<Where><Eq><FieldRef Name='FileName' /><Value Type='Text'>" + dr["FileName"].ToString() + "</Value></Eq></Where>";
                               SPQuery query = new SPQuery();
                               query.Query = strQuery;
                               SPListItemCollection itemToUpdate = list.GetItems(query);
                               if (itemToUpdate.Count > 0)
                               {
                                   // Update the List item by ID

                                   // SPListItem itemToUpdate = list.GetItemById(listItemId);
                                   foreach (SPListItem losoList in itemToUpdate)
                                   {
                                       losoList["Title"] = "b";
                                       losoList["FileName"] = dr["FileName"].ToString();
                                       losoList["StockCode"] = dr["Stock Code"].ToString();
                                       losoList["BaseStockCode"] = dr["Base Stock Code"].ToString();
                                       losoList["ArtboardNumber"] = dr["Artboard Number"].ToString();
                                       losoList["Buyer"] = dr["Buyer"].ToString();
                                       losoList["IsUpload"] = false;
                                       losoList.Update();
                                   }
                               }
                               else
                               {

                                   SPListItem itemToAdd = list.Items.Add();
                                   itemToAdd["Title"] = "b";
                                   itemToAdd["FileName"] = dr["FileName"].ToString();
                                   itemToAdd["StockCode"] = dr["Stock Code"].ToString();
                                   itemToAdd["BaseStockCode"] = dr["Base Stock Code"].ToString();
                                   itemToAdd["ArtboardNumber"] = dr["Artboard Number"].ToString();
                                   itemToAdd["Buyer"] = dr["Buyer"].ToString();
                                   itemToAdd["IsUpload"] = false;

                                   itemToAdd.Update();
                               }
                           }
                           

                           // Get the Item ID
                          // listItemId = itemToAdd.ID;

                           // Update the List item by ID
                           //SPListItem itemToUpdate = list.GetItemById(listItemId);
                           //itemToUpdate["Description"] = "Changed Description";
                           //itemToUpdate.Update();

                           // Delete List item
                           //SPListItem itemToDelete = list.GetItemById(listItemId);
                           //itemToDelete.Delete();

                           oSPWeb.AllowUnsafeUpdates = false;
                       }
                   }

                  // UploadFromLocal();
                   MoveFileInArtwork();
                   lblMsg.Text = "File Migrate Successfully.";
                   MetaFileUpload.Visible = true;
                   MetaFile.PostedFile.InputStream.Dispose();


                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    MetaFileUpload.Visible = true;
                    MetaFile.PostedFile.InputStream.Dispose();
                }
            }

        }

        // public DataTable ReadExcel(string fileName, string fileExt)
        // {


        //string conn = string.Empty;
        //DataTable dtexcel = new DataTable();
        //if (Path.GetExtension(fileName).ToLower().Trim() == ".xls" && Environment.Is64BitOperatingSystem == false)
        //    conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //else
        //    conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

        ////if (fileExt.CompareTo(".xls") == 0)
        ////    conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
        ////else
        ////    conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
        //using (OleDbConnection con = new OleDbConnection(conn))
        //{
        //    try
        //    {
        //        OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
        //        oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
        //    }
        //    catch (Exception ex) { }
        //}
        //return dtexcel;
        // }

    }
    public class Data
    {
        public Results d { get; set; }
    }

    public class Results
    {
        public SharePointListItem[] d { get; set; }
    }

    public class SharePointListItem
    {
        public JsonMetaData __metadata { get; set; }
        public string Brand { get; set; }
        public string BasicSKU { get; set; }
        public string ProductManager { get; set; }
        public string Segment { get; set; }
    }
    public class JsonMetaData
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
}
