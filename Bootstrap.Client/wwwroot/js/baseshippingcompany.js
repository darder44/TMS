$(function () {
    var queryParams = function (params) {
        return $.extend(params,
            {
                CompanyKey: $("#txt_CompanyKey").val(),
                FullName: $("#txt_FullName").val(),
                Contact: $("#txt_Contact").val(),
                Phone: $("#txt_Phone").val()
            });
    };

    $('#baseshippingcompany-table').lgbTable({
        url: 'api/BaseShippingCompany',
        //資料繫結
        dataBinder: {
            map: {
                Id: "#ID",
                CompanyKey: "#CompanyKey",
                FullName: "#FullName",
                EngName: "#EngName",
                ShortName: "#ShortName",
                Phone: "#Phone",
                Contact: "#Contact",
                Address: "#Address",
                Description: "#Description"
            },
            bootstrapTable: '#baseshippingcompany-table',
            callback: function (data) {                    
                //編輯Button
                $("#CompanyKey").attr("disabled", (data && data.oper === 'edit'));                
            }
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'CompanyKey',
            idField: 'CompanyKey',
            //查詢參數
            queryParams: queryParams,

            //Body欄位顯示
            columns: [
                { title: "公司代碼", field: "CompanyKey", sortable: true },
                { title: "中文名稱", field: "FullName", sortable: true },
                { title: "英文名稱", field: "EngName", sortable: true },
                { title: "簡稱", field: "ShortName", sortable: true },
                { title: "電話", field: "Phone", sortable: true },
                { title: "聯絡人", field: "Contact", sortable: true },
                { title: "地址", field: "Address", sortable: true },
                { title: "說明", field: "Description", sortable: true },
                { title: "新增者", field: "AddWho", sortable: true },
                { title: "新增日期", field: "AddDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
                { title: "編輯者", field: "EditWho", sortable: true },
                { title: "編輯日期", field: "EditDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss')} },
            ]
        }
    });   

    //查詢貨運公司代碼主鍵是否重覆
    $("#CompanyKey").change(function () {
        $("#CompanyKey").data("CompanyKey", false);
        $.bc({
            url: "api/BaseShippingCompany/RetrieveByCompanyKey?CompanyKey=" + $(this).val() ,
            callback: function (result) {
                $("#CompanyKey").data("CompanyKey", result ? true : false);
                var ret = $("#CompanyKey").data("CompanyKey");
                $("#btnSubmit").trigger("click.lgb.validate");
                return;
            }
        })
    });

    $.validator.addMethod("CheckCompanyKey", function (value, element, target) {
        var ret = $("#CompanyKey").data("CompanyKey");
        if (ret === undefined) return true;
        return !ret;
    }, "貨運公司代碼重覆，請重新輸入");

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#baseshippingcompany-table').bootstrapTable('getOptions');
        ExcelExport({
            url: 'api/BaseShippingCompany/ExportExcel',
            queryParams: queryParams,
            FileName: '貨運公司',
            Options: options,
            Sheets: [{
                SheetName: "BaseShippingCompany",
                sortName: 'CompanyKey',
                Columns: [
                    { title: "公司代碼", field: "CompanyKey", sortable: true },
                    { title: "中文名稱", field: "FullName", sortable: true },
                    { title: "英文名稱", field: "EngName", sortable: true },
                    { title: "簡稱", field: "ShortName", sortable: true },
                    { title: "電話", field: "Phone", sortable: true },
                    { title: "聯絡人", field: "Contact", sortable: true },
                    { title: "地址", field: "Address", sortable: true },
                    { title: "說明", field: "Description", sortable: true },
                    { title: "新增者", field: "AddWho", sortable: true },
                    { title: "新增時間", field: "AddDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' },
                    { title: "編輯者", field: "EditWho", sortable: true },
                    { title: "編輯時間", field: "EditDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD HH:mm:ss' }
                ]
            }]
        });
    })
});