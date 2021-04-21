$(function () {
    InitialSelect();
    
    var queryParams = function (params) {
        return $.extend(params,
            {
                StorerKey: function () {
                    var values = [];
                    $("#sel_Storerkey option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                ConfirmNotes: function () {
                    var values = [];
                    $("#sel_ConfirmNotes option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                SignDateS: $("#txt_SignDateS").val(),
                SignDateE: $("#txt_SignDateE").val(),
                DeliveryDateS: $("#txt_DeliveryDateS").val(),
                DeliveryDateE: $("#txt_DeliveryDateE").val(),
                RSCCode: $("#txt_RSCCode").val(),
                RBCCode: $("#txt_RBCCode").val(),
                RBCName: $("#txt_RBCName").val(),
                VehicleKey: $("#txt_VehicleKey").val(),
                Consigneekey: $("#txt_ConsigneeKey").val(),
                TMSKey: $("#txt_TMSKey").val(),
                Externorderkey: $("#txt_ExternOrderKey").val(),
                Sku: $("#txt_Sku").val()
            });
    };
    $('#reportdeliveryabnormal-table').lgbTable({
        //url: 'api/ReportDeliveryAbnormal',
        //資料繫結
        dataBinder: {
            map: {
                StorerKey: "#StorerKey",
                ShortName: "#ShortName",
                VehicleKey: "VehicleKey",
                Consigneekey: "#Consigneekey",
                Externorderkey: "#Externorderkey",
                RouteNo: "#RouteNo",
                Orderdate: "#Orderdate",
                DeliveryDate: "#DeliveryDate",
                Sku: "#Sku",
                Descr: "#Descr",
                Notes2: "#Notes2",
                OrderQty: "#OrderQty",
                CaseQty: "#CaseQty",
                ShipQty: "#ShipQty",
                ShipCaseQty: "#ShipCaseQty",
                SignQty: "#SignQty",
                SignCaseQty: "#SignCaseQty",
                ReturnQty: "#ReturnQty",
                ReturnCaseQty: "#ReturnCaseQty",
                Busr1: "#Busr1",
                CaseCnt: "#CaseCnt",
                RSCCode: "#RSCCode",
                RBCCode: "#RBCCode",
                CustHandle: "#CustHandle",
                TMSKeyTMS: "#TMSKeyTMS",
                ConfirmDate: "#ConfirmDate",
                SDNNotes: "#SDNNotes"
            },
            bootstrapTable: '#reportdeliveryabnormal-table',
            click: {
                '#btn_query': function () {                    
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/ReportDeliveryAbnormal'), pageNumber: 1 })
                    $.bootstrapTableQueryBtn(options);
                }
            }
        },
        smartTable: {
            singleSelect: false, //True：單選, False：複選
            showExport: false, //匯出功能
            showColumns: true, //使用者可選擇想看到的欄位
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: false,
            search: true,
            showAdvancedSearchButton: true,
            //查詢參數
            queryParams: queryParams,
            //Body欄位顯示
            columns: [
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "客戶名稱", field: "ShortName", sortable: true },
                { title: "配送車號", field: "VehicleKey", sortable: true },
                { title: "客戶編號", field: "Consigneekey", sortable: true },
                { title: "訂單號碼", field: "Externorderkey", sortable: true },
                { title: "訂單日期", field: "Orderdate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }  },
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); }  },
                { title: "品號", field: "Sku", sortable: true },
                { title: "品名", field: "Descr", sortable: true },
                { title: "條碼", field: "BarCode", sortable: true },
                { title: "訂單數量", field: "OrderQty", sortable: true },
                { title: "訂單箱數", field: "CaseQty", sortable: true },
                { title: "出貨數量", field: "ShipQty", sortable: true },
                { title: "出貨箱數", field: "ShipCaseQty", sortable: true },
                { title: "簽收數量", field: "SignQty", sortable: true },
                { title: "簽收箱數", field: "SignCaseQty", sortable: true },
                { title: "退回數量", field: "ReturnQty", sortable: true },
                { title: "退回箱數", field: "ReturnCaseQty", sortable: true },
                { title: "單位", field: "Uom", sortable: true },
                { title: "箱入數", field: "CaseCnt", sortable: true },
                { title: "異常原因", field: "RSCCode", sortable: true },
                { title: "責任歸屬", field: "RBCCode", sortable: true, },
                { title: "責屬人", field: "RBCName", sortable: true },
                { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                { title: "單號", field: "TMSKey", sortable: true },
                { title: "簽單日期", field: "ConfirmDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "簽單狀態", field: "ConfirmNotes", sortable: true, formatter: function (value) { return $("#sel_ConfirmNotes").getMultiSelectTextByValue(value) } },
            ]
        }
    });

    function InitialSelect() {
        var ConfigurationSet = {
            includeSelectAllOption: true,
            enableFiltering: false
        };
        $("#sel_Storerkey").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇貨主',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_Storerkey").multiselect("setOptions", ConfigurationSet);
        $("#sel_Storerkey").multiselect("rebuild");

        $("#sel_ConfirmNotes").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇簽單狀態',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_ConfirmNotes").multiselect("setOptions", ConfigurationSet);
        $("#sel_ConfirmNotes").multiselect("rebuild");
    };  

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $('#reportdeliveryabnormal-table').bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋配送異常表", type: "warning", confirmButtonText: '確定' });
            return;
        }
        var values = [];     
        $("#sel_Storerkey option:selected").each(function () {
            var storerKey = $(this).val();
            if(storerKey == "LIAH01"){
                var value = {
                    SheetName: storerKey,
                    sortName: "StorerKey",
                    Columns: [
                        { title: "貨主", field: "StorerKey", sortable: true },
                        { title: "客戶名稱", field: "ShortName", sortable: true },
                        { title: "配送車號", field: "VehicleKey", sortable: true },
                        { title: "客戶編號", field: "Consigneekey", sortable: true },
                        { title: "訂單號碼", field: "Externorderkey", sortable: true },
                        { title: "RI單號", field: "ShippingNumber", sortable: true },
                        { title: "訂單日期", field: "Orderdate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                        { title: "到貨日期", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                        { title: "品號", field: "Sku", sortable: true },
                        { title: "品名", field: "Descr", sortable: true },
                        { title: "條碼", field: "BarCode", sortable: true },
                        { title: "訂單數量", field: "OrderQty", sortable: true },
                        { title: "訂單箱數", field: "CaseQty", sortable: true },
                        { title: "出貨數量", field: "ShipQty", sortable: true },
                        { title: "出貨箱數", field: "ShipCaseQty", sortable: true },
                        { title: "簽收數量", field: "SignQty", sortable: true },
                        { title: "簽收箱數", field: "SignCaseQty", sortable: true },
                        { title: "退回數量", field: "ReturnQty", sortable: true },
                        { title: "退回箱數", field: "ReturnCaseQty", sortable: true },
                        { title: "單位", field: "Uom", sortable: true },
                        { title: "箱入數", field: "CaseCnt", sortable: true },
                        { title: "異常原因", field: "RSCCode", sortable: true },
                        { title: "責任歸屬", field: "RBCCode", sortable: true },
                        { title: "責屬人", field: "RBCName", sortable: true },
                        { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                        { title: "單號", field: "TMSKey", sortable: true },
                        { title: "簽單日期", field: "ConfirmDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單狀態", field: "ConfirmNotes", sortable: true }
                    ]
                }
            }
            else {
                var value = {
                    SheetName: storerKey,
                    sortName: "StorerKey",
                    Columns: [
                        { title: "貨主", field: "StorerKey", sortable: true },
                        { title: "客戶名稱", field: "ShortName", sortable: true },
                        { title: "配送車號", field: "VehicleKey", sortable: true },
                        { title: "客戶編號", field: "Consigneekey", sortable: true },
                        { title: "訂單號碼", field: "Externorderkey", sortable: true },
                        { title: "訂單日期", field: "Orderdate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                        { title: "到貨日期", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                        { title: "品號", field: "Sku", sortable: true },
                        { title: "品名", field: "Descr", sortable: true },
                        { title: "條碼", field: "BarCode", sortable: true },
                        { title: "訂單數量", field: "OrderQty", sortable: true },
                        { title: "訂單箱數", field: "CaseQty", sortable: true },
                        { title: "出貨數量", field: "ShipQty", sortable: true },
                        { title: "出貨箱數", field: "ShipCaseQty", sortable: true },
                        { title: "簽收數量", field: "SignQty", sortable: true },
                        { title: "簽收箱數", field: "SignCaseQty", sortable: true },
                        { title: "退回數量", field: "ReturnQty", sortable: true },
                        { title: "退回箱數", field: "ReturnCaseQty", sortable: true },
                        { title: "單位", field: "Uom", sortable: true },
                        { title: "箱入數", field: "CaseCnt", sortable: true },
                        { title: "異常原因", field: "RSCCode", sortable: true },
                        { title: "責任歸屬", field: "RBCCode", sortable: true },
                        { title: "責屬人", field: "RBCName", sortable: true },
                        { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                        { title: "單號", field: "TMSKey", sortable: true },
                        { title: "簽單日期", field: "ConfirmDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                        { title: "簽單狀態", field: "ConfirmNotes", sortable: true },
                    ]
                }
            }
            values.push(value);
        }); 
        if (values.length == 0){
            var value = {
                SheetName: "配送異常表",
                sortName: "StorerKey",
                Columns: [
                    { title: "貨主", field: "StorerKey", sortable: true },
                    { title: "客戶名稱", field: "ShortName", sortable: true },
                    { title: "配送車號", field: "VehicleKey", sortable: true },
                    { title: "客戶編號", field: "Consigneekey", sortable: true },
                    { title: "訂單號碼", field: "Externorderkey", sortable: true },
                    { title: "訂單日期", field: "Orderdate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                    { title: "到貨日期", field: "DeliveryDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD'  },
                    { title: "品號", field: "Sku", sortable: true },
                    { title: "品名", field: "Descr", sortable: true },
                    { title: "條碼", field: "BarCode", sortable: true },
                    { title: "訂單數量", field: "OrderQty", sortable: true },
                    { title: "訂單箱數", field: "CaseQty", sortable: true },
                    { title: "出貨數量", field: "ShipQty", sortable: true },
                    { title: "出貨箱數", field: "ShipCaseQty", sortable: true },
                    { title: "簽收數量", field: "SignQty", sortable: true },
                    { title: "簽收箱數", field: "SignCaseQty", sortable: true },
                    { title: "退回數量", field: "ReturnQty", sortable: true },
                    { title: "退回箱數", field: "ReturnCaseQty", sortable: true },
                    { title: "單位", field: "Uom", sortable: true },
                    { title: "箱入數", field: "CaseCnt", sortable: true },
                    { title: "異常原因", field: "RSCCode", sortable: true },
                    { title: "責任歸屬", field: "RBCCode", sortable: true },
                    { title: "責屬人", field: "RBCName", sortable: true },
                    { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                    { title: "單號", field: "TMSKey", sortable: true },
                    { title: "簽單日期", field: "ConfirmDate", sortable: true, dataType: 3, dataFormat: 'YYYY/MM/DD' },
                    { title: "簽單狀態", field: "ConfirmNotes", sortable: true },
                ]
            }
            values.push(value);
        }
        ExcelExport({
            url: 'api/ReportDeliveryAbnormal/ExportExcel',
            queryParams: queryParams,
            FileName: '配送異常表',
            Options: options,
            Sheets: values
        });       
    })
});
