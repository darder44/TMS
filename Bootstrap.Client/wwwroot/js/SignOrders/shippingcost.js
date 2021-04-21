$(function () {      
    var costHeader = new DataEntity({
        RouteNo: "#RouteNo_signdetail",
        TMSKey: "#TMSKey_signdetail",
        StorerKey: "#StorerKey_signdetail",
        ExternOrderKey: "#ExternOrderKey_signdetail",
        CostStatus: "#CostStatus",
        VehicleKey: "#VehicleKey",
        Driver: "#Driver",
        Receiver: "#Receiver"
    });
    
    var shippingcostDataBinder = new DataEntity({
        CostKind: "#CostKind",
        Uom: "#Uom",
        Receivable: "#Receivable",
        Payable: "#Payable",
        AreaStart: "#AreaStart",
        AreaEnd: "#AreaEnd",
        ChargeQty: "#ChargeQty",  
        STDReceivable: "#STDReceivable",
        STDPayable: "#STDPayable",              
        SumReceivable: "#SumReceivable",
        SumPayable: "#SumPayable",        
        Premium: "#Premium",
    });

    var costcodeDataBinder = new DataEntity({
        ChargeQty: "#ChargeQty",
        Receivable: "#Receivable",
        Payable: "#Payable",
        SumReceivable: "#SumReceivable",
        SumPayable: "#SumPayable",
        STDReceivable: "#STDReceivable",
        STDPayable: "#STDPayable",
        Premium: "#Premium"
    });

    var shippingcostDetailDataBinder = new DataEntity({                
        CostCode: "#CostCode",        
        Reason: "#Reason",                        
        Notes: "#Notes"
    });
    
    $.validator.addMethod("CheckSumReceivable", function (value, element, target) {
        return $("#SumReceivable").val() == $("#ChargeQty").val() * $("#Receivable").val();
    }, "應收總價不等於計費數量乘上應收單價");

    $.validator.addMethod("CheckSumPayable", function (value, element, target) {
        return $("#SumPayable").val() == $("#ChargeQty").val() * $("#Payable").val();
    }, "應付總價不等於計費數量乘上應付單價");

    //初始化路編資料table
    var $signordertable = $('#signorder-table');
    $signordertable.lgbTable({        
        dataBinder: {
            map: {
            },
            bootstrapTable: '#signorder-table',
            click: {
                '#btn_query': function () {}
            }   
        },
        smartTable: {
            showRefresh: false,
            showExport: false,
            showColumns: true,
            showToggle: true,
            sortName: 'RouteNo',    
            search: false,
            sidePagination:'client',
            checkbox: false,
            cookie: false,
            toolbar: '',                 
            columns: [
                { title: "路線編號", field: "RouteNo" },
                { title: "貨主", field: "StorerKey" },                            
                { title: "訂單號碼", field: "TMSKey" },
                { title: "貨主單號", field: "ExternOrderKey" },
                { title: "訂單類別", field: "OrderType" },
                { title: "訂單日期", field: "OrderDate", formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "出車日期", field: "DoRouteDate", formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },
                { title: "到貨日期", field: "DeliveryDate", formatter: function (value) { return $.momentFormat(value, 'YYYY/MM/DD'); } },                
                { title: "貨運公司", field: "CompanyName" },
                { title: "司機", field: "Driver" },
                { title: "車號", field: "VehicleKey" },
                { title: "起始位置", field: "StartAddress" },
                { title: "目的位置", field: "EndAddress" },
                { title: "郵遞區號", field: "Zip" },
                { title: "簽單狀態", field: "ConfirmNotes" }
            ],
            editButtons: {
                id: "",
                events: {}
            }
        }
    });

    //初始化彙總資料table
    var $signorderdatatable = $('#signorderdata-table');
    $signorderdatatable.lgbTable({        
        dataBinder: {
            map: {
            },
            bootstrapTable: '#signorderdata-table',
            click: {
                '#btn_query': function () {}
            }   
        },
        smartTable: {
            showRefresh: false,
            singleSelect: false,
            showExport: false,
            showColumns: true,
            showToggle: true,
            sortName: '',    
            search: false,
            sidePagination:'client',
            checkbox: false,
            cookie: false,
            toolbar: '',                 
            columns: [
                { title: "彙總方式", field: "SummaryType" },
                { title: "箱數", field: "ShipCaseQty", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "材積", field: "Cube", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "重量", field: "Weight", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "件數", field: "OTQty", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "個數", field: "ShipQty", formatter: function(value) { return $.roundDecimal(value, 5)} }
            ],
            editButtons: {
                id: "",
                events: {}
            }
        }
    });

    var editing = false;

    //初始化計費維護table
    var $editcosttable = $("#editcost-table");
    $editcosttable.lgbTable({        
        dataBinder: {
            map: {
            },
            bootstrapTable: '#editcost-table',
            click: {
                '#btn_edit': function () {},
                '#btn_query': function () {}
            },
            events: {
                '#btn_edit_cost': function (row) {
                    var sels = $('#editcost-table input[name="btSelectItem"]:checked');
                    if (sels.length > 1) {
                        lgbSwal({ title: '只能選擇一項要編輯的數據', type: "warning" });
                        return;
                    }
                    EditRow(row, $(sels[0]).data('index'));                    
                },
                '#btn_delete_cost': function (row) {
                    var sels = $('#editcost-table input[name="btSelectItem"]:checked');
                    if (sels.length > 1) {
                        lgbSwal({ title: '只能選擇一項要刪除的數據', type: "warning" });
                        return;
                    }
                    DeleteRow($(sels[0]).data('index'));
                }
            }
        },
        smartTable: {            
            showExport: false,
            showColumns: true,
            showRefresh: false,
            showToggle: true,
            sortName: 'RouteNo',                        
            search: false,
            paginationHAlign: "left", //分頁顯示位置
            pagination: true, //顯示分頁
            sidePagination: 'client', //分頁來源 client 就是bootstrapTable(load, rows), server則是 url
            pageSize: 10,
            pageList: [10, 50, 100], //分頁筆數
            showAdvancedSearchButton: false,     
            singleSelect: true,      
            cookie: false,           
            toolbar: '#toolbar-cost',
            columns: [
                { title: "路線編號", field: "RouteNo" },
                { title: "單位", field: "Uom" },                       
                { title: "計費數量", field: "ChargeQty", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "應收單價", field: "Receivable", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "應付單價", field: "Payable", formatter: function(value) { return $.roundDecimal(value, 5)} },            
                { title: "議價", field: "Premium" },
                { title: "原因", field: "Reason" },
                { title: "總應收", field: "SumReceivable", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "總應付", field: "SumPayable", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "起點", field: "AreaStart" },
                { title: "迄點", field: "AreaEnd" },
                { title: "備註", field: "Notes" },
                { title: "訂單單號", field: "TMSKey" },
                { title: "計費類別", field: "CostKind" },
                { title: "計費代碼", field: "CostCode" },
                { title: "標準應收", field: "STDReceivable", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "標準應付", field: "STDPayable", formatter: function(value) { return $.roundDecimal(value, 5)} },
                { title: "車號", field: "VehicleKey" },
                { title: "司機", field: "Driver" },
                { title: "請款人", field: "Receiver" },
            ],
            editButtons: {
                id: "#tableButtons_cost",
                events: {
                    'click .edit': function (e, value, row, index) {   
                        $editcosttable.bootstrapTable('uncheckAll');     
                        $editcosttable.bootstrapTable('check', index);   
                        EditRow(row, index);
                    },
                    'click .del': function (e, value, row, index) {      
                        DeleteRow(index);
                    }
                }
            }
        }
    });

    $("#btn_refresh_cost").click(() => {
        var swalDeleteOptions = { //彈出提醒畫面
            title: "重置資料",
            html: "所有資料將會重新讀取，您確定要重置資料嗎？",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "確定",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {                                
                var row = $("#nav-shippingcost-tab").data("row");       
                var shippingcost = new ShippingCostEntity();
                shippingcost.load(row, 1);
            }
        });
    });

    function EditRow(row, index) {
        if (editing) {
            swal({ title: "尚有明細在編輯中", type: "warning" });
            return;
        } 
        $("#ShippingCostDetailCard").lgbValidator().reset();
        row.CostCode = row.CostCode.trim();
        shippingcostDataBinder.load(row);
        shippingcostDetailDataBinder.load(row);
        $("#btn_UpdateDetail_cost").data("index", index);
        $("#btn_UpdateDetail_cost").css("display","block");
        $("#btn_CancelUpdate_cost").css("display","block");
        $("#btn_signdetailadd").css("display","none");
        $("#btnShippingCostSubmit").attr("disabled", true);
        $("#CostCode").data("selected", row.CostCode);
        $("#CostKind").data("selected", row.CostKind);
        $("#VehicleKey").data("selected", row.VehicleKey);
        editing = true;
    }

    function DeleteRow(index) {
        if (editing) {
            swal({ title: "尚有明細在編輯中", type: "warning" });
            return;
        }
        var swalDeleteOptions = { //彈出提醒畫面
            title: "刪除數據",
            html: "您確定要刪除選中的數據嗎",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#dc3545', //確認按鈕顏色
            cancelButtonColor: '#6c757d', //取消按鈕顏色
            confirmButtonText: "我要刪除",
            cancelButtonText: "取消"
        };
        swal($.extend({}, swalDeleteOptions)).then(function (result) {
            if (result.value) {                                
                $editcosttable.bootstrapTable('remove', {
                    field: "$index",
                    values: [index]
                })
            }
        });
    }

    $("#btn_UpdateDetail_cost").click(function () { 
        CheckCostCode(function () {
            var data = $.extend({}, costHeader.get(), shippingcostDataBinder.get(), shippingcostDetailDataBinder.get());
            var index = $("#btn_UpdateDetail_cost").data("index");
            $editcosttable.bootstrapTable('updateRow', {
                index: index,
                row: data
            });
            ResetSignDetail();
            GetCostCodes($("#StorerKey_signdetail").val());
            GetCostKinds($("#StorerKey_signdetail").val());
        });           
    });

    $("#btn_CancelUpdate_cost").click(function () {
        ResetSignDetail();
    });    

    //計費代碼帶出資料
    $("#CostCode").editableSelect({ effects: 'fade' }).on('select.editable-select', function (e, li) {
        shippingcostDataBinder.reset();
        $("#CostCode").data("selected", "");
        var value = li.text();
        if(value == "") return;
        RetrieveCostCode(value);
    });

    //計費類別帶出計費代碼
    $("#CostKind").editableSelect({ effects: 'fade' }).on('select.editable-select', function (e, li) {
        costcodeDataBinder.reset();
        $("#CostCode").val("");
        $("#CostCode").data("selected", "");        
        var value = li.text();
        if(value == "") return;        
        GetCostCodesByCostKind($("#StorerKey_signdetail").val(), value);
    });

    //車號帶出司機、請款人
    $("#VehicleKey").editableSelect({ effects: 'fade' }).on('select.editable-select', function (e, li) {
        $("#Driver").val("");
        $("#Receiver").val("");
        $("#VehicleKey").data("selected", "");
        var value = li.text();
        if(value == "") return        
        GetDriver(value);
        GetReceiver(value);
    });

    //計費數量換算應收付總價
    $("#ChargeQty").change(function () {
        if ($(this).val() == "") return;
        if($("#CostCode").val() == "") return;
        $("#SumReceivable").val("");
        $("#SumPayable").val("");
        var SumReceivable = $("#ChargeQty").val() * $("#Receivable").val();
        var SumPayable = $("#ChargeQty").val() * $("#Payable").val();
        $("#SumReceivable").val(SumReceivable);
        $("#SumPayable").val(SumPayable);
        $("#ShippingCostDetailCard").lgbValidator().valid();
    });
    //應收單價換算應收總價
    $("#Receivable").change(function () {
        if ($(this).val() == "") return;
        if($("#CostCode").val() == "") return;
        if($("#ChargeQty").val() == "") return;
        $("#SumReceivable").val("");
        var SumReceivable = $("#ChargeQty").val() * $("#Receivable").val();
        $("#SumReceivable").val(SumReceivable);
        $("#ShippingCostDetailCard").lgbValidator().valid();
    });
    //應付單價換算應付總價
    $("#Payable").change(function () {
        if ($(this).val() == "") return;
        if($("#CostCode").val() == "") return;
        if($("#ChargeQty").val() == "") return;
        $("#SumPayable").val("");
        var SumPayable = $("#ChargeQty").val() * $("#Payable").val();
        $("#SumPayable").val(SumPayable);
        $("#ShippingCostDetailCard").lgbValidator().valid();
    });

    //計費代碼新增明細
    $("#btn_signdetailadd").click(function () {
        CheckCostCode(function () {
            var data = $.extend({}, costHeader.get(), shippingcostDataBinder.get(), shippingcostDetailDataBinder.get());
            $($editcosttable).bootstrapTable('append', data);
            ResetSignDetail();
            GetCostCodes($("#StorerKey_signdetail").val());
            GetCostKinds($("#StorerKey_signdetail").val());
        });        
    })

    function ResetSignDetail() {
        editing = false;
        shippingcostDataBinder.reset();
        shippingcostDetailDataBinder.reset();        
        $("#btnShippingCostSubmit").attr("disabled", false);
        //控制新增與保存按鈕顯示
        $("#btn_UpdateDetail_cost").css("display","none");
        $("#btn_CancelUpdate_cost").css("display","none");
        $("#btn_signdetailadd").css("display","block");
    };

    function GetCostKinds(StorerKey) {
        $("#CostKind").editableSelect('clear');
        $.bc({
            url: "api/BaseCostCode/RetrieveByStorerkeyAndCostKind?Storerkey=" + StorerKey, 
            callback: function (result) {
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此貨主無計費資料，請確認", type: "error" });
                    return;
                }
                var rows = result;
                rows.forEach(function (row) {
                    $("#CostKind").editableSelect('add', row.CostKind.trim());
                });
            }
        })
    }

    function RetrieveCostCode(costcode) {
        if (!costcode) return;        
        $.bc({
            url: "api/NormalSignOrders/RetrieveCostCode?StorerKey=" + $("#StorerKey_signdetail").val() + "&CostCode=" + costcode,
            method: "Get",
            callback: function (result) {
                if(!result)
                {
                    lgbSwal({ title: "此貨主無此計費代碼，請確認", type: "error" });
                    return;
                }
                else
                {
                    $("#CostCode").data("selected", costcode);
                    $("#CostKind").data("selected", result.CostKind);
                    $("#VehicleKey").data("selected", result.VehicleKey);
                    shippingcostDataBinder.load(result);
                    $("#ShippingCostDetailCard").lgbValidator().valid();
                }
            }
        });
    }

    function GetCostCodesByCostKind(StorerKey, CostKind) {
        if (!CostKind) return;        
        $("#CostCode").editableSelect('clear');
        $.bc({
            url: "api/BaseCostCode/RetrieveCostCodeByStorerkeyAndCostKind?Storerkey=" + StorerKey + "&CostKind=" + CostKind, 
            callback: function (result) {  
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此計費類別無計費代碼資料，請確認", type: "error" });
                    return;
                }
                $("#CostKind").data("selected", CostKind);             
                var rows = result;                
                rows.forEach(function (row) {
                    $("#CostCode").editableSelect('add', row.CostCode.trim());                    
                });
                if ($("#CostKind").val() == "" && $("#CostCode").val() == "") return;
                $("#ShippingCostDetailCard").lgbValidator().valid();
            }
        })
    }

    function GetDriver(VehicleKey) {
        if (!VehicleKey) return;        
        $("#Driver").editableSelect('clear');
        $.bc({
            url: "api/NormalSignOrders/RetrieveDriver?VehicleKey=" + VehicleKey, 
            callback: function (result) {                
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此車號無司機資料，請確認", type: "error" });
                    return;
                }
                $("#VehicleKey").data("selected", VehicleKey);             
                var rows = result;
                rows.forEach(function (row) {
                    $("#Driver").editableSelect('add', row.Driver.trim());
                });
                if ($("#CostKind").val() == "" && $("#CostCode").val() == "") return;
                $("#ShippingCostDetailCard").lgbValidator().valid();
            }
        })
    }

    function GetReceiver(VehicleKey) {
        if (!VehicleKey) return;        
        $("#Receiver").editableSelect('clear');
        $.bc({
            url: "api/NormalSignOrders/RetrieveReceiver?VehicleKey=" + VehicleKey, 
            callback: function (result) {               
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此車號無請款人資料，請確認", type: "error" });
                    return;
                }
                $("#VehicleKey").data("selected", VehicleKey);
                var rows = result;
                rows.forEach(function (row) {
                    $("#Receiver").editableSelect('add', row.Receiver.trim());
                });
                if ($("#CostKind").val() == "" && $("#CostCode").val() == "") return;
                $("#ShippingCostDetailCard").lgbValidator().valid();
            }
        })
    }

    $("#RefreshCostCode").click(function () {
        GetCostCodes($("#StorerKey_signdetail").val());
    }); 

    function GetCostCodes(StorerKey) {
        $("#CostCode").editableSelect('clear');
        $.bc({
            url: "api/BaseCostCode/RetrieveByStorerkeyAndCostCode?Storerkey=" + StorerKey, 
            callback: function (result) { 
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此貨主無計費代碼，請確認", type: "error" });
                    return;
                }              
                var rows = result;
                rows.forEach(function (row) {
                    $("#CostCode").editableSelect('add', row.CostCode.trim());
                });
            }
        })
    }

    function GetVehicleKey(RouteNo){
        $.bc({
            url:"api/NormalSignOrders/RetriveVehicleKeyByRouteNo?RouteNo=" + RouteNo,
            method: 'get',
            dataType: "",
            callback: function(result){
                if (!result) {
                    lgbSwal({ title: "此路編無車輛請款資料，請確認", type: "error" });
                    return;
                }
                $("#VehicleKey").val(result.VehicleKey);
                GetDriver($("#VehicleKey").val());
                GetReceiver($("#VehicleKey").val());
                $("#Driver").val(result.Driver);
                $("#Receiver").val(result.Receiver);
            }
        })
    }

    $("#RefreshVehicleKey").click(function () {
        $("#Driver").val("");
        $("#Receiver").val("");
        GetDriver($("#VehicleKey").val());
        GetReceiver($("#VehicleKey").val());
    });

    function GetVehicles(TMSKey) {
        $("#VehicleKey").editableSelect('clear');
        $.bc({
            url: "api/NormalSignOrders/RetriveVehicleKeyByTMSKey?TMSKey=" + TMSKey, 
            method: 'get',
            callback: function (result) {
                if (!result || !Array.isArray(result) || result.length == 0) {
                    lgbSwal({ title: "此單號無車輛請款資料，請確認", type: "error" });
                    return;
                }
                var rows = result;
                rows.forEach(function (row) {
                    $("#VehicleKey").editableSelect('add', row.VehicleKey.trim());
                });
                
            }
        })
    }

    function CheckCostCode(callback) {                        
        //Check CostCode
        const CostCode = $("#CostCode").data("selected");
        if (CostCode && CostCode != "" && $("#CostCode").val().trim() !== $.trim(CostCode)) {
            RetrieveCostCode($("#CostCode").val());
            return;
        }

        //Check CostKind
        const CostKind = $("#CostKind").data("selected");
        if (CostKind && CostKind != "" && $("#CostKind").val().trim() !== $.trim(CostKind)) {
            costcodeDataBinder.reset();
            $("#CostCode").val("");
            $("#CostCode").data("selected", "");            
            GetCostCodesByCostKind($("#StorerKey_signdetail").val(), $("#CostKind").val());
            return;
        }

        //Check VehicleKey
        const VehicleKey = $("#VehicleKey").data("selected");
        if (VehicleKey && VehicleKey != "" && $("#VehicleKey").val() !== $.trim(VehicleKey)) {
            GetDriver($("#VehicleKey").val());
            GetReceiver($("#VehicleKey").val());
            return;
        }

        if($("#CostCode").val().length == 0)
        {
            $("#CostCode").addClass('is-invalid');
            swal({ title: "請輸入計費代碼", type: "error" });
            return;
        }
        $.bc({
            //檢查計費代碼是否存在
            url:"api/NormalSignOrders/CheckCostCode?CostCode=" + $("#CostCode").val() + "&StorerKey=" + $("#StorerKey_signdetail").val(),
            method: 'get',
            callback: function(result){
                if(!result)
                {
                    $("#CostCode").addClass('is-invalid');
                    swal({ title: "此貨主無此計費代碼，請確認", type: "error" });
                    return;
                }
                else if($("#VehicleKey").val().trim().length == 0)
                {
                    $("#VehicleKey").addClass('is-invalid');
                    lgbSwal({ title: "請輸入車號", type: "error" });
                    return;
                }
                else 
                {
                    //檢查車號、司機、請款人
                    $.bc({
                        url:"api/NormalSignOrders/CheckVehicleKey?VehicleKey=" + $("#VehicleKey").val() + "&Driver=" + $("#Driver").val() + "&Receiver=" + $("#Receiver").val(),
                        method: 'get',
                        datatype: "",
                        callback: function(result){
                            if(result != "1")
                            {
                                var errreason = "";

                                if(result == "2")
                                { 
                                    errreason = "此車號不存在";
                                    $("#VehicleKey").addClass('is-invalid');
                                }
                                else if($("#Driver").val().trim().length == 0)
                                {
                                    errreason = "有輸入車號，但沒輸入司機";
                                    $("#Driver").addClass('is-invalid');
                                }
                                // else if($("#Receiver").val().trim().length == 0)
                                // {
                                //     errreason = "有輸入車號，但沒輸入請款人";
                                //     $("#Receiver").addClass('is-invalid');
                                // }
                                else if(result == "3") 
                                {
                                    errreason = "此司機不屬於此車號";
                                    $("#Driver").addClass('is-invalid');
                                }
                                else if(result == "4" && $("#Receiver").val().trim().length != 0) 
                                {
                                    errreason = "此請款人不屬於此車號";
                                    $("#Receiver").addClass('is-invalid');
                                }
                                else if(result == "5") 
                                {
                                    errreason = "此司機與請款人皆不屬於此車號";
                                    $("#Driver").addClass('is-invalid');
                                    $("#Receiver").addClass('is-invalid');
                                }
                                var swalError = {
                                    title: "新增失敗\n 失敗原因:\n " + errreason ,
                                    type: "warning",
                                    confirmButtonText: '#6c757d',
                                    confirmButtonText: "確定"
                                };
                                swal($.extend({}, swalError));
                                return;
                            }           
                            $("#ShippingCostDetailCard").lgbValidator().reset();                                             
                            if (callback) callback();
                        }
                    })
                }
                
            }
        })
    }

    //計費資料保存
    $("#btnShippingCostSubmit").click(() => {
        var signorderdata = $("#editcost-table").bootstrapTable('getData');
        var row = $("#nav-shippingcost-tab").data("row");
        if (row.SdnBack == "0") {
            swal({ title: "簽單尚未回傳，請先維護簽單！", type: "error" });
            return;
        }
        var tmskey = row.TMSKey
        $.bc({
            url: "api/NormalSignOrders/SaveCostData",
            method: "post",
            data: {
                CostData: signorderdata,
                TMSKey: tmskey
            },
            crud: '保存計費',
            btn: this,
            callback: function (result) {
                if(result) {                    
                    $("#dialogSignOrders").modal("hide");
                    $('#normalsignorders-table').bootstrapTable('refresh');
                }
                var title = (result) ? "成功" : "失敗";
                var type = (result) ? "success" : "error";
                if (result) {
                    lgbSwal({ title: "計費資料保存" + title, type: type, timer: 500 });
                    $("#dialogAdvancedSearch").modal("show");
                    return;
                }
                swal({ title: "計費資料保存" + title, type: type });
            }
        })
        
    })

    ShippingCostEntity = function (options) {
        this.options = options
    }

    ShippingCostEntity.prototype = {
        load: function (row, index = 0) {
            if ($("#nav-shippingcost-tab").length == 0) return;
            $.bc({
                url: "api/NormalSignOrders/RetrieveOrders?TMSKey=" + row.TMSKey,
                method: 'GET',
                callback: function (result) {
                    if (!result || !result.rows || result.rows.length != 1) {
                        swal({ title: "無法取得簽單明細 " + row.TMSKey, type: type });
                        return;
                    }
                    row = result.rows[0];
                    $("#nav-shippingcost-tab").data("row", row);                
                    ResetSignDetail();
                    $editcosttable.bootstrapTable('removeAll');
                    $editcosttable.bootstrapTable('showLoading');
                    $signordertable.bootstrapTable('removeAll');
                    $signordertable.bootstrapTable('showLoading');
                    $signorderdatatable.bootstrapTable('removeAll');
                    $signorderdatatable.bootstrapTable('showLoading');
                    $.bc({
                        url: 'api/NormalSignOrders/RetrievesShippingCost',
                        method: 'post',
                        data: row,
                        callback: function (result) {
                            $editcosttable.bootstrapTable('hideLoading');
                            $signordertable.bootstrapTable('hideLoading');
                            $signorderdatatable.bootstrapTable('hideLoading');
                            
                            if (!result) {
                                swal({ title: "初始化運費維護資料失敗，請聯絡相關人員\n單號：" + row.TMSKey, type: type });
                                $("#dialogSignOrders").modal("hide");
                                return;
                            }                            
                            $editcosttable.bootstrapTable('load', result.ShippingCost);                            
                            $signordertable.bootstrapTable('load', result.RouteData);                            
                            $signorderdatatable.bootstrapTable('load', result.SignOrderData);

                            $("#TMSKey_signdetail").val(row.TMSKey)
                            $("#ExternOrderKey_signdetail").val(row.ExternOrderKey)
                            $("#RouteNo_signdetail").val(row.RouteNo)
                            $("#StorerKey_signdetail").val(row.StorerKey)
                            $("#CostStatus").val(row.CostStatus)
                            GetVehicles(row.TMSKey);
                            GetVehicleKey(row.RouteNo);
                            GetCostCodes(row.StorerKey);
                            GetCostKinds(row.StorerKey);                            
                        }
                    });                    
                }
            })              
        }
    }
})