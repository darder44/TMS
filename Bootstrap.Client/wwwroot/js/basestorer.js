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
            }
             
        });
    };    
var $basestorertable =
    $('#basestorer-table').lgbTable({
        url: 'api/BaseStorer',
        //資料繫結
        dataBinder: {
            map: {
                Id: "#ID",
                StorerKey: "#StorerKey",
                Facility: "#Facility",
                ChineseName: "#ChineseName",
                EnglishName: "#EnglishName",
                ShortName: "#ShortName",
                Phone: "#Phone",              
                Contact: "#Contact",
                Address: "#Address",
                Description: "#Description",
                ShipListReport: "#ShipListReport",
                StorerStatus: "#StorerStatus",
                AddWho: "AddWho",
                AddDate: "AddDate",
                EditWho: "EditWho",
                EditDate: "EditDate"
            },
            callback: function (data) {                    
                //編輯Button
                $("#StorerKey").attr("disabled", (data && data.oper === 'edit'));
                $("#Facility").attr("disabled", (data && data.oper === 'edit'));
            },
            click: {
                //"#btn_delete": function () {} //讓底層的delete button沒有功能 
            }
        },
        smartTable: {            
            singleSelect: false, //True：單選, False：複選
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [      
                { title: "貨主代號", field: "StorerKey", sortable: true },
                { title: "轉運站", field: "Facility", sortable: true },
                { title: "中文名稱", field: "ChineseName", sortable: true },
                { title: "英文名稱", field: "EnglishName", sortable: true },
                { title: "簡稱", field: "ShortName", sortable: true },
                { title: "連絡電話", field: "Phone", sortable: true },
                { title: "連絡人", field: "Contact", sortable: true },
                { title: "地址", field: "Address", sortable: true },
                { title: "出貨單", field: "ShipListReport", sortable: true },
                { title: "貨主狀態", field: "StorerStatus", sortable: true },
                { title: "新增", field: "AddWho", sortable: true },
                { title: "新增時間", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
                { title: "編輯", field: "EditWho", sortable: true },
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
    $("#btn_delete").click(function (element) {
        var Storerselections = $basestorertable.bootstrapTable('getSelections'); //取得勾選資料
        if (Storerselections.length === 0) {
            lgbSwal({ title: '請選擇要刪除的數據', type: "warning" });
            return;
        }
        DeleteInfo(Storerselections);
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
                    url: "api/BaseStorer",
                    data: rows,
                    method: "delete",
                    crud: '刪除貨主主檔',
                    callback: function (result) {
                        if (result) {
                            $basestorertable.bootstrapTable('refresh'); //重整畫面
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

    //查詢主鍵是否重覆
    $("#StorerKey").change(function () {
        $.bc({
            url: "api/BaseStorer/RetrieveByStorerKey?StorerKey=" + $(this).val() ,
            callback: function (result) {
                $("#StorerKey").data("StorerKey", result ? true : false);
                var ret = $("#StorerKey").data("StorerKey");
                $("#btnSubmit").trigger("click.lgb.validate");
                return
            }
        })
    });    

    $.validator.addMethod("CheckStorerKey", function (value, element, target) {
        var ret = $("#StorerKey").data("StorerKey");
        if (ret === undefined) return true;
        return !ret;
    }, "貨主代碼重覆，請重新輸入");

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $basestorertable.bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseStorer/ExportExcel',
            queryParams: queryParams,
            FileName: '貨主主檔',
            Options: options,
            Sheets: [{
                SheetName: "BaseStorer",
                sortName: 'Storerkey',
                Columns: [
                    { title: "貨主代號", field: "StorerKey", sortable: true },
                    { title: "轉運站", field: "Facility", sortable: true },
                    { title: "中文名稱", field: "ChineseName", sortable: true },
                    { title: "英文名稱", field: "EnglishName", sortable: true },
                    { title: "簡稱", field: "ShortName", sortable: true },
                    { title: "連絡電話", field: "Phone", sortable: true },
                    { title: "連絡人", field: "Contact", sortable: true },
                    { title: "地址", field: "Address", sortable: true },
                    { title: "出貨單", field: "ShipListReport", sortable: true },
                    { title: "貨主狀態", field: "StorerStatus", sortable: true },
                    { title: "新增", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })
        
});