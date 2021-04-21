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
                        lgbSwal({ title: '只能選擇一項要編輯的數據', type: "warning" });
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
            paginationHAlign: "left", //分頁顯示位置
            pagination: true, //顯示分頁
            sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
            pageSize: 5,
            pageList: [10, 50, 100], //分頁筆數
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
                { title: "項次", field: "OrderLineNumber", formatter: function (value) { return parseInt(value); }, sortable: false },
                { title: "品號", field: "Sku", sortable: false },
                { title: "品名", field: "Descr", sortable: false },
                { title: "訂單個數", field: "OrderQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "訂單箱數", field: "CaseQty", sortable: false, formatter: function (value) { return $.roundDecimal(value, 5) } },
                { title: "出貨個數", field: "ShipQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "出貨箱數", field: "ShipCaseQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "簽收個數", field: "SignQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "簽收箱數", field: "SignCaseQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "異常原因", field: "RSCCode", sortable: false, formatter: function (value) { return value == "" ? value : $("#RSCCode").getTextByValue(value) } },
                { title: "責任歸屬", field: "RBCCode", sortable: false, formatter: function (value) { return value == "" ? value : $("#RBCCode").getTextByValue(value) } },
                { title: "責屬人", field: "RBCName", sortable: false },
                { title: "箱入數", field: "CaseCnt", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "小單位材積", field: "StdCube", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "小單位重量", field: "StdWeight", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
            ]
        }
    });

    $("#btn_SignOrders_UpdateDetail").parent().css("display", "none"); 
    var editing = false;

    function EditRow(row, index) {
        if (editing) {
            swal({ title: "尚有明細在編輯中", type: "warning" });
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
            swal({ title: "請輸入簽收量", type: "error" });
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
            swal({ title: "請選擇簽單狀態", type: "error" });
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
            dataType: '',//回傳格式不轉成JSON
            callback: function (result) {
                if(result != "1") {
                    var status = "";
                    var errreason = "";
                    if(result == "5") status = "正常訂單";
                    else if(result == "6") status = "異常訂單";
                    else if(result == "7") status = "未出訂單";

                    if(result == "5") errreason = "1.有異常原因或責任歸屬\n 2.簽單量加總不等於出貨量";
                    else if(result == "6") errreason = "1.沒有異常原因或責任歸屬";
                    else if(result == "7") errreason = "1.沒有異常原因或責任歸屬\n 2.簽單量加總不為0";
                    var swalError = {
                        title: "維護" + status +"失敗\n 可能原因:\n " + errreason ,
                        type: "warning",
                        confirmButtonText: '#6c757d',
                        confirmButtonText: "確定"
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
                    crud: '簽單維護',
                    btn: that,
                    callback: function (result) {
                        if(result) {                            
                            $('#normalsignorders-table').bootstrapTable('refresh');
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        if (result && $("#nav-shippingcost-tab").length > 0) {
                            lgbSwal({ title: "簽單維護成功", type: "success", timer: 500 });
                        } else {
                            swal({ title: "簽單維護" + title, type: type });
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

    //簽收箱數
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
    //簽收個數
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
                    //簽單狀態
                    var status = "";
                    var ConfirmNotes = data.ConfirmNotes.trim();
                    switch (ConfirmNotes) { 
                        case '正常訂單':
                            status = "5";
                            break;                           
                        case '異常訂單':
                            status = "6";
                            break;
                        case '未出訂單':
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