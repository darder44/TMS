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
                { title: "????????????", field: "ConsigneeKey", sortable: true },
                { title: "????????????", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "????????????", field: "OrderStatus", sortable: true },
                { title: "??????", field: "AreaCode", sortable: true },
                { title: "??????", field: "VehicleKey", sortable: true },
                { title: "??????", field: "Driver", sortable: true },
                { title: "????????????", field: "RouteNo", sortable: true },
                { title: "????????????", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "????????????", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "????????????", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "????????????", field: "SignDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "??????", field: "StorerKey", sortable: true },
                { title: "WaveKey", field: "WaveKey", sortable: true },
                { title: "????????????", field: "TMSKey", sortable: true },
                { title: "????????????", field: "ExternOrderKey", sortable: true },
                { title: "????????????", field: "ShortName", sortable: true },
                { title: "????????????", field: "SoldTo", sortable: true },
                { title: "??????????????????", field: "SoldToName", sortable: true },
                { title: "????????????", field: "ShipTo", sortable: true },
                { title: "??????????????????", field: "ShipToName", sortable: true },
                { title: "????????????", field: "StartAddress", sortable: true },
                { title: "????????????", field: "EndAddress", sortable: true },
                { title: "??????", field: "City", sortable: true },
                { title: "????????????", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "OrderQty", sortable: true },
                { title: "???????????????", field: "TotalQty", sortable: true },
                { title: "????????????", field: "ShipCaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "ShipQty", sortable: true },
                { title: "???????????????", field: "TotalShipQty", sortable: true },
                { title: "???????????????", field: "SignQty", sortable: true },
                { title: "?????????", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "?????????", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "??????", field: "Notes", sortable: true },
                { title: "??????", field: "OnTime", sortable: true },
                { title: "??????", field: "Delay", sortable: true },
                { title: "????????????", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } }
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
            selectAllText: '??????',
            nonSelectedText: '???????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey").multiselect("rebuild");

        $("#sel_ordertypes").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '?????????????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_ordertypes").multiselect("setOptions", ConfigurationSet);
        $("#sel_ordertypes").multiselect("rebuild");

        $("#sel_orderstatus").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '?????????????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_orderstatus").multiselect("setOptions", ConfigurationSet);
        $("#sel_orderstatus").multiselect("rebuild");

        $("#sel_areacodes").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '???????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_areacodes").multiselect("setOptions", ConfigurationSet);
        $("#sel_areacodes").multiselect("rebuild");
    };  

    //??????EXCEL
    $("#btn_export").click(function () {
        var options = $reoporttrptrack.bootstrapTable('getOptions');
        var url = options.url;
        if (!url || url === "") {
            swal({ title: "???????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }
        ExcelExport({
            url: 'api/ReportTRPTrack/ExportExcel',
            queryParams: queryParams,
            FileName: '???????????????',
            Options: options,
            Sheets: [
                {
                    SheetName: "ReportTRPTrack",
                    sortName: 'ConsigneeKey',
                    Columns: [
                        { title: "????????????", field: "ConsigneeKey", sortable: true },
                        { title: "????????????", field: "OrderType", sortable: true },
                        { title: "????????????", field: "OrderStatus", sortable: true },
                        { title: "??????", field: "AreaCode", sortable: true },
                        { title: "??????", field: "VehicleKey", sortable: true },
                        { title: "??????", field: "Driver", sortable: true },
                        { title: "????????????", field: "OrderDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "SignDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "??????", field: "StorerKey", sortable: true },
                        { title: "????????????", field: "ShortName", sortable: true },
                        { title: "????????????", field: "SoldTo", sortable: true },
                        { title: "??????????????????", field: "SoldToName", sortable: true },
                        { title: "????????????", field: "ShipTo", sortable: true },
                        { title: "??????????????????", field: "ShipToName", sortable: true },
                        { title: "??????", field: "City", sortable: true },
                        { title: "????????????", field: "PalletQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "????????????", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "????????????", field: "OrderQty", sortable: true, dataType: 1 },
                        { title: "???????????????", field: "TotalQty", sortable: true, dataType: 1 },
                        { title: "????????????", field: "ShipQty", sortable: true, dataType: 1 },
                        { title: "????????????", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "???????????????", field: "TotalShipQty", sortable: true, dataType: 1 },
                        { title: "?????????", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "?????????", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "???????????????", field: "SignQty", sortable: true, dataType: 1 },
                        { title: "??????", field: "OnTime", sortable: true },
                        { title: "??????", field: "Delay", sortable: true },
                        { title: "????????????", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' }
                    ]
                },
                {
                    SheetName: "Detail",
                    sortName: 'ConsigneeKey',
                    Columns: [
                        { title: "????????????", field: "ConsigneeKey", sortable: true },
                        { title: "????????????", field: "OrderType", sortable: true },
                        { title: "????????????", field: "OrderStatus", sortable: true },
                        { title: "??????", field: "AreaCode", sortable: true },
                        { title: "??????", field: "VehicleKey", sortable: true },
                        { title: "??????", field: "Driver", sortable: true },
                        { title: "????????????", field: "RouteNo", sortable: true },
                        { title: "????????????", field: "OrderDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "DoRouteDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "????????????", field: "SignDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "??????", field: "StorerKey", sortable: true },
                        { title: "WaveKey", field: "WaveKey", sortable: true },
                        { title: "????????????", field: "TMSKey", sortable: true },
                        { title: "????????????", field: "ExternOrderKey", sortable: true },
                        { title: "????????????", field: "ShortName", sortable: true },
                        { title: "????????????", field: "SoldTo", sortable: true },
                        { title: "??????????????????", field: "SoldToName", sortable: true },
                        { title: "????????????", field: "ShipTo", sortable: true },
                        { title: "??????????????????", field: "ShipToName", sortable: true },
                        { title: "????????????", field: "StartAddress", sortable: true },
                        { title: "????????????", field: "EndAddress", sortable: true },
                        { title: "??????", field: "City", sortable: true },
                        { title: "????????????", field: "PalletQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "????????????", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "????????????", field: "OrderQty", sortable: true, dataType: 1 },
                        { title: "???????????????", field: "TotalQty", sortable: true, dataType: 1 },
                        { title: "????????????", field: "ShipQty", sortable: true, dataType: 1 },
                        { title: "????????????", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "???????????????", field: "TotalShipQty", sortable: true, dataType: 1 },
                        { title: "?????????", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "?????????", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                        { title: "???????????????", field: "SignQty", sortable: true, dataType: 1 },
                        { title: "??????", field: "Notes", sortable: true },
                        { title: "??????", field: "OnTime", sortable: true },
                        { title: "??????", field: "Delay", sortable: true },
                        { title: "????????????", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' }
                    ]
                }
            ]
        });       
    });

    //??????????????????
    $.fetchDictionary("OrderType", function (result) {
        if (!result) {
            swal({ title: "??????????????????????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }
        if (result.length == 0) {
            swal({ title: "????????????????????????????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }

        var data = result.map(function (row) {
            return {
                value: $.trim(row.Code),
                label: row.Code + "???" + row.Description,
            }
        });
        
        $("#sel_ordertypes").multiselect('dataprovider', data);
    });
});