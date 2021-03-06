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
            singleSelect: false, //True?????????, False?????????
            showExport: false, //????????????
            showColumns: true, //????????????????????????????????????
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: false,
            search: true,
            detailView: true,
            showAdvancedSearchButton: true,
            queryParams: queryParams,
            columns: [
                { title: "????????????", field: "StorerKey", sortable: true },
                { title: "?????????", field: "Channel", sortable: true },
                { title: "????????????", field: "FullName", sortable: true },
                { title: "????????????", field: "ShortName", sortable: true },
                { title: "?????????", field: "DeliveryDate", sortable: true,formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }  },
                { title: "????????????", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                { title: "????????????", field: "ExternOrderKey", sortable: true },
                { title: "????????????", field: "TMSKey", sortable: true },
                { title: "????????????", field: "SignStatus", sortable: true },
                { title: "????????????", field: "Customerorderkey", sortable: true },
                { title: "????????????", field: "Invback", sortable: true },
                { title: "??????????????????", field: "SdnSendDate", sortable: true },
                { title: "??????", field: "AreaCode", sortable: true, formatter: function (value) { return $("#sel_areacodes").getMultiSelectTextByValue(value) } },
                { title: "????????????", field: "Facility", sortable: true },
                { title: "????????????", field: "SdnNotes", sortable: true },
                { title: "????????????", field: "ConfirmNotes", sortable: true },
                { title: "?????????", field: "SdnSendWho", sortable: true, formatter: function (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value) } }
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
                        pagination: true, //????????????
                        sidePagination: 'client', //???????????? client ??????bootstrapTable(load, rows), server?????? url
                        pageSize: 10,
                        pageList: [10, 20, 50], //????????????
                        toolbar: '',
                        height: "",
                        columns: [
                            { title: "????????????", field: "RouteNo", sortable: false },
                            { title: "??????", field: "Sequence", sortable: false },
                            { title: "????????????", field: "TMSKey", sortable: false },
                            { title: "????????????", field: "ExternOrderKey", sortable: false },
                            { title: "????????????", field: "FromFacility", sortable: false },
                            { title: "????????????", field: "ToFacility", sortable: false },
                            { title: "???????????????", field: "ScheduleDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } }, 
                            { title: "????????????", field: "StartAddress", sortable: false },
                            { title: "????????????", field: "EndAddress", sortable: false },
                            { title: "??????", field: "Driver", sortable: false },
                            { title: "??????", field: "VehicleKey", sortable: false },
                            { title: "????????????", field: "Company", sortable: true,},
                            { title: "????????????", field: "Status", sortable: false },
                            { title: "??????????????????", field: "AddDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "???????????????", field: "AddWho", sortable: false, formatter: function (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value) } }
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

        $("#sel_company").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '?????????????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_company").multiselect("setOptions", ConfigurationSet);
        $("#sel_company").multiselect("rebuild");

        $("#sel_facility").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '?????????????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_facility").multiselect("setOptions", ConfigurationSet);
        $("#sel_facility").multiselect("rebuild");

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

    };  
     
    //??????EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportsdnreturnlist-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "???????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }
        ExcelExport({
            url: 'api/ReportSDNReturnList/ExportExcel',
            queryParams: queryParams,
            FileName: '???????????????',
            Options: options,
            Sheets: [{
                SheetName: "Header",
                sortName: 'StorerKey',
                Columns: [
                    { title: "????????????", field: "StorerKey" },
                    { title: "?????????", field: "Channel" },
                    { title: "????????????", field: "FullName" },
                    { title: "????????????", field: "ShortName" },
                    { title: "?????????", field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "????????????", field: "OrderType" },
                    { title: "????????????", field: "TMSKey" },
                    { title: "????????????", field: "ExternOrderKey" },
                    { title: "????????????", field: "SignStatus" },
                    { title: "????????????", field: "Customerorderkey" },
                    { title: "????????????", field: "Invback" },
                    { title: "??????????????????", field: "SdnSendDate"},
                    { title: "??????", field: "AreaCode" },
                    { title: "????????????", field: "Facility" },
                    { title: "????????????", field: "SdnNotes" },
                    { title: "????????????", field: "ConfirmNotes" },
                    { title: "?????????", field: "SdnSendWho" }
                ]
            },
            {
                SheetName: "Detail",
                sortName: 'ExternOrderKey',
                Columns: [
                    { title: "????????????", field: "RouteNo" },
                    { title: "??????", field: "Sequence" },
                    { title: "????????????", field: "TMSKey" },
                    { title: "????????????", field: "ExternOrderKey" },
                    { title: "?????????", field: "Channel" },
                    { title: "????????????", field: "ShortName" },
                    { title: "????????????", field: "FromFacility" },
                    { title: "????????????", field: "ToFacility" },
                    { title: "???????????????", field: "ScheduleDate", dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "????????????", field: "StartAddress" },
                    { title: "????????????", field: "EndAddress" },
                    { title: "??????", field: "Driver" },
                    { title: "??????", field: "VehicleKey" },
                    { title: "????????????", field: "Company" },
                    { title: "????????????", field: "Status" },
                    { title: "??????????????????", field: "SdnSendDate"},
                    { title: "?????????", field: "SdnSendWho" },
                    { title: "??????????????????", field: "AddDate"},
                    { title: "???????????????", field: "AddWho" }
                ]
            },
            ]
        });       
    })

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
