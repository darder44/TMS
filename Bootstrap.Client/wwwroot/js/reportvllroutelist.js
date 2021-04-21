$(function () {
    InitialSelect();
    
    var queryParams = function (params) {
        return $.extend(params,
            {
                DoRouteDate_Start: $("#txt_VLL_DoRouteDate_Start").val(),
                DoRouteDate_End: $("#txt_VLL_DoRouteDate_End").val(),
                RouteNo_Start: $("#txt_RouteNo_Start").val(),
                RouteNo_End: $("#txt_RouteNo_End").val(),
                WaveKey: $("#txt_WaveKey").val(),                   
            });
    };
    $('#reportvllroutelist-table').lgbTable({
        //url: 'api/ReportVLLRouteList',
        //資料繫結
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reportvllroutelist-table',
            click: {
                '#btn_query': function () {
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportVLLRouteList/RetrieveData'), pageNumber: 1 })
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
            checkbox: true,
            detailView: true,
            showAdvancedSearchButton: true,
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "出車日期", field: "ExpectDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "列印次數", field: "VLListCount", sortable: true },
                { title: "列印時間", field: "VLListPrintDate", sortable: true },
                { title: "車牌號碼", field: "VehicleKey", sortable: true },
                { title: "駕駛人", field: "Driver", sortable: true },
                { title: "車次", field: "DriveTimes", sortable: true },
                { title: "運輸公司", field: "CompanyKey", sortable: true },
            ],
            editButtons: {
                events: {
                    'click .printReport': function (e, value, row, index) {
                        $('#reportvllroutelist-table').bootstrapTable('uncheckAll');     
                        $('#reportvllroutelist-table').bootstrapTable('check', index);  
                        $("#dialogPrint").modal("show");                       
                    },
                }
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    url: 'api/ReportVLLRouteList/RetrievesDetail?RouteNo=' + row.RouteNo,
                    dataBinder: {
                        map: {
                        },
                        bootstrapTable: ''
                    },
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
                            { title: "路線編號", field: "RouteNo", sortable: true },
                            { title: "WaveKey", field: "WaveKey", sortable: true },
                            { title: "排車箱數", field: "CaseQty", sortable: true },
                            { title: "排車個數", field: "OrderQty", sortable: true },
                            { title: "排車總個數", field: "TotalQty", sortable: true },
                            { title: "揀貨箱數", field: "ShipCaseQty", sortable: true },
                            { title: "揀貨個數", field: "ShipQty", sortable: true },
                            { title: "揀貨總個數", field: "TotalShipQty", sortable: true },
                            { title: "排車材積", field: "Cube", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                            { title: "排車重量", field: "Weight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
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

    //出貨單 reportshipment
    var $reportshipment = $('#reportshipment-table');
    $reportshipment.lgbTable({        
        //資料繫結
        dataBinder: {
            map: {
            },
            bootstrapTable: '#reportshipment-table',
            click: {
                '#btn_query': function () {},
                '#btn_query-reportshipment': function () {
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportShipment/RetrieveData'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                },
                '#btn_reset': function () {},
                '#btn_reset-reportshipment': $.bootstrapTableResetBtn
            },
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false, //匯出功能
            showColumns: true, //使用者可選擇想看到的欄位
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: true,
            toolbar: '#toolbar-reportshipment',
            showAdvancedSearchButton: true,
            advancedSearchModal: '#reportshipment-dialogAdvancedSearch',
            queryButton: '',
            //查詢參數
            queryParams: function (params) {
                return $.extend(params,
                    {
                        StorerKey: function () {
                            var values = [];
                            $("#sel_storerkey option:selected").each(function () {
                                var value = $(this).val();
                                values.push(value);
                            });                        
                            return values.join(",");
                        },               
                        RouteNo: $("#txt_RouteNo").val(),
                        DoRouteDate_Start: $("#txt_DoRouteDate_Start").val(),
                        DoRouteDate_End: $("#txt_DoRouteDate_End").val(),
                        DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(),
                        DeliveryDate_End: $("#txt_DeliveryDate_End").val(),
                    });
            },
            //Body欄位顯示
            columns: [ 
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "司機", field: "Driver", sortable: true },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "出貨箱數", field: "ShipCaseQty", sortable: true },
                { title: "出貨個數", field: "ShipQty", sortable: true },
                { title: "訂單數量", field: "OrderQty", sortable: true },
                { title: "材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} }
            ],
            editButtons: {
                id: "#tableButtons_ship",
                events: {
                    'click .printReport': function (e, value, row, index) {
                        $reportshipment.bootstrapTable('uncheckAll');     
                        $reportshipment.bootstrapTable('check', index);  
                        ReportShipList([row]);                        
                    },
                }
            },
        }
    });

    //VLL裝載總表 報表列印
    $("#btn_printReport").click(function () {
        var selections = $('#reportvllroutelist-table').bootstrapTable('getSelections');
        if (selections.length == 0) {
            lgbSwal({ title: "請選擇要列印的報表", type: "warning" });
            return;
        }
        $("#dialogPrint").modal("show");
    });

    $("#btnSubmit_PrintMode").click(function () {
        var selects = $('#reportvllroutelist-table').bootstrapTable('getSelections');
        var printmode = $("#txt_PrintMode").val();
        PrintReport(selects, printmode);
    })

    function PrintReport(rows, printmode) {
        var routes = rows.map(function (row) { return row.RouteNo }).join(",");
        window.open($.formatUrl($.format("Reports/Report/{0}?RouteNo={1}", printmode, routes)));
    }

    //出貨單 報表列印
    $("#btn_printReport_ship").click(function () {
        var selections = $reportshipment.bootstrapTable('getSelections');
        if (selections.length == 0) {
            lgbSwal({ title: "請選擇要列印的報表", type: "warning" });
            return;
        }
        ReportShipList(selections);
    });

    function ReportShipList(rows) {
        var routes = rows.map(function (row) { return row.RouteNo }).join(",");
        var routenos = rows.map(function (row) { return row.RouteNo });
        $.bc({
            url: 'api/ReportShipment/RetrievesByReportShipList',
            method: 'post',
            data: routenos,
            callback: function (result) {
                if (!result) {
                    lgbSwal({ title: "無法取得報表資訊", type: "warning" });
                    return;
                }
                var storers = result;
                $.bc({
                    url: 'api/ReportVLLRouteList/SelectTMSKeyAndOrderTypeByRouteNo',
                    method: 'post',
                    data: routenos,
                    callback: function (result) {
                        if (!result) {
                            lgbSwal({ title: "無法取得報表資訊", type: "warning" });
                            return;
                        }
                        //取得貨主                
                        storers.forEach(function (storer) {              
                            //取得貨主指定報表檔名   
                            var ShipListReport = storer.ShipListReport; 
                            //取得貨主訂單
                            var receipts = result.filter(function (row) { return row.StorerKey.trim() == storer.StorerKey.trim() });
                            var handmades = [];
                            var tmskeys = "";
                            if (storer.StorerKey == "LIAH01") {
                                //取得手開單
                                handmades = receipts.filter(function (row) { return row.UpdateSource.trim() == "手開單" || row.UpdateSource.trim() == "Excel匯入" });
                                //取得非手開單訂單
                                receipts = receipts.filter(function (row) { return row.UpdateSource.trim() != "手開單" && row.UpdateSource.trim() != "Excel匯入" });
                            }
                            if (receipts.length > 0) {
                                tmskeys = receipts.map(function (row) { return row.TMSKey });
                                window.open($.formatUrl($.format('Reports/Report/ReportShipList?RouteNo={0}&TMSKey={1}&ShipListReport={2}', routes, tmskeys, ShipListReport)));
                            }
                            if (handmades.length > 0 && storer.StorerKey == "LIAH01") {
                                tmskeys = handmades.map(function (row) { return row.TMSKey });
                                ShipListReport = "STDShipList";
                                window.open($.formatUrl($.format('Reports/Report/ReportShipList?RouteNo={0}&TMSKey={1}&ShipListReport={2}', routes, tmskeys, ShipListReport)));
                            }
                        });
                    }
                });
            }
        })
    }

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportvllroutelist-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋裝載總表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: 'api/ReportVLLRouteList/ExportExcel',
            queryParams: queryParams,
            FileName: '裝載總表',
            Options: options,
            Sheets: [{
                SheetName: "ReportVLLRouteList",
                sortName: 'RouteNo',
                Columns: [
                    { title: "路線編號", field: "RouteNo", sortable: true },
                    { title: "WaveKey", field: "WaveKey", sortable: true },
                    { title: "出車日期", field: "ExpectDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "車牌號碼", field: "VehicleKey", sortable: true },
                    { title: "駕駛人", field: "Driver", sortable: true },
                    { title: "車次", field: "DriveTimes", sortable: true },
                    { title: "運輸公司", field: "CompanyKey", sortable: true },
                    { title: "排車箱數", field: "CaseQty", sortable: true, dataType: 1 },
                    { title: "排車個數", field: "OrderQty", sortable: true, dataType: 1 },
                    { title: "排車總個數", field: "TotalQty", sortable: true, dataType: 1 },
                    { title: "揀貨箱數", field: "ShipCaseQty", sortable: true, dataType: 1 },
                    { title: "揀貨個數", field: "ShipQty", sortable: true, dataType: 1 },
                    { title: "揀貨總個數", field: "ShipCaseQty", sortable: true, dataType: 1 },
                    { title: "排車材積", field: "Cube", sortable: true , dataType: 2, dataFormat: '0.#####' },
                    { title: "排車重量", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' }
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

});
