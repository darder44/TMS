/**
 * @preserve tableExport.jquery.plugin
 * 
 * Copyright (c) 2015-2017 hejiheji001, https://github.com/hejiheji001/tableExport.jquery.plugin
 * Original work Copyright (c) 2014 Giri Raj, https://github.com/kayalshri/, Copyright (c) 2015-2017 hhurz, https://github.com/hhurz/tableExport.jquery.plugin
 * 
 * Licensed under the MIT License, http://opensource.org/licenses/mit-license
 */
!function(a){a.fn.extend({tableExport:function(b){function p(a,b,d){var e="";if(null!==a){var f=ga(a,b,d),g=null===f||""===f?"":f.toString();"tsv"==c.type?(f instanceof Date&&(e=f.toLocaleString()),e=ea(g,"\t"," ")):f instanceof Date?e=c.csvEnclosure+f.toLocaleString()+c.csvEnclosure:(e=ea(g,c.csvEnclosure,c.csvEnclosure+c.csvEnclosure),(e.indexOf(c.csvSeparator)>=0||/[\r\n ]/g.test(e))&&(e=c.csvEnclosure+e+c.csvEnclosure))}return e}function U(){return(new Date).toISOString()}function W(b){var c=[];return a(b).find("thead").first().find("th").each(function(b,d){void 0!==a(d).attr("data-field")?c[b]=a(d).attr("data-field"):c[b]=b.toString()}),c}function X(b,d){var e=!1;return c.ignoreColumn.length>0&&("string"==typeof c.ignoreColumn[0]?l.length>d&&void 0!==l[d]&&-1!=a.inArray(l[d],c.ignoreColumn)&&(e=!0):"number"==typeof c.ignoreColumn[0]&&(-1==a.inArray(d,c.ignoreColumn)&&-1==a.inArray(d-b.length,c.ignoreColumn)||(e=!0))),e}function Y(b,d,e,f,g){if(-1==a.inArray(e,c.ignoreRow)&&-1==a.inArray(e-f,c.ignoreRow)){var h=a(b).filter(function(){return"none"!=a(this).data("tableexport-display")&&(a(this).is(":visible")||"always"==a(this).data("tableexport-display")||"always"==a(this).closest("table").data("tableexport-display"))}).find(d),i=0,k=0;if(h.each(function(b){if(("always"==a(this).data("tableexport-display")||"none"!=a(this).css("display")&&"hidden"!=a(this).css("visibility")&&"none"!=a(this).data("tableexport-display"))&&!1===X(h,b)&&"function"==typeof g){var c,f,d=0,l=0;if(void 0!==j[e]&&j[e].length>0)for(c=0;c<=b;c++)void 0!==j[e][c]&&(g(null,e,c),delete j[e][c],b++);for(k=b,a(this).is("[colspan]")&&(d=parseInt(a(this).attr("colspan")),i+=d>0?d-1:0),a(this).is("[rowspan]")&&(l=parseInt(a(this).attr("rowspan"))),g(this,e,b),c=0;c<d-1;c++)g(null,e,b+c);if(l)for(f=1;f<l;f++)for(void 0===j[e+f]&&(j[e+f]=[]),j[e+f][b+i]="",c=1;c<d;c++)j[e+f][b+i-c]=""}}),void 0!==j[e]&&j[e].length>0)for(var l=0;l<=j[e].length;l++)void 0!==j[e][l]&&(g(null,e,l),delete j[e][l])}}function Z(a,b){if(!0===c.consoleLog&&console.log(a.output()),"string"===c.outputMode)return a.output();if("base64"===c.outputMode)return ua(a.output());if("window"===c.outputMode)return void window.open(URL.createObjectURL(a.output("blob")));try{var d=a.output("blob");saveAs(d,c.fileName+".pdf")}catch(e){sa(c.fileName+".pdf","data:application/pdf"+(b?"":";base64")+",",b?d:a.output())}}function $(a,b,c){var e=0;if(void 0!==c&&(e=c.colspan),e>=0){for(var f=a.width,g=a.textPos.x,h=b.table.columns.indexOf(b.column),i=1;i<e;i++){f+=b.table.columns[h+i].width}if(e>1&&("right"===a.styles.halign?g=a.textPos.x+f-a.width:"center"===a.styles.halign&&(g=a.textPos.x+(f-a.width)/2)),a.width=f,a.textPos.x=g,void 0!==c&&c.rowspan>1&&(a.height=a.height*c.rowspan),"middle"===a.styles.valign||"bottom"===a.styles.valign){var k="string"==typeof a.text?a.text.split(/\r\n|\r|\n/g):a.text,l=k.length||1;l>2&&(a.textPos.y-=(2-d)/2*b.row.styles.fontSize*(l-2)/3)}return!0}return!1}function _(b,c,d){void 0!==d.images&&c.each(function(){var c=a(this).children();if(a(this).is("img")){var e=ra(this.src);d.images[e]={url:this.src,src:this.src}}void 0!==c&&c.length>0&&_(b,c,d)})}function aa(a,b){function f(){b(d)}function g(a){if(a.url){var b=new Image;d=++e,b.crossOrigin="Anonymous",b.onerror=b.onload=function(){if(b.complete&&(0===b.src.indexOf("data:image/")&&(b.width=a.width||b.width||0,b.height=a.height||b.height||0),b.width+b.height)){var c=document.createElement("canvas"),d=c.getContext("2d");c.width=b.width,c.height=b.height,d.drawImage(b,0,0),a.src=c.toDataURL("image/jpeg")}--e||f()},b.src=a.url}}var c,d=0,e=0;if(void 0!==a.images)for(c in a.images)a.images.hasOwnProperty(c)&&g(a.images[c]);return e||f()}function ba(b,d,e){d.each(function(){var d=a(this).children();if(a(this).is("div")){var f=ia(ka(this,"background-color"),[255,255,255]),g=ia(ka(this,"border-top-color"),[0,0,0]),h=ma(this,"border-top-width",c.jspdf.unit),i=this.getBoundingClientRect(),j=this.offsetLeft*e.dw,k=this.offsetTop*e.dh,l=i.width*e.dw,m=i.height*e.dh;e.doc.setDrawColor.apply(void 0,g),e.doc.setFillColor.apply(void 0,f),e.doc.setLineWidth(h),e.doc.rect(b.x+j,b.y+k,l,m,h?"FD":"F")}else if(a(this).is("img")&&void 0!==e.images){var n=ra(this.src),o=e.images[n];if(void 0!==o){var p=b.width/b.height,q=this.width/this.height,r=b.width,s=b.height,t=19.049976/25.4,k=0;q<=p?(s=Math.min(b.height,this.height),r=this.width*s/this.height):q>p&&(r=Math.min(b.width,this.width),s=this.height*r/this.width),r*=t,s*=t,s<b.height&&(k=(b.height-s)/2);try{e.doc.addImage(o.src,b.textPos.x,b.y+k,r,s)}catch(a){}b.textPos.x+=r}}void 0!==d&&d.length>0&&ba(b,d,e)})}function ca(b,c,d){if("function"==typeof d.onAutotableText)d.onAutotableText(d.doc,b,c);else{var e=b.textPos.x,f=b.textPos.y,g={halign:b.styles.halign,valign:b.styles.valign};if(c.length){for(var h=c[0];h.previousSibling;)h=h.previousSibling;for(var i=!1,j=!1;h;){var k=h.innerText||h.textContent||"";k=(k.length&&" "==k[0]?" ":"")+a.trim(k)+(k.length>1&&" "==k[k.length-1]?" ":""),a(h).is("br")&&(e=b.textPos.x,f+=d.doc.internal.getFontSize()),a(h).is("b")?i=!0:a(h).is("i")&&(j=!0),(i||j)&&d.doc.setFontType(i&&j?"bolditalic":i?"bold":"italic");var l=d.doc.getStringUnitWidth(k)*d.doc.internal.getFontSize();if(l){if("linebreak"===b.styles.overflow&&e>b.textPos.x&&e+l>b.textPos.x+b.width){if(".,!%*;:=-".indexOf(k.charAt(0))>=0){var n=k.charAt(0);l=d.doc.getStringUnitWidth(n)*d.doc.internal.getFontSize(),e+l<=b.textPos.x+b.width&&(d.doc.autoTableText(n,e,f,g),k=k.substring(1,k.length)),l=d.doc.getStringUnitWidth(k)*d.doc.internal.getFontSize()}e=b.textPos.x,f+=d.doc.internal.getFontSize()}for(;k.length&&e+l>b.textPos.x+b.width;)k=k.substring(0,k.length-1),l=d.doc.getStringUnitWidth(k)*d.doc.internal.getFontSize();d.doc.autoTableText(k,e,f,g),e+=l}(i||j)&&(a(h).is("b")?i=!1:a(h).is("i")&&(j=!1),d.doc.setFontType(i||j?i?"bold":"italic":"normal")),h=h.nextSibling}b.textPos.x=e,b.textPos.y=f}else d.doc.autoTableText(b.text,b.textPos.x,b.textPos.y,g)}}function da(a){return a.replace(/([.*+?^=!:${}()|\[\]\/\\])/g,"\\$1")}function ea(a,b,c){return a.replace(new RegExp(da(b),"g"),c)}function fa(a){return a=a||"0",a=ea(a,c.numbers.html.thousandsSeparator,""),("number"==typeof(a=ea(a,c.numbers.html.decimalMark,"."))||!1!==jQuery.isNumeric(a))&&a}function ga(b,d,e){var f="";if(null!==b){var h,g=a(b);if(g[0].hasAttribute("data-tableexport-value"))h=g.data("tableexport-value");else if(h=g.html(),"function"==typeof c.onCellHtmlData)h=c.onCellHtmlData(g,d,e,h);else if(""!=h){var i=a.parseHTML(h),j=0,k=0;h="",a.each(i,function(){a(this).is("input")?h+=g.find("input").eq(j++).val():a(this).is("select")?h+=g.find("select option:selected").eq(k++).text():void 0===a(this).html()?h+=a(this).text():void 0!==jQuery().bootstrapTable&&!0===a(this).hasClass("filterControl")||(h+=a(this).html())})}if(!0===c.htmlContent)f=a.trim(h);else if(""!=h){var l=h.replace(/\n/g,"\u2028").replace(/<br\s*[\/]?>/gi,"⁠"),m=a("<div/>").html(l).contents();if(l="",a.each(m.text().split("\u2028"),function(b,c){b>0&&(l+=" "),l+=a.trim(c)}),a.each(l.split("⁠"),function(b,c){b>0&&(f+="\n"),f+=a.trim(c).replace(/\u00AD/g,"")}),"json"==c.type||!1===c.numbers.output){var n=fa(f);!1!==n&&(f=Number(n))}else if(c.numbers.html.decimalMark!=c.numbers.output.decimalMark||c.numbers.html.thousandsSeparator!=c.numbers.output.thousandsSeparator){var n=fa(f);if(!1!==n){var o=(""+n).split(".");1==o.length&&(o[1]="");var p=o[0].length>3?o[0].length%3:0;f=(n<0?"-":"")+(c.numbers.output.thousandsSeparator?(p?o[0].substr(0,p)+c.numbers.output.thousandsSeparator:"")+o[0].substr(p).replace(/(\d{3})(?=\d)/g,"$1"+c.numbers.output.thousandsSeparator):o[0])+(o[1].length?c.numbers.output.decimalMark+o[1]:"")}}}!0===c.escape&&(f=escape(f)),"function"==typeof c.onCellData&&(f=c.onCellData(g,d,e,f))}return f}function ha(a,b,c){return b+"-"+c.toLowerCase()}function ia(a,b){var c=/^rgb\((\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\)$/,d=c.exec(a),e=b;return d&&(e=[parseInt(d[1]),parseInt(d[2]),parseInt(d[3])]),e}function ja(b){var c=ka(b,"text-align"),d=ka(b,"font-weight"),e=ka(b,"font-style"),f="";"start"==c&&(c="rtl"==ka(b,"direction")?"right":"left"),d>=700&&(f="bold"),"italic"==e&&(f+=e),""===f&&(f="normal");var g={style:{align:c,bcolor:ia(ka(b,"background-color"),[255,255,255]),color:ia(ka(b,"color"),[0,0,0]),fstyle:f},colspan:parseInt(a(b).attr("colspan"))||0,rowspan:parseInt(a(b).attr("rowspan"))||0};if(null!==b){var h=b.getBoundingClientRect();g.rect={width:h.width,height:h.height}}return g}function ka(a,b){try{return window.getComputedStyle?(b=b.replace(/([a-z])([A-Z])/,ha),window.getComputedStyle(a,null).getPropertyValue(b)):a.currentStyle?a.currentStyle[b]:a.style[b]}catch(a){}return""}function la(a,b,c){var d=100,e=document.createElement("div");e.style.overflow="hidden",e.style.visibility="hidden",a.appendChild(e),e.style.width=d+c;var f=d/e.offsetWidth;return a.removeChild(e),b*f}function ma(a,b,c){var d=ka(a,b),e=d.match(/\d+/);return null!==e?(e=e[0],la(a.parentElement,e,c)):0}function na(){if(!(this instanceof na))return new na;this.SheetNames=[],this.Sheets={}}function oa(a){for(var b=new ArrayBuffer(a.length),c=new Uint8Array(b),d=0;d!=a.length;++d)c[d]=255&a.charCodeAt(d);return b}function pa(a,b){return b&&(a+=1462),(Date.parse(a)-new Date(Date.UTC(1899,11,30)))/864e5}function qa(a){for(var b={},c={s:{c:1e7,r:1e7},e:{c:0,r:0}},d=0;d!=a.length;++d)for(var e=0;e!=a[d].length;++e){c.s.r>d&&(c.s.r=d),c.s.c>e&&(c.s.c=e),c.e.r<d&&(c.e.r=d),c.e.c<e&&(c.e.c=e);var f={v:a[d][e]};if(null!==f.v){var g=XLSX.utils.encode_cell({c:e,r:d});"number"==typeof f.v?f.t="n":"boolean"==typeof f.v?f.t="b":f.v instanceof Date?(f.t="n",f.z=XLSX.SSF._table[14],f.v=pa(f.v)):f.t="s",b[g]=f}}return c.s.c<1e7&&(b["!ref"]=XLSX.utils.encode_range(c)),b}function ra(a){var c,d,e,b=0;if(0===a.length)return b;for(c=0,e=a.length;c<e;c++)d=a.charCodeAt(c),b=(b<<5)-b+d,b|=0;return b}function sa(a,b,c){var d=window.navigator.userAgent;if(!1!==a&&(d.indexOf("MSIE ")>0||d.match(/Trident.*rv\:11\./)))if(window.navigator.msSaveOrOpenBlob)window.navigator.msSaveOrOpenBlob(new Blob([c]),a);else{var e=document.createElement("iframe");e&&(document.body.appendChild(e),e.setAttribute("style","display:none"),e.contentDocument.open("txt/html","replace"),e.contentDocument.write(c),e.contentDocument.close(),e.focus(),e.contentDocument.execCommand("SaveAs",!0,a),document.body.removeChild(e))}else{var g=document.createElement("a");if(g){var h=null;g.style.display="none",!1!==a?g.download=a:g.target="_blank","object"==typeof c?(h=window.URL.createObjectURL(c),g.href=h):b.toLowerCase().indexOf("base64,")>=0?g.href=b+ua(c):g.href=b+encodeURIComponent(c),document.body.appendChild(g),document.createEvent?(null===f&&(f=document.createEvent("MouseEvents")),f.initEvent("click",!0,!1),g.dispatchEvent(f)):document.createEventObject?g.fireEvent("onclick"):"function"==typeof g.onclick&&g.onclick(),h&&window.URL.revokeObjectURL(h),document.body.removeChild(g)}}}function ta(a){a=a.replace(/\x0d\x0a/g,"\n");for(var b="",c=0;c<a.length;c++){var d=a.charCodeAt(c);d<128?b+=String.fromCharCode(d):d>127&&d<2048?(b+=String.fromCharCode(d>>6|192),b+=String.fromCharCode(63&d|128)):(b+=String.fromCharCode(d>>12|224),b+=String.fromCharCode(d>>6&63|128),b+=String.fromCharCode(63&d|128))}return b}function ua(a){var d,e,f,g,h,i,j,b="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",c="",k=0;for(a=ta(a);k<a.length;)d=a.charCodeAt(k++),e=a.charCodeAt(k++),f=a.charCodeAt(k++),g=d>>2,h=(3&d)<<4|e>>4,i=(15&e)<<2|f>>6,j=63&f,isNaN(e)?i=j=64:isNaN(f)&&(j=64),c=c+b.charAt(g)+b.charAt(h)+b.charAt(i)+b.charAt(j);return c}var m,c={consoleLog:!1,csvEnclosure:'"',csvSeparator:",",csvUseBOM:!0,displayTableName:!1,escape:!1,excelstyles:[],fileName:"tableExport",htmlContent:!1,ignoreColumn:[],ignoreRow:[],jsonScope:"all",jspdf:{orientation:"p",unit:"pt",format:"a4",margins:{left:20,right:10,top:10,bottom:10},autotable:{styles:{cellPadding:2,rowHeight:12,fontSize:8,fillColor:255,textColor:50,fontStyle:"normal",overflow:"ellipsize",halign:"left",valign:"middle"},headerStyles:{fillColor:[52,73,94],textColor:255,fontStyle:"bold",halign:"center"},alternateRowStyles:{fillColor:245},tableExport:{onAfterAutotable:null,onBeforeAutotable:null,onAutotableText:null,onTable:null,outputImages:!0}}},numbers:{html:{decimalMark:".",thousandsSeparator:","},output:{decimalMark:".",thousandsSeparator:","}},onCellData:null,onCellHtmlData:null,outputMode:"file",pdfmake:{enabled:!1},tbodySelector:"tr",tfootSelector:"tr",theadSelector:"tr",tableName:"myTableName",type:"csv",worksheetName:"xlsWorksheetName"},d=1.15,e=this,f=null,g=[],h=[],i=0,j=[],k="",l=[];if(a.extend(!0,c,b),l=W(e),"csv"==c.type||"tsv"==c.type||"txt"==c.type){var n="",o=0;i=0;var q=function(b,d,e){return b.each(function(){k="",Y(this,d,i,e+b.length,function(a,b,d){k+=p(a,b,d)+("tsv"==c.type?"\t":c.csvSeparator)}),k=a.trim(k).substring(0,k.length-1),k.length>0&&(n.length>0&&(n+="\n"),n+=k),i++}),b.length};if(o+=q(a(e).find("thead").first().find(c.theadSelector),"th,td",o),a(e).find("tbody").each(function(){o+=q(a(this).find(c.tbodySelector),"td,th",o)}),c.tfootSelector.length&&q(a(e).find("tfoot").first().find(c.tfootSelector),"td,th",o),n+="\n",!0===c.consoleLog&&console.log(n),"string"===c.outputMode)return n;if("base64"===c.outputMode)return ua(n);if("window"===c.outputMode)return void sa(!1,"data:text/"+("csv"==c.type?"csv":"plain")+";charset=utf-8,",n);try{m=new Blob([n],{type:"text/"+("csv"==c.type?"csv":"plain")+";charset=utf-8"}),saveAs(m,c.fileName+"."+c.type,"csv"!=c.type||!1===c.csvUseBOM)}catch(a){sa(c.fileName+"."+c.type,"data:text/"+("csv"==c.type?"csv":"plain")+";charset=utf-8,"+("csv"==c.type&&c.csvUseBOM?"\ufeff":""),n)}}else if("sql"==c.type){i=0;var r="INSERT INTO `"+c.tableName+"` (";if(g=a(e).find("thead").first().find(c.theadSelector),g.each(function(){Y(this,"th,td",i,g.length,function(a,b,c){r+="'"+ga(a,b,c)+"',"}),i++,r=a.trim(r),r=a.trim(r).substring(0,r.length-1)}),r+=") VALUES ",a(e).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(e).find("tfoot").find(c.tfootSelector)),a(h).each(function(){k="",Y(this,"td,th",i,g.length+h.length,function(a,b,c){k+="'"+ga(a,b,c)+"',"}),k.length>3&&(r+="("+k,r=a.trim(r).substring(0,r.length-1),r+="),"),i++}),r=a.trim(r).substring(0,r.length-1),r+=";",!0===c.consoleLog&&console.log(r),"string"===c.outputMode)return r;if("base64"===c.outputMode)return ua(r);try{m=new Blob([r],{type:"text/plain;charset=utf-8"}),saveAs(m,c.fileName+".sql")}catch(a){sa(c.fileName+".sql","data:application/sql;charset=utf-8,",r)}}else if("json"==c.type){var s=[];g=a(e).find("thead").first().find(c.theadSelector),g.each(function(){var a=[];Y(this,"th,td",i,g.length,function(b,c,d){a.push(ga(b,c,d))}),s.push(a)});var t=[];a(e).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(e).find("tfoot").find(c.tfootSelector)),a(h).each(function(){var b={},c=0;Y(this,"td,th",i,g.length+h.length,function(a,d,e){s.length?b[s[s.length-1][c]]=ga(a,d,e):b[c]=ga(a,d,e),c++}),!1===a.isEmptyObject(b)&&t.push(b),i++});var u="";if(u="head"==c.jsonScope?JSON.stringify(s):"data"==c.jsonScope?JSON.stringify(t):JSON.stringify({header:s,data:t}),!0===c.consoleLog&&console.log(u),"string"===c.outputMode)return u;if("base64"===c.outputMode)return ua(u);try{m=new Blob([u],{type:"application/json;charset=utf-8"}),saveAs(m,c.fileName+".json")}catch(a){sa(c.fileName+".json","data:application/json;charset=utf-8;base64,",u)}}else if("xml"===c.type){i=0;var v='<?xml version="1.0" encoding="utf-8"?>';v+="<tabledata><fields>",g=a(e).find("thead").first().find(c.theadSelector),g.each(function(){Y(this,"th,td",i,g.length,function(a,b,c){v+="<field>"+ga(a,b,c)+"</field>"}),i++}),v+="</fields><data>";var w=1;if(a(e).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(e).find("tfoot").find(c.tfootSelector)),a(h).each(function(){var a=1;k="",Y(this,"td,th",i,g.length+h.length,function(b,c,d){k+="<column-"+a+">"+ga(b,c,d)+"</column-"+a+">",a++}),k.length>0&&"<column-1></column-1>"!=k&&(v+='<row id="'+w+'">'+k+"</row>",w++),i++}),v+="</data></tabledata>",!0===c.consoleLog&&console.log(v),"string"===c.outputMode)return v;if("base64"===c.outputMode)return ua(v);try{m=new Blob([v],{type:"application/xml;charset=utf-8"}),saveAs(m,c.fileName+".xml")}catch(a){sa(c.fileName+".xml","data:application/xml;charset=utf-8;base64,",v)}}else if("multisheetxls"===c.type){var x=a(e).filter(function(){return"none"!=a(this).data("tableexport-display")&&(a(this).is(":visible")||"always"==a(this).data("tableexport-display"))}),y=[];x.each(function(){var b=a(this),d="";i=0,l=W(this),g=b.find("thead").first().find(c.theadSelector),d+='<Table ss:ExpandedColumnCount="$cellcount" ss:ExpandedRowCount="$rowcount" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="54" ss:DefaultRowHeight="14.25">';var e=0;g.each(function(){k="",Y(this,"th,td",i,g.length,function(a,b,c){null!==a&&(k+='<Cell><Data ss:Type="String">'+ga(a,b,c)+"</Data></Cell>",e++)}),k.length>0&&(d+="<Row>"+k+"</Row>"),i++}),d=d.replace("$cellcount",e),h=[],b.find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),console.log(h),a(h).each(function(){a(this);k="",Y(this,"td,th",i,g.length+h.length,function(b,c,d){if(null!==b){var e=a(b).data("tableexport-msonumberformat"),f="String",g=ga(b,c,d).replace(/\n/g,"<br>");void 0!==e&&""!==e&&(f="Number",g=g.replace(/,/g,""),g.indexOf("%")>-1&&(g=g.replace(/%/g,"")/100)),k+='<Cell><Data ss:Type="'+f+'">'+g+"</Data></Cell>"}}),k.length>0&&(d+="<Row>"+k+"</Row>"),i++}),d+="</Table>",d=d.replace("$rowcount",i),y.push(d),!0===c.consoleLog&&console.log(d)});for(var z='<?xml version="1.0"?> <?mso-application progid="Excel.Sheet"?> <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40"> <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office"> <Author>Henry He</Author> <Created>'+U()+'</Created> <Version>16.00</Version> </DocumentProperties> <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office"> <AllowPNG/> </OfficeDocumentSettings> <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel"> <WindowHeight>4635</WindowHeight> <WindowWidth>10335</WindowWidth> <WindowTopX>0</WindowTopX> <WindowTopY>0</WindowTopY> <ProtectStructure>False</ProtectStructure> <ProtectWindows>False</ProtectWindows> </ExcelWorkbook> <Styles> <Style ss:ID="Default" ss:Name="Normal"> <Alignment ss:Vertical="Center"/> <Borders/> <Font ss:FontName="等线" x:CharSet="134" ss:Size="11" ss:Color="#000000"/> <Interior/> <NumberFormat/> <Protection/> </Style> </Styles>',A=0;A<y.length;A++)z+='<Worksheet ss:Name="'+c.worksheetName[A]+'">'+y[A]+'<WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel"> <Selected/> <ProtectObjects>False</ProtectObjects> <ProtectScenarios>False</ProtectScenarios> </WorksheetOptions></Worksheet>';if(z+="</Workbook>",!0===c.consoleLog&&console.log(z),"string"===c.outputMode)return z;if("base64"===c.outputMode)return ua(z);try{m=new Blob([z],{type:"application/vnd.ms-xls"}),saveAs(m,c.fileName+".xls")}catch(a){sa(c.fileName+".xls","data:application/vnd.ms-xls;base64,",z)}}else if("excel"==c.type||"xls"==c.type||"word"==c.type||"doc"==c.type){var B="excel"==c.type||"xls"==c.type?"excel":"word",C="excel"==B?"xls":"doc",D='xmlns:x="urn:schemas-microsoft-com:office:'+B+'"',x=a(e).filter(function(){return"none"!=a(this).data("tableexport-display")&&(a(this).is(":visible")||"always"==a(this).data("tableexport-display"))}),E="";x.each(function(){var b=a(this);i=0,l=W(this),E+="<table><thead>",g=b.find("thead").first().find(c.theadSelector),g.each(function(){k="",Y(this,"th,td",i,g.length,function(b,d,e){if(null!==b){var f="";k+="<th";for(var g in c.excelstyles)if(c.excelstyles.hasOwnProperty(g)){var h=a(b).css(c.excelstyles[g]);""!==h&&"0px none rgb(0, 0, 0)"!=h&&"rgba(0, 0, 0, 0)"!=h&&(f+=""===f?'style="':";",f+=c.excelstyles[g]+":"+h)}""!==f&&(k+=" "+f+'"'),a(b).is("[colspan]")&&(k+=' colspan="'+a(b).attr("colspan")+'"'),a(b).is("[rowspan]")&&(k+=' rowspan="'+a(b).attr("rowspan")+'"'),k+=">"+ga(b,d,e)+"</th>"}}),k.length>0&&(E+="<tr>"+k+"</tr>"),i++}),E+="</thead><tbody>",b.find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,b.find("tfoot").find(c.tfootSelector)),a(h).each(function(){var b=a(this);k="",Y(this,"td,th",i,g.length+h.length,function(d,e,f){if(null!==d){var g="",h=a(d).data("tableexport-msonumberformat");void 0===h&&"function"==typeof c.onMsoNumberFormat&&(h=c.onMsoNumberFormat(d,e,f)),void 0!==h&&""!==h&&(g="style=\"mso-number-format:'"+h+"'");for(var i in c.excelstyles)c.excelstyles.hasOwnProperty(i)&&(h=a(d).css(c.excelstyles[i]),""===h&&(h=b.css(c.excelstyles[i])),""!==h&&"0px none rgb(0, 0, 0)"!=h&&"rgba(0, 0, 0, 0)"!=h&&(g+=""===g?'style="':";",g+=c.excelstyles[i]+":"+h));k+="<td",""!==g&&(k+=" "+g+'"'),a(d).is("[colspan]")&&(k+=' colspan="'+a(d).attr("colspan")+'"'),a(d).is("[rowspan]")&&(k+=' rowspan="'+a(d).attr("rowspan")+'"'),k+=">"+ga(d,e,f).replace(/\n/g,"<br>")+"</td>"}}),k.length>0&&(E+="<tr>"+k+"</tr>"),i++}),c.displayTableName&&(E+="<tr><td></td></tr><tr><td></td></tr><tr><td>"+ga(a("<p>"+c.tableName+"</p>"))+"</td></tr>"),E+="</tbody></table>",!0===c.consoleLog&&console.log(E)});var z='<html xmlns:o="urn:schemas-microsoft-com:office:office" '+D+' xmlns="http://www.w3.org/TR/REC-html40">';if(z+='<meta http-equiv="content-type" content="application/vnd.ms-'+B+'; charset=UTF-8">',z+="<head>","excel"===B&&(z+="\x3c!--[if gte mso 9]>",z+="<xml>",z+="<x:ExcelWorkbook>",z+="<x:ExcelWorksheets>",z+="<x:ExcelWorksheet>",z+="<x:Name>",z+=c.worksheetName,z+="</x:Name>",z+="<x:WorksheetOptions>",z+="<x:DisplayGridlines/>",z+="</x:WorksheetOptions>",z+="</x:ExcelWorksheet>",z+="</x:ExcelWorksheets>",z+="</x:ExcelWorkbook>",z+="</xml>",z+="<![endif]--\x3e"),z+="<style>br {mso-data-placement:same-cell;}</style>",z+="</head>",z+="<body>",z+=E,z+="</body>",z+="</html>",!0===c.consoleLog&&console.log(z),"string"===c.outputMode)return z;if("base64"===c.outputMode)return ua(z);try{m=new Blob([z],{type:"application/vnd.ms-"+c.type}),saveAs(m,c.fileName+"."+C)}catch(a){sa(c.fileName+"."+C,"data:application/vnd.ms-"+B+";base64,",z)}}else if("xlsx"==c.type){var F=[],G=[];i=0,h=a(e).find("thead").first().find(c.theadSelector),a(e).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(e).find("tfoot").find(c.tfootSelector)),a(h).each(function(){var a=[];Y(this,"th,td",i,h.length,function(b,c,d){if(void 0!==b&&null!==b){var e=parseInt(b.getAttribute("colspan")),f=parseInt(b.getAttribute("rowspan")),g=ga(b,c,d);if(""!==g&&g==+g&&(g=+g),G.forEach(function(b){if(i>=b.s.r&&i<=b.e.r&&a.length>=b.s.c&&a.length<=b.e.c)for(var c=0;c<=b.e.c-b.s.c;++c)a.push(null)}),(f||e)&&(f=f||1,e=e||1,G.push({s:{r:i,c:a.length},e:{r:i+f-1,c:a.length+e-1}})),a.push(""!==g?g:null),e)for(var h=0;h<e-1;++h)a.push(null)}}),F.push(a),i++});var H=new na,I=qa(F);I["!merges"]=G,H.SheetNames.push(c.worksheetName),H.Sheets[c.worksheetName]=I;var J=XLSX.write(H,{bookType:c.type,bookSST:!1,type:"binary"});try{m=new Blob([oa(J)],{type:"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8"}),saveAs(m,c.fileName+"."+c.type)}catch(a){sa(c.fileName+"."+c.type,"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8,",m)}}else if("png"==c.type)html2canvas(a(e)[0]).then(function(a){for(var b=a.toDataURL(),d=atob(b.substring(22)),e=new ArrayBuffer(d.length),f=new Uint8Array(e),g=0;g<d.length;g++)f[g]=d.charCodeAt(g);if(!0===c.consoleLog&&console.log(d),"string"===c.outputMode)return d;if("base64"===c.outputMode)return ua(b);if("window"===c.outputMode)return void window.open(b);try{m=new Blob([e],{type:"image/png"}),saveAs(m,c.fileName+".png")}catch(a){sa(c.fileName+".png","data:image/png,",m)}});else if("pdf"==c.type)if(!0===c.pdfmake.enabled){var K=[],L=[];i=0,g=a(this).find("thead").first().find(c.theadSelector),g.each(function(){var a=[];Y(this,"th,td",i,g.length,function(b,c,d){a.push(ga(b,c,d))}),a.length&&L.push(a);for(var b=K.length;b<a.length;b++)K.push("*");i++}),a(this).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(this).find("tfoot").find(c.tfootSelector)),a(h).each(function(){var a=[];Y(this,"td,th",i,g.length+h.length,function(b,c,d){a.push(ga(b,c,d))}),a.length&&L.push(a),i++});var M={pageOrientation:"landscape",content:[{table:{headerRows:g.length,widths:K,body:L}}],defaultStyle:{font:"Roboto"}};pdfMake.fonts={Roboto:{normal:"Roboto-Regular.ttf",bold:"Roboto-Medium.ttf",italics:"Roboto-Italic.ttf",bolditalics:"Roboto-MediumItalic.ttf"}},pdfMake.createPdf(M).getBuffer(function(a){try{var b=new Blob([a],{type:"application/pdf"});saveAs(b,c.fileName+".pdf")}catch(b){sa(c.fileName+".pdf","data:application/pdf;base64,",a)}})}else if(!1===c.jspdf.autotable){var N={dim:{w:ma(a(e).first().get(0),"width","mm"),h:ma(a(e).first().get(0),"height","mm")},pagesplit:!1},O=new jsPDF(c.jspdf.orientation,c.jspdf.unit,c.jspdf.format);O.addHTML(a(e).first(),c.jspdf.margins.left,c.jspdf.margins.top,N,function(){Z(O,!1)})}else{var P=c.jspdf.autotable.tableExport;if("string"==typeof c.jspdf.format&&"bestfit"===c.jspdf.format.toLowerCase()){var Q={a0:[2383.94,3370.39],a1:[1683.78,2383.94],a2:[1190.55,1683.78],a3:[841.89,1190.55],a4:[595.28,841.89]},R="",S="",T=0;a(e).filter(":visible").each(function(){if("none"!=a(this).css("display")){var b=ma(a(this).get(0),"width","pt");if(b>T){b>Q.a0[0]&&(R="a0",S="l");for(var c in Q)Q.hasOwnProperty(c)&&Q[c][1]>b&&(R=c,S="l",Q[c][0]>b&&(S="p"));T=b}}}),c.jspdf.format=""===R?"a4":R,c.jspdf.orientation=""===S?"w":S}P.doc=new jsPDF(c.jspdf.orientation,c.jspdf.unit,c.jspdf.format),!0===P.outputImages&&(P.images={}),void 0!==P.images&&(a(e).filter(function(){return"none"!=a(this).data("tableexport-display")&&(a(this).is(":visible")||"always"==a(this).data("tableexport-display"))}).each(function(){var b=0;g=a(this).find("thead").find(c.theadSelector),a(this).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(this).find("tfoot").find(c.tfootSelector)),a(h).each(function(){Y(this,"td,th",g.length+b,g.length+h.length,function(b,c,d){if(void 0!==b&&null!==b){var e=a(b).children();void 0!==e&&e.length>0&&_(b,e,P)}}),b++})}),g=[],h=[]),aa(P,function(b){a(e).filter(function(){return"none"!=a(this).data("tableexport-display")&&(a(this).is(":visible")||"always"==a(this).data("tableexport-display"))}).each(function(){var b,e=0;if(l=W(this),P.columns=[],P.rows=[],P.rowoptions={},"function"==typeof P.onTable&&!1===P.onTable(a(this),c))return!0;c.jspdf.autotable.tableExport=null;var f=a.extend(!0,{},c.jspdf.autotable);if(c.jspdf.autotable.tableExport=P,f.margin={},a.extend(!0,f.margin,c.jspdf.margins),f.tableExport=P,"function"!=typeof f.beforePageContent&&(f.beforePageContent=function(a){if(1==a.pageCount){a.table.rows.concat(a.table.headerRow).forEach(function(b){b.height>0&&(b.height+=(2-d)/2*b.styles.fontSize,a.table.height+=(2-d)/2*b.styles.fontSize)})}}),"function"!=typeof f.createdHeaderCell&&(f.createdHeaderCell=function(b,c){if(b.styles=a.extend({},c.row.styles),void 0!==P.columns[c.column.dataKey]){var d=P.columns[c.column.dataKey];if(void 0!==d.rect){var e;b.contentWidth=d.rect.width,void 0!==P.heightRatio&&0!==P.heightRatio||(e=c.row.raw[c.column.dataKey].rowspan?c.row.raw[c.column.dataKey].rect.height/c.row.raw[c.column.dataKey].rowspan:c.row.raw[c.column.dataKey].rect.height,P.heightRatio=b.styles.rowHeight/e),e=c.row.raw[c.column.dataKey].rect.height*P.heightRatio,e>b.styles.rowHeight&&(b.styles.rowHeight=e)}void 0!==d.style&&!0!==d.style.hidden&&(b.styles.halign=d.style.align,"inherit"===f.styles.fillColor&&(b.styles.fillColor=d.style.bcolor),"inherit"===f.styles.textColor&&(b.styles.textColor=d.style.color),"inherit"===f.styles.fontStyle&&(b.styles.fontStyle=d.style.fstyle))}}),"function"!=typeof f.createdCell&&(f.createdCell=function(a,b){var c=P.rowoptions[b.row.index+":"+b.column.dataKey];void 0!==c&&void 0!==c.style&&!0!==c.style.hidden&&(a.styles.halign=c.style.align,"inherit"===f.styles.fillColor&&(a.styles.fillColor=c.style.bcolor),"inherit"===f.styles.textColor&&(a.styles.textColor=c.style.color),"inherit"===f.styles.fontStyle&&(a.styles.fontStyle=c.style.fstyle))}),"function"!=typeof f.drawHeaderCell&&(f.drawHeaderCell=function(a,b){var c=P.columns[b.column.dataKey];return(!0!==c.style.hasOwnProperty("hidden")||!0!==c.style.hidden)&&c.rowIndex>=0&&$(a,b,c)}),"function"!=typeof f.drawCell&&(f.drawCell=function(a,b){var c=P.rowoptions[b.row.index+":"+b.column.dataKey];if($(a,b,c))if(P.doc.rect(a.x,a.y,a.width,a.height,a.styles.fillStyle),void 0!==c&&void 0!==c.kids&&c.kids.length>0){var d=a.height/c.rect.height;(d>P.dh||void 0===P.dh)&&(P.dh=d),P.dw=a.width/c.rect.width;var e=a.textPos.y;ba(a,c.kids,P),a.textPos.y=e,ca(a,c.kids,P)}else ca(a,{},P);return!1}),P.headerrows=[],g=a(this).find("thead").find(c.theadSelector),g.each(function(){b=0,P.headerrows[e]=[],Y(this,"th,td",e,g.length,function(a,c,d){var f=ja(a);f.title=ga(a,c,d),f.key=b++,f.rowIndex=e,P.headerrows[e].push(f)}),e++}),e>0)for(var i=e-1;i>=0;)a.each(P.headerrows[i],function(){var a=this;i>0&&null===this.rect&&(a=P.headerrows[i-1][this.key]),null!==a&&a.rowIndex>=0&&(!0!==a.style.hasOwnProperty("hidden")||!0!==a.style.hidden)&&P.columns.push(a)}),i=P.columns.length>0?-1:i-1;var j=0;h=[],a(this).find("tbody").each(function(){h.push.apply(h,a(this).find(c.tbodySelector))}),c.tfootSelector.length&&h.push.apply(h,a(this).find("tfoot").find(c.tfootSelector)),a(h).each(function(){var c=[];b=0,Y(this,"td,th",e,g.length+h.length,function(d,e,f){if(void 0===P.columns[b]){var g={title:"",key:b,style:{hidden:!0}};P.columns.push(g)}if(void 0!==d&&null!==d){var g=ja(d);g.kids=a(d).children(),P.rowoptions[j+":"+b++]=g}else{var g=a.extend(!0,{},P.rowoptions[j+":"+(b-1)]);g.colspan=-1,P.rowoptions[j+":"+b++]=g}c.push(ga(d,e,f))}),c.length&&(P.rows.push(c),j++),e++}),"function"==typeof P.onBeforeAutotable&&P.onBeforeAutotable(a(this),P.columns,P.rows,f),P.doc.autoTable(P.columns,P.rows,f),"function"==typeof P.onAfterAutotable&&P.onAfterAutotable(a(this),f),c.jspdf.autotable.startY=P.doc.autoTableEndPosY()+f.margin.top}),Z(P.doc,void 0!==P.images&&!1===jQuery.isEmptyObject(P.images)),void 0!==P.headerrows&&(P.headerrows.length=0),void 0!==P.columns&&(P.columns.length=0),void 0!==P.rows&&(P.rows.length=0),delete P.doc,P.doc=null})}return this}})}(jQuery);
