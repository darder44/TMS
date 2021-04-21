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
                ConsigneeKey: $("#txt_ConsigneeKey").val(),
                FullName: $("#txt_FullName").val(),
            });
        };  
    var $basecustomertable =
        $('#BaseCustomer-table').lgbTable({
            url: 'api/BaseCustomer',
            //資料繫結
            dataBinder: {
                map: {
                    Id: "#BCID",
                    StorerKey: "#StorerKey",
                    ConsigneeKey: "#ConsigneeKey",
                    AreaCode: "#AreaCode",
                    Zip: "#Zip",
                    FullName: "#FullName",
                    ShortName: "#ShortName",
                    Contact: "#Contact",
                    Phone: "#Phone",
                    ShipToAddress: "#ShipToAddress",
                    VehicleType: "#VehicleType",
                    DemandCode1: "#DemandCode1",
                    DemandCode2: "#DemandCode2",
                    ChannelType: "#ChannelType",
                    PickTool: "#PickTool",
                    Channel: "#Channel",
                    CodeDate1: "#CodeDate1",
                    CodeDate2: "#CodeDate2",
                    Notes: "#Notes",
                    Fax: "#Fax",
                    PalletType: "#PalletType",
                    PalletSpec: "#PalletSpec",
                    Penalties: "#Penalties",
                    DC: "#DC",
                    UpdateSource: "#UpdateSource",               
                    CustGroup: "#CustGroup",
                    CustomerType: "#CustomerType",
                    InvoiceAddress: "#InvoiceAddress",
                    OrderGroup: "#OrderGroup",
                    CustomerEAN: "#CustomerEAN",
                    SalesOffice: "#SalesOffice",
                    SoldTo: "#SoldTo",
                    SoldToName: "#SoldToName",
                    SoldToAddress: "#SoldToAddress",
                    ShipTo: "#ShipTo",
                    ShipToName: "#ShipToName",
                    SN: "#SN",
                    PaperSize: "#PaperSize",
                },
                //bootstrapTable: '#BaseCustomer-table',
                callback: function (data) {                    
                    //編輯Button
                    $("#StorerKey").attr("disabled", (data && data.oper === 'edit'));
                    $("#ConsigneeKey").attr("disabled", (data && data.oper === 'edit'));                    
                    if (data.oper !== 'edit') return;
                    $('#DC').prop('checked', (data.data.DC == "N") ? false : true);
                    //取得DC ID值，修改Class樣式修改
                    if (data.data.DC == "N") {
                        $('#DC').prop('checked', false);
                        $('#DC').parent().addClass("off");
                        $('#DC').parent().addClass("btn-default"); //移除Class樣式
                        $('#DC').parent().removeClass("btn-success");
                    } else {
                        $('#DC').prop('checked', true);
                        $('#DC').parent().removeClass("off");
                        $('#DC').parent().addClass("btn-success");
                        $('#DC').parent().removeClass("btn-default");
                    }
                }
            },
            smartTable: {
                singleSelect: false, //True：單選, False：複選
                showExport: false,
                showColumns: true,
                showRefresh: true,
                showToggle: true,
                sortName: 'ConsigneeKey',
                //查詢參數
                queryParams: queryParams,

                //Body欄位顯示
                columns: [
                    { title: "貨主編號", field: "StorerKey", sortable: true },
                    { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true, formatter: function (value) { return value == "" ? "" : $("#AreaCode").getTextByValue(value) } },
                    { title: "郵遞區號", field: "Zip", sortable: true },
                    { title: "全名", field: "FullName", sortable: true },
                    { title: "簡稱", field: "ShortName", sortable: true },
                    { title: "聯絡人", field: "Contact", sortable: true },
                    { title: "電話", field: "Phone", sortable: true },
                    { title: "到貨點地址", field: "ShipToAddress", sortable: true },
                    { title: "車種代碼", field: "VehicleType", sortable: true, formatter: function (value) { return value == "" ? "" : $("#VehicleType").getTextByValue(value) } },
                    { title: "特殊需求一", field: "DemandCode1", sortable: true, formatter: function (value) { return value == "" ? "" : $("#DemandCode1").getTextByValue(value) } },
                    { title: "特殊需求二", field: "DemandCode2", sortable: true, formatter: function (value) { return value == "" ? "" : $("#DemandCode1").getTextByValue(value) } },
                    { title: "通路型態", field: "ChannelType", sortable: true },
                    { title: "搬運工具", field: "PickTool", sortable: true, formatter: function (value) { return value == "" ? "" : $("#PickTool").getTextByValue(value) } },
                    { title: "通路別", field: "Channel", sortable: true },
                    { title: "允收期一", field: "CodeDate1", sortable: true },
                    { title: "允收期二", field: "CodeDate2", sortable: true },
                    { title: "備註", field: "Notes", sortable: true },
                    { title: "傳真", field: "Fax", sortable: true },
                    { title: "棧板類別", field: "PalletType", sortable: true },
                    { title: "棧板規格", field: "PalletSpec", sortable: true },
                    { title: "罰款通路", field: "Penalties", sortable: true },
                    { title: "統倉", field: "DC", sortable: true, formatter: function (value) { return value == "N" ? "否" : "是" } },
                    { title: "資料來源", field: "UpdateSource", sortable: true },                    
                    { title: "客戶群組", field: "CustGroup", sortable: true },
                    { title: "客戶型態", field: "CustomerType", sortable: true },
                    { title: "發票地址", field: "InvoiceAddress", sortable: true },
                    { title: "訂單群組", field: "OrderGroup", sortable: true },                    
                    { title: "業務辦公室", field: "SalesOffice", sortable: true },
                    { title: "銷售客編", field: "SoldTo", sortable: true },
                    { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                    { title: "銷售點地址", field: "SoldToAddress", sortable: true },
                    { title: "到貨客編", field: "ShipTo", sortable: true },
                    { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                    { title: "特殊一", field: "CustomerEAN", sortable: true },
                    { title: "特殊二", field: "SN", sortable: true },
                    { title: "紙張格式", field: "PaperSize", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "Adddate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } }
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
    $("#btn_deleteCustomer").click(function (element) {
        var Customerselections = $basecustomertable.bootstrapTable('getSelections'); //取得勾選資料
        if (Customerselections.length === 0) {
            lgbSwal({ title: '請選擇要刪除的數據', type: "warning" });
            return;
        }
        DeleteInfo(Customerselections);
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
                    url: "api/BaseCustomer",
                    data: rows,
                    method: "delete",
                    crud: '刪除客戶主檔',
                    callback: function (result) {
                        if (result) {
                            $basecustomertable.bootstrapTable('refresh'); //重整畫面
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

    $.bc({
        url: "api/BaseZip",        
        callback: function (result) {
            if (!result) return;
            $('#Zip').typeahead({
                source: result,
                displayText: function (item) {                    
                    return item.ZIPINFO
                },
                afterSelect: function (item) {
                    $("#Zip").val(item.ZIP);
                },
                showHintOnFocus: 'all',
                fitToElement: true,
                items: 'all'
            });
        }
    });
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

    //查詢客戶主檔主鍵是否重覆
    $("#ConsigneeKey").change(function () {
        $.bc({
            url: "api/BaseCustomer/RetrieveByStorerKeyAndConsigneeKey?ConsigneeKey=" + $(this).val() + "&StorerKey=" + $("#StorerKey").val(),
            callback: function (result) {
                $("#ConsigneeKey").data("ConsigneeKey", result ? true : false);
                var ret = $("#ConsigneeKey").data("ConsigneeKey");
                $("#btnSubmit").trigger("click.lgb.validate");
                return
            }
        })
    });

    $.validator.addMethod("CheckConsigneeKey", function (value, element, target) {
        var ret = $("#ConsigneeKey").data("ConsigneeKey");
        if (ret === undefined) return true;
        return !ret;
    }, "此貨主已有相同客戶編號，請重新輸入");    

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $basecustomertable.bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseCustomer/ExportExcel',
            queryParams: queryParams,
            FileName: '客戶主檔',
            Options: options,
            Sheets: [{
                SheetName: "BaseCustomer",
                sortName: 'ConsigneeKey',
                Columns: [
                    { title: "貨主編號", field: "StorerKey", sortable: true },
                    { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                    { title: "區碼", field: "AreaCode", sortable: true },
                    { title: "郵遞區號", field: "Zip", sortable: true },
                    { title: "全名", field: "FullName", sortable: true },
                    { title: "簡稱", field: "ShortName", sortable: true },
                    { title: "聯絡人", field: "Contact", sortable: true },
                    { title: "電話", field: "Phone", sortable: true },
                    { title: "到貨點地址", field: "ShipToAddress", sortable: true },
                    { title: "車種代碼", field: "VehicleType", sortable: true },
                    { title: "特殊需求一", field: "DemandCode1", sortable: true },
                    { title: "特殊需求二", field: "DemandCode2", sortable: true },
                    { title: "通路型態", field: "ChannelType", sortable: true },
                    { title: "搬運工具", field: "PickTool", sortable: true },
                    { title: "通路別", field: "Channel", sortable: true },
                    { title: "允收期一", field: "CodeDate1", sortable: true },
                    { title: "允收期二", field: "CodeDate2", sortable: true },
                    { title: "備註", field: "Notes", sortable: true },
                    { title: "傳真", field: "Fax", sortable: true },
                    { title: "棧板類別", field: "PalletType", sortable: true },
                    { title: "棧板規格", field: "PalletSpec", sortable: true },
                    { title: "罰款通路", field: "Penalties", sortable: true },
                    { title: "統倉", field: "DC", sortable: true },
                    { title: "資料來源", field: "UpdateSource", sortable: true },                    
                    { title: "客戶群組", field: "CustGroup", sortable: true },
                    { title: "客戶型態", field: "CustomerType", sortable: true },
                    { title: "發票地址", field: "InvoiceAddress", sortable: true },
                    { title: "訂單群組", field: "OrderGroup", sortable: true },                    
                    { title: "業務辦公室", field: "SalesOffice", sortable: true },
                    { title: "銷售客編", field: "SoldTo", sortable: true },
                    { title: "銷售客戶名稱", field: "SoldToName", sortable: true },
                    { title: "銷售點地址", field: "SoldToAddress", sortable: true },
                    { title: "到貨客編", field: "ShipTo", sortable: true },
                    { title: "到貨客戶名稱", field: "ShipToName", sortable: true },
                    { title: "特殊一", field: "CustomerEAN", sortable: true },
                    { title: "特殊二", field: "SN", sortable: true },
                    { title: "紙張格式", field: "PaperSize", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "Adddate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })
});