/**
 * 20201023 Modified by Andy.
 * 通用Functions
 * 修改bootstrapTable預設icon圖示
 *  - paginationSwitchDown : 'fa-caret-square-down' -> 'fa-caret-down'
 *  - paginationSwitchUp : 'fa-caret-square-up' -> 'fa-caret-up'
 * 修改bootstrapTable預設屬性
 *  - showColumnsToggleAll 切換所有Columns
 *  - showPaginationSwitch 顯示全部資料
 * 擴充JQuery functions
 *  - roundDecimal 小數點四捨五入
 *  - momentFormat 擴充Moment.js Format
 *  - syntaxData 比對JSON
 *  - arrayEquals 比對兩個陣列是否相等
 *  - bootstrapTableQueryBtn 搜尋按鈕
 *  - bootstrapTableResetBtn 重置按鈕
 *  - fetchDictionary 查詢字典表
 *  - checkbcResult 嚴格檢查 $.bc 回傳資料 並顯示異常訊息
 * 自定義datetimepicker
 *  - $(".input-group.input-append.datepicker > input") 日期選擇
 *  - $(".input-group.input-append.timepicker > input") 時間選擇
 * 自定義比較規則
 *  - $.validator.addMethod
 * lgbTable擴充
 *  - showAdvancedSearchButton 預設options && options.dataBinder && options.url !== undefined && options.url !== ""
 * logPlugin擴充
 *  - 追蹤POST資料
 * bc擴充
 *  - 嚴格檢查回傳值
 */
//                            _ooOoo_  
//                           o8888888o  
//                           88" . "88  
//                           (| -_- |)  
//                            O\ = /O  
//                        ____/`---'\____  
//                      .   ' \\| |// `.  
//                       / \\||| : |||// \  
//                     / _||||| -:- |||||- \  
//                       | | \\\ - /// | |  
//                     | \_| ''\---/'' | |  
//                      \ .-\__ `-` ___/-. /  
//                   ___`. .' /--.--\ `. . __  
//                ."" '< `.___\_<|>_/___.' >'"".  
//               | | : `- \`.;`\ _ /`;.`/ - ` : | |  
//                 \ \ `-. \_ __\ /__ _/ .-` / /  
//         ======`-.____`-.___\_____/___.-`____.-'======  
//                            `=---='  
//  
//         .............................................  
//                  佛祖保佑             永無BUG 
$(function () {
    $("input").addClass("input-sm");
    //修正icon
    if ($.fn.bootstrapTable) {
        $.extend($.fn.bootstrapTable.defaults.icons, {
            paginationSwitchDown: 'fa-caret-down',
            paginationSwitchUp: 'fa-caret-up',
        });

        $.extend($.fn.bootstrapTable.defaults, {
            showColumnsToggleAll: true,
            showPaginationSwitch: true,
        })
    }

    $.extend({
        roundDecimal: function (val, precision) {
            return Math.round(Math.round(val * Math.pow(10, (precision || 0) + 1)) / 10) / Math.pow(10, (precision || 0));
        },
        momentFormat: function (val, format) {
            if (!val) return null;
            return moment(val).format(format);
        },
        syntaxData: function (data, comparedata = undefined) {
            var ret = $("<div></div>");
            Object.keys(data).forEach(function(key) {
                //key                
                ret.append('<span class="">' + key + '：</span>');
                var val = data[key];
                //value
                var cls = comparedata && comparedata[key] === val ? "string" : comparedata ? 'key' : 'string';
                ret.append('<span class="' + cls + '">' + val + '</span>\r\n');
            });
            var html = ret.html();
            return html;
        },
        arrayEquals: function (source, dest) {
            return Array.isArray(source) &&
              Array.isArray(dest) &&
              source.length === dest.length &&
              source.every((val, index) => val === dest[index]);
        },
        bootstrapTableQueryBtn: function (opts) {
            var options = this.options || opts;
            options = options.bootstrapTable.bootstrapTable('getOptions');
            if (options.advancedSearchModal) {
                $(options.advancedSearchModal).modal('hide');
                if (options.cansetAdvanceSearchCookie) {
                    setAdvanceSearchCookie(options.advancedSearchModal);
                }
            }
        },
        bootstrapTableResetBtn: function () {
            if (this.options.bootstrapTable !== null) {
                var options = this.options.bootstrapTable.bootstrapTable('getOptions');
                if (options.advancedSearchModal) {
                    $(options.advancedSearchModal).find('[data-default-val]').each(function (index, element) {
                        var $ele = $(element);
                        var val = $ele.attr('data-default-val');
                        if ($ele.prop('nodeName') === 'INPUT') {
                            if ($ele.hasClass('form-select-input')) {
                                $ele.prev().lgbSelect('val', val);
                            }
                            else if ($ele.data('toggle') === 'lgbSelect') {
                                $ele.lgbSelect('val', val);
                            }
                            else {
                                $ele.val(val);
                            }
                        }
                    });

                    $(options.advancedSearchModal).find('select[multiple]').each(function (index, element) {
                        var $ele = $(element);
                        $ele.multiselect('clearSelection');
                    });
                    if (options.cansetAdvanceSearchCookie) {
                        setAdvanceSearchCookie(options.advancedSearchModal, true);
                    }
                }
            }
        },
        fetchDictionary: function (CodeName, callback) {
            $.bc({
                url: "api/BaseDictionary/RetrieveCodes?CodeName=" + CodeName,
                method: "GET",
                callback: function (result) {
                    if (callback) callback(result);                   
                }
            });
        },
        checkbcResult: function (result) {            
            if (typeof result === "object" && Object.keys(result).length === 2 && result.error && result.message) {
                swal({ title: result.error, html: result.message, type: "error" });
                return false;
            }
            return true;
        }
    });

    $.fn.extend({
        getMultiSelectTextByValue: function (value) {
            if (!value) return;
            let text;
            $(this).multiselect("getOptionByValue", value).find("option[value]").each(function (index, element) {
                if (element.value == value) {
                    text = element.text
                    return false;
                }
            });
            return text;
        }
    });

    var oldbc = $.bc;

    $.bc = function (options) {
        var btn = options.btn;
        $.logData = [];
        $.logData.log = undefined;
        var bcdata = {      
            requestData: JSON.stringify({
                url: options.url,
                data: options.data
            })
        };
        if (options.crud) {
            bcdata.crud = options.crud;
        } else if (options.title && options.title !== '') {
            bcdata.crud = options.title;
        }
        if (btn) {
            $(btn).attr("disabled", true);
        }
        if ($.isFunction(options.callback)) {
            var oldcallback = options.callback;
            var customcallback = function (result) {
                if (btn) {
                    $(btn).attr("disabled", false);
                }
                if (bcdata.crud) {
                    $.logPlugin().log(bcdata);
                }
                if (!$.checkbcResult(result)) return;
                oldcallback.call(oldcallback, result);
            }
            options.callback = customcallback;
        }
        oldbc.apply(this, arguments);
    }

    var oldlgbTable = $.fn.lgbTable;

    $.fn.lgbTable = function (options) {
        var id = $(this).attr('id');
        if (id && id !== "") {
            if (options.smartTable.cookie !== false) {
                var newsettings = {
                    cookie: true,
                    cookieIdTable: '#' + id,
                    cookieStorage: 'localStorage',
                };
                options.smartTable = $.extend(true, options.smartTable, newsettings);
            }
            options.smartTable = $.extend(true, options.smartTable, { bootstrapTable: '#' + id });
        }
        var showAdvancedSearchButton = options && options.dataBinder && options.url !== undefined && options.url !== "" && options.smartTable.toolbar !== "";
        showAdvancedSearchButton = showAdvancedSearchButton || options.smartTable.showAdvancedSearchButton;
        var search = options.smartTable.toolbar !== "";
        search = search || options.smartTable.search;
        options = $.extend(true, {
            dataBinder: {
                click: {
                    '#btn_reset': $.bootstrapTableResetBtn,
                    //'#btn_query': $.bootstrapTableQueryBtn
                }
            },
            smartTable: {
                cansetAdvanceSearchCookie: false,//showAdvancedSearchButton,
                showAdvancedSearchButton: false,
                search: search,
                //clickToSelect: true,
                //maintainMetaData: true,
                //sidePagination:'client'
            }
        }, options);

        var modal = options.smartTable.advancedSearchModal;
        if (modal == undefined) modal = '#dialogAdvancedSearch';

        options = $.extend(true, options, {
            smartTable: {
                onReorderColumn: function () {
                    resetAdvancedSearchButton(modal, options);
                }
            }
        });

        if (options.smartTable.cansetAdvanceSearchCookie) {
            restoreAdvanceSearchCookieValue(modal);
        }

        var ret = oldlgbTable.apply(this, arguments);

        if (options.smartTable.showAdvancedSearchButton === false && options.smartTable.search && options.smartTable.advancedSearchModal === 'undefined' && options.smartTable.toolbar === '' && id !== "") {
            var queryParams = options.smartTable.queryParams || function (params) { };
            options.smartTable.queryParams = function (params) {
                return $.extend({}, queryParams(params), { search: $('#' + id).parent().parent().prev().find('.search-input').val() });
            }
            $('#' + id).bootstrapTable('refreshOptions', { queryParams: options.smartTable.queryParams });
        }

        if (showAdvancedSearchButton) {   
            resetAdvancedSearchButton(modal, options);
        }        

        $("table").addClass("table-sm");
        return ret;
    }

    function resetAdvancedSearchButton(modal, options) {
        //找到columns
        var $toolbar = $(options.dataBinder.bootstrapTable).parent().parent().parent().find('.fixed-table-toolbar');
        var $columns = $('<div class="columns columns-right btn-group float-right"></div>');
        $toolbar.find('.search').remove();
        $toolbar.append($columns);
        // template            
        var $advancedSearchButtonHtml = $('<button class="btn btn-secondary" type="button" name="advancedSearch" title="搜尋"><i class="fa fa-search-plus"></i><span>搜尋</span></button>');
        $columns.append($advancedSearchButtonHtml);
        $advancedSearchButtonHtml.on('click', function () {
            // 彈出高級查詢對話框
            $(modal).modal('show');
        });

        var checkvalue = function () {
            var $modal = $(modal);
            var hasValue = false;
            $modal.find('[data-default-val]').each(function (index, element) {
                var $ele = $(element);
                var val = $ele.attr('data-default-val');
                if ($ele.prop('nodeName') === 'INPUT') {
                    // 20200819 Modified by Andy 改為不是空值則代表有搜尋
                    if ($ele.hasClass('form-select-input')) {
                        //hasValue = $ele.prev().val() !== val;
                        hasValue = $ele.prev().val() !== "";
                    }
                    else {                            
                        //hasValue = $ele.val() !== val;
                        hasValue = $ele.val() !== "";
                    }
                }      
                if (hasValue) return false;
            });
            
            if (!hasValue) {
                //多選
                $modal.find('select[multiple]').each(function (index, element) {
                    hasValue = $(element).find("option:selected").length > 0;
                    if (hasValue) return false;
                });
            };

            if (hasValue) $advancedSearchButtonHtml.removeClass('btn-secondary').addClass('btn-primary');
            else $advancedSearchButtonHtml.removeClass('btn-primary').addClass('btn-secondary');
        }
        $advancedSearchButtonHtml.on('click', function () {
            $(modal).modal('show');
        });

        // 進階搜尋有值時顏色為藍色
        $(modal).on('hide.bs.modal', checkvalue);

        checkvalue();
    }

    function setAdvanceSearchCookie(modal, reset = false) {
        if (!modal) return;
        var $modal = $(modal);
        var id = $modal.attr('id');
        if (!id || id == "") return;        
        var cookieName = __cookie__.concat(".", id);
        if (reset) {
            localStorage.removeItem(cookieName);
            return;
        }
        var cookieValue = {};

        $modal.find('[data-default-val]').each(function (index, element) {
            var $ele = $(element);
            var val = $ele.val();
            var _concat = "";
            if ($ele.prop('nodeName') === 'INPUT') {
                if ($ele.parent().hasClass('datepicker') || $ele.parent().hasClass('timepicker')) {                        
                    if (val && val !== "") {
                        val = moment($ele.datetimepicker('getDate')).format();
                    }
                    if ($ele.parent().hasClass('datepicker')) {
                        _concat = 'datepicker';
                    }
                    else if ($ele.parent().hasClass('timepicker')) {
                        _concat = 'timepicker';
                    }
                }
                else {
                    _concat = ($ele.data('toggle') === 'lgbSelect') ? 'lgbSelect' : 'input';
                }                
            }
            if (_concat == "") return;
            cookieValue[$ele.attr('id').concat(".", _concat)] = val;
        });

        $modal.find('select[multiple]').each(function (index, element) {
            var $ele = $(element);
            var values = [];
            $ele.find("option:selected").each(function () {
                var value = $(this).val();
                values.push(value);
            });
            cookieValue[$ele.attr('id').concat(".", "select[multiple]")] = values;
        });
        localStorage.setItem(cookieName, JSON.stringify(cookieValue));
    }

    function restoreAdvanceSearchCookieValue(modal) {
        if (!modal) return;
        var $modal = $(modal);
        var id = $modal.attr('id');
        if (!id || id == "") return;
        var cookieName = __cookie__.concat(".", id);
        var item = localStorage.getItem(cookieName);
        if (!item) return;
        item = JSON.parse(item);
        for (var key in item){
            var names = key.split(".");
            if (names.length != 2) continue;
            var id = "#" + names[0];
            var eltype = names[1];
            var val = item[key];
            if (!Array.isArray(val)) {
                switch (eltype) {
                    case 'lgbSelect':
                        $(id).lgbSelect("val", val);
                        break;
                    case 'datepicker': case 'timepicker':
                        if (val !== "") {
                            var date = moment(val);
                            if (date.isValid()) {
                                $(id).datetimepicker('setDate', date.toDate());
                            }
                        }
                        break;             
                    default:
                        $(id).val(val);
                        break;
                }
            } 
            else {
                if (val.length > 0) {
                    $(id).multiselect({
                        buttonClass: 'form-control',
                        selectAllText: '全選',                    
                        allSelectedText: '全選',
                        nSelectedText: '個選項',
                        maxHeight: 400,
                        buttonWidth: '150px'
                    });                    
                    $(id).multiselect('select', val);
                }                
            }
        }
    }

    $(".input-group.input-append.datepicker").each(function (index, element) {
        initialdatetimepicker(element);
    });

    $(".input-group.input-append.timepicker").each(function (index, element) {
        initialdatetimepicker(element, false);
    })

    function initialdatetimepicker(element, dateonly = true) {
        var $el = $(element);
        var $input = $el.find('input');

        if ($input.attr('disabled')) return;

        var options = {
            language: 'zh-TW',
            pickerPosition: 'bottom-left',
            format: dateonly ? 'yyyy/mm/dd' : 'hhii',
            formatViewType: dateonly ? 'datetime' : 'time',
            minView: dateonly ? 2 : 0,
            startView: dateonly ? 2 : 1,
            weekStart: dateonly,
            clearBtn: true,
            autoclose: true,
            todayHighlight: dateonly,
            fontAwesome: true
        };

        if (!dateonly) options.maxView = 1;

        var dtp = $input.datetimepicker(options);

        var times = $el.find(".fa-times").parent();
        //var calendar = dateonly ? $el.find(".fa-calendar").parent() : $el.find(".fa-clock-o").parent();
        var div = undefined;

        if (!times || times.length == 0) {
            div = $('<div class="input-group-append input-group-addon"></div>');
            times = $('<div class="input-group-text" style="cursor: pointer;"><span class="fa fa-times"></span></div>');
            div.append(times);
            $el.append(div);
        };
        /*
        if (!calendar || calendar.length == 0) {
            div = $('<div class="input-group-append input-group-addon"></div>');
            var icon = dateonly ? 'fa-calendar' : 'fa-clock-o';
            calendar = $('<div class="input-group-text" style="cursor: pointer;"><span class="fa ' + icon + '"></span></div>');
            div.append(calendar);
            $el.append(div);
        };*/

        times.css('cursor', 'pointer');
        times.click(function () {
            dtp.datetimepicker('reset');
        })
        /*
        calendar.css('cursor', 'pointer');
        calendar.click(function () {
            dtp.datetimepicker('show');
        })*/
    }

    // autocomplete
    //取得貨主代號
    if ($('#txt_StorerKey').length > 0) {
        $.bc({
            url: "api/BaseStorer",
            callback: function (result) {
                if (!result || !result.rows) return;
                $('#txt_StorerKey').typeahead({
                    source: result.rows.map(function (el) {
                        return (el.StorerKey) ? el.StorerKey.trim() : el.StorerKey
                    }),
                    showHintOnFocus: 'all',
                    fitToElement: true,
                    items: 'all'
                });
            }
        });
    }

    if ($.isFunction($.validator)) {
        $.validator.addMethod("equalLoc", function (value, element, target) {
            return $(target).val().trim() != value.trim();
        }, "不可等於來源儲位");
    
        $.validator.addMethod("lessThanQty", function (value, element, target) {
            return parseInt($(target).val().trim()) >= parseInt(value);
        }, "請輸入小於等於可用量的數量");
    
        $.validator.addMethod("QtyEqualsZero", function (value, element, target) {
            return parseInt($(target).val().trim()) != 0;
        }, "調整量不可等於零");
    
        $.validator.addMethod("QtyCompare", function (value, element, target) {
            return parseInt($(target).val().trim()) + parseInt(value) >= 0;
        }, "調整量與可用量相加後不可為負數");
    }
})
/*
                       ::
                      :;J7, :,                        ::;7:
                      ,ivYi, ,                       ;LLLFS:
                      :iv7Yi                       :7ri;j5PL
                     ,:ivYLvr                    ,ivrrirrY2X,
                     :;r@Wwz.7r:                :ivu@kexianli.
                    :iL7::,:::iiirii:ii;::::,,irvF7rvvLujL7ur
                   ri::,:,::i:iiiiiii:i:irrv177JX7rYXqZEkvv17
                ;i:, , ::::iirrririi:i:::iiir2XXvii;L8OGJr71i
              :,, ,,:   ,::ir@mingyi.irii:i:::j1jri7ZBOS7ivv,
                 ,::,    ::rv77iiiriii:iii:i::,rvLq@huhao.Li
             ,,      ,, ,:ir7ir::,:::i;ir:::i:i::rSGGYri712:
           :::  ,v7r:: ::rrv77:, ,, ,:i7rrii:::::, ir7ri7Lri
          ,     2OBBOi,iiir;r::        ,irriiii::,, ,iv7Luur:
        ,,     i78MBBi,:,:::,:,  :7FSL: ,iriii:::i::,,:rLqXv::
        :      iuMMP: :,:::,:ii;2GY7OBB0viiii:i:iii:i:::iJqL;::
       ,     ::::i   ,,,,, ::LuBBu BBBBBErii:i:i:i:i:i:i:r77ii
      ,       :       , ,,:::rruBZ1MBBqi, :,,,:::,::::::iiriri:
     ,               ,,,,::::i:  #arqiao.       ,:,, ,:::ii;i7:
    :,       rjujLYLi   ,,:::::,:::::::::,,   ,:i,:,,,,,::i:iii
    ::      BBBBBBBBB0,    ,,::: , ,:::::: ,      ,,,, ,,:::::::
    i,  ,  ,8BMMBBBBBBi     ,,:,,     ,,, , ,   , , , :,::ii::i::
    :      iZMOMOMBBM2::::::::::,,,,     ,,,,,,:,,,::::i:irr:i:::,
    i   ,,:;u0MBMOG1L:::i::::::  ,,,::,   ,,, ::::::i:i:iirii:i:i:
    :    ,iuUuuXUkFu7i:iii:i:::, :,:,: ::::::::i:i:::::iirr7iiri::
    :     :rk@Yizero.i:::::, ,:ii:::::::i:::::i::,::::iirrriiiri::,
     :      5BMBBBBBBSr:,::rv2kuii:::iii::,:i:,, , ,,:,:i@petermu.,
          , :r50EZ8MBBBBGOBBBZP7::::i::,:::::,: :,:,::i;rrririiii::
              :jujYY7LS0ujJL7r::,::i::,::::::::::::::iirirrrrrrr:ii:
           ,:  :#kevensun.:,:,,,::::i:i:::::,,::::::iir;ii;7v77;ii;i,
           ,,,     ,,:,::::::i:iiiii:i::::,, ::::iiiir@xingjief.r;7:i,
        , , ,,,:,,::::::::iiiiiiiiii:,:,:::::::::iiir;ri7vL77rrirri::
         :,, , ::::::::i:::i:::i:i::,,,,,:,::i:i:::iir;#Secbone.ii:::

*/