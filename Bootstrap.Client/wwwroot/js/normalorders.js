$(function () {
    InitialSelect();
    
    var ASNSearchDataBinder = new DataEntity({
        StorerKey: "#txt_ASN_StorerKey",
        ASNKey: "#txt_ASN_ASNKey",
        ExternASNKey: "#txt_ASN_ExternASNKey",
        Sku: "#txt_ASN_Sku",
        Status: "#txt_ASN_Status",
        StorerKey_ASN: "#StorerKey_ASN",
        ConsigneeKey_ASN: "#ConsigneeKey_ASN",
        ShortName_ASN: "#ShortName_ASN"
    });

    var OrderTypes = [];
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

        OrderTypes = result;

        var data = result.map(function (row) {
            return {
                value: $.trim(row.Code),
                label: row.Code + "　" + row.Description,
            }
        });
        
        $("#sel_ordertypes").multiselect('dataprovider', data);
    });

    var queryParams = function (params) {
        return $.extend(params, {
            StorerKeys: function () {
                var values = [];
                $("#sel_storerkey option:selected").each(function () {
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
            Status: $("#txt_Status").val(),
            ExternOrderKey: $("#txt_ExternOrderKey").val(),
            OrderDate_Start: $("#txt_OrderDate_Start").val(),
            OrderDate_End: $("#txt_OrderDate_End").val(),
            DeliveryDate_Start: $("#txt_DeliveryDate_Start").val(),
            DeliveryDate_End: $("#txt_DeliveryDate_End").val(),
            AddDate_Start: $("#txt_AddDate_Start").val(),
            AddDate_End: $("#txt_AddDate_End").val(),
        });
    };
    //初始化table
    var $normalorders = $('#normalorders-table');
    $normalorders.lgbTable({
        //url: 'api/NormalOrders/Retrieves',
        dataBinder: {
            bootstrapTable: '#normalorders-table',
            click: {
                '#btn_query': function () {                                        
                    var options = this.options;
                    options.bootstrapTable.bootstrapTable('refresh', { url: $.formatUrl('api/NormalOrders/Retrieves'), pageNumber: 1 });
                    $.bootstrapTableQueryBtn(options);
                },                
                '#btn_edit': function () {
                    
                }
            },
            events: {
                '#btn_edit': function (row) {
                    EditOrder(row);                            
                }
            },
            //重新初始化畫面
            callback: function (data) {
                if (data && data.oper === 'create') {
                    normal.showAddDetailBtn = true;
                    normal.Reset();        
                    $.bc({
                        url: "api/NCOUNTER?keyname=TMSKey",
                        callback: function (result) {
                            if (!result) {
                                lgbSwal({ title: '無法產生訂單號碼', type: "error" });
                                return;
                            }
                            var kc = result.keycount.toString();
                            kc = (kc.length < 10) ? new Array(10 - kc.length + 1).join("0") + kc : kc;
                            $("#TMSKey").val(kc);                     
                            $("#OrderType").lgbSelect('reset', OrderTypes.filter(function (row) { return row.Code.trim() != "I" }).map(function (row) { return { value: row.Code, text: $.format("{0}　{1}", row.Code, row.Description) } }));
                            var now = new Date();
                            $("#OrderDate").datetimepicker("setDate", now);
                            $("#DeliveryDate").datetimepicker("setDate", now);
                        }
                    });                    
                }
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
            search: true,
            showAdvancedSearchButton: true,
            checkbox: true,
            detailView: true,
            queryParams: queryParams,
            columns: [
                {title: "貨主",field: "StorerKey",sortable: true},
                {title: "所屬倉別",field: "Facility",sortable: true},
                {title: "訂單狀態",field: "Status",sortable: true, formatter: function (value) { return $("#txt_Status").getTextByValue(value) } },
                {title: "訂單號碼",field: "TMSKey",sortable: true},
                {title: "貨主單號",field: "ExternOrderKey",sortable: true},
                {title: "採購單號",field: "CustomerOrderKey",sortable: true},
                {title: "訂單類型",field: "OrderType",sortable: true, formatter: function (value) { return $("#sel_ordertypes").getMultiSelectTextByValue(value) } },
                {title: "訂單日期",field: "OrderDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD')}},
                {title: "到貨日期",field: "DeliveryDate",sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD')}},
                {title: "轉入日期",field: "AddDate",sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD')}},
                {title: "客戶編號",field: "ConsigneeKey",sortable: true},
                {title: "客戶簡稱",field: "ShortName",sortable: true},
                {title: "提貨客編",field: "PickUpConsigneeKey",sortable: true},
                {title: "提貨名稱",field: "PickUpName",sortable: true},
                {title: "提貨地址",field: "PickUpAddress",sortable: true},
                {title: "到貨地址",field: "ShipToAddress",sortable: true},
                {title: "郵遞區號",field: "Zip",sortable: true},
                {title: "聯絡人",field: "Contact",sortable: true},
                {title: "電話",field: "Phone",sortable: true},
                {title: "備註",field: "Notes",sortable: true}
            ],
            editButtons: {
                events: {
                    'click .edit': function (e, value, row) {
                        EditOrder(row);
                    },
                    'click .del': function (e, value, row) {
                        DeleteOrders([row]);
                    }
                }
            },
            onExpandRow: function (index, row, $detail) {
                var $el = $detail.html('<table></table>').find('table');
                $el.lgbTable({
                    url: 'api/NormalOrders/RetrieveDetailByTMSKey?TMSKey=' + row.TMSKey.trim(),
                    dataBinder: {
                        click: { '#btn_edit': function () {}, '#btnSubmit': function () {} },
                        bootstrapTable: ''
                    },
                    smartTable: {
                        checkboxHeader: false,
                        showExport: false,
                        showColumns: false,
                        showRefresh: false,
                        showToggle: false,
                        checkbox: false,
                        paginationHAlign: "left",
                        pagination: true, //顯示分頁
                        sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
                        pageSize: 10,
                        pageList: [10, 20, 50], //分頁筆數
                        toolbar: "",
                        search: false,
                        editButtons: { 
                            id: "",
                            events: {}
                        },
                        columns: [
                            {title: "項次",field: "OrderLineNumber",sortable: true},
                            {title: "品號",field: "Sku",sortable: true},
                            {title: "品名",field: "Descr",sortable: true},
                            {title: "訂單個數",field: "OrderQty",sortable: true},
                            {title: "訂單材積",field: "Cube",sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}},
                            {title: "訂單重量",field: "Weight",sortable: true, formatter: function(value) { return $.roundDecimal(value, 5)}}
                        ]
                    }
                })
            }

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
    };  

    //轉出EXCEL
    $("#btn_export").click(function () {
        var options = $normalorders.bootstrapTable('getOptions');
        var url = options.url;        
        if (!url || url === "") {
            swal({ title: "請先搜尋訂單維護", type: "warning", confirmButtonText: '確定' });
            return;
        }
        ExcelExport({
            url: "api/NormalOrders/ExportExcel",
            queryParams: queryParams,
            FileName: '訂單維護',
            Options: options,
            Sheets: [{
                SheetName: "NormalOrders",
                sortName: 'AddDate',
                sortOrder: 'desc',
                Columns: [
                    {title: "貨主",field: "StorerKey"},
                    {title: "所屬倉別",field: "Facility"},
                    {title: "訂單號碼",field: "TMSKey"},
                    {title: "貨主單號",field: "ExternOrderKey"},
                    {title: "採購單號",field: "CustomerOrderKey"},
                    {title: "訂單類型",field: "OrderType"},
                    {title: "訂單日期",field: "OrderDate", dataType: 3, dataFormat: 'YYYY/MM/DD'},
                    {title: "到貨日期",field: "DeliveryDate", dataType: 3, dataFormat: 'YYYY/MM/DD'},
                    {title: "轉入日期",field: "AddDate", dataType: 3, dataFormat: 'YYYY/MM/DD'},
                    {title: "客戶編號",field: "ConsigneeKey"},
                    {title: "客戶簡稱",field: "ShortName"},
                    {title: "提貨客編",field: "PickUpConsigneeKey"},
                    {title: "提貨名稱",field: "PickUpName"},
                    {title: "提貨地址",field: "PickUpAddress"},
                    {title: "到貨地址",field: "ShipToAddress"},
                    {title: "郵遞區號",field: "Zip"},
                    {title: "聯絡人",field: "Contact"},
                    {title: "電話",field: "Phone"},
                    {title: "備註",field: "Notes"},
                    {title: "項次",field: "OrderLineNumber"},
                    {title: "品號",field: "Sku"},
                    {title: "品名",field: "Descr"},
                    {title: "訂單個數",field: "OrderQty", dataType: 1, dataFormat: '0'},
                    {title: "訂單材積",field: "Cube", dataType: 2, dataFormat: '0.#####'},
                    {title: "訂單重量",field: "Weight", dataType: 2, dataFormat: '0.#####'},
                ]
            }]
        })       
    });

    $("#btn_deleteorders").click(function () {
        var selectedorders = $normalorders.bootstrapTable('getSelections');
        if (selectedorders.length === 0) {
            lgbSwal({ title: '請選擇要刪除的訂單', type: "warning" });
            return;
        }
        DeleteOrders(selectedorders);
    });

    function EditOrder(row) {
        $("#OrderDate").parent().find('div').css("display", parseInt($.trim(row.Status)) >= 2 ? "none" : "");
        $("#OrderDate").parent().children().prop("disabled", parseInt($.trim(row.Status)) >= 2);
        $("#DeliveryDate").parent().find('div').css("display", parseInt($.trim(row.Status)) >= 2 ? "none" : "");
        $("#DeliveryDate").parent().children().prop("disabled", parseInt($.trim(row.Status)) >= 2)
        $.bc({
            url: 'api/NormalOrders/RetrieveDetailByTMSKey?TMSKey=' + row.TMSKey.trim(),
            callback: function (result) {
                if (!result) {
                    lgbSwal({ title: '無法取得訂單明細', type: "error" });
                    return;
                }
                normal.Reset();
                $("#OrderType").lgbSelect('reset', OrderTypes.map(function (row) { return { value: row.Code, text: $.format("{0}　{1}", row.Code, row.Description) } }))
                normal.headerDataBinder.load(row);
                $("#OrderDate").datetimepicker("setDate", new Date(row.OrderDate));
                $("#DeliveryDate").datetimepicker("setDate", new Date(row.DeliveryDate));
                normal.ShowDetail(result, false, true);
                $("#dialogNew").modal('show');            
            }
        });
    }

    function DeleteOrders(rows) {        
        if (!rows.every(function (row) { return row.Status == "0" || row.Status == "1" || row.Status == "8" })) {
            lgbSwal({ title: '只能刪除未排車前的訂單', type: "warning" });
            return;
        }
        var swalDeleteOptions = {
            title: "刪除訂單",
            html: "您確定要刪除已選擇的訂單嗎",
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
                        $normalorders.bootstrapTable("refresh");
                        return;
                    }
                    if (!result) {
                        swal({ title: "刪除訂單失敗，請再試一次", type: "warning", confirmButtonText: '確定' });
                        return;
                    }
                    swal({ title: result.message, type: "warning", confirmButtonText: '確定' });
                }
            });       
        })
    }

    //輸入完商品後搜尋商品主檔
    $("#Sku").change(function () {
        $("#Descr").val("");
        $("#CaseCnt").val("");
        $("#Pallet").val("");
        $("#StdCube").val("");
        $("#StdWeight").val("");
        if ($(this).val() == "") return;
        var header = normal.headerDataBinder.get();
        var storerkey = header.StorerKey;
        $.bc({
            url: "api/NormalOrders/RetrieveSkuByStorerKey?sku=" + $(this).val() + "&storerkey=" + storerkey,
            callback: function (result) {
                var cls = (result) ? "is-valid" : "is-invalid";
                $("#Sku").removeClass("is-valid");
                $("#Sku").removeClass("is-invalid");
                $("#Sku").addClass(cls);
                if(!result) {
                    $("#Sku").tooltip('show');
                    $("#detailCard").lgbValidator().valid();
                    return;
                }
                $("#Sku").tooltip('dispose');
                $("#Descr").val(result.Descr);
                $("#CaseCnt").val(result.CaseCnt);
                $("#Pallet").val(result.Pallet);
                $("#StdCube").val(result.StdCube);  
                $("#StdWeight").val(result.StdWeight);
                $("#detailCard").lgbValidator().valid();                                                                                                                                     
            }
        })
    });

    $("#OrderType").on("changed.lgbSelect", function () {
        var ordertype = $(this).val();
        switch (ordertype) {
            case "A2B":
                $("#PickUpConsigneeKey").attr("data-valid", "true");
                $("#PickUpConsigneeKey").attr("placeholder", "不可為空，100字以内");
                break;
            default:
                $("#PickUpConsigneeKey").val("");
                $("#PickUpName").val("");
                $("#PickUpAddress").val("");
                $("#PickUpConsigneeKey").removeClass("is-valid").removeClass("is-invalid")
                $("#PickUpConsigneeKey").attr("data-valid", "false");
                $("#PickUpConsigneeKey").attr("placeholder", "100字以内");
                break;
        }
    })

    $("#RefreshSku").click(function () {
        $("#Sku").trigger("change");
    }); 

    $("#RefreshPickUpConsigneeKey").click(function () {
        $("#PickUpConsigneeKey").trigger("change");
    }); 

    $("#RefreshConsigneeKey").click(function () {
        $("#ConsigneeKey").trigger("change");
    }); 

    //輸入完客戶編號後搜尋客戶主檔
    $("#ConsigneeKey").change(function () {
        if ($("#StorerKey").val() == "") {
            lgbSwal({
                title: '請先選擇貨主',
                type: "error"
            });
            return;
        }
        $("#ShortName").val("");
        $("#ShipToAddress").val("");
        $("#Zip").val("");
        $("#Contact").val("");
        $("#Phone").val("");
        $.bc({
            url: "api/NormalOrders/RetrieveConsigneeKeyByStorerKeyAndConsigneeKey?ConsigneeKey=" + $(this).val() + "&storerkey=" + $("#StorerKey").val(),
            callback: function (result) {
                var cls = (result) ? "is-valid" : "is-invalid";
                $("#ConsigneeKey").removeClass("is-valid").removeClass("is-invalid").addClass(cls);
                if (!result) {
                    $("#ConsigneeKey").tooltip('show');
                    return;
                }
                $("#ConsigneeKey").tooltip('dispose');
                $("#ShortName").val(result.ShortName);
                $("#ShipToAddress").val(result.ShipToAddress);
                $("#Zip").val(result.Zip);
                $("#Contact").val(result.Contact);
                $("#Phone").val(result.Phone);
            }
        })
    });

    //輸入完提貨客編後搜尋客戶主檔
    $("#PickUpConsigneeKey").change(function () {
        if ($("#StorerKey").val() == "") {
            lgbSwal({
                title: '請先選擇貨主',
                type: "error"
            });
            return;
        }
        $("#PickUpName").val("");
        $("#PickUpAddress").val("");
        $.bc({
            url: "api/NormalOrders/RetrieveConsigneeKeyByStorerKeyAndConsigneeKey?ConsigneeKey=" + $(this).val() + "&StorerKey=" + $("#StorerKey").val(),
            callback: function (result) {
                var cls = (result) ? "is-valid" : "is-invalid";
                $("#PickUpConsigneeKey").removeClass("is-valid").removeClass("is-invalid").addClass(cls);
                if (!result) {
                    $("#PickUpConsigneeKey").tooltip('show');
                    return;
                }
                $("#PickUpConsigneeKey").tooltip('dispose');
                //與客戶編號搜尋客戶主檔共用，所以抓的欄位看起來怪
                $("#PickUpName").val(result.ShortName);
                $("#PickUpAddress").val(result.ShipToAddress);
            }
        })
    });
    
    //輸入完貨主單號後搜尋是否存在過此單號
    $("#ExternOrderKey").change(function () {
        $.bc({
            url: "api/NormalOrders/RetrieveExternOrderKey?ExternOrderKey=" + $(this).val() + "&StorerKey=" + $("#StorerKey").val(),
            callback: function (result) {
                if (result) {
                    lgbSwal({
                        title: '此貨主單號已經存在，請確認',
                        type: "error"
                    });
                    return;
                }
            }
        })
    });

    $("#btn_selectfile").change(function () {
        var files = this.files;
        if (files.length == 0) {
            $("#selectfilename").text("選擇檔案");
            return;
        }
        $("#selectfilename").text($(this).val().split(/(\\|\/)/g).pop());
    });

    //master detail 插件
    var normal = NormalExtend({
        headerDataMap: {
            StorerKey: "#StorerKey",
            Facility: "#Facility",
            TMSKey: "#TMSKey",
            ExternOrderKey: "#ExternOrderKey",
            CustomerOrderKey: "#CustomerOrderKey",
            OrderType: "#OrderType",
            OrderDate: "#OrderDate",
            DeliveryDate: "#DeliveryDate",
            AddDate: "#AddDate",
            ConsigneeKey: "#ConsigneeKey",
            ShortName: "#ShortName",
            PickUpConsigneeKey: "#PickUpConsigneeKey",
            PickUpName: "#PickUpName",
            PickUpAddress: "#PickUpAddress",
            ShipToAddress: "#ShipToAddress",
            Zip: "#Zip",
            Contact: "#Contact",
            Phone: "#Phone",
            Notes: "#Notes"
        },
        detailDataMap: {
            Sku: "#Sku",
            Descr: "#Descr",
            CaseCnt: "#CaseCnt",
            Pallet: "#Pallet",
            StdCube: "#StdCube",
            StdWeight: "#StdWeight",
            OrderQty: "#OrderQty"
        },
        detailsmartTable: {
            columns: 
            [
                {title: "項次",field: "OrderLineNumber",formatter: function (value) {return parseInt(value);},sortable: false},
                {title: "品號",field: "Sku",sortable: false},
                {title: "品名",field: "Descr",sortable: false},
                {title: "訂單量",field: "OrderQty",sortable: false},
            ]
        },
        validateForm: function (index, callback) {
            if(index ===0)
            {
                if($("#OrderType").val() == "A2B" && $("#PickUpConsigneeKey").val().trim() == "")
                {
                    lgbSwal({ title: "訂單類別為'A2B'，請輸入提貨客編", type: "error" });
                    return;
                }
                var tmskey = normal.headerDataBinder.get().TMSKey;
                $.bc({
                    url: "api/NormalOrders/RetrieveDetailByTMSKey?TMSKey=" + tmskey,
                    callback: function (result) {  
                        $(normal.detailTable).bootstrapTable('resetView');                    
                        $(normal.detailTable).bootstrapTable('load', result);
                    }
                })
            }
            
            callback(true);
        },
        prevForm: function (prevIndex) {
            
        },
        beforeAddDetail: function () {
            //取得Detail資料
            var datas = [];
            //取得最後一筆明細
            var data = $(normal.detailTable).bootstrapTable('getData', {useCurrentPage:false, includeHiddenRows:false});
            if (data && data.length > 0) data = data[data.length -1];   
            //宣告最大項次
            var maxnumber = data.OrderLineNumber ? data.OrderLineNumber : 0;
            maxnumber = parseInt(maxnumber);
            data = normal.detailDataBinder.get();
            var ln = (maxnumber + 1).toString();
            ln = padLeft(ln, 5);
            data.OrderLineNumber = ln;
            datas.push(data);
            $(normal.detailTable).bootstrapTable('append', datas);
            $(normal.detailTable).bootstrapTable('scrollTo', 'bottom');
            normal.detailDataBinder.reset();
            return false;
        },
        //提交前修改資料
        beforeEditDetail: function (row, callback) {                       
            callback(true, "", function () {
                $("#Sku").trigger('change')
            })
        },
        beforeUpdateDetail: function (currentIndex, callback) {            
            callback(true);
        },
        beforeDetailFormatter: function (value, row, index) {            
            return true;
        },
        //刪除明細
        deleteDetail: function (row, callback) {
            var swalDeleteOptions = {
                title: "刪除此筆細項 " + row.OrderLineNumber.toString().trim(),
                html: "您確定要刪除此品項嗎",
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
                details = details.filter(function (ret) { return ret.OrderLineNumber == row.OrderLineNumber });
                if (details.length == 0) {
                    lgbSwal({ title: "找不到項次，請再試一次", type: "error" });
                    return;
                }
                
                if (details.length > 1) {
                    lgbSwal({ title: "找到重複項次，請再試一次", type: "error" });
                    return;
                }

                var ret = details[0];
                $(normal.detailTable).bootstrapTable('updateRow', {
                    index: currentIndex,
                    row: ret
                });
                callback(true, "", "OrderLineNumber");

            })            
        },
        submitForm: function () {
            var header = normal.headerDataBinder.get();
            var details = $(normal.detailTable).bootstrapTable('getData', {
                useCurrentPage: false,
                includeHiddenRows: false
            });
            if (details.length == 0) {
                swal({ title: "沒有明細，無法提交", type: "error" });
                return;
            }
            $.bc({
                url: "api/NormalOrders/SaveHeaderDetail",
                method: "post",
                data: {
                    Header: header,
                    Detail: details
                },
                crud: '保存訂單明細',
                btn: '#submitBtn',
                callback: function (result) {
                    if (result && result.success) {
                        $("#dialogNew").modal('hide');
                        $normalorders.bootstrapTable('refresh');
                    }
                    var title = (result && result.success) ? "成功" : "失敗";
                    var type = (result && result.success) ? "success" : "error";
                    swal({
                        title: "保存" + title,
                        html: result.message,
                        type: type
                    });
                }
            })
        },

    })

    //左邊補0
    function padLeft(str, len) {
        str = '' + str;
        if (str.length >= len) {
            return str;
        } else {
            return padLeft("0" + str, len);
        }
    }

    $.validator.addMethod("CheckSku", function (value, element, target) {
        return $("#Pallet").val() != "" && $("#CaseCnt").val() != "" && $("#StdCube").val() != "" && $("#StdWeight").val() != "";
    }, "找不到商品，請先維護商品主檔");

    $.validator.addMethod("CheckConsigneeKey", function (value, element, target) {
        if (target == "#PickUpConsigneeKey") {
            return $("#PickUpName").val() != "";
        } else {
            return $("#ShortName").val() != "";
        }
    }, "找不到客編，請先維護客戶主檔");

    //彈出視窗時調整ASN轉RC訂單table大小
    $('#dialogASNToRC').on('shown.bs.modal', function () {        
        $normalasntorc2ndTable.bootstrapTable('resetView');
    });

    // ASN轉RC訂單按鈕
    $("#btn_asntorc").click(function () {
        ASNSearchDataBinder.reset();
        $("#dialogASN").modal('show');
    });
    
    // ASN轉RC訂單按鈕 (下一步)
    $("#nextBtn_ASN").click(function () {
        $normalasntorc2ndTable.bootstrapTable('refresh', {
            url: $.formatUrl('api/Normal_ASN')
        });
        $("#dialogASNToRC").modal('show');
        $("#dialogASN").modal('hide');
    });
    

    // ASN轉RC訂單 2nd table
    var $normalasntorc2ndTable = $('#normal_asntorc_2nd-table');
    $normalasntorc2ndTable.lgbTable({
        url: "",
        dataBinder: {
            map: {
            },
            click: { '#btn_edit': function () {}, '#btnSubmit': function () {} },
            bootstrapTable: ''
        },
        smartTable: {
            singleSelect: false,
            showExport: false,
            showColumns: true,
            showRefresh: true,
            showToggle: true,
            sortName: 'StorerKey',
            checkbox: true,
            //detailView: false,
            toolbar: "",
            pagination: true, //顯示分頁
            sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
            pageSize: 10,
            pageList: [10, 20, 50], //分頁筆數
            queryButton: "#btn_ASN_query",
            queryParams: function (params) {
                return $.extend(params, ASNSearchDataBinder.get());
            },
            columns: [
                { title: "貨主代號", field: "StorerKey", sortable: true },
                { title: "入庫通知單號", field: "ASNKey", sortable: true },
                { title: "貨主通知單號", field: "ExternASNKey", sortable: true },
                { title: "承運商名稱", field: "SellersReference2", sortable: true },
                { title: "承運方式", field: "TransMethod", sortable: true },
                { title: "預計入庫日期", field: "ASNDate", sortable: true, formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "入庫類型", field: "ASNType", sortable: true },
                { title: "入庫箱數", field: "CaseQty", sortable: true }, 
                { title: "入庫數量", field: "OrderQty", sortable: true },
            ],
            editButtons: {
                id: "",
                events: {
                }
            }
            
        }
    });

    //輸入客戶編號後重新整理 (ASN轉RC訂單)
    $("#RefreshConsigneeKey_ASN").click(function () {
        $("#ConsigneeKey_ASN").trigger("change");
    }); 

    //輸入客戶編號後搜尋客戶主檔 (ASN轉RC訂單)
    $("#ConsigneeKey_ASN").change(function () {
        if ($("#StorerKey_ASN").val() == "") {
            lgbSwal({
                title: '請先選擇貨主',
                type: "error"
            });
            return;
        }
        $("#ShortName_ASN").val("");
        $.bc({
            url: "api/NormalOrders/RetrieveConsigneeKeyByStorerKeyAndConsigneeKey?ConsigneeKey=" + $(this).val() + "&storerkey=" + $("#StorerKey_ASN").val(),
            callback: function (result) {
                var cls = (result) ? "is-valid" : "is-invalid";
                $("#ConsigneeKey_ASN").removeClass("is-valid").removeClass("is-invalid").addClass(cls);
                if (!result) {
                    $("#ConsigneeKey_ASN").tooltip('show');
                    return;
                }
                $("#ConsigneeKey_ASN").tooltip('dispose');
                $("#ShortName_ASN").val(result.ShortName);
            }
        })
    });

    $.validator.addMethod("CheckConsigneeKey_ASN", function (value, element, target) {
        if (target == "#ConsigneeKey_ASN") {
            return $("#ShortName_ASN").val() != "";
        }
    }, "找不到客編，請先維護客戶主檔");

    //轉換按鈕 (ASN轉RC訂單)
    $("#btnASNtoRC").click(function () {
        var selectedasnkeys = $normalasntorc2ndTable.bootstrapTable('getSelections');
        if (selectedasnkeys.length === 0) {
            lgbSwal({ title: '請選擇要轉換為RC訂單的ASN單號', type: "warning" });
            return;
        }
        ASNtoRC(selectedasnkeys, this);        
    });

    function ASNtoRC(rows, that) {     
        var ASNKeys = rows.map(function (row) { return row.ASNKey }).join(",");
        var StorerKey = $("#StorerKey").val();
        var ConsigneeKey = $("#ConsigneeKey_ASN").val();
        var WareHouse = '';
        var Facility = '';
        $.bc({
            url: "api/NormalOrders/ASNtoRC?asnkeys=" + ASNKeys + "&storerkey=" + StorerKey + "&consigneekey=" + ConsigneeKey,
            crud: '轉換為RC訂單',
            btn: that,
            callback: function (result) {
                if (result && result.success) {
                    $normalasntorc2ndTable.bootstrapTable("removeAll");
                    $normalorders.bootstrapTable("refresh");
                    ASNSearchDataBinder.reset();
                    $("#dialogASNToRC").modal('hide');
                }
                var title = (result && result.success) ? "成功" : "失敗";
                var type = (result && result.success) ? "success" : "error";
                swal({
                    title: "轉換" + title,
                    html: result.message,
                    type: type
                });
            }
        })
    }

});