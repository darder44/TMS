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
                CostCode: $("#txt_CostCode").val(),
            });
    };
    var costcodeData = new DataEntity({
        Id: "#ID",
        Storerkey: "#Storerkey",
        CostCode: "#CostCode",
        CostName: "#CostName",
        UOM: "#UOM",
        Receivable: "#Receivable",
        Payable: "#Payable",
        AreaStart: "#AreaStart",
        AreaEnd: "#AreaEnd",
        Notes: "#Notes",
        CostKind: "#CostKind",
        ARnoDistribution: "#ARnoDistribution",
        APnoDistribution: "#APnoDistribution",
        APnoFix: "#APnoFix",
        Abbreviation: "#Abbreviation",
        Display: "#Display",
        MinimumCharge: "#MinimumCharge",
        Facility: "#Facility"
    });
    var $basecostcodetable =
        $('#BaseCostCode-table').lgbTable({
            url: 'api/BaseCostCode/Get',
            //資料繫結
            dataBinder: {
                //新增編輯對應html的id,編輯時把選取的row的每個欄位資料帶入對應的html元素
                map: {
                    Id: "#ID",
                    Storerkey: "#Storerkey",
                    CostCode: "#CostCode",
                    CostName: "#CostName",
                    UOM: "#UOM",
                    Receivable: "#Receivable",
                    Payable: "#Payable",
                    AreaStart: "#AreaStart",
                    AreaEnd: "#AreaEnd",
                    Notes: "#Notes",
                    CostKind: "#CostKind",
                    ARnoDistribution: "#ARnoDistribution",
                    APnoDistribution: "#APnoDistribution",
                    APnoFix: "#APnoFix",
                    Abbreviation: "#Abbreviation",
                    Display: "#Display",
                    MinimumCharge: "#MinimumCharge",
                    Facility: "#Facility"
                },
                callback: function (data) {
                    $("#Storerkey").attr("disabled", (data && data.oper === 'edit'));
                    $("#CostCode").attr("disabled", (data && data.oper === 'edit'));
                    $("#Facility").attr("disabled", (data && data.oper === 'edit'));
                },
                click: {
                    '#btnSubmit': function () {}, //讓底層的保存 button沒有功能 
                    "#btn_delete": function () {} //讓底層的delete button沒有功能 
                }
            },
            smartTable: {
                singleSelect: false, //True：單選, False：複選
                showExport: false,
                showColumns: true,
                showRefresh: true,
                showToggle: true,
                sortName: 'CostCode',
                //查詢參數
                queryParams: queryParams,

                //Body欄位顯示
                columns: [
                    { title: "貨主編號", field: "Storerkey", sortable: true },
                    { title: "代碼", field: "CostCode", sortable: true },
                    { title: "請款類別", field: "CostKind", sortable: true },
                    { title: "單位", field: "UOM", sortable: true },
                    { title: "應收單價", field: "Receivable", sortable: true },
                    { title: "應付單價", field: "Payable", sortable: true },
                    { title: "起點", field: "AreaStart", sortable: true },
                    { title: "迄點", field: "AreaEnd", sortable: true },
                    { title: "計費名稱", field: "CostName", sortable: true },
                    { title: "說明", field: "Notes", sortable: true },
                    { title: "應收不分攤", field: "ARnoDistribution", sortable: true },
                    { title: "應付不分攤", field: "APnoDistribution", sortable: true },
                    { title: "最低補貼加總", field: "MinimumCharge", sortable: true },
                    { title: "倉別", field: "Facility", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true }
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

    //保存按鈕
    $('#btnSubmit').click(function () {
        var data = costcodeData.get();
        $.bc({
            url: "api/BaseCostCode/Save",
            data: data,
            method: "post",
            crud: '保存計費',
            btn: this,
            callback: function (result) {
                if (result) {
                    $basecostcodetable.bootstrapTable('refresh'); //重整畫面
                    $("#dialogNew").modal("hide");
                } 
                var title = (result) ? "成功" : "失敗";
                var type = (result) ? "success" : "error";
                lgbSwal({ title: "保存" + title, type: type });
            }
        });
    });

    //刪除按鈕
    $("#btn_delete_costcode").click(function (element) {
        var CostCodeselections = $basecostcodetable.bootstrapTable('getSelections'); //取得勾選資料
        if (CostCodeselections.length === 0) {
            lgbSwal({ title: '請選擇要刪除的數據', type: "warning" });
            return;
        }
        DeleteInfo(CostCodeselections)
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
                    url: "api/BaseCostCode/Delete",
                    data: rows,
                    method: "delete",
                    crud: '刪除計費',
                    callback: function (result) {
                        if (result) {
                            $basecostcodetable.bootstrapTable('refresh'); //重整畫面
                            $("#dialogNew").modal("hide");
                        } 
                        var title = (result) ? "成功" : "失敗";
                        var type = (result) ? "success" : "error";
                        lgbSwal({ title: "刪除" + title, type: type });
                    }
                });
            }
        });
    }
    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $basecostcodetable.bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseCostCode/ExportExcel',
            queryParams: queryParams,
            FileName: '計費代碼',
            Options: options,
            Sheets: [{
                SheetName: "BaseCostCode",
                sortName: 'Storerkey',
                Columns: [
                    { title: "貨主編號", field: "Storerkey", sortable: true },
                    { title: "代碼", field: "CostCode", sortable: true },
                    { title: "請款類別", field: "CostKind", sortable: true },
                    { title: "單位", field: "UOM", sortable: true },
                    { title: "應收單價", field: "Receivable", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "應付單價", field: "Payable", sortable: true, dataType: 2, dataFormat: '0.###' },
                    { title: "起點", field: "AreaStart", sortable: true },
                    { title: "迄點", field: "AreaEnd", sortable: true },
                    { title: "計費名稱", field: "CostName", sortable: true },
                    { title: "說明", field: "Notes", sortable: true },
                    { title: "應收不分攤", field: "ARnoDistribution", sortable: true },
                    { title: "應付不分攤", field: "APnoDistribution", sortable: true },
                    { title: "最低補貼加總", field: "MinimumCharge", sortable: true },
                    { title: "倉別", field: "Facility", sortable: true }
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
    InitialSelect();

    //新增內查詢主鍵是否重覆
    $("#CostCode").change(function () {
        $.bc({
            url: "api/BaseCostCode/RetrieveByStorerkeyAndCostCode?CostCode=" + $(this).val() + "&Storerkey=" + $("#Storerkey").val(), 
            callback: function (result) {
                    var ret = result && result.length > 0;
                    $("#CostCode").data("CostCode", ret ? true : false);
                    var ret = $("#CostCode").data("CostCode");
                    $("#btnSubmit").trigger("click.lgb.validate");
                    return
            }
        })
    });
    $.validator.addMethod("CheckCostCode", function (value, element, target) {
        var ret = $("#CostCode").data("CostCode");
        if (ret === undefined) return true;
        return !ret;
    }, "此貨主已有相同計費代碼，請重新輸入");
    
});