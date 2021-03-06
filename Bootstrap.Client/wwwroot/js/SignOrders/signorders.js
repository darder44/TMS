$(function () {
    var detailValidData = [];
    $('#SignOrdersDetailCard [data-valid="true"]').each((index, el) => detailValidData.push(el));
    detailValidData.forEach(el => $(el).attr("data-valid", "false"));

    var headerDataBinder = new DataEntity({
        StorerKey: "#StorerKey",
        RouteNo: "#RouteNo",
        Facility: "#Facility",
        TMSKey: "#TMSKey",
        ExternOrderKey: "#ExternOrderKey",
        CustomerOrderKey: "#CustomerOrderKey",
        OrderType: "#OrderType",
        OrderDate: "#OrderDate",
        DeliveryDate: "#DeliveryDate",
        DoRouteDate: "#DoRouteDate",
        ConsigneeKey: "#ConsigneeKey",
        ShortName: "#ShortName",
        PickUpConsigneeKey: "#PickUpConsigneeKey",
        PickUpName: "#PickUpName",
        PickUpAddress: "#PickUpAddress",
        ShipToAddress: "#ShipToAddress",
        Zip: "#Zip",
        Contact: "#Contact",
        Phone: "#Phone",
        UrgentMark: "#UrgentMark",
        ReserveMark: "#ReserveMark",
        ColdMark: "#ColdMark",
        ConfirmNotes: "#ConfirmNotes",
        ConfirmDate: "#ConfirmDate",
        ConfirmUser: "#ConfirmUser",
        InvBack: "#InvBack",
        CustHandle: "#CustHandle",
        SdnNotes: "#SdnNotes"
    });

    var detailDataBinder = new DataEntity({
        OrderLineNumber: "#OrderLineNumber",
        Sku: "#Sku",
        Descr: "#Descr",
        OrderQty: "#OrderQty",
        CaseQty: "#CaseQty",
        ShipQty: "#ShipQty",
        ShipCaseQty: "#ShipCaseQty",
        SignQty: "#SignQty",
        SignCaseQty: "#SignCaseQty",
        CaseCnt: "#CaseCnt",
        Pallet: "#Pallet",
        Cube: "#Cube",
        Weight: "#Weight",
        StdCube: "#StdCube",
        StdWeight: "#StdWeight",
        RSCCode: "#RSCCode",
        RBCCode: "#RBCCode",
        RBCName: "#RBCName"

    });

    var $signorderstable = $("#signorders-table");
    $signorderstable.lgbTable({
        dataBinder: {
            click: {
                '#btn_query': function () {},
                '#btn_edit': function () {},
                '#btnSubmit': function () {},
                '#btn_reset': function () {},
            },
            events: {
                '#btn_edit_signorders': function (row) {
                    var sels = $('#signorders-table input[name="btSelectItem"]:checked');
                    if (sels.length > 1) {
                        lgbSwal({ title: '????????????????????????????????????', type: "warning" });
                        return;
                    }
                    EditRow(row, $(sels[0]).data('index'));                    
                }
            },
            bootstrapTable: "#signorders-table"
        },
        smartTable: {
            showExport: false, 
            showColumns: true,
            showRefresh: false,
            showToggle: true,            
            queryButton: '#',
            paginationHAlign: "left", //??????????????????
            pagination: true, //????????????
            sidePagination: 'client', //???????????? client ??????bootstrapTable(load, rows), server?????? url
            pageSize: 5,
            pageList: [10, 50, 100], //????????????
            search: false,
            showAdvancedSearchButton: false,
            singleSelect: true,
            cookie: false,
            editButtons: { 
                id: "#signordersButtons",
                events: {
                    'click .edit': function (e, value, row, index) {                                   
                        EditRow(row, index);
                    }
                }
            },
            toolbar: "#toolbar-signorders",
            queryButton: "",
            columns: [
                { title: "??????", field: "OrderLineNumber", formatter: function (value) { return parseInt(value); }, sortable: false },
                { title: "??????", field: "Sku", sortable: false },
                { title: "??????", field: "Descr", sortable: false },
                { title: "????????????", field: "OrderQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "CaseQty", sortable: false, formatter: function (value) { return $.roundDecimal(value, 5) } },
                { title: "????????????", field: "ShipQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "ShipCaseQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "SignQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "SignCaseQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "????????????", field: "RSCCode", sortable: false, formatter: function (value) { return value == "" ? value : $("#RSCCode").getTextByValue(value) } },
                { title: "????????????", field: "RBCCode", sortable: false, formatter: function (value) { return value == "" ? value : $("#RBCCode").getTextByValue(value) } },
                { title: "?????????", field: "RBCName", sortable: false },
                { title: "?????????", field: "CaseCnt", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "???????????????", field: "StdCube", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "???????????????", field: "StdWeight", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
            ]
        }
    });

    $("#btn_SignOrders_UpdateDetail").parent().css("display", "none"); 
    var editing = false;

    function EditRow(row, index) {
        if (editing) {
            swal({ title: "????????????????????????", type: "warning" });
            return;
        } 
        detailValidData.forEach(el => $(el).attr("data-valid", "true"));
        detailDataBinder.reset();        
        $("#SignOrdersDetailCard").lgbValidator().reset();
        $("#btn_SignOrders_UpdateDetail").parent().css("display", ""); 
        $("#btnSignOrdersSubmit").attr("disabled", true);
        editing = true;
        detailDataBinder.load(row);
        currentIndex = index;
    }

    $("#btn_SignOrders_CancelUpdate").click(() => {
        Reset();
    });

    function Reset() {
        detailDataBinder.reset();
        $("#SignOrdersDetailCard").lgbValidator().reset();
        $("#btn_SignOrders_UpdateDetail").parent().css("display", "none");
        $("#btnSignOrdersSubmit").attr("disabled", false);
        editing = false;
        detailValidData.forEach(el => $(el).attr("data-valid", "false"));
    }

    $("#btn_SignOrders_UpdateDetail").click(() => {
        if ($("#SignQty").val() == "") {
            swal({ title: "??????????????????", type: "error" });
            return;
        };
        var row = detailDataBinder.get();

        var SignQty = parseInt(row.SignQty);
        var CaseCnt = parseInt($("#CaseCnt").val());
        row.SignCaseQty = (SignQty / CaseCnt).toString();

        $signorderstable.bootstrapTable('updateRow', {
            index: currentIndex,
            row: row
        });
        $("#btn_SignOrders_CancelUpdate").trigger("click");
    });

    $("#btnSignOrdersSubmit").click(() => {
        if ($("#SignStatus").val() == "") {
            swal({ title: "?????????????????????", type: "error" });
            return;
        }
        var that = this;
        var header = headerDataBinder.get();
        var details = $signorderstable.bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
        var signstatus = $("#SignStatus").val();
        var sdnsenddate = $("#SdnSendDate").val();
        $.bc({
            url: "api/NormalSignOrders/CheckSignOrders",
            method: "post",
            data: {       
                Details: details,
                SignOrderStatus: signstatus
            },
            dataType: '',//?????????????????????JSON
            callback: function (result) {
                if(result != "1") {
                    var status = "";
                    var errreason = "";
                    if(result == "5") status = "????????????";
                    else if(result == "6") status = "????????????";
                    else if(result == "7") status = "????????????";

                    if(result == "5") errreason = "1.??????????????????????????????\n 2.?????????????????????????????????";
                    else if(result == "6") errreason = "1.?????????????????????????????????";
                    else if(result == "7") errreason = "1.?????????????????????????????????\n 2.?????????????????????0";
                    var swalError = {
                        title: "??????" + status +"??????\n ????????????:\n " + errreason ,
                        type: "warning",
                        confirmButtonText: '#6c757d',
                        confirmButtonText: "??????"
                    };
                    swal($.extend({}, swalError));
                    $('#SdnSendDate').datetimepicker('reset')
                    return;
                }
                $.bc({
                    url: "api/NormalSignOrders/SignOrders",
                    method: "post",
                    data: {       
                        Header: header,
                        Details: details,
                        SignOrderStatus: signstatus,
                        SdnSendDate: sdnsenddate
                    },
                    crud: '????????????',
                    btn: that,
                    callback: function (result) {
                        if(result) {                            
                            $('#normalsignorders-table').bootstrapTable('refresh');
                        }
                        var title = (result) ? "??????" : "??????";
                        var type = (result) ? "success" : "error";
                        if (result && $("#nav-shippingcost-tab").length > 0) {
                            lgbSwal({ title: "??????????????????", type: "success", timer: 500 });
                        } else {
                            swal({ title: "????????????" + title, type: type });
                        }                        
                        if (!result) {
                            return;
                        }
                        $('#SdnSendDate').datetimepicker('reset');
                        if ($("#nav-shippingcost-tab").length > 0) {                            
                            var row = $("#nav-shippingcost-tab").data("row");
                            row.SdnBack = "1";
                            row.ConfirmNotes = $("#SignStatus").getTextByValue($("#SignStatus").val());
                            $("#SignStatus_ShippingCost").val($("#SignStatus").getTextByValue($("#SignStatus").val()));
                            $("#nav-shippingcost-tab").data("row", row);
                            var shippingcost = new ShippingCostEntity();
                            var signorders = new SignOrdersEntity();
                            shippingcost.load(row);
                            signorders.load(row);
                            $("#nav-shippingcost-tab").trigger("click");
                        } else {
                            $("#dialogSignOrders").modal("hide");
                        }
                    }
                })
                
            }
        })
    });

    //????????????
    $("#SignCaseQty").change(function () {
        $("#SignQty").val("");
        var SignQty = $("#SignCaseQty").val() * $("#CaseCnt").val();
        $("#SignQty").val(SignQty);
        var ShipCaseQty = parseInt($("#ShipCaseQty").val());
        var SignCaseQty = parseInt($("#SignCaseQty").val());
        if(SignCaseQty == ShipCaseQty)
        {
            $("#RSCCode").val("");
            $("#RBCCode").val("");
            $("#RBCName").val("");

        } 
        
    });
    //????????????
    $("#SignQty").change(function () {
        var SignQty = $("#SignQty").val();
        var ShipQty = $("#ShipQty").val();
        if(SignQty == ShipQty)
        {
            $("#RSCCode").val("");
            $("#RBCCode").val("");
            $("#RBCName").val("");
        } 
    });

    SignOrdersEntity = function (options) {
        this.options = options
    }

    SignOrdersEntity.prototype = {
        load: function (row) {
            Reset();       
            headerDataBinder.reset();
            detailDataBinder.reset();
            row.OrderDate = $.momentFormat(row.OrderDate, "YYYY/MM/DD")
            row.DeliveryDate = $.momentFormat(row.DeliveryDate, "YYYY/MM/DD");
            headerDataBinder.load(row);
            $signorderstable.bootstrapTable('removeAll');
            $signorderstable.bootstrapTable('showLoading');
            $("#StorerKey").lgbSelect("disabled");
            $("#rowPickUp").css("display", (!row.PickUpAddress && !row.PickUpConsigneeKey && !row.PickUpName) ? "none" : "");
            var data = headerDataBinder.get();
            var tmskey = data.TMSKey;
            $.bc({
                url: "api/NormalSignOrders/RetrieveDetailByTMSKey?TMSKey=" + tmskey,
                callback: function (result) {  
                    $signorderstable.bootstrapTable('hideLoading');                 
                    $signorderstable.bootstrapTable('load', result);
                    //????????????
                    var status = "";
                    var ConfirmNotes = data.ConfirmNotes.trim();
                    switch (ConfirmNotes) { 
                        case '????????????':
                            status = "5";
                            break;                           
                        case '????????????':
                            status = "6";
                            break;
                        case '????????????':
                            status = "7";
                            break;                            
                    }
                    $("#SignStatus").lgbSelect('val', status);
                    $("#SignStatus_ShippingCost").val($("#SignStatus").getTextByValue(status));
                }
            })            
        }
    }
    
});