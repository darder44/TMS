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
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "配送倉別", field: "Facility", sortable: true },
                { title: "訂單號碼", field: "TMSKey", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "採購單號", field: "CustomerOrderKey", sortable: true },
                { title: "訂單類別", field: "OrderType", sortable: true },
                { title: "訂單日期", field: "OrderDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "到貨日期", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "提貨客編", field: "PickUpConsigneeKey", sortable: true },
                { title: "提貨名稱", field: "PickUpName", sortable: true },
                { title: "提貨地址", field: "PickUpAddress", sortable: true },
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "配送點編號", field: "ShipTo", sortable: true },
                { title: "配送點名稱", field: "ShipToName", sortable: true },
                { title: "到貨地址", field: "ShipToAddress", sortable: true },
                { title: "郵遞區號", field: "Zip", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true },
                { title: "聯絡人", field: "Contact", sortable: true },
                { title: "電話", field: "Phone", sortable: true },
                { title: "訂單備註", field: "Notes", sortable: true },
                { title: "箱數", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "板數", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "急單", field: "UrgentMark", sortable: true },
                { title: "專車", field: "ReserveMark", sortable: true },
                { title: "冷藏", field: "ColdMark", sortable: true },
                { title: "來源路編", field: "OriginalRouteNo", sortable: true }
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
                    { title: "貨主", field: "StorerKey", sortable: true },
                    { title: "配送倉別", field: "Facility", sortable: true },
                    { title: "訂單號碼", field: "TMSKey", sortable: true },
                    { title: "WaveKey", field: "WaveKey", sortable: true },
                    { title: "倉庫狀態", field: "AllocateStatus", sortable: true },                    
                    { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                    { title: "郵遞區號", field: "Zip", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true },
                    { title: "採購單號", field: "CustomerOrderKey", sortable: true },
                    { title: "訂單類別", field: "OrderType", sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                    { title: "訂單日期", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                    { title: "到貨日期", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                    { title: "提貨客編", field: "PickUpConsigneeKey", sortable: true },
                    { title: "提貨名稱", field: "PickUpName", sortable: true },
                    { title: "提貨地址", field: "PickUpAddress", sortable: true },
                    { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                    { title: "客戶簡稱", field: "ShortName", sortable: true },
                    { title: "配送點編號", field: "ShipTo", sortable: true },
                    { title: "配送點名稱", field: "ShipToName", sortable: true },
                    { title: "到貨地址", field: "ShipToAddress", sortable: true },                    
                    { title: "聯絡人", field: "Contact", sortable: true },
                    { title: "電話", field: "Phone", sortable: true },
                    { title: "訂單備註", field: "Notes", sortable: true },
                    { title: "箱數", field: "CaseQty", sortable: true },
                    { title: "板數", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                    { title: "急單", field: "UrgentMark", sortable: true },
                    { title: "專車", field: "ReserveMark", sortable: true },
                    { title: "冷藏", field: "ColdMark", sortable: true },
                    { title: "原路編", field: "OriginalRouteNo", sortable: true }
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
            lgbSwal({ title: '請選擇要保留的訂單', type: "warning" });
            return;
        }    
        var canreserve = arrselections.every(function (row) { return row.Status.trim() == "1" });
        if (!canreserve) {
            lgbSwal({ title: '選取的訂單包含了轉運訂單', type: "warning" });
            return;
        }
        $("#btn_reserveorder").attr('disabled', true);
        swal({ //彈出提醒畫面
            title: "即將保留訂單",
            html: "您確定要保留嗎？",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/ReserveOrders',
                    method: "post",
                    data: arrselections,
                    crud: '訂單保留',
                    callback: function (result) {
                        $("#btn_reserveorder").attr('disabled', false);
                        if (result) {                           
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "訂單保留" + title,
                            type: type
                        });
                    }
                });
            } else {
                $("#btn_reserveorder").attr('disabled', false);
            }
        });        
    });


    //將主檔資料更新進訂單明細
    $("#btn_updateskudata").click(function () {
        var $table = $('#normalstandbyorders-table');
        var arrselections = $table.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '請選擇要更新的訂單', type: "warning" });
            return;
        }    
        var canupdate = arrselections.every(function (row) { return row.Status.trim() == "1" });
        if (!canupdate) {
            lgbSwal({ title: '選取的訂單包含了轉運訂單', type: "warning" });
            return;
        }
        $("#btn_updateskudata").attr('disabled', true);
        swal({ //彈出提醒畫面
            title: "即將更新箱板材重",
            html: "您確定要更新嗎？",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/UpdateSkuData',
                    method: "post",
                    data: arrselections,
                    crud: '更新箱板材重',
                    callback: function (result) {
                        $("#btn_updateskudata").attr('disabled', false);
                        if (result) {                           
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "更新" + title,
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
            lgbSwal({ title: '請選擇訂單', type: "warning" });
            return;
        }
        else {        
            $selectedorderstable.bootstrapTable('showLoading');
            var TMSKey = arrselections.map(function (element, index) { return element["TMSKey"]; });
            $.bc({
                url: "api/NormalStandbyOrders/RetrieveSelectedOrders",
                data: TMSKey,
                method: "post", 
                crud: '讀取待建立路編訂單',
                btn: this,
                callback: function (result) {
                    if (result.length <= 0) {
                        swal({ title: '無法取得訂單資料，請再試一次', type: "error" });
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
            lgbSwal({ title: '司機資料有誤，請確認', type: "warning" });
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
            lgbSwal({ title: '請選擇轉運站', type: "warning" });
            return;
        }
        var that = this;

        if (data.Transfer) {
            swal({ //彈出提醒畫面
                title: "即將建立路編",
                html: "此路編有選擇轉運站，您確定要建立嗎？",
                type: "warning",
                showCancelButton: true,
                //confirmButtonColor: '#dc3545', //確認按鈕顏色
                cancelButtonColor: '#6c757d', //取消按鈕顏色
                confirmButtonText: "確定",
                cancelButtonText: "取消"
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
        //Add by Terry 20201214 新增防呆，出車日、預計出車日不能小於今日
        $("#txt_DoRouteDate").removeClass('is-invalid');
        $("#txt_ExpectDate").removeClass('is-invalid');
        if(data.DoRouteDate >= $.momentFormat(Date(), "YYYY/MM/DD") && data.ExpectDate >= $.momentFormat(Date(), "YYYY/MM/DD")){
            $.bc({
                url: "api/NormalStandbyOrders/CreateNewRoute",
                data: data,
                method: "post",
                crud: '建立路編',
                btn: that,
                callback: function (result) {
                    if (result && result.success) { 
                        if (typeof result.routeno !== "string") {
                            swal({ title: '建立路編成功，但無法取得路線編號，請關閉視窗後重新整理已排車訂單', type: "error" });
                            return;
                        }
                        $("#dialogAssign").modal("hide");
                        var swalRouteNo = {
                            title: "路線編號已建立 ",
                            html: "路線編號：" + result.routeno,
                            type: "success",
                            confirmButtonText: '#6c757d',
                            confirmButtonText: "確定"
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
                        $('#sel_Transfer').parent().addClass("btn-default"); //移除Class樣式
                        $('#sel_Transfer').parent().removeClass("btn-success");
                        $("#sel_TransferTo").fadeOut();
                    } else {
                        if (!result) {
                            swal({ title: "派車失敗，請再試一次", type: "warning", confirmButtonText: '確定' });
                            return;
                        }
                        swal({ title: result.message, type: "warning", confirmButtonText: '確定' });
                    }
                }
            })
        }
        else{
            if(data.DoRouteDate < $.momentFormat(Date(), "YYYY/MM/DD")) { $("#txt_DoRouteDate").addClass('is-invalid'); }
            if(data.ExpectDate < $.momentFormat(Date(), "YYYY/MM/DD")) { $("#txt_ExpectDate").addClass('is-invalid'); }

            swal({ title: "出車日期或預計出車日期小於今日，請重新輸入", type: "warning", confirmButtonText: '確定' });
            return;
        }
    }

    //轉運按鈕顯示欲轉運倉別
    $("#sel_Transfer").change(function () {
        $("#txt_TransferTo").lgbSelect('val', '');
        $(this).prop("checked") ? $("#sel_TransferTo").fadeIn() && $("#txt_TransferTo").val('') : $("#sel_TransferTo").fadeOut() && $("#txt_TransferTo").val('');
     });

    //已排車訂單 tab
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
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "轉運倉別", field: "FacilityName", sortable: true },
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "預計出車日期", field: "ExpectDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "預計出車時間", field: "ExpectTime", sortable: true },
                { title: "訂單箱數", field: "CaseQty", sortable: true },
                { title: "訂單板數", field: "PalletQty", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "訂單材積", field: "Cube", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "訂單重量", field: "Weight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "載貨碼頭", field: "DockNo", sortable: true },
                { title: "車號", field: "VehicleKey", sortable: true },
                { title: "司機", field: "Driver", sortable: true },
                { title: "司機電話", field: "DriverPhone", sortable: true },
                { title: "備註", field: "Notes", sortable: true },
                { title: "趟次", field: "DriveTimes", sortable: true },
                { title: "列印次數", field: "VLListCount", sortable: true },
                { title: "列印時間", field: "VLListPrintDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "排車時間", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "排車者", field: "AddWho", sortable: true, formatter (value, row) { return (value === "") ? "" : $.format("{0}({1})", row.DisplayName, value); } },
                { title: "修改時間", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "修改者", field: "EditWho", sortable: true },
                //{ title: "回傳狀態", field: "ToWMS", sortable: true, formatter: function (value) { return $("#txt_ToWMS").getTextByValue(value) } }
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
                        pagination: true, //顯示分頁
                        sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
                        pageSize: 10,
                        pageList: [10, 20, 50], //分頁筆數
                        toolbar: '',
                        height: "",
                        columns: [
                            { title: "路線編號", field: "RouteNo", sortable: false },
                            { title: "貨主", field: "StorerKey", sortable: false },
                            { title: "訂單號碼", field: "TMSKey", sortable: false },
                            { title: "貨主單號", field: "ExternOrderKey", sortable: false },
                            { title: "訂單日期", field: "OrderDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "到貨日期", field: "DeliveryDate", sortable: false, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                            { title: "客戶編號", field: "ConsigneeKey", sortable: false },
                            { title: "客戶名稱", field: "ShortName", sortable: false },
                            { title: "到貨地址", field: "ShipToAddress", sortable: false },
                            { title: "訂單量", field: "OrderQty", sortable: false },
                            { title: "出貨量", field: "ShipQty", sortable: false },
                            { title: "訂單箱數", field: "CaseQty", sortable: false },
                            { title: "訂單板數", field: "PalletQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                            { title: "訂單材積", field: "Cube", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)} },
                            { title: "訂單重量", field: "Weight", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)}},
                            { title: "備註", field: "Notes", sortable: false }
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
            lgbSwal({ title: '請選擇要出車的路編', type: "warning" });
            $("#btn_checkroute").attr("disabled", false);
            return;
        }
        //篩選I單路編
        var checkroutenos = selectedroutes.filter(function (row) { return row.OrderType == "I" }).map(function (element, index) { return element.RouteNo; });
        //篩選所有路編
        var allroutenos = selectedroutes.map(function (element, index) { return element.RouteNo; });   
        if (checkroutenos.length == 0) {
            CarLeave(allroutenos);
            return;
        }     
        //檢查I單路編是否能出車
        $.bc({
            url: 'api/NormalRouteList/CheckBeforeCarLeave',
            method: "post",
            data: checkroutenos,
            callback: function (result) {
                //回傳result: 是否已完成扣帳
                if (!result) {
                    lgbSwal({ title: '有訂單尚未扣帳，請確認', type: "error" });
                    $("#btn_checkroute").attr("disabled", false);
                    return;
                }
                CarLeave(allroutenos);              
            }
        });        
    });

    function CarLeave(routes) {
        swal({ //彈出提醒畫面
            title: "出車確認",
            html: "路編訂單將無法再修改，您確定要出車嗎？",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalRouteList/CheckCarLeave',
                    method: "post",
                    data: routes,
                    callback: function (result) {
                        //回傳result: 是否已出車
                        if (!result) {
                            swal({ title: '有路編已經出車確認，請將畫面重新整理', type: "error" });
                            $("#btn_checkroute").attr("disabled", false);
                            return;
                        }
                        $.bc({
                            url: 'api/NormalRouteList/CarLeave',
                            method: "post",
                            data: routes,
                            crud: '出車確認',
                            callback: function (result) {
                                $("#btn_checkroute").attr("disabled", false);
                                if (result) {
                                    $("#dialogNew").modal('hide');     
                                    $orderstable.bootstrapTable('refresh');
                                    $normalroutelist.bootstrapTable('refresh');  
                                }
                                var title = (result) ? "成功" : "失敗";
                                var type = (result) ? "success" : "error";
                                lgbSwal({
                                    title: "出車" + title,
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
            swal({ title: '請選擇要回傳的路編', type: "warning" });
            return;
        }
        var routenos = selectedroutes.map(function (element, index) { return element.RouteNo; });
        $.bc({
            url: 'api/NormalRouteList/ReturnWMS',
            method: "post",
            data: routenos,
            crud: '回傳倉庫',
            btn: this,
            callback: function (result) {
                if (result) {
                    $("#dialogNew").modal('hide');     
                    $orderstable.bootstrapTable('refresh');
                    $normalroutelist.bootstrapTable('refresh');  
                }
                var title = (result) ? "成功" : "失敗";
                var type = (result) ? "success" : "error";
                swal({
                    title: "回傳" + title,
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
            crud: '訂單轉入',
            btn: this,
            callback: function (result) {     
                $(that).attr("disabled", false);
                $customertemptable.bootstrapTable('refresh');
                var title = (result && result > 0) ? "成功" : "失敗";
                var type = (result && result > 0) ? "success" : "error";
                swal({
                    title: "訂單轉入" + title,
                    text: "訂單轉入筆數：" + result,
                    type: type,
                    confirmButtonText: "確定"
                });
            }
        });
    });

    $("#btn_delroute").click(function () {
        var selectedroutes = $normalroutelist.bootstrapTable('getSelections');
        if (selectedroutes.length === 0) {
            lgbSwal({ title: '請選擇要刪除的路編', type: "warning" });
            return;
        }
        DeleteRoutes(selectedroutes);
    })

    function DeleteRoutes(rows) {
        if (rows.every(function (row) { return row.ToWMS.trim() == "1" })) {
            swal({ title: '無法刪除已回傳的路編', type: "warning" });
            return;
        }
        var swalDeleteOptions = {
            title: "刪除路編",
            html: "您確定要刪除已選擇的路編嗎",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545',
            cancelButtonColor: '#6c757d',
            confirmButtonText: "我要刪除",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (!result.value) return;
            var routenos = rows.map(function (element, index) { return element.RouteNo; }); 
            $.bc({
                url: "api/NormalRouteList/DeleteRoutes",
                data: routenos,
                method: "delete",
                crud: '刪除路編',
                callback: function (result) {
                    lgbSwal({ title: result ? '刪除成功' : '刪除失敗', type: result ? 'success' : 'error' });
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
        { title: "貨主", field: "StorerKey", sortable: true },
        { title: "訂單號碼", field: "TMSKey", sortable: true },
        { title: "貨主單號", field: "ExternOrderKey", sortable: true },
        { title: "客戶編號", field: "ConsigneeKey", sortable: true },
        { title: "客戶簡稱", field: "ShortName", sortable: true },
        { title: "到貨地址", field: "ShipToAddress", sortable: true },
        { title: "郵遞區號", field: "Zip", sortable: true },
        { title: "聯絡人", field: "Contact", sortable: true },
        { title: "電話", field: "Phone", sortable: true }
    ]
    //訂單轉入及客戶異動 table
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
                        swal({ title: "請先維護郵遞區號", type: "error" });
                        return;
                    }
                    $.bc({                        
                        url: 'api/NormalStandbyOrders/SaveCustomer',
                        data: data, 
                        method: "post",
                        crud: '保存待維護客戶',
                        btn: '#btnSubmit_Temp',
                        callback: function (result) {
                            if (result && result.success) {
                                lgbSwal({ title: "維護成功", type: "success" });
                                $("#dialogNew_Temp").modal('hide');
                                options.bootstrapTable.bootstrapTable('refresh');                                
                                $orderstable.bootstrapTable('refresh');
                                return;
                            }                            
                            swal({
                                title: "維護失敗",
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
                    swal({ title: '找不到客戶，請先維護客戶主檔', type: "error" });
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

    //保留訂單 tab
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
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "配送倉別", field: "Facility", sortable: true },
                { title: "訂單號碼", field: "TMSKey", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "採購單號", field: "CustomerOrderKey", sortable: true },
                { title: "訂單類別", field: "OrderType", sortable: true },
                { title: "訂單日期", field: "OrderDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "到貨日期", field: "DeliveryDate", sortable: true , formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }},
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "配送點編號", field: "ShipTo", sortable: true },
                { title: "配送點名稱", field: "ShipToName", sortable: true },
                { title: "到貨地址", field: "ShipToAddress", sortable: true },
                { title: "郵遞區號", field: "Zip", sortable: true },
                { title: "區碼", field: "AreaCode", sortable: true, formatter: function (value) { return $("#sel_AreaCode_reserverorder").getTextByValue(value) } },
                { title: "聯絡人", field: "Contact", sortable: true },
                { title: "電話", field: "Phone", sortable: true },
                { title: "訂單備註", field: "Notes", sortable: true },
                { title: "箱數", field: "CaseQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "板數", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "急單", field: "UrgentMark", sortable: true },
                { title: "專車", field: "ReserveMark", sortable: true },
                { title: "冷藏", field: "ColdMark", sortable: true },
                { title: "原路編", field: "OriginalRouteNo", sortable: true }
            ]
        }
    });

    $("#btn_returnorder").click(function () {           
        var arrselections = $reserveorderstable.bootstrapTable('getSelections');
        if (arrselections.length === 0) {
            lgbSwal({ title: '請選擇要移動的訂單', type: "warning" });
            return;
        }        
        ReturnOrder(arrselections);
    });

    function ReturnOrder(rows) {
        swal({ //彈出提醒畫面
            title: "即將移至待排車",
            html: "您確定要移動至待排車嗎？",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        }).then(function (result) {
            if (result.value) {
                $.bc({
                    url: 'api/NormalStandbyOrders/ReturnOrder',
                    method: "post",
                    data: rows,
                    crud: '訂單移動',
                    callback: function (result) {
                        if (result) {
                            $("#dialogNew").modal('hide');
                            $orderstable.bootstrapTable('refresh');
                            $reserveorderstable.bootstrapTable('refresh');
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({
                            title: "訂單移動" + title,
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
            lgbSwal({ title: '請選擇要刪除的訂單', type: "warning" });
            return;
        }
        DeleteOrders(selectedorders);
    })

    function DeleteOrders(rows) {
        var swalDeleteOptions = {
            title: "刪除訂單",
            html: "訂單將封存在系統內不再顯示，您確定要刪除已選擇的訂單嗎？",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545',
            cancelButtonColor: '#6c757d',
            confirmButtonText: "我要刪除",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (!result.value) return;
            $.bc({
                url: "api/NormalOrders/DeleteOrders",
                data: rows.map(function (element, index) { return element.TMSKey; }),
                method: "delete",
                crud: '刪除訂單',
                callback: function (result) {
                    if (result && result.success) { 
                        lgbSwal({ title: '刪除成功', type: "success" });    
                        $orderstable.bootstrapTable('refresh');
                        $reserveorderstable.bootstrapTable('refresh');
                        return;
                    }
                    swal({
                        title: "刪除失敗",
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

    //路編維護
    $("#btn_updaterouteno").click(function () {    
        var selections = $normalroutelist.bootstrapTable('getSelections');
        if (selections.length === 0) {
            lgbSwal({ title: '請選擇要維護的路編', type: "warning" });
            return;
        }
        if (selections.length > 1) {
            lgbSwal({ title: '一次僅能維護一趟路編', type: "warning" });
            return;
        }
        UpdateRouteNo(selections[0]);
    });

    //路編維護資料聯繫元件
    var updateroutenoData = new DataEntity({
        RouteNo: "#txt_RouteNo_update",
        DoRouteDate: '#txt_DoRouteDate_update',
        DockNo: '#txt_Dock_update',
        ExpectDate: '#txt_ExpectDate_update',
        ExpectTime: '#txt_ExpectTime_update'        
    });

    //呼叫路編維護視窗
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

    //提交路編維護
    $("#btn_submit-updaterouteno").click(function () {
        var driver = CheckDriverUpdate({ VehicleKey: $('#VehicleKey_update').val(), Driver: $('#Driver_update').val() });
        if (!driver) {
            lgbSwal({ title: '司機資料有誤，請確認', type: "warning" });
            return;
        }
        var data = updateroutenoData.get();
        data.Driver = driver
        swal({ //彈出提醒畫面
            title: "即將維護路編" + data.RouteNo,
            html: "您確定要維護嗎？",
            type: "warning",
            showCancelButton: true,
            //confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        }).then(function (result) {
            if (result.value) {                
                $.bc({
                    url: 'api/NormalRouteList/UpdateRouteNo',
                    method: "post",
                    data: data,
                    crud: '路編維護',
                    callback: function (result) {                        
                        if (result) {
                            $("#dialogupdaterouteno").modal('hide');
                            $normalroutelist.bootstrapTable('refresh');
                            updateroutenoData.reset();
                            driverUpdateDataEntity.reset();
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
        
                        lgbSwal({
                            title: "路編維護" + title,
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
            selectAllText: '全選',
            nonSelectedText: '請選擇貨主',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey_temp").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey_temp").multiselect("rebuild");

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

        $("#sel_orderstatus").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇訂單狀態',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_orderstatus").multiselect("setOptions", ConfigurationSet);
        $("#sel_orderstatus").multiselect("rebuild");

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

        $("#sel_storerkey_reserveorder").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇貨主',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_storerkey_reserveorder").multiselect("setOptions", ConfigurationSet);
        $("#sel_storerkey_reserveorder").multiselect("rebuild");

    };  
    InitialSelect();

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