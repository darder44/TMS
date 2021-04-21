$(function () {
    $.extend({
        NormalExtend: function (options) {
            var settings = $.extend({
                modalbodycard: "#modalbodycard",
                card: ".card",
                skipnormalCard: true,
                headerCard: "#headerCard",
                normalCard: ".normalCard",
                detailCard: "#detailCard",
                prevBtn: ".normal-prevbtn",
                nextBtn: ".normal-nextbtn",
                submitBtn: "#submitBtn",
                nextBtnNextText: "下一步",
                nextBtnSubmitText: "提交",
                addDetailBtn: "#btn_AddDetail",
                updateDetailBtn: "#btn_UpdateDetail",
                cancelUpdateBtn: "#btn_CancelUpdate",
                detailTable: "#NormalDetail-table",
                headerDataMap: {},
                detailDataMap: {},
                normalDataMaps: [],
                detailsmartTable: {},    
                validateForm: function () {},
                prevForm: function () {},
                submitForm: function () {},
                beforeAddDetail: function () {},
                beforeEditDetail: function () {},
                beforeUpdateDetail: function () {},
                beforeDetailFormatter: function (value, row, index) {},
                deleteDetail: function (data, callback) {}
            }, options);
            this.options = settings;
            this.skipnormalCard = settings.skipnormalCard;
            this.headerDataBinder = new DataEntity(settings.headerDataMap);
            this.detailDataBinder = new DataEntity(settings.detailDataMap);
            this.normalDataBinders = settings.normalDataMaps.map(function (val) { return new DataEntity(val) });
            this.detailTable = settings.detailTable;
            this.currentIndex = -1;
            this.currentTab = 0; // Current tab is set to be the first tab (0)
            this.removeDelBtn = false;
            this.editTable = detailTable;
            this.showAddDetailBtn = true;
            this.editing = false;
            this.showTab = function (n) {
                currentTab = n;
                // This function will display the specified tab of the form...
                var x = $(settings.modalbodycard).find(settings.card);
                $(x[n]).css("display", "block");
                $(detailTable).bootstrapTable('resetView');
            }

            this.editMode = function () {
                $(settings.prevBtn).css("display", "none");
            };

            this.prev = function () {
                var x = $(settings.modalbodycard).find(settings.card);
                // Hide the current tab:
                $(x[currentTab]).css("display", "none");
                currentTab = skipnormalCard ? 0 : currentTab - 1;
                settings.prevForm(currentTab);
                showTab(currentTab);
            }
    
            this.next = function () {
                settings.validateForm(currentTab, function (result) {
                    // Exit the function if any field in the current tab is invalid:
                    if (!result) return false;
                    // This function will figure out which tab to display
                    var x = $(settings.modalbodycard).find(settings.card);
                    var oldTab = currentTab;
                    // Increase or decrease the current tab by 1:
                    currentTab = skipnormalCard ? x.length -1 : currentTab + 1;
                    // if you have reached the end of the form...
                    if (currentTab >= x.length) return false;
                    // Hide the current tab:
                    $(x[oldTab]).css("display", "none");
                    // Otherwise, display the correct tab:
                    showTab(currentTab);
                })
            }

            this.ShowDetail = function (rows, showAddDetail, goToHeader) {
                showAddDetailBtn = showAddDetail;
                $(settings.modalbodycard + ">" + settings.card).css("display", "none"); 
                ResetDetail();
                currentTab = 0; 
                showTab(1);
                editMode();
                $(settings.addDetailBtn).parent().css("display", showAddDetailBtn ? "" : "none");
                $(detailTable).bootstrapTable('load', rows);
                if (goToHeader) {
                    prev();
                    $(settings.prevBtn).css("display", "");
                }
            };

            this.ResetHeader = function () {
                headerDataBinder.reset();
                $(settings.headerCard).lgbValidator().reset();
            };

            this.ResetStep = function () {
                if ($(settings.setpCard).length > 0) $(settings.setpCard).lgbValidator().reset();
            };

            this.ResetDetail = function () {
                detailDataBinder.reset();
                $(settings.detailCard).lgbValidator().reset();
                if ($(settings.normalCard).length > 0) $(settings.normalCard).lgbValidator().reset();
                $(settings.addDetailBtn).parent().css("display", showAddDetailBtn ? "" : "none");
                $(settings.updateDetailBtn).parent().css("display", "none");
                $(settings.submitBtn).attr("disabled", false);
                editing = false;
            };

            this.Reset = function () {
                ResetHeader();
                ResetStep();
                ResetDetail();
                $(detailTable).bootstrapTable('removeAll');                
                $(settings.modalbodycard + ">" + settings.card).css("display", "none");  
                $(settings.addDetailBtn).parent().css("display", showAddDetailBtn ? "" : "none");
                $(settings.updateDetailBtn).parent().css("display", "none");
                $(settings.prevBtn).css("display", "");
                currentTab = 0;
                showTab(currentTab);
            }

            this.EditRow = function (row, index, table) {
                editTable = table;
                detailDataBinder.reset();
                $(settings.detailCard).lgbValidator().reset();                                                           
                settings.beforeEditDetail(row, function (result, msg, callback) {
                    if (!result) {
                        $(settings.addDetailBtn).parent().css("display", showAddDetailBtn ? "" : "none");
                        $(settings.updateDetailBtn).parent().css("display", "none");
                        lgbSwal({ title: msg, type: "error" });
                        return;
                    }
                    $(settings.addDetailBtn).parent().css("display", "none");
                    $(settings.updateDetailBtn).parent().css("display", ""); 
                    $(settings.submitBtn).attr("disabled", true);
                    editing = true;
                    detailDataBinder.load(row);
                    currentIndex = index;
                    callback();
                });
            }

            this.DeleteRow = function (row, table) {       
                if (editing) {
                    lgbSwal({ title: "尚有明細在編輯中", type: "warning" });
                    return;
                }
                settings.deleteDetail(row, function (result, msg, field) {   
                    if (result) {
                        var data = { field: field, values: [row[field]] };
                        $(table).bootstrapTable('remove', data)
                    } else {
                        lgbSwal({ title: msg, type: "error" });
                    }                                         
                });
            }

            $(settings.prevBtn).click(function () {
                prev();
            });
    
            $(settings.nextBtn).click(function () {
                next();        
            });

            $(settings.submitBtn).click(function () {
                // ... the form gets submitted:
                settings.submitForm();
            });

            $(settings.addDetailBtn).click(function () {
                var result = settings.beforeAddDetail();
                if (!result) {
                    ResetDetail();
                    return;
                } 
                var data = detailDataBinder.get();
                $(detailTable).bootstrapTable('append', data);
                ResetDetail();
            });

            $(settings.updateDetailBtn).click(function () {
                settings.beforeUpdateDetail(currentIndex,function (result) {
                    if (result) {
                        $(editTable).bootstrapTable('updateRow', {
                            index: currentIndex,
                            row: detailDataBinder.get()
                        });
                    }
                    ResetDetail();
                })
            });    

            $(settings.cancelUpdateBtn).click(function () {
                ResetDetail();
            })
            
            $(settings.detailTable).lgbTable({
                dataBinder: {
                    click: { '#btn_edit': function () {}, '#btnSubmit': function () {}, '#btn_reset': function () {} },
                    bootstrapTable: settings.detailTable
                },
                smartTable: $.extend({
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
                    showAdvancedSearchButton: false,
                    editButtons: { 
                        id: "#detailtableButtons",
                        events: {
                            'click .edit': function (e, value, row, index) {                                   
                                EditRow(row, index, detailTable);
                            },
                            'click .del': function (e, value, row, index) {         
                                DeleteRow(row, detailTable);
                            }
                        },
                        formatter: function (value, row, index) {
                            if (!settings.beforeDetailFormatter(value, row, index)) return;
                            var $this = this.clone();
                            if (removeDelBtn) $($this).find(".del").remove();
                            return $this.html();
                        }
                    },
                    toolbar: "",
                    queryButton: "",
                    columns: []
                }, settings.detailsmartTable),
            });            

            $('#dialogNew').on('shown.bs.modal', function () {
                $(detailTable).bootstrapTable('resetView');
            })
            $(settings.addDetailBtn).parent().css("display", showAddDetailBtn ? "" : "none");
            $(settings.updateDetailBtn).parent().css("display", "none"); 
            showTab(currentTab); // Display the current tab
            return this;
        }
    })

    window.NormalExtend = $.NormalExtend;
});