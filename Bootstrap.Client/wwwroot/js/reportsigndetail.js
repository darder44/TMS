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
                { title: "????????????", field: "RouteNo", sortable: true },
                { title: "??????", field: "VehicleKey", sortable: true },
                { title: "??????", field: "Driver", sortable: true },
                { title: "????????????", field: "ShippingCompany", sortable: true },
                { title: "????????????", field: "DoRouteDate", sortable: true },
                { title: "????????????", field: "DeliveryDate", sortable: true },
                { title: "??????", field: "StorerKey", sortable: true },
                { title: "????????????", field: "StorerName", sortable: true },
                { title: "??????", field: "Notes", sortable: true },
                { title: "????????????", field: "ConsigneeKey", sortable: true },
                { title: "????????????", field: "ShortName", sortable: true },
                { title: "????????????", field: "Zip", sortable: true },
                { title: "??????", field: "AreaCode", sortable: true },
                { title: "????????????", field: "ShipToAddress", sortable: true },
                { title: "????????????", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "????????????", field: "OrderDate", sortable: true },
                { title: "????????????", field: "ExternOrderKey", sortable: true },
                { title: "????????????", field: "ConFirmDate", sortable: true },
                { title: "??????", field: "ConfirmNotes", sortable: true },
                { title: "??????????????????", field: "CustomerOrderKey", sortable: true },
                { title: "??????????????????", field: "SdnSendDate", sortable: true },
                { title: "????????????????????????", field: "CustHandle", sortable: true },
                { title: "????????????", field: "SDNNotes", sortable: true },
                { title: "????????????", field: "InvBack", sortable: true },
                { title: "????????????", field: "SDNBack", sortable: true },
                { title: "????????????", field: "TMSKey", sortable: true },
                { title: "??????", field: "OrderLineNumber", sortable: true },
                { title: "??????", field: "Sku", sortable: true },
                { title: "??????", field: "Descr", sortable: true },
                { title: "??????", field: "Uom", sortable: true },
                { title: "?????????", field: "OrderQty", sortable: true },
                { title: "????????????", field: "CaseQty", sortable: true },
                { title: "?????????", field: "ShipQty", sortable: true },
                { title: "????????????", field: "ShipCaseQty", sortable: true },
                { title: "?????????", field: "SignQty", sortable: true },
                { title: "????????????", field: "SignCaseQty", sortable: true },
                { title: "???????????????", field: "CaseCnt", sortable: true },
                { title: "????????????", field: "STDCUBE", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "STDGROSSWGT", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "???????????????", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "???????????????", field: "ShipCube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "???????????????", field: "SignCube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "ShipWeight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "SignWeighr", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "?????????", field: "RSC", sortable: true },
                { title: "?????????", field: "RBC", sortable: true }
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
    };  

    //??????EXCEL
    $("#btn_export").click(function () {
        var options = $reportsigndetail.bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "??????????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }
        ExcelExport({
            url: 'api/ReportSignDetail/ExportExcel',
            queryParams: queryParams,
            FileName: '??????????????????',
            Options: options,
            Sheets: [{
                SheetName: "ReportSignDetail",
                sortName: 'RouteNo',
                Columns: [
                    { title: "????????????", field: "RouteNo", sortable: true },
                    { title: "??????", field: "VehicleKey", sortable: true },
                    { title: "??????", field: "Driver", sortable: true },
                    { title: "????????????", field: "ShippingCompany", sortable: true },
                    { title: "????????????", field: "DoRouteDate", sortable: true },
                    { title: "????????????", field: "DeliveryDate", sortable: true },
                    { title: "??????", field: "StorerKey", sortable: true },
                    { title: "????????????", field: "StorerName", sortable: true },
                    { title: "??????", field: "Notes", sortable: true },
                    { title: "????????????", field: "ConsigneeKey", sortable: true },
                    { title: "????????????", field: "ShortName", sortable: true },
                    { title: "????????????", field: "Zip", sortable: true },
                    { title: "??????", field: "AreaCode", sortable: true },
                    { title: "????????????", field: "ShipToAddress", sortable: true },
                    { title: "????????????", field: "OrderType", sortable: true },
                    { title: "????????????", field: "OrderDate", sortable: true },
                    { title: "????????????", field: "ExternOrderKey", sortable: true },
                    { title: "????????????", field: "ConFirmDate", sortable: true },
                    { title: "??????", field: "ConfirmNotes", sortable: true },
                    { title: "??????????????????", field: "CustomerOrderKey", sortable: true },
                    { title: "??????????????????", field: "SdnSendDate", sortable: true },
                    { title: "????????????????????????", field: "CustHandle", sortable: true },
                    { title: "????????????", field: "SDNNotes", sortable: true },
                    { title: "????????????", field: "InvBack", sortable: true },
                    { title: "????????????", field: "SDNBack", sortable: true },
                    { title: "????????????", field: "TMSKey", sortable: true },
                    { title: "??????", field: "OrderLineNumber", sortable: true },
                    { title: "??????", field: "Sku", sortable: true },
                    { title: "??????", field: "Descr", sortable: true },
                    { title: "??????", field: "Uom", sortable: true },
                    { title: "?????????", field: "OrderQty", sortable: true, dataType: 1 },
                    { title: "????????????", field: "CaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "?????????", field: "ShipQty", sortable: true, dataType: 1 },
                    { title: "????????????", field: "ShipCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "?????????", field: "SignQty", sortable: true, dataType: 1 },
                    { title: "????????????", field: "SignCaseQty", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "???????????????", field: "CaseCnt", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "????????????", field: "STDCUBE", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "????????????", field: "STDGROSSWGT", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "???????????????", field: "Cube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "???????????????", field: "ShipCube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "???????????????", field: "SignCube", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "????????????", field: "Weight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "????????????", field: "ShipWeight", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "????????????", field: "SignWeighr", sortable: true, dataType: 2, dataFormat: '0.#####' },
                    { title: "?????????", field: "RSC", sortable: true },
                    { title: "?????????", field: "RBC", sortable: true }
                ]
            }]
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