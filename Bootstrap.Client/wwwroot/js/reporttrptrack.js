$(function () {
    InitialSelect();
    
    var queryParams = function (params) {
        return $.extend(params, {
            StorerKeys: function () {
                var values = [];
                $("#sel_storerkey option:selected").each(function () {
                    var value = $(this).val();
                    values.push(value);
                });                        
                return values.join(",");
            },
            OrderTypes: function () {
                var values = [];
                $("#sel_ordertypes option:selected").each(function () {
                    var value = $(this).val();
                    values.push(value);
                });                        
                return values.join(",");
            },
            OrderStatus: function () {
                var values = [];
                $("#sel_orderstatus option:selected").each(function () {
                    var value = $(this).val();
                    values.push(value);
                });                        
                return values.join(",");
            },
            AreaCodes: function () {
                var values = [];
                $("#sel_areacodes option:selected").each(function () {
                    var value = $(this).val();
                    values.push(value);
                });                        
                return values.join(",");
            },
            ConsigneeKey: $("#txt_ConsigneeKey").val(),
            RouteNo: $("#txt_RouteNo").val(),
            WaveKey: $("#txt_WaveKey").val(),
            TMSKey: $("#txt_TMSKey").val(),
            ExternOrderKey: $("#txt_ExternOrderKey").val(),
            DoRouteDate_Start: $("#txt_DoRouteDate_Start").val(),
            DoRouteDate_End: $("#txt_DoRouteDate_End").val(),
            DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(),
            DeliveryDate_End: $("#txt_DeliveryDate_End").val(),
        });
    };
    var $reoporttrptrack = $('#reporttrptrack-table');
    $reoporttrptrack.lgbTable({             
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reporttrptrack-table',
            click: {
                '#btn_query': function () {                    
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportTRPTrack/RetrieveData'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                }
            }
        },
        smartTable: {
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'OrderStatus',
            checkbox: false,
            search: true,
            showAdvancedSearchButton: true,
            queryParams: queryParams,
            columns: [
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "訂單類別", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "訂單狀態", field: "OrderStatus", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "司機", field: "Driver", sortable: true },
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "訂單日期", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "簽單日期", field: "SignDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "WaveKey", field: "WaveKey", sortable: true },
                { title: "訂單號碼", field: "TMSKey", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "銷售客編", field: "SoldTo", sortable: true },
                { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                { title: "到貨客編", field: "ShipTo", sortable: true },
                { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                { title: "路編起點", field: "StartAddress", sortable: true },
                { title: "路編迄點", field: "EndAddress", sortable: true },
                { title: "縣市", field: "City", sortable: true },
                { title: "訂單板數", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "訂單箱數", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "訂單個數", field: "OrderQty", sortable: true },
                { title: "訂單總個數", field: "TotalQty", sortable: true },
                { title: "出貨箱數", field: "ShipCaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "出貨個數", field: "ShipQty", sortable: true },
                { title: "出貨總個數", field: "TotalShipQty", sortable: true },
                { title: "簽收總個數", field: "SignQty", sortable: true },
                { title: "總重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "總材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "備註", field: "Notes", sortable: true },
                { title: "達交", field: "OnTime", sortable: true },
                { title: "遲交", field: "Delay", sortable: true },
                { title: "新增時間", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } }
            ]
        }
    });

    function InitialSelect() {
        var ConfigurationSet = {
            includeSelectAllOption: true,
            enableFiltering: false
        };
        $("#sel_storerkey").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇貨主',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey").multiselect("rebuild");

        $("#sel_ordertypes").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇訂單類別',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_ordertypes").multiselect("setOptions", ConfigurationSet);
        $("#sel_ordertypes").multiselect("rebuild");

        $("#sel_orderstatus").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇訂單狀態',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_orderstatus").multiselect("setOptions", ConfigurationSet);
        $("#sel_orderstatus").multiselect("rebuild");

        $("#sel_areacodes").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇區碼',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_areacodes").multiselect("setOptions", ConfigurationSet);
        $("#sel_areacodes").multiselect("rebuild");
    };  

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $reoporttrptrack.bootstrapTable('getOptions');
        var url = options.url;
        if (!url || url === "") {
            swal({ title: "請先搜尋到貨追蹤表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportTRPTrack/ExportExcel',
            queryParams: queryParams,
            FileName: '到貨追蹤表',
            Options: options,
            Sheets: [
                {
                    SheetName: "ReportTRPTrack",
                    sortName: 'ConsigneeKey',
                    Columns: [
                        { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                        { title: "訂單類別", field: "OrderType", sortable: true },
                        { title: "訂單狀態", field: "OrderStatus", sortable: true },
                        { title: "區碼", field: "AreaCode", sortable: true },
                        { title: "車號", field: "VehicleKey", sortable: true },
                        { title: "司機", field: "Driver", sortable: true },
                        { title: "訂單日期", field: "OrderDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "到貨日期", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "出車日期", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單日期", field: "SignDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "貨主", field: "StorerKey", sortable: true },
                        { title: "客戶簡稱", field: "ShortName", sortable: true },
                        { title: "銷售客編", field: "SoldTo", sortable: true },
                        { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                        { title: "到貨客編", field: "ShipTo", sortable: true },
                        { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                        { title: "縣市", field: "City", sortable: true },
                        { title: "訂單板數", field: "PalletQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "訂單箱數", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "訂單個數", field: "OrderQty", sortable: true, dataType: 1 },
                        { title: "訂單總個數", field: "TotalQty", sortable: true, dataType: 1 },
                        { title: "出貨個數", field: "ShipQty", sortable: true, dataType: 1 },
                        { title: "出貨箱數", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "出貨總個數", field: "TotalShipQty", sortable: true, dataType: 1 },
                        { title: "總重量", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "總材積", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "簽收總個數", field: "SignQty", sortable: true, dataType: 1 },
                        { title: "達交", field: "OnTime", sortable: true },
                        { title: "遲交", field: "Delay", sortable: true },
                        { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' }
                    ]
                },
                {
                    SheetName: "Detail",
                    sortName: 'ConsigneeKey',
                    Columns: [
                        { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                        { title: "訂單類別", field: "OrderType", sortable: true },
                        { title: "訂單狀態", field: "OrderStatus", sortable: true },
                        { title: "區碼", field: "AreaCode", sortable: true },
                        { title: "車號", field: "VehicleKey", sortable: true },
                        { title: "司機", field: "Driver", sortable: true },
                        { title: "路線編號", field: "RouteNo", sortable: true },
                        { title: "訂單日期", field: "OrderDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "到貨日期", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "出車日期", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單日期", field: "SignDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "貨主", field: "StorerKey", sortable: true },
                        { title: "WaveKey", field: "WaveKey", sortable: true },
                        { title: "訂單號碼", field: "TMSKey", sortable: true },
                        { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                        { title: "客戶簡稱", field: "ShortName", sortable: true },
                        { title: "銷售客編", field: "SoldTo", sortable: true },
                        { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                        { title: "到貨客編", field: "ShipTo", sortable: true },
                        { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                        { title: "路編起點", field: "StartAddress", sortable: true },
                        { title: "路編迄點", field: "EndAddress", sortable: true },
                        { title: "縣市", field: "City", sortable: true },
                        { title: "訂單板數", field: "PalletQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "訂單箱數", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "訂單個數", field: "OrderQty", sortable: true, dataType: 1 },
                        { title: "訂單總個數", field: "TotalQty", sortable: true, dataType: 1 },
                        { title: "出貨個數", field: "ShipQty", sortable: true, dataType: 1 },
                        { title: "出貨箱數", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "出貨總個數", field: "TotalShipQty", sortable: true, dataType: 1 },
                        { title: "總重量", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "總材積", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "簽收總個數", field: "SignQty", sortable: true, dataType: 1 },
                        { title: "備註", field: "Notes", sortable: true },
                        { title: "達交", field: "OnTime", sortable: true },
                        { title: "遲交", field: "Delay", sortable: true },
                        { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' }
                    ]
                }
            ]
        });       
    });

    //取得訂單類型
    $.fetchDictionary("OrderType", function (result) {
        if (!result) {
            swal({ title: "無法取得訂單類型，請重整頁面", type: "warning", confirmButtonText: '確定' });
            return;
        }
        if (result.length == 0) {
            swal({ title: "尚未設定訂單類型，請聯繫相關人員", type: "warning", confirmButtonText: '確定' });
            return;
        }

        var data = result.map(function (row) {
            return {
                value: $.trim(row.Code),
                label: row.Code + "　" + row.Description,
            }
        });
        
        $("#sel_ordertypes").multiselect('dataprovider', data);
    });
});