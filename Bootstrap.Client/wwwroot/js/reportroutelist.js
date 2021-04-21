$(function () {
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
                DoRouteDateS: $("#txt_DoRouteDateS").val(),
                DoRouteDateE: $("#txt_DoRouteDateE").val(),
                DeliveryDateS: $("#txt_DeliveryDateS").val(),
                DeliveryDateE: $("#txt_DeliveryDateE").val(),
                RouteNo: $("#txt_RouteNo").val()                  
            });
    };
    $('#reportroutelist-table').lgbTable({
        //url: 'api/ReportRouteList',
        //資料繫結
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reportroutelist-table',
            click: {
                '#btn_query': function () {
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportRouteList'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                }
            },
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false, //匯出功能
            showColumns: true, //使用者可選擇想看到的欄位
            showRefresh: true,
            showToggle: true,
            sortName: 'RouteNo',
            checkbox: false,            
            showAdvancedSearchButton: true,
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "預計報到時間", field: "ExpectDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true },
                { title: "運送區域", field: "AreaDescription", sortable: true },
                { title: "暫存區", field: "DockNo", sortable: true },
                { title: "縣市別", field: "City", sortable: true },
                { title: "貨運公司", field: "CompanyName", sortable: true },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "車次", field: "DriveTimes", sortable: true },
                { title: "一單多車", field: "MultiVehicle", sortable: true },
                { title: "司機", field: "Driver", sortable: true },
                { title: "可載重量", field: "LoadingWeight", sortable: true  , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "可載材積", field: "LoadingCube", sortable: true  , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "運送點數", field: "CountOrders", sortable: true },
                { title: "運送箱數", field: "ShipCaseQty", sortable: true },
                { title: "運送個數", field: "ShipQty", sortable: true },
                { title: "運送板數", field: "ShipPalletQty", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "運送重量", field: "ShipWeight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "運送材積", field: "ShipCube", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "貨運公司代碼", field: "CompanyKey", sortable: true },
                { title: "備註", field: "Notes", sortable: true}
                
            ],
            editButtons: {
                events: {
                    'click .printReport': function (e, value, row, index) {
                        $('#reportroutelist-table').bootstrapTable('uncheckAll');     
                        $('#reportroutelist-table').bootstrapTable('check', index);  
                        $("#dialogPrint").modal("show");                       
                    },
                }
            },
        }
    });

    //分貨單 reportgooddistribution
    var $reportgooddistribution = $('#reportgooddistribution-table');
    $reportgooddistribution.lgbTable({        
        //資料繫結
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reportgooddistribution-table',
            click: {
                '#btn_query': function () {},
                '#btn_query-reportgooddistribution': function () {
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportGoodDistribution'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                },
                '#btn_reset': function () {},
                '#btn_reset-reportgooddistribution': $.bootstrapTableResetBtn
            },
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false, //匯出功能
            showColumns: true, //使用者可選擇想看到的欄位
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: false,
            toolbar: '#toolbar-reportgooddistribution',
            showAdvancedSearchButton: true,
            advancedSearchModal: '#reportgooddistribution-dialogAdvancedSearch',
            queryButton: '',
            //查詢參數
            queryParams: function (params) {
                return $.extend(params,
                    {
                        DoRouteDateS: $("#txt_GDoRouteDateS").val(),
                        DoRouteDateE: $("#txt_GDoRouteDateE").val(),
                        DeliveryDateS: $("#txt_GDeliveryDateS").val(),
                        DeliveryDateE: $("#txt_GDeliveryDateE").val(),
                        RouteNo: $("#txt_GRouteNo").val()
                    });
            },
            //Body欄位顯示
            columns: [ 
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "預計報到時間", field: "ExpectDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日期", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true},
                { title: "區域", field: "AreaDescription", sortable: true},
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "品號", field: "Sku", sortable: true },
                { title: "品名", field: "Descr", sortable: true },
                { title: "運送箱數", field: "ShipCaseQty", sortable: true},
                { title: "運送個數", field: "ShipQty", sortable: true},
                { title: "運送板數", field: "ShipPalletQty", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "運送重量", field: "ShipWeight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "運送材積", field: "ShipCube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}}
            ],
            editButtons: {
                id: "#tableButtons_ship",
                events: {
                    'click .printReport': function (e, value, row, index) {
                        $reportgooddistribution.bootstrapTable('uncheckAll');     
                        $reportgooddistribution.bootstrapTable('check', index);  
                        ReportShipList([row]);                        
                    },
                }
            },
        }
    });

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportroutelist-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportRouteList/ExportExcel',
            queryParams: queryParams,            
            FileName: '排車一覽表',
            Options: options,
            Sheets: [{
                SheetName: "ReportRouteList",
                sortName: 'RouteNo',
                Columns: [
                    { title: "貨主", field: "StorerKey", sortable: true },
                    { title: "出車日期", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "預計報到時間", field: "ExpectDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "到貨日期", field: "DeliveryDate", sortable: true,  dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "路線編號", field: "RouteNo", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true },
                    { title: "運送區域", field: "AreaDescription", sortable: true },
                    { title: "暫存區", field: "DockNo", sortable: true },
                    { title: "縣市別", field: "City", sortable: true },
                    { title: "貨運公司", field: "CompanyName", sortable: true },
                    { title: "車號", field: "VehicleKey", sortable: true },
                    { title: "車次", field: "DriveTimes", sortable: true },
                    { title: "一單多車", field: "MultiVehicle", sortable: true },
                    { title: "司機", field: "Driver", sortable: true },
                    { title: "可載重量", field: "LoadingWeight", sortable: true  , dataType: 2, dataFormat: '0.#####'},
                    { title: "可載材積", field: "LoadingCube", sortable: true  ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送點數", field: "CountOrders", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送箱數", field: "ShipCaseQty", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送個數", field: "ShipQty", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送板數", field: "ShipPalletQty", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送重量", field: "ShipWeight", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "運送材積", field: "ShipCube", sortable: true ,  dataType: 2, dataFormat: '0.#####'},
                    { title: "貨運公司代碼", field: "CompanyKey", sortable: true },
                    { title: "備註", field: "Notes", sortable: true}
                ]
            }]
        });      
    })


     //轉出EXCEL
     $("#btn_export_Batch").click(function () {
        var options = $('#reportgooddistribution-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportGoodDistribution/ExportExcel',
            queryParams: queryParams,
            FileName: '分貨表',
            Options: options,
            Sheets: [{
                SheetName: "ReportGoodDistribution",
                sortName: 'RouteNo',
                Columns: [
                    { title: "出車日期", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "預計報到時間", field: "ExpectDate", sortable: true, sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "到貨日期", field: "DeliveryDate", sortable: true , sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'},
                    { title: "路線編號", field: "RouteNo", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true},
                    { title: "區域", field: "AreaDescription", sortable: true},
                    { title: "車號", field: "VehicleKey", sortable: true },
                    { title: "品號", field: "Sku", sortable: true },
                    { title: "品名", field: "Descr", sortable: true },
                    { title: "運送箱數", field: "ShipCaseQty", sortable: true},
                    { title: "運送個數", field: "ShipQty", sortable: true},
                    { title: "運送板數", field: "ShipPalletQty", sortable: true , dataType: 2, dataFormat: '0.#####'},
                    { title: "運送重量", field: "ShipWeight", sortable: true, dataType: 2, dataFormat: '0.#####'},
                    { title: "運送材積", field: "ShipCube", sortable: true, dataType: 2, dataFormat: '0.#####'}
                ]
            }]
        });      
    })


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
    };  
    InitialSelect();
});
