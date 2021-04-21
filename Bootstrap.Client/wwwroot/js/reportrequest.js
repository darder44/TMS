$(function () {
    
    var queryParams = function (params) {
        return $.extend(params,
            {
                StorerKey: $("#txt_StorerKey").val(),
                Facility: $("#txt_Facility").val(),
                DoRouteDateS: $("#txt_DoRouteDateS").val(),
                DoRouteDateE: $("#txt_DoRouteDateE").val(),
                DeliveryDateS: $("#txt_DeliveryDateS").val(),
                DeliveryDateE: $("#txt_DeliveryDateE").val(),
            });
    };

    var $reportsdnreturnlist = $('#reportrequest-table');
    $reportsdnreturnlist.lgbTable({
        //url: 'api/ReportSDNReturnList/RetrieveData',
        dataBinder: {
            map: {
            },
            bootstrapTable: '',
            click: {
                '#btn_query': function () {                    
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportRequest/RetrieveData'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                }
            }
        },        
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false, //匯出功能
            showColumns: true, //使用者可選擇想看到的欄位
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: false,
            search: true,
            detailView: false,
            showAdvancedSearchButton: true,
            queryParams: queryParams,
            columns: [

                { title: "計費類別", field: "CostStatus", sortable: true },
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "簽單日", field: "SignDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "出車日", field: "DoRouteDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "訂單日", field: "OrderDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日", field: "DeliveryDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "駕駛", field: "Driver", sortable: true },
                { title: "請款人", field: "Receiver", sortable: true },
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "TMS單號", field: "TMSKey", sortable: true },
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶名稱", field: "CustName", sortable: true },
                { title: "單位", field: "Uom", sortable: true },
                { title: "數量", field: "ChargeQty", sortable: true },
                { title: "應收單價", field: "Receivable", sortable: true },
                { title: "應付單價", field: "Payable", sortable: true },
                { title: "議價", field: "Premium", sortable: true },
                { title: "項次", field: "CostName", sortable: true },
                { title: "原因", field: "Reason", sortable: true },
                { title: "應收總價", field: "SumReceivable", sortable: true },
                { title: "應付總價", field: "SumPayable", sortable: true },
                { title: "實付總價", field: "SumPremium", sortable: true },
                { title: "起點", field: "AreaStart", sortable: true },
                { title: "迄點", field: "AreaEnd", sortable: true },
                { title: "備註", field: "Notes", sortable: true },
                { title: "請款類別", field: "CostKind", sortable: true },
                { title: "請款代碼", field: "CostCode", sortable: true },
                { title: "簽單回傳日期", field: "SdnSendDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "最終確認", field: "SdnSendWho", sortable: true },
                { title: "確認時間", field: "CheckDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "簽單狀態", field: "ConfirmNotes", sortable: true },
                { title: "簽單備註", field: "SdnNotes", sortable: true },
                { title: "計費時間", field: "CostDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "應付不分攤", field: "APnoDistribution", sortable: true }



            ],
            editButtons: { 
                id: "",
                events: {}
            }
        }
    });

     
    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportrequest-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋請付款報表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportRequest/ExportExcel',
            queryParams: queryParams,
            FileName: '請付款報表',
            Options: options,
            Sheets: 
            [
                {
                    SheetName: "運費明細",
                    sortName: 'DeliveryDate',
                    Columns: [
                
                        { title: "計費類別", field: "CostStatus" },
                        { title: "貨主", field: "StorerKey" },
                        { title: "簽單日", field: "SignDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "出車日", field: "DoRouteDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "訂單日", field: "OrderDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "到貨日", field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "車號", field: "VehicleKey" },
                        { title: "駕駛", field: "Driver" },
                        { title: "請款人", field: "Receiver" },
                        { title: "路線編號", field: "RouteNo" },
                        { title: "貨主單號", field: "ExternOrderKey" },
                        { title: "TMS單號", field: "TMSKey" },
                        { title: "客戶編號", field: "ConsigneeKey" },
                        { title: "客戶名稱", field: "CustName" },
                        { title: "單位", field: "Uom" },
                        { title: "數量", field: "ChargeQty", dataType: 2, dataFormat: '0.#####' },
                        { title: "應收單價", field: "Receivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "應付單價", field: "Payable", dataType: 2, dataFormat: '0.#####' },
                        { title: "議價", field: "Premium", dataType: 2, dataFormat: '0.#####' },
                        { title: "項次", field: "CostName" },
                        { title: "原因", field: "Reason" },
                        { title: "應收總價", field: "SumReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "應付總價", field: "SumPayable", dataType: 2, dataFormat: '0.#####' },
                        { title: "實付總價", field: "SumPremium", dataType: 2, dataFormat: '0.#####' },
                        { title: "起點", field: "AreaStart" },
                        { title: "迄點", field: "AreaEnd" },
                        { title: "備註", field: "Notes" },
                        { title: "請款類別", field: "CostKind" },
                        { title: "請款代碼", field: "CostCode" },
                        { title: "簽單回傳日期", field: "SdnSendDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "最終確認", field: "SdnSendWho" },
                        { title: "確認時間", field: "CheckDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單狀態", field: "ConfirmNotes" },
                        { title: "簽單備註", field: "SdnNotes" },
                        { title: "計費時間", field: "CostDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "應付不分攤", field: "APnoDistribution" }
                    ]
                },
                {
                    SheetName: "應收明細",
                    sortName: 'DeliveryDate',
                    Columns: [
                
                        { title: "貨主", field: "StorerKey" },
                        { title: "請款類別", field: "CostKind" },
                        { title: "到貨日期", field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "車號", field: "VehicleKey" },
                        { title: "起點", field: "AreaStart" },
                        { title: "迄點", field: "AreaEnd" },
                        { title: "客戶名稱", field: "CustName" },
                        { title: "訂單號碼", field: "ExternOrderKey" },
                        { title: "計費數量", field: "ChargeQty", dataType: 2, dataFormat: '0.#####' },
                        { title: "單位", field: "Uom" },
                        { title: "單價", field: "Receivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "總價", field: "SumReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "通路別", field: "Channel" },
                        { title: "路線編號", field: "RouteNo" }
                    ]
                },
                {
                    SheetName: "應收付",
                    sortName: 'DeliveryDate',
                    Columns: [
                
                        { title: "貨主", field: "StorerKey" },
                        { title: "請款類別", field: "CostName" },
                        { title: "序號", field: "Sequence" },
                        { title: "出車日", field: "DoRouteDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單日", field: "SignDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "車號", field: "VehicleKey" },
                        { title: "駕駛人", field: "Driver" },
                        { title: "請款人", field: "Receiver" },
                        { title: "數量", field: "ChargeQty", dataType: 2, dataFormat: '0.#####' },
                        { title: "單位", field: "Uom" },
                        { title: "代碼", field: "Code" },
                        { title: "起點", field: "AreaStart" },
                        { title: "迄點", field: "AreaEnd" },
                        { title: "客戶單價", field: "Receivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "司機運價", field: "Payable", dataType: 2, dataFormat: '0.#####' },
                        { title: "議價", field: "Premium", dataType: 2, dataFormat: '0.#####' },
                        { title: "客戶名稱", field: "CustName" },
                        { title: "單號", field: "ExternOrderKey" },
                        { title: "備註", field: "Notes" },
                        { title: "標準應收單價", field: "StdReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "標準應付單價", field: "StdPayable", dataType: 2, dataFormat: '0.#####' },
                        { title: "部門代碼", field: "Channel" },
                        { title: "到貨日", field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "庫別", field: "Facility" },
                        { title: "配送路編", field: "RouteNo" },
                        { title: "應收分攤金額", field: "ARDistribution", dataType: 2, dataFormat: '0.#####' },
                        { title: "應收總金額", field: "SumReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "應付總金額", field: "SumPayable", dataType: 2, dataFormat: '0.#####' },
                        { title: "實付總價", field: "SumPremium", dataType: 2, dataFormat: '0.#####' }
                    ]
                },
                {
                    SheetName: "日報表",
                    sortName: 'DeliveryDate',
                    Columns: [
                
                        { title: "貨主", field: "StorerKey" },
                        { title: "請款類別", field: "CostName" },
                        { title: "序號", field: "Sequence" },
                        { title: "載貨日期", field: "DoRouteDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單日期", field: "SignDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "車號", field: "VehicleKey" },
                        { title: "司機姓名", field: "Driver" },
                        { title: "請款人", field: "Receiver" },
                        { title: "數量", field: "ChargeQty", dataType: 2, dataFormat: '0.#####' },
                        { title: "單位", field: "Uom" },
                        { title: "代碼", field: "Code" },
                        { title: "起點", field: "AreaStart" },
                        { title: "迄點", field: "AreaEnd" },
                        { title: "應收單價", field: "Receivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "應付單價", field: "Payable", dataType: 2, dataFormat: '0.#####' },
                        { title: "議價", field: "Premium", dataType: 2, dataFormat: '0.#####' },
                        { title: "客戶名稱", field: "CustName" },
                        { title: "貨主單號", field: "ExternOrderKey" },
                        { title: "備註", field: "Notes" },
                        { title: "標準應收", field: "StdReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "標準應付", field: "StdPayable", dataType: 2, dataFormat: '0.#####' },
                        { title: "部門別", field: "department" },
                        { title: "應收總價", field: "SumReceivable", dataType: 2, dataFormat: '0.#####' },
                        { title: "應付總價", field: "SumPayable", dataType: 2, dataFormat: '0.#####' },
                        { title: "實付總價", field: "SumPremium", dataType: 2, dataFormat: '0.#####' },
                        { title: "配送路編", field: "RouteNo" },
                        { title: "TMS單號", field: "TMSKey" },
                        { title: "地址", field: "ShipToAddress" },
                        { title: "通路別", field: "Channel" }
                    ]
                },
            ]
        });       
    })
});
