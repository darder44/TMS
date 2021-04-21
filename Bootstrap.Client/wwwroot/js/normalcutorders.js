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
                TMSKey: $("#txt_TMSKey").val(),
                ConsigneeKey: $("#txt_ConsigneeKey").val(),
                Zip: $("#txt_Zip").val(),
                ExternOrderKey: $("#txt_ExternOrderKey").val(),
                OrderDate_Start: $("#txt_OrderDate_Start").val(),
                OrderDate_End: $("#txt_OrderDate_End").val(),
                DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(),
                DeliveryDate_End: $("#txt_DeliveryDate_End").val()
                 
            });
        };    
    //初始化table
    var CutOrderDetailTable = "#CutOrderDetail-table";
    var $normalcutordersTable = $('#normalcutorders-table');
    $normalcutordersTable.lgbTable({
        url: 'api/NormalCutOrders/RetrieveOrders',
        dataBinder: {
            bootstrapTable: '#normalcutorders-table'          
        },
        smartTable: {
            singleSelect: true,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'TMSKey',
            detailView: true,
            queryParams: queryParams,
            columns: [
                { title: "貨主代號", field: "StorerKey", sortable: true },
                { title: "訂單單號", field: "TMSKey", sortable: true },
                { title: "貨主單號", field: "ExternOrderKey", sortable: true },
                { title: "訂單日期", field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日期", field: "DeliveryDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "客戶編號", field: "ConsigneeKey", sortable: true },
                { title: "客戶簡稱", field: "ShortName", sortable: true },
                { title: "到貨地址", field: "ShipToAddress", sortable: true },
                { title: "郵遞區號", field: "Zip", sortable: true },
                { title: "訂單箱數", field: "CaseQty", sortable: true },
                { title: "訂單板數", field: "PalletQty", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "訂單材積", field: "Cube", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "訂單重量", field: "Weight", sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                { title: "備註", field: "Notes", sortable: true }
            ],
            editButtons: {                
                events: {
                    'click .cutorders': function (e, value, row, index) { 
                        $normalcutordersTable.bootstrapTable('uncheckAll');     
                        $normalcutordersTable.bootstrapTable('check', index);  
                        CutOrder(row);
                    }
                }
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    url: 'api/NormalCutOrders/RetrieveDetailByTMSKey?TMSKey=' + row.TMSKey.trim() ,
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
                        sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
                        pageSize: 10,
                        pageList: [10, 50, 100], //分頁筆數
                        toolbar: "",
                        search: false,
                        editButtons: { 
                            id: "",
                            events: {}
                        },
                        columns: [
                            { title: "品號", field: "Sku", sortable: false },
                            { title: "品名", field: "Descr", sortable: false },
                            { title: "箱入數", field: "CaseCnt", sortable: false },
                            { title: "板入數", field: "Pallet", sortable: false },
                            { title: "訂單箱數", field: "CaseQty", sortable: false },
                            { title: "板數", field: "PalletQty", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)}},
                            { title: "材積", field: "Cube", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)}},
                            { title: "重量", field: "Weight", sortable: false, formatter: function(value) { return $.roundDecimal(value, 5)}}
                        ]
                    }
                })
            }
        }
    });  


    //訂單切割按鈕
    $("#btn_cutorders").click(function () {            
        var selections = $normalcutordersTable.bootstrapTable('getSelections');
        if (selections.length == 0) {
            swal({ title: "請選擇要切割的訂單", type: "warning", confirmButtonText: '確定' });
            return;
        }
        if (selections.length > 1) {
            swal({ title: "請選擇一個要切割的訂單", type: "warning", confirmButtonText: '確定' });
            return;
        }        
        CutOrder(selections[0]);
    });

    function CutOrder(row) {
        $.bc({
            url: "api/NormalCutOrders/RetrieveDetailByTMSKey?TMSKey=" + row.TMSKey,
            callback: function (result) {                
                //彈出收貨視窗前重置內容
                normal.headerDataBinder.load(row);
                $(normal.detailTable).bootstrapTable('resetView');
                result = result.map(function (ret) { return $.extend(ret, { FromCaseQty: ret.CaseQty }) })
                normal.ShowDetail(result, false);
                $(CutOrderDetailTable).bootstrapTable("removeAll");
                $("#totalCase").text("");
                $("#totalCube").text("");
                $("#totalWeight").text("");
                $("#dialogNew").modal('show');
            }
        })       
    }

    //項次轉字串並補0   
    function FixedOrderLineNumber(val) {        
        var ln = val;
        ln = ln.toString();
        return (ln.length < 5) ? new Array(5 - ln.length + 1).join("0") + ln : ln;        
    }

    //分析明細
    function AnalyseData(currentIndex) {
        //取得要保存的明細內容
        var data = normal.detailDataBinder.get();
        //取得分割明細
        var CutOrderDatas = $(CutOrderDetailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:true});        
        //取得切割箱數
        var CutCase = parseFloat($("#CutCase").val());
        //取得訂單箱數
        var CaseQty = parseFloat(data.CaseQty.trim());
        //取得訂單材積
        var CutCube = parseFloat($("#CutCube").val());
        //取得訂單材積
        var Cube = parseFloat(data.Cube.trim());
        //取得訂單重量
        var CutWeight = parseFloat($("#CutWeight").val());
        //取得訂單重量
        var Weight = parseFloat(data.Weight.trim());
        //保存後已編輯        
        data.CaseQty = $.roundDecimal(CaseQty - CutCase, 3);
        data.Cube = $.roundDecimal(Cube - CutCube, 3);
        data.Weight = $.roundDecimal(Weight - CutWeight, 3);
        data.Modified = true;
        //保存更新
        $(normal.detailTable).bootstrapTable('updateRow', {
            index: currentIndex,
            row: data
        });        
        //更新分割明細
        data.FromOrderLineNumber = data.OrderLineNumber;        
        data.CaseQty = CutCase;
        data.Cube = $.roundDecimal(CutCube, 3);
        data.Weight = $.roundDecimal(CutWeight, 3);
        var sameCutOrderDataIndex = CutOrderDatas.map(function (ret) { return ret.FromOrderLineNumber }).indexOf(data.OrderLineNumber);
        if (sameCutOrderDataIndex > -1) {
            //更新
            var ret = CutOrderDatas.filter(function (ret) { return ret.FromOrderLineNumber == data.OrderLineNumber })[0];
            data.CaseQty += ret.CaseQty;
            data.Cube += ret.Cube;
            data.Cube = $.roundDecimal(data.Cube, 3);
            data.Weight += ret.Weight;
            data.Weight = $.roundDecimal(data.Weight, 3);
            $(CutOrderDetailTable).bootstrapTable('updateRow', {
                index: sameCutOrderDataIndex,
                row: data
            });
        }
        else {
            //新增
            $(CutOrderDetailTable).bootstrapTable('append', [data]);
            //滾動到最底
            $(CutOrderDetailTable).bootstrapTable('scrollTo', 'bottom');
        }
    }

    function CalcCutOrderDetail() {
        var details = $(CutOrderDetailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
        var caseqty = 0;
        details.map(function (d) { return d.CaseQty }).forEach(function (d) { caseqty += parseFloat(d) });
        var cube = 0;
        details.map(function (d) { return d.Cube }).forEach(function (d) { cube += parseFloat(d) });
        var weight = 0;
        details.map(function (d) { return d.Weight }).forEach(function (d) { weight += parseFloat(d) });
        $("#totalCase").text($.roundDecimal(caseqty, 3));
        $("#totalCube").text($.roundDecimal(cube, 3));
        $("#totalWeight").text($.roundDecimal(weight, 3));
    }

    $(CutOrderDetailTable).lgbTable({
        dataBinder: {
            click: { '#btn_edit': function () {}, '#btnSubmit': function () {} },
            bootstrapTable: CutOrderDetailTable
        },
        smartTable: {
            checkboxHeader: false,
            showExport: false, 
            showColumns: true,
            showRefresh: false,
            showToggle: true,
            singleSelect: false,
            checkbox: false,
            queryButton: '#',
            paginationHAlign: "left", //分頁顯示位置
            pagination: true, //顯示分頁
            sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
            pageSize: 10,
            pageList: [10, 50, 100], //分頁筆數
            clickToSelect: true,
            editButtons: { 
                id: "#cutorderdetailtableButtons",
                events: {
                    'click .edit': function (e, value, row, index) {                                                                    
                        normal.EditRow(row, index, CutOrderDetailTable);
                    },
                    'click .del': function (e, value, row, index) {         
                        normal.DeleteRow(row, CutOrderDetailTable);
                    }
                }
            },
            toolbar: "",
            queryButton: "",
            columns: [
                { title: "項次", field: "OrderLineNumber", formatter: function (value) { return parseInt(value); }, sortable: false },
                { title: "品號", field: "Sku", sortable: false },
                { title: "品名", field: "Descr", sortable: false },
                { title: "訂單箱數", field: "CaseQty", sortable: false },
                { title: "訂單材積", field: "Cube", sortable: false },
                { title: "訂單重量", field: "Weight", sortable: false }
            ]
        }
    });

    $("#CutCase").change(function () {
        $("#CutPallet").val("");
        $("#CutCube").val("");
        $("#CutWeight").val("");
        if ($(this).val() == "") return;
        RetrieveSkuByTMSKey($(this).val());
    });

    function RetrieveSkuByTMSKey(CutCase, callback) {
        $("#CutPallet").val("");
        $("#CutCube").val("");
        $("#CutWeight").val("");
        var header = $normalcutordersTable.bootstrapTable('getSelections');
        var detail = normal.detailDataBinder.get();
        var tmskey = header[0].TMSKey
        var sku = detail.Sku
        var orderlinenumber = detail.OrderLineNumber
        $.bc({
            url: "api/NormalCutOrders/RetrieveSkuByTMSKey?qty=" + CutCase + "&tmskey=" + tmskey + "&sku=" + sku + "&orderlinenumber=" + orderlinenumber,
            callback: function (result) {
                if (!result) {
                    lgbSwal({
                        title: '此筆明細有異常，請確認',
                        type: "error"
                    });
                    return;
                }
                $("#CutPallet").val(result.CutPallet);
                $("#CutCube").val(result.CutCube);
                $("#CutWeight").val(result.CutWeight);
                if (callback) callback();
            }
        })
    }


    //master detail 插件
    var normal = NormalExtend({
        headerDataMap: {
            StorerKey: "#StorerKey",
            TMSKey: "#TMSKey",
            ExternOrderKey: "#ExternOrderKey",      
            ConsigneeKey: "#ConsigneeKey",
            ShortName: "#ShortName",
            ShipToAddress: "#ShipToAddress",
            Zip: "#Zip",           
            Notes: "#Notes"
        },
        detailDataMap: {
            OrderLineNumber: "#OrderLineNumber",
            Sku: "#Sku",
            Descr: "#Descr",
            CaseCnt: "#CaseCnt",
            Pallet: "#Pallet",
            CaseQty: "#CaseQty",
            PalletQty: "#PalletQty",
            Cube: "#OrderCube",
            Weight: "#OrderWeight",
            CutCase: "#CutCase",
            CutPallet: "#CutPallet",
            CutCube: "#CutCube",
            CutWeight: "#CutWeight"
        },
        detailsmartTable: {
            pageSize: 5,     
            columns: [
                { 
                    title: "已編輯", field: "Modified", align: 'center',
                    formatter: function (value, row) {
                        var Modified = row.Modified ? "是" : "否";
                        var color = row.Modified ? "#28a745" : "#dc3545";
                        return '<div style="color: ' + color + '">' + Modified + '</div>';
                    }, 
                    sortable: false            
                },
                { title: "項次", field: "OrderLineNumber", formatter: function (value) { return parseInt(value); }, sortable: false },
                { title: "品號", field: "Sku", sortable: false },
                { title: "品名", field: "Descr", sortable: false },
                { title: "訂單箱數", field: "CaseQty", sortable: false },
                { title: "訂單材積", field: "Cube", sortable: false },
                { title: "訂單重量", field: "Weight", sortable: false }
            ]
        },        
        //index為畫面編號
        validateForm: function (index, callback) {
            
        },
        prevForm: function (prevIndex) {

        },
        //按下提交前新增資料入暫存Detail Table
        beforeAddDetail: function () {
            return false;
        },
        //提交前修改資料
        beforeEditDetail: function (row, callback) {                       
            callback(true, "", function () {
                $("#CutCase").val($("#CaseQty").val());
                $("#CutCase").trigger("change");
            })
        },
        beforeUpdateDetail: function (currentIndex, callback) {
            if ($("#CutCase").val() == "") {
                lgbSwal({ title: "請輸入切割箱數", type: "error" });
                callback(false);
                return;
            };
            let isLastDetail = function () {
                var zeroCount = 0;
                var details = $(normal.detailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});                
                details.forEach(function (detail) {
                    if (detail.CaseQty == 0) zeroCount ++;
                });                
                return zeroCount == details.length - 1;
            }
            
            var detail = normal.detailDataBinder.get();
            var CaseQty = parseFloat(detail.CaseQty.trim());
            var CutCase = parseFloat($("#CutCase").val());
            if (CutCase > CaseQty) {
                $("#CutCase").addClass('is-invalid');
                lgbSwal({ title: "請輸入小於訂單箱數的數值", type: "error" });
                return;
            }
            if (CutCase != CaseQty && parseInt(CutCase) != parseFloat(CutCase)) {
                $("#CutCase").addClass('is-invalid');
                lgbSwal({ title: "只切部分數量時，僅能以箱為單位，不可切零散", type: "error" });
                return;
            }
            if (isLastDetail() && (CutCase - CaseQty == 0)) {
                $("#CutCase").addClass('is-invalid');
                lgbSwal({ title: "無法切割整張訂單", type: "error" });
                return;
            }
            RetrieveSkuByTMSKey($("#CutCase").val(), function () {
                AnalyseData(currentIndex);
                CalcCutOrderDetail();
                callback(false);
            });
        },
        //編輯
        beforeDetailFormatter: function (value, row, index) {            
            normal.removeDelBtn = true;
            return true;
        },
        //刪除明細
        deleteDetail: function (row, callback) {
            var swalDeleteOptions = {
                title: "刪除分割明細項次 " + parseInt(row.OrderLineNumber),
                html: "您確定要刪除此筆分割明細嗎",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: "我要刪除",
                cancelButtonText: "取消"
            };
            swal(swalDeleteOptions).then(function (result) {
                if (!result.value) return;
                var details = $(normal.detailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
                details = details.filter(function (ret) { return ret.OrderLineNumber == row.FromOrderLineNumber });
                if (details.length == 0) {
                    lgbSwal({ title: "找不到項次，請再試一次", type: "error" });
                    return;
                }
                if (details.length > 1) {
                    lgbSwal({ title: "找到重複項次，請再試一次", type: "error" });
                    return;
                }
                var ret = details[0];
                var CaseQty = ret.CaseQty + row.CaseQty;
                var Cube = $.roundDecimal(ret.Cube + row.Cube, 3);
                var Weight = $.roundDecimal(ret.Weight + row.Weight, 3); 
                if (CaseQty > ret.FromCaseQty) {
                    lgbSwal({ title: "修改數量大於原始訂單箱數", type: "error" });
                    return;
                }
                ret.CaseQty = CaseQty;
                ret.Cube = Cube;
                ret.Weight = Weight;
                ret.Modified = !(ret.CaseQty == ret.FromCaseQty);
                
                $(normal.detailTable).bootstrapTable('updateRow', {
                    field: "OrderLineNumber",
                    row: ret
                });
                callback(true, "", "OrderLineNumber");
                CalcCutOrderDetail();
            })            
        },
        //提交
        submitForm: function () {       
            var selections = $normalcutordersTable.bootstrapTable('getSelections');     
            var details = $(normal.detailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
            var newdetails = $(CutOrderDetailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
            if (newdetails.length == 0) {
                lgbSwal({ title: "沒有明細，無法提交", type: "error" });
                return;
            }
            var row = selections[0];
            var tmskey = row.TMSKey;
            $.bc({
                url: "api/NormalCutOrders/CutOrders",
                method: "post",
                data: {       
                    TMSKey: tmskey,
                    Details: details,
                    NewDetails: newdetails
                },
                crud: '切割訂單',
                btn: '#submitBtn',
                callback: function (result) {
                    if(result) {
                        $("#dialogNew").modal('hide');
                        $normalcutordersTable.bootstrapTable('refresh');
                    }
                    var title = (result) ? "成功" : "失敗";
                    var type = (result) ? "success" : "error";
                    lgbSwal({ title: "訂單切割" + title, type: type });
                }
            })
        },
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
});