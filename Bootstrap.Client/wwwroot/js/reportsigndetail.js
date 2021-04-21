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
            OrderDate_Start: $("#txt_OrderDate_Start").val(),
            OrderDate_End: $("#txt_OrderDate_End").val(),
            DoRouteDate_Start: $("#txt_DoRouteDate_Start").val(),
            DoRouteDateEnd: $("#txt_DoRouteDate_End").val(),
            DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(),
            DeliveryDate_End: $("#txt_DeliveryDate_End").val(),
            RouteNo: $("#txt_RouteNo").val(),
            VehicleKey: $("#txt_VehicleKey").val(),
            ConsigneeKey: $("#txt_ConsigneeKey").val(),
            TMSKey: $("#txt_TMSKey").val(),
            ExternOrderKey: $("#txt_ExternOrderKey").val(),
            Sku: $("#txt_Sku").val()
        });
    };
    var $reportsigndetail = $('#reportsigndetail-table');
    $reportsigndetail.lgbTable({             
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reportsigndetail-table',
            click: {
                '#btn_query': function () {                    
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportSignDetail'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                }
            }
        },
        smartTable: {
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'RouteNo',
            checkbox: false,
            search: true,
            showAdvancedSearchButton: true,
            queryParams: queryParams,
            columns: [ //, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } 
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "司機", field: "Driver", sortable: true },
                { title: "貨運公司", field: "ShippingCompany", sortable: true },
                { title: "出車日期", field: "DoRouteDate", sortable: true },
                { title: "到貨日期", field: "DeliveryDate", sortable: true },
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "貨主名稱", field: "StorerName", sortable: true },
                { title: "備註", field: "Notes", sortable: true },
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶名稱", field: "ShortName", sortable: true },
                { title: "郵遞區號", field: "Zip", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true },
                { title: "送貨地址", field: "ShipToAddress", sortable: true },
                { title: "訂單類別", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "訂單日期", field: "OrderDate", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "簽單日期", field: "ConFirmDate", sortable: true },
                { title: "狀態", field: "ConfirmNotes", sortable: true },
                { title: "客戶驗收單號", field: "CustomerOrderKey", sortable: true },
                { title: "簽單回傳日期", field: "SdnSendDate", sortable: true },
                { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                { title: "簽單備註", field: "SDNNotes", sortable: true },
                { title: "發票回收", field: "InvBack", sortable: true },
                { title: "簽單已回", field: "SDNBack", sortable: true },
                { title: "訂單號碼", field: "TMSKey", sortable: true },
                { title: "項次", field: "OrderLineNumber", sortable: true },
                { title: "品號", field: "Sku", sortable: true },
                { title: "品名", field: "Descr", sortable: true },
                { title: "單位", field: "Uom", sortable: true },
                { title: "訂單量", field: "OrderQty", sortable: true },
                { title: "訂單箱數", field: "CaseQty", sortable: true },
                { title: "送貨量", field: "ShipQty", sortable: true },
                { title: "送貨箱數", field: "ShipCaseQty", sortable: true },
                { title: "簽單量", field: "SignQty", sortable: true },
                { title: "簽單箱數", field: "SignCaseQty", sortable: true },
                { title: "箱包轉換率", field: "CaseCnt", sortable: true },
                { title: "單位材積", field: "STDCUBE", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "單位重量", field: "STDGROSSWGT", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "訂單總材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "送貨總材積", field: "ShipCube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "簽單總材積", field: "SignCube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "訂單總重", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "送貨總重", field: "ShipWeight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "簽單總重", field: "SignWeighr", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "異常碼", field: "RSC", sortable: true },
                { title: "責屬碼", field: "RBC", sortable: true }
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
    };  

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $reportsigndetail.bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋簽單明細報表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportSignDetail/ExportExcel',
            queryParams: queryParams,
            FileName: '簽單明細報表',
            Options: options,
            Sheets: [{
                SheetName: "ReportSignDetail",
                sortName: 'RouteNo',
                Columns: [
                    { title: "路線編號", field: "RouteNo", sortable: true },
                    { title: "車號", field: "VehicleKey", sortable: true },
                    { title: "司機", field: "Driver", sortable: true },
                    { title: "貨運公司", field: "ShippingCompany", sortable: true },
                    { title: "出車日期", field: "DoRouteDate", sortable: true },
                    { title: "到貨日期", field: "DeliveryDate", sortable: true },
                    { title: "貨主", field: "StorerKey", sortable: true },
                    { title: "貨主名稱", field: "StorerName", sortable: true },
                    { title: "備註", field: "Notes", sortable: true },
                    { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                    { title: "客戶名稱", field: "ShortName", sortable: true },
                    { title: "郵遞區號", field: "Zip", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true },
                    { title: "送貨地址", field: "ShipToAddress", sortable: true },
                    { title: "訂單類別", field: "OrderType", sortable: true },
                    { title: "訂單日期", field: "OrderDate", sortable: true },
                    { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                    { title: "簽單日期", field: "ConFirmDate", sortable: true },
                    { title: "狀態", field: "ConfirmNotes", sortable: true },
                    { title: "客戶驗收單號", field: "CustomerOrderKey", sortable: true },
                    { title: "簽單回傳日期", field: "SdnSendDate", sortable: true },
                    { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                    { title: "簽單備註", field: "SDNNotes", sortable: true },
                    { title: "發票回收", field: "InvBack", sortable: true },
                    { title: "簽單已回", field: "SDNBack", sortable: true },
                    { title: "訂單號碼", field: "TMSKey", sortable: true },
                    { title: "項次", field: "OrderLineNumber", sortable: true },
                    { title: "品號", field: "Sku", sortable: true },
                    { title: "品名", field: "Descr", sortable: true },
                    { title: "單位", field: "Uom", sortable: true },
                    { title: "訂單量", field: "OrderQty", sortable: true, dataType: 1 },
                    { title: "訂單箱數", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "送貨量", field: "ShipQty", sortable: true, dataType: 1 },
                    { title: "送貨箱數", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "簽單量", field: "SignQty", sortable: true, dataType: 1 },
                    { title: "簽單箱數", field: "SignCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "箱包轉換率", field: "CaseCnt", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "單位材積", field: "STDCUBE", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "單位重量", field: "STDGROSSWGT", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "訂單總材積", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "送貨總材積", field: "ShipCube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "簽單總材積", field: "SignCube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "訂單總重", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "送貨總重", field: "ShipWeight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "簽單總重", field: "SignWeighr", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "異常碼", field: "RSC", sortable: true },
                    { title: "責屬碼", field: "RBC", sortable: true }
                ]
            }]
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