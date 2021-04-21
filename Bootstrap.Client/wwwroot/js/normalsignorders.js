$(function () {
// 出Bug了？
// 　　　∩∩
// 　　（´･ω･）
// 　 ＿|　⊃／(＿＿_
// 　／ └-(＿＿＿／
// 　￣￣￣￣￣￣￣
// 算了反正不是我寫的
// 　　 ⊂⌒／ヽ-、＿
// 　／⊂_/＿＿＿＿ ／
// 　￣￣￣￣￣￣￣
// 萬一是我寫的呢
// 　　　∩∩
// 　　（´･ω･）
// 　 ＿|　⊃／(＿＿_
// 　／ └-(＿＿＿／
// 　￣￣￣￣￣￣￣
// 算了反正改了一個又出三個
// 　　 ⊂⌒／ヽ-、＿
// 　／⊂_/＿＿＿＿ ／
// 　￣￣￣    
    var fromQuery = false;
    var autoShow = localStorage.getItem("normalsignorders-autoShow");
    if (autoShow == "true") {
        $('#autoShow').prop('checked', true);
        $('#autoShow').parent().removeClass("off");
        $('#autoShow').parent().addClass("btn-success");
        $('#autoShow').parent().removeClass("btn-default");
        $("#dialogAdvancedSearch").modal("show");
    }
    var autoSignOrder = localStorage.getItem("normalsignorders-autoSignOrder");
    if (autoSignOrder == "true") {
        $('#autoSignOrder').prop('checked', true);
        $('#autoSignOrder').parent().removeClass("off");
        $('#autoSignOrder').parent().addClass("btn-success");
        $('#autoSignOrder').parent().removeClass("btn-default");        
    }
    //
    if ($("#nav-shippingcost-tab").length == 0) {
        $("#dialogSignOrders").attr("data-keyboard", "true");
    }
    //簽單維護元件
    var signOrdersEntity = new SignOrdersEntity();
    //運費維護元件
    var shippingCostEntity = new ShippingCostEntity();

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
                TMSKey: $("#txt_TMSKey").val(), 
                RouteNo: $("#txt_RouteNo").val(), 
                ExternOrderKey: $("#txt_ExternOrderKey").val(), 
                SdnBack: function () {
                    var values = [];
                    $("#sel_sdnback option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                SignStatus: function () {
                    var values = [];
                    $("#sel_signstatus option:selected").each(function () {
                        var value = $(this).val();
                        values.push(value);
                    });                        
                    return values.join(",");
                },
                DoRouteDate_Start: $("#txt_DoRouteDate_Start").val(),
                DoRouteDate_End: $("#txt_DoRouteDate_End").val(),
                OrderDate_Start: $("#txt_OrderDate_Start").val(), 
                OrderDate_End: $("#txt_OrderDate_End").val(), 
                DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(), 
                DeliveryDate_End: $("#txt_DeliveryDate_End").val()

            });
        }; 
    //簽單維護table
    var $normalsignordersTable = $('#normalsignorders-table');
    $normalsignordersTable.lgbTable({
        //url: 'api/NormalSignOrders/RetrieveOrders',
        dataBinder: {
            map: {
            },
            bootstrapTable: '#normalsignorders-table',
            click: {
                '#btn_query': function () {                    
                    localStorage.setItem("normalsignorders-autoShow", $("#autoShow").prop('checked'));
                    autoShow = $("#autoShow").prop('checked');
                    localStorage.setItem("normalsignorders-autoSignOrder", $("#autoSignOrder").prop('checked'));
                    autoSignOrder = $("#autoSignOrder").prop('checked');
                    var options = this.options;
                    fromQuery = true;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/NormalSignOrders/RetrieveOrders'), pageNumber: 1 });
                    $.bootstrapTableQueryBtn(options);
                }
            }   
        },
        smartTable: {
            checkbox: true,
            singleSelect: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            showExport: false,
            sortName: 'TMSKey',
            search: true,
            showAdvancedSearchButton: true,
            detailView: true,
            queryParams: queryParams,
            columns: [
                { title: "貨主", field: "StorerKey", sortable: true },
                { title: "路線編號", field: "RouteNo", sortable: true },
                { title: "所屬倉別", field: "Facility", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "簽單狀態", field: "ConfirmNotes", sortable: true , formatter: function (value) { return value == "0" ? "" : value } },
                { title: "異常維護日期", field: "ConfirmDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "異常維護人", field: "ConfirmUser", sortable: true },
                { title: "簽單備註", field: "SdnNotes", sortable: true },
                { title: "簽單回傳日期", field: "SdnSendDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD HH:mm:ss'); } },
                { title: "簽單回傳人", field: "SdnSendWho", sortable: true},
                { title: "簽單回傳", field: "SdnBack", sortable: true, formatter: function (value) { return value == "1" ? "已回傳" : "未回傳" } },
                { title: "客戶回覆處理方式", field: "CustHandle", sortable: true },
                { title: "到貨地址", field: "ShipToAddress", sortable: true },
                { title: "郵遞區號", field: "Zip", sortable: true },
                { title: "訂單類別", field: "OrderType", sortable: true },
                { title: "訂單單號", field: "TMSKey", sortable: true },
                { title: "訂單日期", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "出車日期", field: "DoRouteDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "提貨客編", field: "PickUpConsigneeKey", sortable: true },
                { title: "提貨名稱", field: "PickUpName", sortable: true },
                { title: "提貨地址", field: "PickUpAddress", sortable: true },
                { title: "聯絡人", field: "Contact", sortable: true },
                { title: "電話", field: "Phone", sortable: true },
                { title: "急單", field: "UrgentMark", sortable: true },
                { title: "專車", field: "ReserveMark", sortable: true },
                { title: "冷藏", field: "ColdMark", sortable: true },
                { title: "發票回傳", field: "InvBack", sortable: true, formatter: function (value) { return $("#InvBack").getTextByValue(value) } }
            ],
            editButtons: {   
                id: "",            
                events: {
                    'click .signorders': function (e, value, row, index) {
                        $normalsignordersTable.bootstrapTable('uncheckAll');     
                        $normalsignordersTable.bootstrapTable('check', index);   
                        SignOrders(row);                        
                    },
                    'click .normalorders': function (e, value, row, index) { 
                        $normalsignordersTable.bootstrapTable('uncheckAll');     
                        $normalsignordersTable.bootstrapTable('check', index);  
                        NormalOrders([row], '.normalorders');
                    },
                    'click .unshippedorders': function (e, value, row, index) { 
                        $normalsignordersTable.bootstrapTable('uncheckAll');     
                        $normalsignordersTable.bootstrapTable('check', index);  
                        UnshippedOrders([row]);
                    },
                    'click .shippingcost': function (e, value, row, index) { 
                        $normalsignordersTable.bootstrapTable('uncheckAll');     
                        $normalsignordersTable.bootstrapTable('check', index);  
                    }
                }
            },
            onPostBody: function () {                
                var rows = $normalsignordersTable.bootstrapTable("getData", {useCurrentPage:false, includeHiddenRows:true} );
                if (rows.length == 1 && fromQuery) {
                    $normalsignordersTable.bootstrapTable('uncheckAll');     
                    $normalsignordersTable.bootstrapTable('check', 0);
                    if (autoSignOrder) {
                        $("#btn_signorders").trigger("click");
                    }
                }
                fromQuery = false;
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    url: 'api/NormalSignOrders/RetrieveOrderTrackByTMSKey?TMSKey=' + row.TMSKey.trim() ,
                    dataBinder: {
                        click: { '#btn_edit': function () {}, '#btnSubmit': function () {} },
                        bootstrapTable: ''
                    },
                    smartTable: {
                        checkboxHeader: false,
                        showExport: false, 
                        showColumns: true,
                        showRefresh: true,
                        showToggle: true,
                        checkbox: false,
                        paginationHAlign: "left",
                        pagination: true, //顯示分頁
                        search: false,
                        sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
                        pageSize: 10,
                        pageList: [10, 50, 100], //分頁筆數
                        toolbar: "",
                        editButtons: { 
                            id: "",
                            events: {}
                        },
                        columns: [
                            { title: "路線編號", field: "RouteNo", sortable: false ,width: 200},
                            { title: "趟次", field: "Sequence", sortable: false ,width: 100},
                            { title: "司機", field: "Driver", sortable: true ,width: 200},
                            { title: "車號", field: "VehicleKey", sortable: true ,width: 200},
                            { title: "起始位置", field: "StartAddress", sortable: false ,width: 1500},
                            { title: "目的位置", field: "EndAddress", sortable: true ,width: 6500}
                        ]
                    }
                })
            }
        }
    });

    //批次維護正常訂單
    $("#btn_normalorders").click(function () {       
        var selections = $normalsignordersTable.bootstrapTable('getSelections');
        if (selections.length === 0) {
            lgbSwal({ title: '請選擇要維護的訂單', type: "warning" });
            return;
        }
        NormalOrders(selections, this);

    });

    //呼叫正常訂單
    function NormalOrders(rows, that) {
        $.bc({
            url: 'api/NormalSignOrders/CheckOrderStatus',
            method: 'post',
            data: rows,
            btn: that,
            callback: function(result){
                if(result){
                    var swalCheckOptions = { //彈出提醒畫面
                        title: "批次維護正常訂單",
                        html: "此訂單已維護，確認是否再維護",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: '#dc3545', //確認按鈕顏色
                        cancelButtonColor: '#6c757d', //取消按鈕顏色
                        confirmButtonText: "維護",
                        cancelButtonText: "取消"
                    };
                    swal($.extend({}, swalCheckOptions)).then(function (result) {
                        if (result.value) {
                            $.bc({
                                url: 'api/NormalSignOrders/NormalOrders',
                                method: "post",
                                data: rows,
                                crud: '正常訂單',
                                btn: that,
                                callback: function (result) {
                                    if (result) {
                                        fromQuery = false;
                                        $normalsignordersTable.bootstrapTable('refresh');
                                    }
                                    var title = (result) ? "成功" : "失敗，選取的訂單中有異常訂單或未出訂單";
                                    var type = (result) ? "success" : "error";
                                    lgbSwal({
                                        title: "簽單維護" + title,
                                        type: type
                                    });
                                }
                            });
                        }
                        
                    });
                }
                else{
                    $.bc({
                        url: 'api/NormalSignOrders/NormalOrders',
                        method: "post",
                        data: rows,
                        crud: '正常訂單',
                        btn: that,
                        callback: function (result) {
                            if (result) {
                                fromQuery = false;
                                $normalsignordersTable.bootstrapTable('refresh');
                            }
                            var title = (result) ? "成功" : "失敗，選取的訂單中有異常訂單或未出訂單";
                            var type = (result) ? "success" : "error";
                            lgbSwal({
                                title: "簽單維護" + title,
                                type: type
                            });
                        }
                    });
                }
            }

        })
    }
    
    //未出訂單的異常原因的資料聯繫元件
    var unshippedordersData = new DataEntity({
        rsccode: '#txt_RSCCode',
        rbccode: '#txt_RBCCode',
        rbcname: '#txt_RBCName'
    });

    //批次維護未出訂單
    $("#btn_unshippedorders").click(function () {    
        var selections = $normalsignordersTable.bootstrapTable('getSelections');
        if (selections.length === 0) {
            lgbSwal({ title: '請選擇要維護的訂單', type: "warning" });
            return;
        }        
        UnshippedOrders(selections);
    });

    //呼叫未出訂單的異常原因視窗
    function UnshippedOrders(rows) {
        unshippedordersData.reset();
        $("#dialogunshippedorders").data('details', rows);
        $("#dialogunshippedorders").modal('show');
    }

    //呼叫未出訂單
    $("#btn_submit-unshippedorders").click(function () {
        var data = unshippedordersData.get();        
        data.selections = $("#dialogunshippedorders").data('details');        
        $.bc({
            url: 'api/NormalSignOrders/UnShippedOrders',
            method: "post",
            data: data,
            crud: '未出訂單',
            btn: this,
            callback: function (result) {
                if (result) {
                    $("#dialogunshippedorders").modal('hide');
                    fromQuery = false;
                    $normalsignordersTable.bootstrapTable('refresh');
                }
                var title = (result) ? "成功" : "失敗，選取的訂單中有出貨量不為0的訂單";
                var type = (result) ? "success" : "error";

                lgbSwal({
                    title: "簽單維護" + title,
                    type: type
                });
            }
        });
    });



    //簽單維護按鈕
    $("#btn_signorders").click(function () {            
        var selections = $normalsignordersTable.bootstrapTable('getSelections');
        if (selections.length == 0) {
            lgbSwal({ title: "請選擇要維護的簽單", type: "warning" });
            return;
        }
        if (selections.length > 1) {
            lgbSwal({ title: "此功能一次只能維護一張簽單", type: "warning" });
            return;
        }
        SignOrders(selections[0]);
    });

    //呼叫維護視窗
    function SignOrders(row) {
        var tmskey = row.TMSKey;
        var sdnsendwho = row.SdnSendWho;
        //非簽單行政，已維護簽單不可修改
        $.bc({
            url: "api/NormalSignOrders/CheckSdnBack",
            data: {
                TMSKey: tmskey,
                SdnSendWho: sdnsendwho
            },
            method: "post",
            dataType: '',//回傳格式不轉成JSON
            callback: function (result) {
                if (result != 1) {
                    var swalError = {
                        title: "此簽單已回傳,\n若要變更請先清空簽單狀態\n此簽單回傳者: " + result,
                        type: "warning",
                        confirmButtonText: '#6c757d',
                        confirmButtonText: "確定"
                    };
                    swal($.extend({}, swalError));
                    return;
                }
                $("#nav-signorder-tab").trigger("click");
                signOrdersEntity.load(row);
                shippingCostEntity.load(row);
                $("#dialogSignOrders").modal('show');
            }
        });
    };

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
        
        $("#sel_sdnback").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇...',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_sdnback").multiselect("setOptions", ConfigurationSet);
        $("#sel_sdnback").multiselect("rebuild");

        $("#sel_signstatus").multiselect({
            buttonClass: 'form-control',
            selectAllText: '全選',
            nonSelectedText: '請選擇...',
            allSelectedText: '全選',
            nSelectedText: '個選項',
            maxHeight: 400,
            buttonWidth: '150px'
        });
        $("#sel_signstatus").multiselect("setOptions", ConfigurationSet);
        $("#sel_signstatus").multiselect("rebuild");
    };  

    InitialSelect();

    //呼叫出車異常重組視窗
    $("#btn_deliveryabnormal").click(function () {
        var Selects = $normalsignordersTable.bootstrapTable('getSelections');
        if (Selects.length === 0) {
            lgbSwal({
                title: '請選擇要異常重組的路編',
                type: "warning"
            });
            return;
        }
        $("#dialogdeliveryabnormal").modal('show');
    });
    
    //提交
    $("#btnSubmit_deliveryabnormal").click(function () {
        var Selects = $normalsignordersTable.bootstrapTable('getSelections');
        var errorcode = $("#txt_DeliveryAbnormal").val();
        var data = {
            Selects: Selects,
            ErrorCode: errorcode
        }
        $.bc({
            url: "api/NormalSignOrders/BreakRoute",
            data: data,
            method: "post",
            crud: '出車異常打散',
            btn: this,
            callback: function (result) {
                if (result == "0") {
                    $("#dialogdeliveryabnormal").modal("hide");
                    fromQuery = false;
                    $normalsignordersTable.bootstrapTable('refresh');
                    lgbSwal({ title: "打散成功", type: "success" });
                }
                else
                {
                    var errreason = "";
                    if(result == "1") errreason = "已維護簽單資料";
                    if(result == "2") errreason = "已維護棧板資料";
                    if(result == "3") errreason = "僅能打散自己建立的路編";
                    var swalError = {
                        title: "打散失敗\n可能原因：\n" + errreason,
                        type: "warning",
                        confirmButtonText: '#6c757d',
                        confirmButtonText: "確定"
                    };
                    swal($.extend({}, swalError));
                }
            }
        });
        
    })

    //清空簽單狀態
    $("#btn_clearsignorder").click(function () {
        var Selects = $normalsignordersTable.bootstrapTable('getSelections');
        if (Selects.length === 0) {
            lgbSwal({
                title: '請選擇需要清空狀態的簽單',
                type: "warning"
            });
            return;
        }
        var swalDeleteOptions = { //彈出提醒畫面
            title: "清空簽單狀態",
            html: "您確定要清空選中的簽單嗎？",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {
                $.bc({
                    url: "api/NormalSignOrders/ClearSignOrders",
                    data: Selects,
                    method: "post",
                    dataType: '',//回傳格式不轉成JSON
                    crud: '清空簽單維護資料',
                    callback: function (result) {
                        if (result == "1") {
                            fromQuery = false;
                            $normalsignordersTable.bootstrapTable('refresh');                                        
                            lgbSwal({ title: "簽單狀態清空成功", type: "success" });
                        }
                        else
                        {
                            var swalError = {
                                title: "清空失敗\n僅可清空自己維護的簽單",
                                type: "warning",
                                confirmButtonText: '#6c757d',
                                confirmButtonText: "確定"
                            };
                            swal($.extend({}, swalError));
                        }
                    }
                });
            }
        })
        
    });

    //重新計費
    $("#btn_calculate").click(function () {
        var Selects = $normalsignordersTable.bootstrapTable('getSelections');
        if (Selects.length === 0) {
            lgbSwal({
                title: '請選擇需要清空狀態的簽單',
                type: "warning"
            });
            return;
        }
        var swalDeleteOptions = { //彈出提醒畫面
            title: "重新計費",
            html: "您確定要重新計費嗎",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {
                $.bc({
                    url: "api/NormalSignOrders/Calculate",
                    data: Selects,
                    method: "post",
                    dataType: '',//回傳格式不轉成JSON
                    crud: '重新計費',
                    callback: function (result) {
                        if (result == "1") {
                            fromQuery = false;
                            $normalsignordersTable.bootstrapTable('refresh');                                        
                            lgbSwal({ title: "重新計費成功", type: "success" });
                        }
                        else
                        {
                            var errorreason = "";
                            if(result == "2") errorreason = "僅可重算自己維護的簽單";
                            else if(result == "3") errorreason = "有未維護的簽單";
                            var swalError = {
                                title: errorreason,
                                type: "warning",
                                confirmButtonText: '#6c757d',
                                confirmButtonText: "確定"
                            };
                            swal($.extend({}, swalError));
                        }
                    }
                });
            }
        })
        
    });

});
/**
 *                                         ,s555SB@@&                          
 *                                      :9H####@@@@@Xi                        
 *                                     1@@@@@@@@@@@@@@8                       
 *                                   ,8@@@@@@@@@B@@@@@@8                      
 *                                  :B@@@@X3hi8Bs;B@@@@@Ah,                   
 *             ,8i                  r@@@B:     1S ,M@@@@@@#8;                 
 *            1AB35.i:               X@@8 .   SGhr ,A@@@@@@@@S                
 *            1@h31MX8                18Hhh3i .i3r ,A@@@@@@@@@5               
 *            ;@&i,58r5                 rGSS:     :B@@@@@@@@@@A               
 *             1#i  . 9i                 hX.  .: .5@@@@@@@@@@@1               
 *              sG1,  ,G53s.              9#Xi;hS5 3B@@@@@@@B1                
 *               .h8h.,A@@@MXSs,           #@H1:    3ssSSX@1                  
 *               s ,@@@@@@@@@@@@Xhi,       r#@@X1s9M8    .GA981               
 *               ,. rS8H#@@@@@@@@@@#HG51;.  .h31i;9@r    .8@@@@BS;i;          
 *                .19AXXXAB@@@@@@@@@@@@@@#MHXG893hrX#XGGXM@@@@@@@@@@MS        
 *                s@@MM@@@hsX#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&,      
 *              :GB@#3G@@Brs ,1GM@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@B,     
 *            .hM@@@#@@#MX 51  r;iSGAM@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@8     
 *          :3B@@@@@@@@@@@&9@h :Gs   .;sSXH@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@:    
 *      s&HA#@@@@@@@@@@@@@@M89A;.8S.       ,r3@@@@@@@@@@@@@@@@@@@@@@@@@@@r    
 *   ,13B@@@@@@@@@@@@@@@@@@@5 5B3 ;.         ;@@@@@@@@@@@@@@@@@@@@@@@@@@@i    
 *  5#@@#&@@@@@@@@@@@@@@@@@@9  .39:          ;@@@@@@@@@@@@@@@@@@@@@@@@@@@;    
 *  9@@@X:MM@@@@@@@@@@@@@@@#;    ;31.         H@@@@@@@@@@@@@@@@@@@@@@@@@@:    
 *   SH#@B9.rM@@@@@@@@@@@@@B       :.         3@@@@@@@@@@@@@@@@@@@@@@@@@@5    
 *     ,:.   9@@@@@@@@@@@#HB5                 .M@@@@@@@@@@@@@@@@@@@@@@@@@B    
 *           ,ssirhSM@&1;i19911i,.             s@@@@@@@@@@@@@@@@@@@@@@@@@@S   
 *              ,,,rHAri1h1rh&@#353Sh:          8@@@@@@@@@@@@@@@@@@@@@@@@@#:  
 *            .A3hH@#5S553&@@#h   i:i9S          #@@@@@@@@@@@@@@@@@@@@@@@@@A. 
 *
 *
 *    看你妹呀！
 */