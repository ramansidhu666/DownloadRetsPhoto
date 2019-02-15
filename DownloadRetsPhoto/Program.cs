using librets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DownloadRetsPhoto
{
    class Program
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
        static string Idx_URL = "http://rets.torontomls.net:6103/rets-treb3pv/server/login";
        static string Idx_UserName = "D15ofa";
        static string Idx_Password = "G#n5189";


        static string Vow_URL = "http://rets.torontomls.net:6103/rets-treb3pv/server/login";
        static string Vow_UserName = "V15ofa";
        static string Vow_Password = "N$g1216";

        static int CountOfDownloadedMLS = 0;
        static int CountOfDownloadedPhotos = 0;
        static string PropertyType = "";
        static string XMLRooTTag = "";
        static string ListingPhotoSavePath = "";
        static int MlsIndexNO = 0;
        static void Main(string[] args)
        {
            //GetPhotosForFeatureListing();
            //GetIdxCommercialPhotos();
            GetIdxResidentialPhotos();
            //// GetIdCondoPhotos();
            //GetIdxCondoPhotos();
            //GetVowCommercialPhotos();
            //GetVowResidentialPhotos();
            //GetVowCondoPhotos();

        }

        #region IDX Data


        public static void GetIdxResidentialPhotos()
        {
            PropertyType = "Idx Residential";
            XMLRooTTag = "";
            try
            {
                RetsSession session = new RetsSession(Idx_URL);
                session.SetUserAgent("RETS - Connector / 1.2");
                var path = "";
                path = @"D:\Sample projects\DownloadRetsPhoto\DownloadRetsPhoto\mls\IdxCondo.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathResidential"].ToString();
                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login(Idx_UserName, Idx_Password);
                ConvertXmlFileToList_new(path, session);
                Console.Write("IdxResidential Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public static void GetIdxCommercialPhotos()
        {

            MlsIndexNO = 141;
            PropertyType = "Idx Commercial";
            XMLRooTTag = "REProperties/CommercialProperty";
            ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathCommercial"].ToString();
            try
            {
                RetsSession session = new RetsSession(Idx_URL);
                session.SetUserAgent("RETS - Connector / 1.2");
                var path = "";
                path = @"C:\MlsData\IdximagesCommercial\IDXCommercial.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);

                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login(Idx_UserName, Idx_Password);
                ConvertXmlFileToList_new(path, session);
                Console.Write("IdxCommercial Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public static void GetIdxCondoPhotos()
        {
            PropertyType = "Idx Condo";
            try
            {
                RetsSession session = new RetsSession("http://rets.torontomls.net:6103/rets-treb3pv/server/login");
                session.SetUserAgent("RETS - Connector / 1.2");
                ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathcondo"].ToString();
                var path = @"D:\Sample projects\DownloadRetsPhoto\DownloadRetsPhoto\mls\IdxCondo.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);

                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login("V15ofa", "N$g1216");
                ConvertXmlFileToList_new(path, session);
                Console.Write("IdxCondos Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        #endregion


        #region Vow Data

        public static void GetVowCommercialPhotos()
        {
            PropertyType = "Vow Commercial";
            try
            {
                RetsSession session = new RetsSession(Vow_URL);
                session.SetUserAgent("RETS - Connector / 1.2");

                var path = @"D:\Sample projects\DownloadRetsPhoto\DownloadRetsPhoto\mls\VowCommercial.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathCommercialVOW"].ToString();
                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login(Vow_UserName, Vow_Password);
                ConvertXmlFileToList_new(path, session);
                Console.Write("VowCommercial Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        public static void GetVowResidentialPhotos()
        {
            PropertyType = "Vow Residential";
            try
            {
                RetsSession session = new RetsSession("http://rets.torontomls.net:6103/rets-treb3pv/server/login");
                session.SetUserAgent("RETS - Connector / 1.2");

                var path = @"D:\Sample projects\DownloadRetsPhoto\DownloadRetsPhoto\mls\VowCommercial.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathResidentialVOW"].ToString();
                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login("V15ofa", "N$g1216");
                ConvertXmlFileToList_new(path, session);
                Console.Write("VowResidential Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        public static void GetVowCondoPhotos()
        {
            PropertyType = "Vow Condo";
            try
            {
                RetsSession session = new RetsSession("http://rets.torontomls.net:6103/rets-treb3pv/server/login");
                session.SetUserAgent("RETS - Connector / 1.2");

                var path = @"D:\Sample projects\DownloadRetsPhoto\DownloadRetsPhoto\mls\VowCondo.xml";
                //session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                ListingPhotoSavePath = ConfigurationManager.AppSettings["ListingPhotoSavePathCondoVOW"].ToString();
                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login("V15ofa", "N$g1216");
                ConvertXmlFileToList_new(path, session);
                Console.Write("VowCondos Photos successfully downloaded.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }


        #endregion
        #region Active Property
        public static void ProcessFiles(string path)
        {
            string ErrorMessageUser = "";
            try
            {
                List<string> MLSActiveFileList = new List<string>();
                MLSActiveFileList.Add("ResidentialIDX.txt");
                MLSActiveFileList.Add("Condo_Active.txt");
                MLSActiveFileList.Add("Commercial_Active.txt");

                List<string> MlsAddedList = new List<string>();
                string QStr = "";

                ErrorMessageUser = "Processing file.";
                // WriteLog(ErrorMessageUser);
                Console.WriteLine(Environment.NewLine + ErrorMessageUser + Environment.NewLine + "Please wait....");

                foreach (var MLSActiveFile in MLSActiveFileList)
                {
                    string NewPath = path + "/" + MLSActiveFile;
                    var webRequest = System.Net.WebRequest.Create(NewPath);

                    using (var response = webRequest.GetResponse())
                    using (var content = response.GetResponseStream())
                    using (var reader = new StreamReader(content))
                    {
                        var strContent = reader.ReadToEnd();
                        string[] Lines = strContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in Lines)
                        {
                            string[] MlsId = line.Split(' ');
                            if (MlsId.Length > 0)
                            {
                                string MlsIdMain = MlsId[0].Trim();
                                if (MlsIdMain != "")
                                {
                                    if (!MlsAddedList.Contains(MlsIdMain))
                                    {
                                        QStr += "INSERT INTO [ActiveMLSIDS]([MLSID])VALUES('" + MlsIdMain + "');";
                                        MlsAddedList.Add(MlsIdMain);
                                    }
                                }
                            }
                        }
                    }
                }



                //   CommonClass clsObj = new CommonClass();

                if (MlsAddedList.Count() > 0)
                {
                    //   clsObj.ExecuteNonQuery("TRUNCATE TABLE [ActiveMLSIDS]");
                    //Start Insert Procedure

                    QStr += "DELETE FROM PropertyData where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "DELETE FROM PropertyData_Comm where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "DELETE FROM PropertyData_Comm_VOX where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "DELETE FROM PropertyData_Condo where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "DELETE FROM PropertyData_Condo_Vox where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "DELETE FROM PropertyData_Vox_Residential where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
                    QStr += "EXEC [dbo].[UpdateFeaturedProperties];";

                    ErrorMessageUser = "Insert started.";
                    //WriteLog(ErrorMessageUser);
                    Console.WriteLine(Environment.NewLine + ErrorMessageUser + Environment.NewLine + "Please wait....");

                    // clsObj.ExecuteNonQuery(QStr);

                    ErrorMessageUser = "Insert done.";
                    // WriteLog(ErrorMessageUser);
                    Console.WriteLine(ErrorMessageUser);
                    //End : Start Insert Procedure
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                // WriteLog("Error in Insert ActiveMLSIDS : " + ErrorMessage);
                Console.WriteLine(Environment.NewLine + ErrorMessage);
            }
        }
        #endregion

        #region Get Featured Properties photos
        public static void GetPhotosForFeatureListing()
        {
            CountOfDownloadedMLS = 0;
            CountOfDownloadedPhotos = 0;
            string qry = "exec GetResidentialProperties";
            string mls = "";
            try
            {
                RetsSession session = new RetsSession(Idx_URL);
                session.SetUserAgent("RETS - Connector / 1.2");
                session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
                session.Login(Idx_UserName, Idx_Password);
                DataTable FeaturedData = GetdataTable(qry);
                if (FeaturedData.Rows.Count > 0)
                {
                    foreach (DataRow row in FeaturedData.Rows)
                    {
                        var Mls = row["MLS"].ToString();
                        DownloadAllListingPhotos(mls.ToString(), session);
                        CountOfDownloadedMLS++;
                        Console.Write("Total MLS of " + PropertyType + " is Downloaded =" + CountOfDownloadedMLS + Environment.NewLine);
                    }


                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }
        #endregion

        #region Common methods

        public static void RefineXmlFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var xml = new StringBuilder();
            var isXml = false;
            foreach (var line in lines)
            {


                if (line.Contains("<!DOCTYPE RETS SYSTEM "))
                    isXml = true;
                if (isXml)
                {
                    xml.Append(line.Replace("<!DOCTYPE RETS SYSTEM '" + "RETS-20041001.dtd" + "'>", ""));
                    isXml = false;
                }
                else if (line.Contains("<RETS ReplyCode="))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("<RETS ReplyCode=\"0\" ReplyText=\"\">", ""));
                    isXml = false;
                }
                else if (line.Contains("<REData>"))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("<REData>", ""));
                    isXml = false;
                }
                else if (line.Contains("</RETS>"))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("</RETS>", ""));
                    isXml = false;
                }
                else if (line.Contains("</REData>"))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("</REData>", ""));
                    isXml = false;
                }
                else if (line.Contains("<MAXROWS/>"))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("<MAXROWS/>", ""));
                    isXml = false;
                }
                else if (line.Contains("<?xml version="))
                {
                    isXml = true;
                    if (isXml)
                        xml.Append(line.Replace("<?xml version=\"1.0\" standalone=\"no\"?>", ""));
                    isXml = false;
                }
                else
                {
                    xml.Append(line);
                }
            }
        }
        public static void ConvertXmlFileToList_new(string path, RetsSession session)
        {
            CountOfDownloadedMLS = 0;
            CountOfDownloadedPhotos = 0;
            try
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(path);

                //XmlNodeList idNodes = doc.SelectNodes(XMLRooTTag);

                ArrayList list = new ArrayList();
                //foreach (var node in idNodes)
                //{
                //    XmlNode childnode = (XmlNode)node;
                //    list.Add(childnode.ChildNodes[0].ChildNodes[MlsIndexNO].InnerText);

                //}
              
                string tbName = "";
                if (PropertyType == "Idx Condo")
                {
                    tbName = "PropertyData_Condo";
                }
                else if (PropertyType == "Idx Residential")
                {
                    tbName = "PropertyData";
                }
                else if (PropertyType == "Idx Commercial")
                {
                    tbName = "PropertyData_Comm";
                }
                else if (PropertyType == "Vow Residential")
                {
                    tbName = "PropertyData_Vox_Residential";
                }
                else if (PropertyType == "Vow Condo")
                {
                    tbName = "PropertyData_Condo_Vox";
                }
                else if (PropertyType == "Vow Commercial")
                {
                    tbName = "PropertyData_Comm_VOX";
                }
                var DbQry = "select mls from " + tbName+ " where mls not in(select mls from DownloadPhotosRecords where phototype='"+PropertyType+"')";
                //var DbQry = "select mls from " + tbName + " where mls='PhotoW4149324'";
                DataTable dt = GetdataTable(DbQry);
                if (dt.Rows.Count > 0)
                {
                    foreach ( DataRow row in dt.Rows)
                    {
                        //if (row[0].ToString() == "W4149941")
                        //{
                            list.Add(row[0].ToString());
                        //}
                       // list.Add(row[0].ToString());
                       
                        

                    }
                    SaveMlsRecord(list);
                }
               
                foreach (var item in list)
                {
                    CountOfDownloadedMLS++;
                    Console.Write("Total MLS "+ item.ToString() + " of " + PropertyType + " is Downloaded =" + CountOfDownloadedMLS + Environment.NewLine);

                    if (item != null)
                    {
                        

                        DownloadAllListingPhotos(item.ToString(), session);
                        UpdateMlsRecord(item.ToString());
                    }


                }

                //delete inactive property images
                //List<string> db_mlsids = list.Cast<string>().ToList();


                //List<string> allImages = Directory.GetFiles(ListingPhotoSavePath, "*.jpg")
                //                         .Select(Path.GetFileNameWithoutExtension)
                //                         .Select(s => s.Split('-')[0].Replace("Photo", ""))
                //                         .Distinct()
                //                         .ToList();
                //List<string> inactive_images = allImages.Except(db_mlsids).ToList();


                //foreach (string fileName in inactive_images)
                //{

                //    Process process = new Process();
                //    process.StartInfo.FileName = "cmd.exe";
                //    process.StartInfo.Arguments = "del " + fileName + "*.jpg";// string.Format("/c del \"{0}\"", path);
                //    process.Start();
                //    //string filename = fileName + "*.jpg";
                //    //List<string> Images_toBe_Deleted = Directory.GetFiles(ListingPhotoSavePath, filename).Select(Path.GetFileName).ToList();
                //    //foreach (var item in Images_toBe_Deleted)
                //    //{
                //    //    File.Delete(item);
                //    //}
                //    //var fileToDelete = ListingPhotoSavePath + "\\" + fileName + ".jpg";

                //}

                IsAllPhotosCreated(session);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }
        public static void ConvertXmlFileToList(string path, RetsSession session)
        {
            CountOfDownloadedMLS = 0;
            CountOfDownloadedPhotos = 0;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNodeList idNodes = doc.SelectNodes(XMLRooTTag);

                ArrayList list = new ArrayList();
                foreach (var node in idNodes)
                {
                    XmlNode childnode = (XmlNode)node;
                    list.Add(childnode.ChildNodes[0].ChildNodes[MlsIndexNO].InnerText);

                }
                SaveMlsRecord(list);
                foreach (var item in list)
                {
                    CountOfDownloadedMLS++;
                    Console.Write("Total MLS of " + PropertyType + " is Downloaded =" + CountOfDownloadedMLS + Environment.NewLine);

                    if (item != null)
                    {
                        DownloadAllListingPhotos(item.ToString(), session);
                        UpdateMlsRecord(item.ToString());
                    }


                }
                IsAllPhotosCreated(session);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        public static void DownloadPhotoAccordingToDatabase()
        {
            string qry = "select * from PropertyData";
            DataTable dt = GetdataTable(qry);
            RetsSession session = new RetsSession(Idx_URL);
            session.SetUserAgent("RETS - Connector / 1.2");
            session.SetUserAgentAuthType(UserAgentAuthType.USER_AGENT_AUTH_RETS_1_7);
            session.Login(Idx_UserName, Idx_Password);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var Mls = Convert.ToString(dt.Rows[0]["MLS"]);
                    DownloadAllListingPhotos(Mls, session);
                }


            }

        }

        /// <summary>
        /// Downloads all listing photos per listing query 
        /// (query only returning certain listing statuses as of now, not full list)
        /// </summary>
        /// <param name="MLSNumber">MLSNumber of listing currently being queried</param>
        public static void DownloadAllListingPhotos(string MLSNumber, RetsSession session)
        {
            string filepath = ListingPhotoSavePath + "Photo" + MLSNumber + "-1.jpg";
            if (File.Exists(filepath))
            {
                FileInfo f = new FileInfo(filepath);
                if (f.Length > 500)
                    return;
            }
            SearchRequest searchRequest = session.CreateSearchRequest("Property", "ResidentialProperty", "(timestamp_sql=2015-01-25T00:00:00+ OR idx_dt=2015-01-25+)");

            searchRequest.SetStandardNames(true);
            searchRequest.SetOffset(SearchRequest.OFFSET_NONE);
            searchRequest.SetCountType(SearchRequest.CountType.RECORD_COUNT_AND_RESULTS);

            try
            {
                string photoFilePath = ListingPhotoSavePath;
                string CurrentMLS = MLSNumber.ToString();
                int intCurrentPhotoNo = 1;
                
                //loop through objects until obj is null. It does not error out if the objectID does not exist.
                ObjectDescriptor obj;
                do
                {
                    using (librets.GetObjectRequest request = new GetObjectRequest("Property", "Photo"))
                    {
                       
                        request.AddObject(CurrentMLS, intCurrentPhotoNo);
                        GetObjectResponse response = session.GetObject(request);
                        obj = response.NextObject();
                        if (obj != null)
                        {
                            string strFilename = string.Empty;
                            string currentPhotoLetter = string.Empty;

                            // Get the photo letter
                            currentPhotoLetter = intCurrentPhotoNo.ToString();

                            // Create the file name.
                            strFilename = "Photo" + MLSNumber + "-" + currentPhotoLetter + ".jpg";

                            // get the bytes of the downloaded image
                            byte[] imageBytes = obj.GetDataAsBytes();

                            // Create the full path to the file.
                            string fullPath = Path.Combine(photoFilePath, strFilename);

                            // Write the file.
                            File.WriteAllBytes(fullPath, imageBytes);

                            // Increment photo number.
                            intCurrentPhotoNo++;

                            if (intCurrentPhotoNo > 7)
                            {
                                obj = null;
                            }
                        }
                    }
                    Application.DoEvents();
                    CountOfDownloadedPhotos++;
                    Console.Write("Total Photos of " + PropertyType + " is Downloaded =" + CountOfDownloadedPhotos + Environment.NewLine);
                } while (obj != null);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetPhotoLetter(int intCurrentPhotoNo)
        {
            string currentPhotoLetter = string.Empty;

            switch (intCurrentPhotoNo)
            {
                case 1:
                    currentPhotoLetter = "1";
                    break;
                case 2:
                    currentPhotoLetter = "2";
                    break;
                case 3:
                    currentPhotoLetter = "2";
                    break;
                case 4:
                    currentPhotoLetter = "3";
                    break;
                case 5:
                    currentPhotoLetter = "4";
                    break;
                case 6:
                    currentPhotoLetter = "5";
                    break;
                case 7:
                    currentPhotoLetter = "6";
                    break;
                case 8:
                    currentPhotoLetter = "7";
                    break;
                case 9:
                    currentPhotoLetter = "h";
                    break;
                case 10:
                    currentPhotoLetter = "i";
                    break;
                case 11:
                    currentPhotoLetter = "j";
                    break;
                case 12:
                    currentPhotoLetter = "k";
                    break;
                case 13:
                    currentPhotoLetter = "l";
                    break;
                case 14:
                    currentPhotoLetter = "m";
                    break;
                case 15:
                    currentPhotoLetter = "n";
                    break;
                default:
                    break;
            }
            return currentPhotoLetter;
        }

        public static DataTable GetdataTable(string qry)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(qry, conn);
            Adp.Fill(dt);

            return dt;

        }
        public static string ExecuteNonQuery(string QStr)
        {
            string ErrorMessage = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
            SqlCommand cmd = null;
            try
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                cmd = new SqlCommand(QStr, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();

                ErrorMessage = "success";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    ErrorMessage = "FK";
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }

            }

            return ErrorMessage;
        }

        public static void SaveMlsRecord(ArrayList list)
        {
            string tbName = ReturnTableName();
            var qry = "";
            foreach (var item in list)
            {
                var DbQry = "select * from " + tbName + " where Mls='" + item.ToString() + "'";
                DataTable dt = GetdataTable(DbQry);

                if (dt.Rows.Count > 0)
                {
                    qry += "INSERT INTO [dbo].[DownloadPhotosRecords]  VALUES ('" + Convert.ToString(dt.Rows[0]["MLS"]) + "','" + PropertyType + "','" + ListingPhotoSavePath + "','" + DateTime.Now + "',0);";
                    UpdateMlsRecord(dt.Rows[0]["MLS"].ToString());
                }


            }
            ExecuteNonQuery(qry);
        }
        public static void UpdateMlsRecord(string mls)
        {
            var qry = "";
            string tbName = ReturnTableName();


            if (mls != null)
            {
                var DbQry = "select * from " + tbName + " where Mls='" + mls.ToString() + "'";
                DataTable dt = GetdataTable(DbQry);

                if (dt.Rows.Count > 0)
                {
                    qry += "update [dbo].[DownloadPhotosRecords] set status=1 where MLS='" + mls.ToString() + "'";
                    ExecuteNonQuery(qry);
                }
            }
        }
        public static void IsAllPhotosCreated(RetsSession session)
        {
            try
            {
                var tbName = ReturnTableName();
                var DbQry = "select * from [dbo].[DownloadPhotosRecords] where status=0";
                DataTable dt = GetdataTable(DbQry);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var mls = dt.Rows[i]["MLS"].ToString();
                        DownloadAllListingPhotos(mls, session);
                        UpdateMlsRecord(mls);
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        public static string ReturnTableName()
        {
            string tbName = "";
            if (PropertyType == "Idx Condo")
            {
                tbName = "PropertyData_Condo";
            }
            else if (PropertyType == "Idx Residential")
            {
                tbName = "PropertyData";
            }
            else if (PropertyType == "Idx Commercial")
            {
                tbName = "PropertyData_Comm";
            }
            else if (PropertyType == "Vow Residential")
            {
                tbName = "PropertyData_Vox_Residential";
            }
            else if (PropertyType == "Vow Condo")
            {
                tbName = "PropertyData_Condo_Vox";
            }
            else if (PropertyType == "Vow Commercial")
            {
                tbName = "PropertyData_Comm_VOX";
            }
            return tbName;
        }
        public static void DeleteRecords()
        {
            var QStr = "";
            QStr += "DELETE FROM PropertyData where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            QStr += "DELETE FROM PropertyData_Comm where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            QStr += "DELETE FROM PropertyData_Comm_VOX where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            QStr += "DELETE FROM PropertyData_Condo where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            QStr += "DELETE FROM PropertyData_Condo_Vox where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            QStr += "DELETE FROM PropertyData_Vox_Residential where RTRIM(LTRIM(MLS)) NOT IN(SELECT MLSID FROM [ActiveMLSIDS]);";
            GetdataTable(QStr);
        }
        #endregion


    }
}
