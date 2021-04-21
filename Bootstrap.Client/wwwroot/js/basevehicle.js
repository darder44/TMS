$(function () {
    var queryParams = function (params) {
        return $.extend(params,
            {
                VehicleKey: $("#txt_VehicleKey").val(),
                Driver: $("#txt_Driver").val(),
                DriverPhone: $("#txt_DriverPhone").val(),
                Receiver: $("#txt_Receiver").val(),
                CompanyKey: $("#txt_CompanyKey").val(),
                VehicleType: $("#txt_VehicleType").val(),
                BoxType: $("#txt_BoxType").val(),
                UnloadingType: $("#txt_UnloadingType").val(),
                EmployType: $("#txt_EmployType").val(),
                Active: $("#txt_Active").val(),
                PND: $("#txt_PND").val(),
            });
    };

    var $basevehicletable =
        $('#basevehicle-table').lgbTable({
            url: 'api/BaseVehicle',
            //資料繫結
            dataBinder: {
                map: {
                    Id: "#ID",
                    VehicleKey: "#VehicleKey",
                    CompanyKey: "#CompanyKey",
                    VehicleType: "#VehicleType",
                    LoadingWeight: "#LoadingWeight",
                    LoadingCube: "#LoadingCube",
                    Driver: "#Driver",
                    DriverPhone: "#DriverPhone",
                    Description: "#Description",
                    LoadingPallet: "#LoadingPallet",
                    CarWeight: "#CarWeight",
                    BoxType: "#BoxType",
                    CarHeight: "#CarHeight",
                    UnloadingType: "#UnloadingType",
                    EmployType: "#EmployType",
                    Receiver: "#Receiver",
                    PND: "#PND",
                    ApproveWho: "#ApproveWho",
                    ApproveDate: "#ApproveDate",
                    ConfirmWho: "#ConfirmWho",
                    ConfirmDate: "#ConfirmDate",
                    Active: "#Active"
                },
                callback: function (data) {
                    $("#VehicleKey").attr("disabled", (data && data.oper === 'edit'));
                    $("#Driver").attr("disabled", (data && data.oper === 'edit'));
                    if (data.oper !== 'edit') return;
                    $('#PND').prop('checked', (data.data.PND == "N") ? false : true);
                    //取得PND 值，修改Class樣式修改
                    if (data.data.PND == "N") {
                        $('#PND').prop('checked', false);
                        $('#PND').parent().addClass("off");
                        $('#PND').parent().addClass("btn-default"); //移除Class樣式
                        $('#PND').parent().removeClass("btn-success");
                    } else {
                        $('#PND').prop('checked', true);
                        $('#PND').parent().removeClass("off");
                        $('#PND').parent().addClass("btn-success");
                        $('#PND').parent().removeClass("btn-default");
                    }
                },
                click: {
                    "#btn_delete": function () {}
                }
            },
            smartTable: {
                singleSelect: false, //True：單選, False：複選
                showExport: false,
                showColumns: true,
                showRefresh: true,
                showToggle: true,
                sortName: 'VehicleKey',
                //查詢參數
                queryParams: queryParams,
                //Body欄位顯示
                columns: [
                    { title: "狀態", field: "Active", sortable: true, formatter: function (value) { return value == "1" ? "使用中" : "未使用" } },
                    { title: "車牌號碼", field: "VehicleKey", sortable: true },
                    { title: "駕駛人", field: "Driver", sortable: true },
                    { title: "貨運公司", field: "CompanyKey", sortable: true, formatter: function (value) { return value == "" ? "" : $("#CompanyKey").getTextByValue(value) } },
                    { title: "車種", field: "VehicleType", sortable: true, formatter: function (value) { return value == "" ? "" : $("#VehicleType").getTextByValue(value) } },
                    { title: "裝載重量", field: "LoadingWeight", sortable: true, formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} },
                    { title: "裝載材積", field: "LoadingCube", sortable: true, formatter: function(value) { return value == "" ? "" : $.roundDecimal(value, 5)} },
                    { title: "電話", field: "DriverPhone", sortable: true },
                    { title: "說明", field: "Description", sortable: true },
                    { title: "裝載板數", field: "LoadingPallet", sortable: true },
                    { title: "總重", field: "CarWeight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)} },
                    { title: "車廂形式", field: "BoxType", sortable: true, formatter: function (value) { return value == "" ? "" : $("#BoxType").getTextByValue(value); } },
                    { title: "車床高度", field: "CarHeight", sortable: true , formatter: function(value) { return $.roundDecimal(value, 5)} },
                    { title: "裝卸方式", field: "UnloadingType", sortable: true, formatter: function (value) { return value == "" ? "" : $("#UnloadingType").getTextByValue(value); } },
                    { title: "僱用方式", field: "EmployType", sortable: true, formatter: function (value) { return value == "" ? "" : $("#EmployType").getTextByValue(value); } },
                    { title: "請款人", field: "Receiver", sortable: true },
                    { title: "PND到貨追蹤", field: "PND", sortable: true, formatter: function (value) { return value == "N" ? "否" : "是" } },
                    { title: "審核", field: "ApproveWho", sortable: true },
                    { title: "審核時間", field: "ApproveDate", sortable: true },
                    { title: "確認", field: "ConfirmWho", sortable: true },
                    { title: "確認時間", field: "ConfirmDate", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} }
                ],
                editButtons: {
                    events: {
                        'click .delInfo': function (e, value, row) {
                            DeleteInfo([row]);
                        }
                    }
                }
            }
        });

    //刪除按鈕
    $("#btn_delete_vehicle").click(function (element) {
        var Vehicleselections = $basevehicletable.bootstrapTable('getSelections'); //取得勾選資料
        if (Vehicleselections.length === 0) {
            lgbSwal({ title: '請選擇要刪除的數據', type: "warning" });
            return;
        }        
        DeleteInfo(Vehicleselections)
    });

    function DeleteInfo(rows) {
        var swalDeleteOptions = { //彈出提醒畫面
            title: "刪除數據",
            html: "您確定要刪除選中的所有數據嗎",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "我要刪除",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {
                $.bc({
                    url: "api/BaseVehicle",
                    data: rows,
                    method: "delete",
                    crud: '刪除車輛資料',
                    callback: function (result) {
                        if (result) {
                            $basevehicletable.bootstrapTable('refresh'); //重整畫面
                            $("#dialogNew").modal("hide");
                        }
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({ title: "保存" + title, type: type });
                    }
                });
            }
        });
    }
    
    //查詢主鍵車號+駕駛人 是否重覆
    $("#Driver").change(function () {
        var VehicleKey = $("#VehicleKey").val();
        $.bc({
            url: "api/BaseVehicle/RetrieveByVehicleKeyAndDriver?Driver=" + $(this).val() + "&VehicleKey=" + VehicleKey,
            callback: function (result) {
                $("#Driver").data("Driver", result ? true : false);
                var ret = $("#Driver").data("Driver");
                $("#btnSubmit").trigger("click.lgb.validate");
                return
            }
        })
        
    });

    $.validator.addMethod("CheckDriver", function (value, element, target) {
        var ret = $("#Driver").data("Driver");
        if (ret === undefined) return true;
        return !ret;
    }, "此車號已有相同駕駛人，請重新輸入");

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $basevehicletable.bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseVehicle/ExportExcel',
            queryParams: queryParams,
            FileName: '車輛資料',
            Options: options,
            Sheets: [{
                SheetName: "BaseVehicle",
                sortName: 'VehicleKey',
                Columns: [
                    { title: "車牌號碼", field: "VehicleKey", sortable: true },
                    { title: "貨運公司", field: "CompanyKey", sortable: true },
                    { title: "車種", field: "VehicleType", sortable: true },
                    { title: "裝載重量", field: "LoadingWeight", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "裝載材積", field: "LoadingCube", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "駕駛人", field: "Driver", sortable: true },
                    { title: "電話", field: "DriverPhone", sortable: true },
                    { title: "說明", field: "Description", sortable: true },
                    { title: "裝載板數", field: "LoadingPallet", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "總重", field: "CarWeight", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "車廂形式", field: "BoxType", sortable: true },
                    { title: "車床高度", field: "CarHeight", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "裝卸方式", field: "UnloadingType", sortable: true },
                    { title: "僱用方式", field: "EmployType", sortable: true },
                    { title: "請款人", field: "Receiver", sortable: true },
                    { title: "PND到貨追蹤", field: "PND", sortable: true },
                    { title: "審核", field: "ApproveWho", sortable: true },
                    { title: "審核時間", field: "ApproveDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "確認", field: "ConfirmWho", sortable: true },
                    { title: "確認時間", field: "ConfirmDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "狀態", field: "Active", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })

});