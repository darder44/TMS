$(function () {
    var queryParams_Temp = function (params) {
        return $.extend(params,
            {
                StorerKeys: function () {
                    var values = [];
                    $("#sel_storerkey_temp option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                ConsigneeKey: $("#txt_ConsigneeKey_Temp").val(), 
                TMSKey: $("#txt_TMSKey_Temp").val()
            });
        };
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
                OrderStatus: function () {
                    var values = [];
                    $("#sel_orderstatus option:selected").each(function () {
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
                TMSKey: $("#txt_TMSKey").val(),
                ExternOrderKey: $("#txt_ExternOrderKey").val(), 
                ConsigneeKey: $("#txt_ConsigneeKey").val(), 
                Zip: $("#txt_Zip").val(),
                OrderDate_Start: $("#txt_OrderDate_Start").val(),
                OrderDate_End: $("#txt_OrderDate_End").val(),
                DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(), 
                DeliveryDate_End: $("#txt_DeliveryDate_End").val()  
                });
        };
    var queryParams_reserve = function (params) {
        return $.extend(params,
            {
                StorerKeys: function () {
                    var values = [];
                    $("#sel_storerkey_reserveorder option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                TMSKey: $("#txt_TMSKey_reserveorder").val(),
                ExternOrderKey: $("#txt_ExternOrderKey_reserveorder").val(),
                ConsigneeKey: $("#txt_ConsigneeKey_reserveorder").val(),
                AreaCode: $("#sel_AreaCode_reserverorder").val(),
                OrderType: $("#txt_OrderType").val(),
                OrderDate_Start: $("#txt_OrderDate_Start_reserveorder").val(), 
                OrderDate_End: $("#txt_OrderDate_End_reserveorder").val(),
                DeliveryDate_Start: $("#txt_DeliveryDate_Start_reserveorder").val(), 
                DeliveryDate_End: $("#txt_DeliveryDate_End_reserveorder").val()
                });
            };                 
    var $selectedorderstable = $('#selected-orders-table').lgbTable({
        dataBinder: {
            map: {
                TMSKey: "#TMSKey",
            },
            bootstrapTable: '#selected-orders-table'         
        },
        smartTable: {  
            showExport: false,
            showColumns: true,
            showRefresh: false,
            showToggle: true,
            checkbox: false,
            toolbar: '',
            search: false,
            columns: [
                { title: "??????", field: "StorerKey", sortable: true },
                { title: "????????????", field: "Facility", sortable: true },
                { title: "????????????", field: "TMSKey", sortable: true },
                { title: "????????????", field: "ExternOrderKey", sortable: true },
                { title: "????????????", field: "CustomerOrderKey", sortable: true },
                { title: "????????????", field: "OrderType", sortable: true },
                { title: "????????????", field: "OrderDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "????????????", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "????????????", field: "PickUpConsigneeKey", sortable: true },
                { title: "????????????", field: "PickUpName", sortable: true },
                { title: "????????????", field: "PickUpAddress", sortable: true },
                { title: "????????????", field: "ConsigneeKey", sortable: true },
                { title: "????????????", field: "ShortName", sortable: true },
                { title: "???????????????", field: "ShipTo", sortable: true },
                { title: "???????????????", field: "ShipToName", sortable: true },
                { title: "????????????", field: "ShipToAddress", sortable: true },
                { title: "????????????", field: "Zip", sortable: true },
                { title: "??????", field: "AreaCode", sortable: true },
                { title: "?????????", field: "Contact", sortable: true },
                { title: "??????", field: "Phone", sortable: true },
                { title: "????????????", field: "Notes", sortable: true },
                { title: "??????", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "UrgentMark", sortable: true },
                { title: "??????", field: "ReserveMark", sortable: true },
                { title: "??????", field: "ColdMark", sortable: true },
                { title: "????????????", field: "OriginalRouteNo", sortable: true }
            ],
            editButtons: { 
                id: "",
                events: {}
            }
        }
    });

    var $orderstable = 
        $('#normalstandbyorders-table').lgbTable({
            url: 'api/NormalStandbyOrders/RetrieveOrders',
            dataBinder: {
                map: {
                    StorerKey: "#StorerKey",
                    Facility: "#Facility",
                    TMSKey: "#TMSKey",
                    ExternOrderKey: "#ExternOrderKey",
                    CustomerOrderKey: "#CustomerOrderKey",
                    OrderType: "#OrderType",
                    OrderDate: "#OrderDate",
                    DeliveryDate: "#DeliveryDate",
                    PickUpConsigneeKey: "#PickUpConsigneeKey",
                    PickUpName: "#PickUpName",
                    PickUpAddress: "#PickUpAddress",
                    ConsigneeKey: "#ConsigneeKey",
                    ShortName: "#ShortName",
                    ShipTo: "#ShipTo",
                    ShipToName: "#ShipToName",
                    ShipToAddress: "#ShipToAddress",
                    Zip: "#Zip",
                    AreaCode: "#AreaCode",
                    Contact: "#Contact",
                    Phone: "#Phone",
                    Notes: "#Notes",
                    CaseQty: "#CaseQty",
                    PalletQty: "#PalletQty",
                    Cube: "#Cube",
                    Weight: "#Weight",
                    UrgentMark: "#UrgentMark",
                    ReserveMark: "#ReserveMark",
                    ColdMark: "#ColdMark",
                    OriginalRouteNo: "#OriginalRouteNo"
                },
                bootstrapTable: '#normalstandbyorders-table',
            },
            smartTable: {
                singleSelect: false,
                showExport: false,
                showColumns: true,
                showRefresh: true,
                showToggle: true,
                sortName: 'TMSKey',
                toolbar: '#toolbar-normalstandbyorders',
                onCheck: function (row) { Calculate() },
                onUncheck: function (row){ Calculate() },
                onCheckAll: function (row) { Calculate() },
                onUncheckAll: function (row) { Calculate() },
                queryParams: queryParams,
                columns: [
                    { title: "??????", field: "StorerKey", sortable: true },
                    { title: "????????????", field: "Facility", sortable: true },
                    { title: "????????????", field: "TMSKey", sortable: true },
                    { title: "WaveKey", field: "WaveKey", sortable: true },
                    { title: "????????????", field: "AllocateStatus", sortable: true },                    
                    { title: "????????????", field: "ExternOrderKey", sortable: true },
                    { title: "????????????", field: "Zip", sortable: true },
                    { title: "??????", field: "AreaCode", sortable: true },
                    { title: "????????????", field: "CustomerOrderKey", sortable: true },
                    { title: "????????????", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                    { title: "????????????", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                    { title: "????????????", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                    { title: "????????????", field: "PickUpConsigneeKey", sortable: true },
                    { title: "????????????", field: "PickUpName", sortable: true },
                    { title: "????????????", field: "PickUpAddress", sortable: true },
                    { title: "????????????", field: "ConsigneeKey", sortable: true },
                    { title: "????????????", field: "ShortName", sortable: true },
                    { title: "???????????????", field: "ShipTo", sortable: true },
                    { title: "???????????????", field: "ShipToName", sortable: true },
                    { title: "????????????", field: "ShipToAddress", sortable: true },                    
                    { title: "?????????", field: "Contact", sortable: true },
                    { title: "??????", field: "Phone", sortable: true },
                    { title: "????????????", field: "Notes", sortable: true },
                    { title: "??????", field: "CaseQty", sortable: true },
                    { title: "??????", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "??????", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "??????", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "??????", field: "UrgentMark", sortable: true },
                    { title: "??????", field: "ReserveMark", sortable: true },
                    { title: "??????", field: "ColdMark", sortable: true },
                    { title: "?????????", field: "OriginalRouteNo", sortable: true }
                ],
                editButtons: { 
                    events: {
                        'click .del': function (e, value, row, index) {         
                            DeleteOrders([row]);
                        }
                    }
                }
            }
        });
        
    function Calculate() {
        var orderselection = $orderstable.bootstrapTable('getSelections');
        var Case = 0
        var Pallet = 0
        var Cube = 0
        var Weight = 0
        orderselection.forEach(function (orderselection) {
            Case = Case + parseFloat(orderselection.CaseQty)
            Pallet = Pallet + parseFloat(orderselection.PalletQty)
            Cube = Cube + parseFloat(orderselection.Cube)
            Weight = Weight + parseFloat(orderselection.Weight)
        });
        $('#txt_Case').val($.roundDecimal(Case, 5));
        $('#txt_Pallet').val($.roundDecimal(Pallet, 5));
        $('#txt_Cube').val($.roundDecimal(Cube, 5));
        $('#txt_Weight').val($.roundDecimal(Weight, 5));
    }

    $("#btn_reserveorder").click(function () {       
        var $table = $('#normalstandbyorders-table');        
        var arrselections = $table.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }    
        var canreserve = arrselections.every(function (row) { return row.Status.trim() == "1" });
        if (!canreserve) {
            lgbSwal({ title: '????????????????????????????????????', type: "warning" });
            return;
        }
        $("#btn_reserveorder").attr('disabled', true);
        swal({ //??????????????????
            title: "??????????????????",
            html: "????????????????????????",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //??????????????????
            cancelButtonColor: '#6c757d', //??????????????????
            confirmButtonText: "??????",
            cancelButtonText: "??????"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/ReserveOrders',
                    method: "post",
                    data: arrselections,
                    crud: '????????????',
                    callback: function (result) {
                        $("#btn_reserveorder").attr('disabled', false);
                        if (result) {                           
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "??????" : "??????";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "????????????" + title,
                            type: type
                        });
                    }
                });
            } else {
                $("#btn_reserveorder").attr('disabled', false);
            }
        });        
    });


    //????????????????????????????????????
    $("#btn_updateskudata").click(function () {
        var $table = $('#normalstandbyorders-table');
        var arrselections = $table.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }    
        var canupdate = arrselections.every(function (row) { return row.Status.trim() == "1" });
        if (!canupdate) {
            lgbSwal({ title: '????????????????????????????????????', type: "warning" });
            return;
        }
        $("#btn_updateskudata").attr('disabled', true);
        swal({ //??????????????????
            title: "????????????????????????",
            html: "????????????????????????",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //??????????????????
            cancelButtonColor: '#6c757d', //??????????????????
            confirmButtonText: "??????",
            cancelButtonText: "??????"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/UpdateSkuData',
                    method: "post",
                    data: arrselections,
                    crud: '??????????????????',
                    callback: function (result) {
                        $("#btn_updateskudata").attr('disabled', false);
                        if (result) {                           
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "??????" : "??????";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "??????" + title,
                            type: type
                        });
                    }
                });
            } else {
                $("#btn_updateskudata").attr('disabled', false);
            }
        });        
    });

    $('#btn_assign').click(function () {  
        var arrselections = $orderstable.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '???????????????', type: "warning" });
            return;
        }
        else {        
            $selectedorderstable.bootstrapTable('showLoading');
            var TMSKey = arrselections.map(function (element, index) { return element["TMSKey"]; });
            $.bc({
                url: "api/NormalStandbyOrders/RetrieveSelectedOrders",
                data: TMSKey,
                method: "post", 
                crud: '???????????????????????????',
                btn: this,
                callback: function (result) {
                    if (result.length <= 0) {
                        swal({ title: '??????????????????????????????????????????', type: "error" });
                        return;
                    }
                    $selectedorderstable.bootstrapTable('hideLoading');
                    $selectedorderstable.bootstrapTable("load", result);
                    $("#txt_DoRouteDate").datetimepicker("setDate", moment(new Date()).add(1, 'days').toDate());
                    $("#dialogAssign .fixed-table-pagination").css("display", "none");
                    $("#dialogAssign").modal("show");
                    driverDataEntity.reset();
                }
            });
        }
    });  
    $("#btn_Submit_Assign").click(function () {        
        var driver = CheckDriver();
        if (!driver) {
            lgbSwal({ title: '??????????????????????????????', type: "warning" });
            return;
        }
        var selectedorders = $selectedorderstable.bootstrapTable('getData');
        var data = {
            Driver: driver,
            SelectedOrders: selectedorders,
            DoRouteDate: $("#txt_DoRouteDate").val(),
            Dock: $("#txt_Dock").val(),
            ExpectDate: $("#txt_ExpectDate").val(),
            ExpectTime: $("#txt_ExpectTime").val(),
            Transfer: $("#sel_Transfer").prop('checked'),
            ToFacility: $("#txt_TransferTo").val()
        };
        if (data.Transfer && data.ToFacility === "") {
            lgbSwal({ title: '??????????????????', type: "warning" });
            return;
        }
        var that = this;

        if (data.Transfer) {
            swal({ //??????????????????
                title: "??????????????????",
                html: "??????????????????????????????????????????????????????",
                type: "warning",
                showCancelButton: true,
                //confirmButtonColor: '#dc3545', //??????????????????
                cancelButtonColor: '#6c757d', //??????????????????
                confirmButtonText: "??????",
                cancelButtonText: "??????"
            }).then(function (result) {
                if (result.value) {
                    CreateNewRoute(data, that);
                }
            });
        } else {
            CreateNewRoute(data, that);
        }
    });

    function CreateNewRoute(data, that) {
        //Add by Terry 20201214 ????????????????????????????????????????????????????????????
        $("#txt_DoRouteDate").removeClass('is-invalid');
        $("#txt_ExpectDate").removeClass('is-invalid');
        if(data.DoRouteDate >= $.momentFormat(Date(), "YYYY/MM/DD") && data.ExpectDate >= $.momentFormat(Date(), "YYYY/MM/DD")){
            $.bc({
                url: "api/NormalStandbyOrders/CreateNewRoute",
                data: data,
                method: "post",
                crud: '????????????',
                btn: that,
                callback: function (result) {
                    if (result && result.success) { 
                        if (typeof result.routeno !== "string") {
                            swal({ title: '????????????????????????????????????????????????????????????????????????????????????????????????', type: "error" });
                            return;
                        }
                        $("#dialogAssign").modal("hide");
                        var swalRouteNo = {
                            title: "????????????????????? ",
                            html: "???????????????" + result.routeno,
                            type: "success",
                            confirmButtonText: '#6c757d',
                            confirmButtonText: "??????"
                        };
                        swal($.extend({}, swalRouteNo));
                        $orderstable.bootstrapTable('refresh');
                        $normalroutelist.bootstrapTable('refresh');
                        $("#txt_DoRouteDate").val("");
                        $("#txt_Dock").val("");
                        $("#txt_ExpectDate").val("");
                        $("#txt_ExpectTime").val("");
                        $('#txt_Case').val("0");
                        $('#txt_Pallet').val("0");
                        $('#txt_Cube').val("0");
                        $('#txt_Weight').val("0");
                        $('#sel_Transfer').prop('checked', false);
                        $('#sel_Transfer').parent().addClass("off");
                        $('#sel_Transfer').parent().addClass("btn-default"); //??????Class??????
                        $('#sel_Transfer').parent().removeClass("btn-success");
                        $("#sel_TransferTo").fadeOut();
                    } else {
                        if (!result) {
                            swal({ title: "??????????????????????????????", type: "warning", confirmButtonText: '??????' });
                            return;
                        }
                        swal({ title: result.message, type: "warning", confirmButtonText: '??????' });
                    }
                }
            })
        }
        else{
            if(data.DoRouteDate < $.momentFormat(Date(), "YYYY/MM/DD")) { $("#txt_DoRouteDate").addClass('is-invalid'); }
            if(data.ExpectDate < $.momentFormat(Date(), "YYYY/MM/DD")) { $("#txt_ExpectDate").addClass('is-invalid'); }

            swal({ title: "???????????????????????????????????????????????????????????????", type: "warning", confirmButtonText: '??????' });
            return;
        }
    }

    //?????????????????????????????????
    $("#sel_Transfer").change(function () {
        $("#txt_TransferTo").lgbSelect('val', '');
        $(this).prop("checked") ? $("#sel_TransferTo").fadeIn() && $("#txt_TransferTo").val('') : $("#sel_TransferTo").fadeOut() && $("#txt_TransferTo").val('');
     });

    //??????????????? tab
    var $normalroutelist = $('#normalroutelist-table');
    $normalroutelist.lgbTable({
        url: 'api/NormalRouteList/RetrieveData',
        dataBinder: {
            map: {
            },
            bootstrapTable: '#normalroutelist-table',
            click: {
                '#btn_query': function () {},
                '#btn_query-routelist': $.bootstrapTableQueryBtn,
                '#btn_reset': function () {},
                '#btn_reset-routelist': $.bootstrapTableResetBtn,
                '#btn_add': function () {},
                '#btn_edit': function () {},
                '#btn_delete': function () {},
                '#btnSubmit': function () {},
            }
        },        
        smartTable: {
            singleSelect: false,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'AddDate',
            sortOrder: 'desc',
            advancedSearchModal: '#normalroutelist-dialogAdvancedSearch',
            queryButton: '#btn_query-routelist',
            checkbox: true,
            detailView: true,
            queryParams: function (params) {
                return $.extend(params, {
                    RouteNo: $("#txt_RouteNo").val(),
                    DoRouteDate_Start: $("#txt_DoRouteDate_Start").val(),
                    DoRouteDate_End: $("#txt_DoRouteDate_End").val(),
                    ToWMS: $("#txt_ToWMS").val(),
                    VehicleKey: $("#txt_VehicleKey").val()
                });
            },
            columns: [
                { title: "????????????", field: "RouteNo", sortable: true },
                { title: "????????????", field: "FacilityName", sortable: true },
                { title: "????????????", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "??????????????????", field: "ExpectDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "??????????????????", field: "ExpectTime", sortable: true },
                { title: "????????????", field: "CaseQty", sortable: true },
                { title: "????????????", field: "PalletQty", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "????????????", field: "Cube", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "????????????", field: "Weight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "????????????", field: "DockNo", sortable: true },
                { title: "??????", field: "VehicleKey", sortable: true },
                { title: "??????", field: "Driver", sortable: true },
                { title: "????????????", field: "DriverPhone", sortable: true },
                { title: "??????", field: "Notes", sortable: true },
                { title: "??????", field: "DriveTimes", sortable: true },
                { title: "????????????", field: "VLListCount", sortable: true },
                { title: "????????????", field: "VLListPrintDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "????????????", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "?????????", field: "AddWho", sortable: true, formatter (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value); } },
                { title: "????????????", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "?????????", field: "EditWho", sortable: true },
                //{ title: "????????????", field: "ToWMS", sortable: true, formatter: function (value) { return $("#txt_ToWMS").getTextByValue(value) } }
            ],
            editButtons: {
                events: {
                    'click .del': function (e, value, row, index) {
                        $normalroutelist.bootstrapTable('uncheckAll');     
                        $normalroutelist.bootstrapTable('check', index);                   
                        DeleteRoutes([row]);
                    }
                },
                formatter: function (value, row, index) {
                    var $this = this.clone();
                    if (row.ToWMS && row.ToWMS.trim() === "1") {
                        $($this).find(".del").remove();
                    }
                    return $this.html();
                }
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    dataBinder: {
                        map: {
                        },
                        bootstrapTable: ''
                    },
                    url: 'api/NormalRouteList/RetrieveDetailByRouteNo?RouteNo=' + row.RouteNo,
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
                            { title: "??????", field: "StorerKey", sortable: false },
                            { title: "????????????", field: "TMSKey", sortable: false },
                            { title: "????????????", field: "ExternOrderKey", sortable: false },
                            { title: "????????????", field: "OrderDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "????????????", field: "DeliveryDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "????????????", field: "ConsigneeKey", sortable: false },
                            { title: "????????????", field: "ShortName", sortable: false },
                            { title: "????????????", field: "ShipToAddress", sortable: false },
                            { title: "?????????", field: "OrderQty", sortable: false },
                            { title: "?????????", field: "ShipQty", sortable: false },
                            { title: "????????????", field: "CaseQty", sortable: false },
                            { title: "????????????", field: "PalletQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                            { title: "????????????", field: "Cube", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                            { title: "????????????", field: "Weight", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)}},
                            { title: "??????", field: "Notes", sortable: false }
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

    $("#btn_checkroute").click(function () {
        $("#btn_checkroute").attr("disabled", true);
        var selectedroutes = $normalroutelist.bootstrapTable('getSelections');
        if (selectedroutes.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            $("#btn_checkroute").attr("disabled", false);
            return;
        }
        //??????I?????????
        var checkroutenos = selectedroutes.filter(function (row) { return row.OrderType == "I" }).map(function (element, index) { return element.RouteNo; });
        //??????????????????
        var allroutenos = selectedroutes.map(function (element, index) { return element.RouteNo; });   
        if (checkroutenos.length == 0) {
            CarLeave(allroutenos);
            return;
        }     
        //??????I????????????????????????
        $.bc({
            url: 'api/NormalRouteList/CheckBeforeCarLeave',
            method: "post",
            data: checkroutenos,
            callback: function (result) {
                //??????result: ?????????????????????
                if (!result) {
                    lgbSwal({ title: '?????????????????????????????????', type: "error" });
                    $("#btn_checkroute").attr("disabled", false);
                    return;
                }
                CarLeave(allroutenos);              
            }
        });        
    });

    function CarLeave(routes) {
        swal({ //??????????????????
            title: "????????????",
            html: "?????????????????????????????????????????????????????????",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //??????????????????
            cancelButtonColor: '#6c757d', //??????????????????
            confirmButtonText: "??????",
            cancelButtonText: "??????"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalRouteList/CheckCarLeave',
                    method: "post",
                    data: routes,
                    callback: function (result) {
                        //??????result: ???????????????
                        if (!result) {
                            swal({ title: '??????????????????????????????????????????????????????', type: "error" });
                            $("#btn_checkroute").attr("disabled", false);
                            return;
                        }
                        $.bc({
                            url: 'api/NormalRouteList/CarLeave',
                            method: "post",
                            data: routes,
                            crud: '????????????',
                            callback: function (result) {
                                $("#btn_checkroute").attr("disabled", false);
                                if (result) {
                                    $("#dialogNew").modal('hide');     
                                    $orderstable.bootstrapTable('refresh');
                                    $normalroutelist.bootstrapTable('refresh');  
                                }
                                var title = (result) ? "??????" : "??????";
                                var type = (result) ? "success" : "error";
                                lgbSwal({
                                    title: "??????" + title,
                                    type: type
                                });                             
                            }
                        });
                    }
                });
            } else {
                $("#btn_checkroute").attr("disabled", false);
            }
        });
    }

    $("#btn_returnwms").click(function () {
        var selectedroutes = $normalroutelist.bootstrapTable('getSelections');
        if (selectedroutes.length === 0) {
            swal({ title: '???????????????????????????', type: "warning" });
            return;
        }
        var routenos = selectedroutes.map(function (element, index) { return element.RouteNo; });
        $.bc({
            url: 'api/NormalRouteList/ReturnWMS',
            method: "post",
            data: routenos,
            crud: '????????????',
            btn: this,
            callback: function (result) {
                if (result) {
                    $("#dialogNew").modal('hide');     
                    $orderstable.bootstrapTable('refresh');
                    $normalroutelist.bootstrapTable('refresh');  
                }
                var title = (result) ? "??????" : "??????";
                var type = (result) ? "success" : "error";
                swal({
                    title: "??????" + title,
                    type: type
                });
            }
        });
    });


    $("#btn_checkcustomer").click(function () {
        var that = this;
        $(that).attr("disabled", true);
        var data = {
        }
        $.bc({
            url: 'api/NormalStandbyOrders/OrderImport',
            method: "post",
            data: data,
            crud: '????????????',
            btn: this,
            callback: function (result) {     
                $(that).attr("disabled", false);
                $customertemptable.bootstrapTable('refresh');
                var title = (result && result > 0) ? "??????" : "??????";
                var type = (result && result > 0) ? "success" : "error";
                swal({
                    title: "????????????" + title,
                    text: "?????????????????????" + result,
                    type: type,
                    confirmButtonText: "??????"
                });
            }
        });
    });

    $("#btn_delroute").click(function () {
        var selectedroutes = $normalroutelist.bootstrapTable('getSelections');
        if (selectedroutes.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }
        DeleteRoutes(selectedroutes);
    })

    function DeleteRoutes(rows) {
        if (rows.every(function (row) { return row.ToWMS.trim() == "1" })) {
            swal({ title: '??????????????????????????????', type: "warning" });
            return;
        }
        var swalDeleteOptions = {
            title: "????????????",
            html: "???????????????????????????????????????",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545',
            cancelButtonColor: '#6c757d',
            confirmButtonText: "????????????",
            cancelButtonText: "??????"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (!result.value) return;
            var routenos = rows.map(function (element, index) { return element.RouteNo; }); 
            $.bc({
                url: "api/NormalRouteList/DeleteRoutes",
                data: routenos,
                method: "delete",
                crud: '????????????',
                callback: function (result) {
                    lgbSwal({ title: result ? '????????????' : '????????????', type: result ? 'success' : 'error' });
                    $normalroutelist.bootstrapTable('refresh');
                    $orderstable.bootstrapTable('refresh');
                }
            });       
        })
    }

    var tempdatamap = {
        StorerKey: "#StorerKey_Temp",
        ConsigneeKey: "#ConsigneeKey_Temp",
        TMSKey: "#TMSKey_Temp",
        ExternOrderKey: "#ExternOrderKey_Temp",
        ShortName: "#ShortName_Temp",
        ShipToAddress: "#ShipToAddress_Temp",
        Zip: "#Zip_Temp",
        Contact: "#Contact_Temp",
        Phone: "#Phone_Temp"
    }

    var tempdataEntity = new DataEntity(tempdatamap);

    var customercolumn = [
        { title: "??????", field: "StorerKey", sortable: true },
        { title: "????????????", field: "TMSKey", sortable: true },
        { title: "????????????", field: "ExternOrderKey", sortable: true },
        { title: "????????????", field: "ConsigneeKey", sortable: true },
        { title: "????????????", field: "ShortName", sortable: true },
        { title: "????????????", field: "ShipToAddress", sortable: true },
        { title: "????????????", field: "Zip", sortable: true },
        { title: "?????????", field: "Contact", sortable: true },
        { title: "??????", field: "Phone", sortable: true }
    ]
    //??????????????????????????? table
    var $customertemptable =
    $('#customer-temp-table').lgbTable({
        url: 'api/NormalStandbyOrders/RetrievesCustomerTemp',
        dataBinder: {
            map: tempdatamap,            
            click: {
                '#btn_query': function () {},
                '#btn_query-customer-temp': $.bootstrapTableQueryBtn,
                '#btn_reset': function () {},
                '#btn_reset-customer-temp': $.bootstrapTableResetBtn,
                '#btn_add': function () {},
                '#btn_edit': function () {},
                '#btn_delete': function () {},
                '#btnSubmit': function () {},
                '#btnSubmit_Temp': function (element) {
                    var options = this.options;
                    var data = tempdataEntity.get();
                    if ($.trim(data.Zip) === "") {
                        swal({ title: "????????????????????????", type: "error" });
                        return;
                    }
                    $.bc({                        
                        url: 'api/NormalStandbyOrders/SaveCustomer',
                        data: data, 
                        method: "post",
                        crud: '?????????????????????',
                        btn: '#btnSubmit_Temp',
                        callback: function (result) {
                            if (result && result.success) {
                                lgbSwal({ title: "????????????", type: "success" });
                                $("#dialogNew_Temp").modal('hide');
                                options.bootstrapTable.bootstrapTable('refresh');                                
                                $orderstable.bootstrapTable('refresh');
                                return;
                            }                            
                            swal({
                                title: "????????????",
                                html: result.message,
                                type: "error"
                            });
                        }
                    });
                }
            },
            events: {
                "#btn_edit_temp": function (row) {
                    EditTempOrder(row);
                } 
            },
            bootstrapTable: '#customer-temp-table',
            modal: "#dialogNew_Temp"
        },
        smartTable: {
            singleSelect: true,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            toolbar: '#toolbar-customer-temp',
            queryButton: '#btn_query-customer-temp',
            sortName: 'TMSKey',
            advancedSearchModal: '#customer-dialogAdvancedSearch',
            editButtons: {
                id: "#tableButtons-customer-temp",
                events: {
                    "click .edit": function (e, value, row, index) {
                        $customertemptable.bootstrapTable('uncheckAll');     
                        $customertemptable.bootstrapTable('check', index);  
                        EditTempOrder(row);
                    }
                }
            },
            queryParams: queryParams_Temp,
            //function (params) { return $.extend(params, { ConsigneeKey: $("#txt_ConsigneeKey_Temp").val(), TMSKey: $("#txt_TMSKey_Temp").val(), StorerKey: $("#txt_StorerKey_Temp").val() } ); },
            columns: customercolumn
        }
    });

    function EditTempOrder(row) {
        $.bc({
            url: $.format("api/NormalStandbyOrders/RetrievesCustomerByConsigneeKeyAndStorerKey?storerkey={0}&consigneekey={1}&tmskey={2}", row.StorerKey, row.ConsigneeKey, row.TMSKey),
            callback: function (result) {
                if(!result) {
                    swal({ title: '??????????????????????????????????????????', type: "error" });
                    return;
                }           
                var $customerData = $("#customerData");
                var $customerData_temp = $("#customerData_temp");
                var curdata = {};
                var tempdata = {};
                var tmpmap = Object.assign({}, tempdatamap);
                Object.keys(tmpmap).forEach(function(key) {
                    var column = customercolumn.find(function (col) { return col.field == key });
                    if (!column) return;
                    curdata[column.title] = result[key];
                    tempdata[column.title] = row[key];
                });
                tempdataEntity.load(row);
                $customerData.html($.syntaxData(curdata));
                $customerData_temp.html($.syntaxData(tempdata, curdata));
                $customerData.css("font-size", "1rem");
                $customerData_temp.css("font-size", "1rem");
                $("#dialogNew_Temp").modal('show');
            }
        })
    }

    //???????????? tab
    var $reserveorderstable =
    $('#normalreserveorder-table').lgbTable({
        url: 'api/NormalStandbyOrders/RetrievesReturnOrders',
        dataBinder: {
            map: tempdatamap,            
            click: {
                '#btn_query': function () {},
                '#btn_query-reserveorders': $.bootstrapTableQueryBtn,
                '#btn_reset': function () {},
                '#btn_reset-reserveorders': $.bootstrapTableResetBtn,
                '#btn_add': function () {},
                '#btn_edit': function () {},
                '#btn_delete': function () {},
                '#btnSubmit': function () {},
            },
            bootstrapTable: '#normalreserveorder-table',
        },
        smartTable: {
            singleSelect: false,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            toolbar: '#toolbar-normalreserveorder',
            queryButton: '#btn_query-reserveorders',
            sortName: 'TMSKey',
            advancedSearchModal: '#reserveorders-dialogAdvancedSearch',
            editButtons: {
                id: "#tableButtons-normalreserveorder",
                events: {
                    "click .returnorder": function (e, value, row, index) {
                        $reserveorderstable.bootstrapTable('uncheckAll');     
                        $reserveorderstable.bootstrapTable('check', index);  
                        ReturnOrder([row]);
                    }
                }
            },
            queryParams: queryParams_reserve,
            columns: [
                { title: "??????", field: "StorerKey", sortable: true },
                { title: "????????????", field: "Facility", sortable: true },
                { title: "????????????", field: "TMSKey", sortable: true },
                { title: "????????????", field: "ExternOrderKey", sortable: true },
                { title: "????????????", field: "CustomerOrderKey", sortable: true },
                { title: "????????????", field: "OrderType", sortable: true },
                { title: "????????????", field: "OrderDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "????????????", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "????????????", field: "ConsigneeKey", sortable: true },
                { title: "????????????", field: "ShortName", sortable: true },
                { title: "???????????????", field: "ShipTo", sortable: true },
                { title: "???????????????", field: "ShipToName", sortable: true },
                { title: "????????????", field: "ShipToAddress", sortable: true },
                { title: "????????????", field: "Zip", sortable: true },
                { title: "??????", field: "AreaCode", sortable: true, formatter: function (value) { return $("#sel_AreaCode_reserverorder").getTextByValue(value) } },
                { title: "?????????", field: "Contact", sortable: true },
                { title: "??????", field: "Phone", sortable: true },
                { title: "????????????", field: "Notes", sortable: true },
                { title: "??????", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "??????", field: "UrgentMark", sortable: true },
                { title: "??????", field: "ReserveMark", sortable: true },
                { title: "??????", field: "ColdMark", sortable: true },
                { title: "?????????", field: "OriginalRouteNo", sortable: true }
            ]
        }
    });

    $("#btn_returnorder").click(function () {           
        var arrselections = $reserveorderstable.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }        
        ReturnOrder(arrselections);
    });

    function ReturnOrder(rows) {
        swal({ //??????????????????
            title: "?????????????????????",
            html: "????????????????????????????????????",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //??????????????????
            cancelButtonColor: '#6c757d', //??????????????????
            confirmButtonText: "??????",
            cancelButtonText: "??????"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/ReturnOrder',
                    method: "post",
                    data: rows,
                    crud: '????????????',
                    callback: function (result) {
                        if (result) {
                            $("#dialogNew").modal('hide');
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "??????" : "??????";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "????????????" + title,
                            type: type
                        });
                    }
                });
            }
        });
    }

    $("#btn_deleteorder").click(function () {
        var selectedorders = $reserveorderstable.bootstrapTable('getSelections');
        if (selectedorders.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }
        DeleteOrders(selectedorders);
    })

    function DeleteOrders(rows) {
        var swalDeleteOptions = {
            title: "????????????",
            html: "????????????????????????????????????????????????????????????????????????????????????",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545',
            cancelButtonColor: '#6c757d',
            confirmButtonText: "????????????",
            cancelButtonText: "??????"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (!result.value) return;
            $.bc({
                url: "api/NormalOrders/DeleteOrders",
                data: rows.map(function (element, index) { return element.TMSKey; }),
                method: "delete",
                crud: '????????????',
                callback: function (result) {
                    if (result && result.success) { 
                        lgbSwal({ title: '????????????', type: "success" });    
                        $orderstable.bootstrapTable('refresh');
                        $reserveorderstable.bootstrapTable('refresh');
                        return;
                    }
                    swal({
                        title: "????????????",
                        html: result.message,
                        type: "error"
                    });
                }
            });       
        })        
    }

    var driverDataEntity = new DataEntity({
        VehicleKey: "#VehicleKey",
        Driver: "#Driver",
        DriverPhone: "#DriverPhone",
        Receiver: "#DriverReceiver",
        CompanyKey: "#DriverCompanyKey"
    });

    var driverUpdateDataEntity = new DataEntity({
        VehicleKey: "#VehicleKey_update",
        Driver: "#Driver_update",
        DriverPhone: "#DriverPhone_update",
        Receiver: "#DriverReceiver_update",
        CompanyKey: "#DriverCompanyKey_update"
    });

    var BaseVehicles = [];

    function onlyUnique(value, index, self) {
        return self.indexOf(value) === index;
    }

    $.bc({
        url: "api/BaseVehicle?Active=1",
        method: 'GET',
        callback: function (result) {
            var rows = result.rows;
            BaseVehicles = rows;
            rows.map(row => row.VehicleKey).filter(onlyUnique).forEach(key => {
                $("#VehicleKey").editableSelect('add', key);
                $("#VehicleKey_update").editableSelect('add', key);
            });
        }
    });

    $("#RefreshDriver").click(() => {
        GetBaseVehicle($("#VehicleKey").val());
    });

    $("#RefreshDriver_update").click(() => {
        GetBaseVehicleUpdate($("#VehicleKey_update").val());
    });

    function GetBaseVehicle(VehicleKey) {
        $("#Driver").val("");
        $("#Driver").editableSelect('clear');
        if (!VehicleKey || VehicleKey == "") return;
        $("#dialogAssign .modal-content").lgbValidator().valid();
        BaseVehicles.filter(row => $.trim(row.VehicleKey) == $.trim(VehicleKey)).map(row => row.Driver).forEach(key => $("#Driver").editableSelect('add', key));
        $("#Driver").editableSelect('show');
    }

    function GetBaseVehicleUpdate(VehicleKey, show = true) {
        $("#Driver_update").val("");
        $("#Driver_update").editableSelect('clear');
        if (!VehicleKey || VehicleKey == "") return;
        $("#dialogupdaterouteno .modal-content").lgbValidator().valid();
        BaseVehicles.filter(row => $.trim(row.VehicleKey) == $.trim(VehicleKey)).map(row => row.Driver).forEach(key => $("#Driver_update").editableSelect('add', key));
        if (show) $("#Driver_update").editableSelect('show');
    }

    $('#VehicleKey').editableSelect().on('select.editable-select', function (e) {
        GetBaseVehicle($("#VehicleKey").val());
    });

    $('#Driver').editableSelect().on('select.editable-select', function (e) {
        CheckDriver();
    });

    $('#VehicleKey_update').editableSelect().on('select.editable-select', function (e) {
        GetBaseVehicleUpdate($("#VehicleKey_update").val());
    });

    $('#Driver_update').editableSelect().on('select.editable-select', function (e) {
        CheckDriverUpdate({ VehicleKey: $('#VehicleKey_update').val(), Driver: $('#Driver_update').val() });
    });

    function CheckDriver() {
        $("#DriverPhone").val("");
        $("#DriverReceiver").val("");
        $("#DriverCompany").val("");
        if ($('#VehicleKey').val() == "" || $('#Driver').val() == "") return;
        var row = BaseVehicles.find(row => $.trim(row.VehicleKey) == $.trim($('#VehicleKey').val()) && $.trim(row.Driver) == $.trim($('#Driver').val()));
        if (row) {
            $("#DriverPhone").val(row.DriverPhone);
            $("#DriverReceiver").val(row.Receiver);
            $("#DriverCompany").val(row.CompanyKey);
        } else {
            $('#Driver').val("");
            GetBaseVehicle($("#VehicleKey").val());
        }
        $("#dialogAssign .modal-content").lgbValidator().valid();
        return row;
    }

    function CheckDriverUpdate(data) {
        $("#DriverPhone_update").val("");
        $("#DriverReceiver_update").val("");
        $("#DriverCompany_update").val("");
        if ($('#VehicleKey_update').val() == "" || $('#Driver_update').val() == "") return;
        var row = BaseVehicles.find(row => $.trim(row.VehicleKey) == $.trim(data.VehicleKey) && $.trim(row.Driver) == $.trim(data.Driver));
        if (row) {
            $("#DriverPhone_update").val(row.DriverPhone);
            $("#DriverReceiver_update").val(row.Receiver);
            $("#DriverCompany_update").val(row.CompanyKey);
        } else {
            $('#Driver_update').val("");
            GetBaseVehicleUpdate(data.VehicleKey);
        }
        $("#dialogupdaterouteno .modal-content").lgbValidator().valid();
        return row;
    }

    $.bc({
        url: "api/Base_Dock",        
        callback: function (result) {
            if (!result) return;
            $('#txt_Dock').typeahead({
                source: result.rows.map(function (row) { return row.Dock }),
                showHintOnFocus: 'all',
                fitToElement: true,
                items: 'all'
            });
        }
    });

    $.bc({
        url: "api/Base_Dock",        
        callback: function (result) {
            if (!result) return;
            $('#txt_Dock_update').typeahead({
                source: result.rows.map(function (row) { return row.Dock }),
                showHintOnFocus: 'all',
                fitToElement: true,
                items: 'all'
            });
        }
    });

    //????????????
    $("#btn_updaterouteno").click(function () {    
        var selections = $normalroutelist.bootstrapTable('getSelections');
        if (selections.length === 0) {
            lgbSwal({ title: '???????????????????????????', type: "warning" });
            return;
        }
        if (selections.length > 1) {
            lgbSwal({ title: '??????????????????????????????', type: "warning" });
            return;
        }
        UpdateRouteNo(selections[0]);
    });

    //??????????????????????????????
    var updateroutenoData = new DataEntity({
        RouteNo: "#txt_RouteNo_update",
        DoRouteDate: '#txt_DoRouteDate_update',
        DockNo: '#txt_Dock_update',
        ExpectDate: '#txt_ExpectDate_update',
        ExpectTime: '#txt_ExpectTime_update'        
    });

    //????????????????????????
    function UpdateRouteNo(row) {
        row.DoRouteDate = $.momentFormat(row.DoRouteDate, 'YYYY/MM/DD');
        row.ExpectDate = $.momentFormat(row.ExpectDate, 'YYYY/MM/DD');
        updateroutenoData.load(row);
        $("#VehicleKey_update").val(row.VehicleKey);
        GetBaseVehicleUpdate(row.VehicleKey, false);
        $("#Driver_update").val(row.Driver);
        CheckDriverUpdate(row);
        $("#dialogupdaterouteno").modal('show');
    }

    //??????????????????
    $("#btn_submit-updaterouteno").click(function () {
        var driver = CheckDriverUpdate({ VehicleKey: $('#VehicleKey_update').val(), Driver: $('#Driver_update').val() });
        if (!driver) {
            lgbSwal({ title: '??????????????????????????????', type: "warning" });
            return;
        }
        var data = updateroutenoData.get();
        data.Driver = driver
        swal({ //??????????????????
            title: "??????????????????" + data.RouteNo,
            html: "????????????????????????",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //??????????????????
            cancelButtonColor: '#6c757d', //??????????????????
            confirmButtonText: "??????",
            cancelButtonText: "??????"
        }).then(function (result) {
            if (result.value) {                
                $.bc({
                    url: 'api/NormalRouteList/UpdateRouteNo',
                    method: "post",
                    data: data,
                    crud: '????????????',
                    callback: function (result) {                        
                        if (result) {
                            $("#dialogupdaterouteno").modal('hide');
                            $normalroutelist.bootstrapTable('refresh');
                            updateroutenoData.reset();
                            driverUpdateDataEntity.reset();
                        }
                        var title = (result) ? "??????" : "??????";
                        var type = (result) ? "success" : "error";
        
                        lgbSwal({
                            title: "????????????" + title,
                            type: type
                        });
                    }
                });
            }
        });        
    });

    
    function InitialSelect() {
        var ConfigurationSet = {
            includeSelectAllOption: true,
            enableFiltering: false
        };
        $("#sel_storerkey_temp").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '???????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey_temp").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey_temp").multiselect("rebuild");

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

        $("#sel_storerkey_reserveorder").multiselect({
            buttonClass: 'form-control',
            selectAllText: '??????',
            nonSelectedText: '???????????????',
            allSelectedText: '??????',
            nSelectedText: '?????????',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey_reserveorder").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey_reserveorder").multiselect("rebuild");

    };  
    InitialSelect();

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