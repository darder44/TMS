$(function () {
    InitialSelect();
    
    var queryParams = function (params) {
        return $.extend(params,
            {
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
                AreaCodes: function () {
                    var values = [];
                    $("#sel_areacodes option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                Companies: function () {
                    var values = [];
                    $("#sel_company option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                Facilities: function () {
                    var values = [];
                    $("#sel_facility option:selected").each(function () {
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
                TMSKeys: $("#txt_TMSKey").val(),
                ExternOrderKeys: $("#txt_ExternOrderKey").val(),
                OrderMethod: $("#ordermethod").val(),
                //RouteNo: $("#txt_RouteNo").val(),
                ConfirmDateS: $("#txt_ConfirmDateS").val(),
                ConfirmDateE: $("#txt_ConfirmDateE").val(),
                DeliveryDateS: $("#txt_DeliveryDateS").val(),
                DeliveryDateE: $("#txt_DeliveryDateE").val(),
            });
    };

    var $reportsdnreturnlist = $('#reportsdnreturnlist-table');
    $reportsdnreturnlist.lgbTable({
        //url: 'api/ReportSDNReturnList/RetrieveData',
        dataBinder: {
            map: {
            },
            bootstrapTable: '',
            click: {
                '#btn_query': function () {                    
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportSDNReturnList/RetrieveData'), pageNumber: 1 })
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
            detailView: true,
            showAdvancedSearchButton: true,
            queryParams: queryParams,
            columns: [
                { title: "貨主代號", field: "StorerKey", sortable: true },
                { title: "通路別", field: "Channel", sortable: true },
                { title: "客戶全名", field: "FullName", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "到貨日", field: "DeliveryDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }  },
                { title: "訂單類別", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "訂單號碼", field: "TMSKey", sortable: true },
                { title: "異常狀況", field: "SignStatus", sortable: true },
                { title: "採購單號", field: "Customerorderkey", sortable: true },
                { title: "發票退回", field: "Invback", sortable: true },
                { title: "簽單確認時間", field: "SdnSendDate", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true, formatter: function (value) { return $("#sel_areacodes").getMultiSelectTextByValue(value) } },
                { title: "配送倉別", field: "Facility", sortable: true },
                { title: "簽單備註", field: "SdnNotes", sortable: true },
                { title: "簽單狀態", field: "ConfirmNotes", sortable: true },
                { title: "維護者", field: "SdnSendWho", sortable: true, formatter: function (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value) } }
            ],
            editButtons: { 
                id: "",
                events: {}
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    dataBinder: {
                        map: {
                        },
                        bootstrapTable: ''
                    },
                    url: 'api/ReportSDNReturnList/RetrieveDetail?TMSKey=' + row.TMSKey,
                    smartTable: {
                        checkboxHeader: false,
                        showExport: false, 
                        showColumns: true,
                        showRefresh: true,
                        showToggle: true,
                        checkbox: false,
                        queryButton: '#none',
                        toolbar: "",
                        search: false,
                        paginationHAlign: "left",
                        pagination: true, //顯示分頁
                        sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
                        pageSize: 10,
                        pageList: [10, 20, 50], //分頁筆數
                        toolbar: '',
                        height: "",
                        columns: [
                            { title: "路線編號", field: "RouteNo", sortable: false },
                            { title: "車次", field: "Sequence", sortable: false },
                            { title: "訂單號碼", field: "TMSKey", sortable: false },
                            { title: "貨主單號", field: "ExternOrderKey", sortable: false },
                            { title: "來源倉別", field: "FromFacility", sortable: false },
                            { title: "目的倉別", field: "ToFacility", sortable: false },
                            { title: "預定寄送日", field: "ScheduleDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } }, 
                            { title: "來源地址", field: "StartAddress", sortable: false },
                            { title: "目的地址", field: "EndAddress", sortable: false },
                            { title: "司機", field: "Driver", sortable: false },
                            { title: "車號", field: "VehicleKey", sortable: false },
                            { title: "貨運公司", field: "Company", sortable: true,},
                            { title: "路編狀態", field: "Status", sortable: false },
                            { title: "路編建立時間", field: "AddDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "路編建立人", field: "AddWho", sortable: false, formatter: function (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value) } }
                        ],
                        editButtons: { 
                            id: "",
                            events: {}
                        }
                    }
                })
            }
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

        $("#sel_company").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇貨運公司',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_company").multiselect("setOptions", ConfigurationSet);
        $("#sel_company").multiselect("rebuild");

        $("#sel_facility").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇配送倉別',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_facility").multiselect("setOptions", ConfigurationSet);
        $("#sel_facility").multiselect("rebuild");

        $("#sel_orderstatus").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇簽單狀態',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_orderstatus").multiselect("setOptions", ConfigurationSet);
        $("#sel_orderstatus").multiselect("rebuild");

    };  
     
    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportsdnreturnlist-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋回單檢核表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportSDNReturnList/ExportExcel',
            queryParams: queryParams,
            FileName: '回單檢核表',
            Options: options,
            Sheets: [{
                SheetName: "Header",
                sortName: 'StorerKey',
                Columns: [
                    { title: "貨主代號", field: "StorerKey" },
                    { title: "通路別", field: "Channel" },
                    { title: "客戶全名", field: "FullName" },
                    { title: "客戶簡稱", field: "ShortName" },
                    { title: "到貨日", field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "訂單類別", field: "OrderType" },
                    { title: "訂單號碼", field: "TMSKey" },
                    { title: "貨主單號", field: "ExternOrderKey" },
                    { title: "異常狀況", field: "SignStatus" },
                    { title: "採購單號", field: "Customerorderkey" },
                    { title: "發票退回", field: "Invback" },
                    { title: "簽單確認時間", field: "SdnSendDate"},
                    { title: "區碼", field: "AreaCode" },
                    { title: "配送倉別", field: "Facility" },
                    { title: "簽單備註", field: "SdnNotes" },
                    { title: "簽單狀態", field: "ConfirmNotes" },
                    { title: "維護者", field: "SdnSendWho" }
                ]
            },
            {
                SheetName: "Detail",
                sortName: 'ExternOrderKey',
                Columns: [
                    { title: "路線編號", field: "RouteNo" },
                    { title: "車次", field: "Sequence" },
                    { title: "訂單號碼", field: "TMSKey" },
                    { title: "貨主單號", field: "ExternOrderKey" },
                    { title: "通路別", field: "Channel" },
                    { title: "客戶簡稱", field: "ShortName" },
                    { title: "來源倉別", field: "FromFacility" },
                    { title: "目的倉別", field: "ToFacility" },
                    { title: "預定寄送日", field: "ScheduleDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "來源地址", field: "StartAddress" },
                    { title: "目的地址", field: "EndAddress" },
                    { title: "司機", field: "Driver" },
                    { title: "車號", field: "VehicleKey" },
                    { title: "貨運公司", field: "Company" },
                    { title: "路編狀態", field: "Status" },
                    { title: "簽單確認時間", field: "SdnSendDate"},
                    { title: "維護者", field: "SdnSendWho" },
                    { title: "路編建立時間", field: "AddDate"},
                    { title: "路編建立人", field: "AddWho" }
                ]
            },
            ]
        });       
    })

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
