
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Bootstrap.Client.DataAccess.Helper
{
    public static class NpoiHelper
    {
        /// <summary>
        /// 創造 Excel 檔
        /// </summary>
        public static string ExportExcel<T>(string WebRootPath, string username, string filename, List<NpoiParam<T>> npoiParams)
        {            
            IWorkbook workbook = new HSSFWorkbook();
            try
            {
                foreach (var p in npoiParams)
                {
                    //依情況決定要建新的 Sheet 或是用舊的 (即來自範本)
                    ISheet sht = GetSheet(workbook, p);

                    if (p.Data.Any())
                    {
                        //有資料塞格子
                        SetSheetValue(workbook, p, ref sht);
                    }
                    else
                    {
                        //若沒資料在起點寫入No Data !
                        CreateNewRowOrNot(ref sht, p.RowStartFrom, p.ColumnStartFrom);
                        sht.GetRow(p.RowStartFrom).CreateCell(p.ColumnStartFrom).SetCellValue("No Data !");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
            
            return Flush(WebRootPath, username, filename, workbook);
        }

        /// <summary>
        /// 創造 Excel 檔
        /// </summary>
        public static DownloadCache ExportExcel<T>(List<NpoiParam<T>> npoiParams, string filename)
        {            
            IWorkbook workbook = new HSSFWorkbook();
            DownloadCache downloadCache = new DownloadCache();
            MemoryStream ms = new MemoryStream();
            try
            {
                foreach (var p in npoiParams)
                {
                    //依情況決定要建新的 Sheet 或是用舊的 (即來自範本)
                    ISheet sht = GetSheet(workbook, p);

                    if (p.Data.Any())
                    {
                        //有資料塞格子
                        SetSheetValue(workbook, p, ref sht);
                    }
                    else
                    {
                        //若沒資料在起點寫入No Data !
                        CreateNewRowOrNot(ref sht, p.RowStartFrom, p.ColumnStartFrom);
                        sht.GetRow(p.RowStartFrom).CreateCell(p.ColumnStartFrom).SetCellValue("No Data !");
                    }
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                downloadCache.mem = ms;
                downloadCache.fileName = filename + ".xls";
            }
            catch(Exception ex)
            {
                throw ex;
            }            
            
            return downloadCache;
        }

        private static ISheet GetSheet<T>(IWorkbook workbook, NpoiParam<T> param)
        {
            //在 workbook 中以 sheet name 尋找 是否找得到sheet
            if (workbook.GetSheet(param.SheetName) == null)
            {
                //找不到建一張新的sheet
                ISheet sht = workbook.CreateSheet(param.SheetName);
                sht = CreateColumn(workbook, param);

                return sht;
            }
            else
            {
                //找得到即為要塞值的目標
                return workbook.GetSheet(param.SheetName);
            }
        }

        private static ISheet CreateColumn<T>(IWorkbook workbook, NpoiParam<T> p)
        {
            var sht = workbook.GetSheet(p.SheetName);
            sht.CreateRow(0);
            ICellStyle columnStyle = GetBaseCellStyle(workbook, p.FontStyle);

            if (p.ShowHeader)
            {
                for (int i = 0; i < p.ColumnMapping.Count; i++)
                {
                    var offset = i + p.ColumnStartFrom;

                    sht.GetRow(0).CreateCell(offset);                                                //先創建格子
                    sht.GetRow(0).GetCell(offset).CellStyle = columnStyle;                           //綁定基本格式
                    sht.GetRow(0).GetCell(offset).SetCellValue(p.ColumnMapping[i].title);  //給值
                }
            }

            return sht;
        }

        private static void SetSheetValue<T>(IWorkbook workbook, NpoiParam<T> p, ref ISheet sht)
        {
            //要從哪一行開始塞資料 (有可能自定範本 可能你原本範本內就有好幾行表頭 2行 3行...)
            int line = p.RowStartFrom;

            //有可能前面幾欄是自訂好的 得跳過幾個欄位再開始塞
            int columnOffset = p.ColumnStartFrom;

            //根據標題列先處理所有Style (對Npoi來說 '創建'Style在workbook中是很慢的操作 作越少次越好 絕對不要foreach在塞每行列實際資料時重覆作 只通通在標題列做一次就好)
            ICellStyle[] cellStyleArr = InitialColumnStyle(workbook, p.ColumnMapping, p.FontStyle);

            if (workbook.GetType() == typeof(HSSFWorkbook) && p.Data.Count() > 65535)
            {      
                bool isFirst = false;         
                if (string.IsNullOrEmpty(p.OriginSheetName)) {                    
                    p.OriginSheetName = p.SheetName;
                    p.SheetName = p.SheetName + "_0_" + 65535.ToString();                    
                    isFirst = true;
                }     
                IEnumerable<T> cutdata = p.Data.Skip(65535).Take(p.Data.Count() - 65535);
                int cutdatacount = cutdata.Count();
                int from = isFirst ? 65536 : p.LastTo + 1;
                int to = cutdatacount > 65535 ? from + 65535 - 1 : from + cutdatacount - 1;
                NpoiParam<T> cutp = new NpoiParam<T>()
                {
                    SheetName = string.Format("{0}_{1}_{2}", p.OriginSheetName, from.ToString(), to.ToString()),
                    ColumnMapping = p.ColumnMapping,
                    Data = cutdata,
                    OriginSheetName = p.OriginSheetName,
                    LastTo = to
                };                
                ISheet cutsheet = GetSheet(workbook, cutp);
                SetSheetValue(workbook, cutp, ref cutsheet);                           
                p.Data = p.Data.Take(65535);
            }

            foreach (var item in p.Data)
            {
                //如果 x 軸有偏移值 則表示這行他已經自己建了某幾欄的資料 我們只負責塞後面幾欄 所以並非每次都create new row
                CreateNewRowOrNot(ref sht, line, columnOffset);

                for (int i = 0; i < p.ColumnMapping.Count; i++)
                {
                    //建立格子 (需考量 x 軸有偏移值)
                    var cell = sht.GetRow(line).CreateCell(i + columnOffset);

                    //綁定style (記得 綁定是不慢的 但建新style是慢的 不要在迴圈裡無意義的反覆建style 只在標題處理一次即可)
                    cell.CellStyle = cellStyleArr[i];

                    //給值
                    string value = GetValue(item, p.ColumnMapping, i);               //reflection取值
                    SetCellValue(value, ref cell, p.ColumnMapping[i].dataType);      //幫cell填值
                }

                line++;
            }

            //處理AutoFit (必定是在最後做的 因為你得把所有格子都塞完以後才知道每欄多寬是你需要的)
            if (p.IsAutoFit)
            {
                for (int i = 0; i < p.ColumnMapping.Count; i++)
                {
                    sht.AutoSizeColumn(i);
                }
            }
        }

        private static ICellStyle[] InitialColumnStyle(IWorkbook wb, List<ColumnMapping> columnMapping, FontStyle fontStyle)
        {
            ICellStyle[] styleArr = new ICellStyle[columnMapping.Count];

            for (int i = 0; i < columnMapping.Count; i++)
            {
                //取通用格式
                ICellStyle cellStyle = GetBaseCellStyle(wb, fontStyle);

                //處理格式輸出
                if (!String.IsNullOrWhiteSpace(columnMapping[i].dataFormat))
                {
                    cellStyle.DataFormat = GetCellFormat(wb, columnMapping[i].dataFormat);
                }

                styleArr[i] = cellStyle;
            }

            return styleArr;
        }

        private static void CreateNewRowOrNot(ref ISheet sht, int line, int columnOffset)
        {
            //如果是從自定範本來則不能重畫格子 例如他給我範本 只要我畫後面三格 前兩格他自己做好了 如果我整行重畫 他自己畫的兩格也會消失
            if (columnOffset == 0 || line > sht.LastRowNum)
            {
                sht.CreateRow(line);
            }
        }

        private static void SetCellValue(string value, ref ICell cell, NpoiDataType type)
        {
            switch (type)
            {
                //字串沒有格式
                case NpoiDataType.String:
                    if (!String.IsNullOrWhiteSpace(value)) cell.SetCellValue(value);
                    break;

                //轉數字(整數)
                case NpoiDataType.Int:
                    if (!String.IsNullOrWhiteSpace(value)) cell.SetCellValue(Convert.ToInt32(value));
                    break;

                //轉數字(含小數點)
                case NpoiDataType.Double:
                    if (!String.IsNullOrWhiteSpace(value)) cell.SetCellValue(Convert.ToDouble(value));
                    break;

                //轉日期
                case NpoiDataType.Date:
                    if (!String.IsNullOrWhiteSpace(value))
                    {   
                        DateTime date = DateTime.Now;
                        if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) 
                        {
                            cell.SetCellValue(date);
                        }
                        else
                        {
                            cell.SetCellValue(Convert.ToDateTime(value));
                        }
                    }
                    break;                

                //不會發生;
                default:
                    break;
            }
        }

        private static ICellStyle GetBaseCellStyle(IWorkbook wb, FontStyle fontStyle, bool paintBorder = false)
        {
            //畫線
            ICellStyle cellStyle = wb.CreateCellStyle();
            if (paintBorder) 
            {
                cellStyle.BorderLeft = BorderStyle.Thin;
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderTop = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
            }            

            //預設字型大小
            IFont font1 = wb.CreateFont();
            font1.FontName = (fontStyle.FontName == null) ? "Arial" : fontStyle.FontName;
            font1.FontHeightInPoints = (fontStyle.FontHeightInPoints == null) ? (short)10 : fontStyle.FontHeightInPoints.Value;
            cellStyle.SetFont(font1);

            return cellStyle;
        }

        private static short GetCellFormat(IWorkbook wb, string formatStr)
        {
            IDataFormat dataFormat = wb.CreateDataFormat();
            return dataFormat.GetFormat(formatStr);
        }

        private static string GetValue<T>(T obj, List<ColumnMapping> columnMapping, int order)
        {
            var fieldName = columnMapping[order].field;
            var prop = typeof(T).GetProperties().Where(q => q.Name == fieldName).SingleOrDefault();
            var value = (prop == null) ? null : prop.GetValue(obj, null);

            return (value == null) ? "" : value.ToString();
        }

        private static string Flush(string WebRootPath, string username, string filename, IWorkbook workbook)
        {
            //初始化匯出資料的資料夾
            var webSiteUrl = "~/ExcelExport/";
            //產生資料夾
            var ExcelExportPath = Path.Combine(WebRootPath, webSiteUrl.Replace("~", string.Empty).Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));
            if (!Directory.Exists(ExcelExportPath)) Directory.CreateDirectory(ExcelExportPath);
            //產生user資料夾
            ExcelExportPath = Path.Combine(ExcelExportPath, username);
            if (!Directory.Exists(ExcelExportPath)) Directory.CreateDirectory(ExcelExportPath);
            var fileFolder = Path.Combine(ExcelExportPath, $"{filename}.xls");
            FileStream fs = null;
            try
            {
                //清除歷史文件，避免歷史文件越來越多，可進行刪除
                DirectoryInfo dyInfo = new DirectoryInfo(ExcelExportPath);
                //獲取文件夾下所有的文件
                foreach (FileInfo feInfo in dyInfo.GetFiles())
                {
                    //判斷文件日期是否小於兩天前，是則刪除
                    if (feInfo.CreationTime < DateTime.Today.AddDays(-2))
                        feInfo.Delete();
                }
                fs = new FileStream(fileFolder, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                workbook.Write(fs);
                workbook = null;
                fs.Close();
            }
            catch(Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                throw ex;
            } 
            return $"/ExcelExport/{username}/{filename}.xls";                             
        }
    }
}
