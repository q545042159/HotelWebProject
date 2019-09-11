(function ($) {
    $.fn.extend({
        valid: function () {
            //获取参数
            //

            var items = $.isArray(arguments[0]) ? arguments[0] : [],
			isBindSubmit = arguments[2] ? arguments[2] : true,
			ajaxSubmit = arguments[1] ? arguments[1] : false,
            //验证规则
			rule = {
			    // 正则规则
			    "eng": /^[A-Za-z]+$/,
			    "chn": /^[\u0391-\uFFE5]+$/,
			    "mail": /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/,
			    "number": /^\d+$/,
			    "username": /^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){6,16}$/,
			    "password": /^(\w){6,18}$/,
			    //"safe" : />|<|,|\[|\]|\{|\}|\?|\/|\+|=|\||\'|\\|\"|:|;|\~|\!|\@|\#|\*|\$|\%|\^|\&|\(|\)|`/i,
			    "dbc": /[ａ-ｚＡ-Ｚ０-９！＠＃￥％＾＆＊（）＿＋｛｝［］｜：＂＇；．，／？＜＞｀～　]/,
			    "int": /^[0-9]{1,30}$/,
			    "mobile": /^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$/,
			    "phone": /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/,
			    "qq": /[1-9][0-9]{4,}/,
			    "url": /^http[s]?:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,
			    "bodycard": /^((1[1-5])|(2[1-3])|(3[1-7])|(4[1-6])|(5[0-4])|(6[1-5])|71|(8[12])|91)\d{4}((19\d{2}(0[13-9]|1[012])(0[1-9]|[12]\d|30))|(19\d{2}(0[13578]|1[02])31)|(19\d{2}02(0[1-9]|1\d|2[0-8]))|(19([13579][26]|[2468][048]|0[48])0229))\d{3}(\d|X|x)?$/,


			    // 函数规则
			    "eq": function (arg1, arg2) { var a2 = $.trim($("[name='" + arg2.to + "']").val()); return arg1 == a2 ? true : false; },
			    "gt": function (arg1, arg2) { return arg1 > arg2 ? true : false; },
			    "gte": function (arg1, arg2) { return arg1 >= arg2 ? true : false; },
			    "lt": function (arg1, arg2) { return arg1 < arg2 ? true : false; },
			    "lte": function (arg1, arg2) { return arg1 <= arg2 ? true : false; },
			    "checkbox": function (arg1, arg2) { var n = arg2.min ? arg2.min : 1, falg = $("[name='" + arg1 + "']:checkbox:checked").length; return falg >= n ? true : false; },
			    "radio": function (arg1, arg2) { return $("[name='" + arg1 + "']:checked").length > 0 ? true : false; },
			    "length": function (arg1, arg2) {
			        if (arg2.min || arg2.max) {
			            var len = arg1.length, min = arg2.min || 0, max = arg2.max;
			            return ((max && (len > max || len < min)) || (!max && len < min)) ? false : true;
			        }
			    },
			    "ajax": function (arg1, arg2) {
			        var url = arg2.url, value = arg1, bl = false;
			        $.ajax({
			            type: "get",
			            url: url,
			            async: false,
			            data: "value=" + value,
			            success: function (msg) {
			                msg == 1 ? bl = true : bl = false;
			                $.data("bl", bl);
			            }
			        });
			        return bl;
			    }


			},
			msg = "", formObj = $(this), checkRet = false,
			tipname = function (namestr) { return "tip" + namestr.replace(/\$/g, '').replace(/([a-zA-Z0-9])/g, "_$1"); },
            //错误信息提示  ****** 自定义修改 ******
		showError = function (fieldObj, filedName, warnInfo) {
		    checkRet = false;
		    fieldObj.addClass('vError');
		    settip(filedName, warnInfo, 'validatorError');
		},

            //正确信息提示  ****** 自定义修改 ******
		showRight = function (fieldObj, filedName) {
		    var warnInfo = arguments[2] ? arguments[2] : "&nbsp;";
		    settip(filedName, warnInfo, 'validatorValid');
		},
            //规则类型匹配检测
		typeTest = function () {
		    var result = true, args = arguments;
		    if (rule.hasOwnProperty(args[0])) {
		        var t = rule[args[0]], v = args[1];
		        result = $.isFunction(t) ? t(v, args[2]) : t.test(v);
		    }
		    return result;
		},
		fieldfocus = function (item) {
		    var field = $("[name='" + item.name + "']", formObj[0]);
		    settip(item.name, item.onFocus, 'validatorFocus');
		    field.removeClass('vError');
		},
		settip = function (name, warnInfo, sclass) {
		    var field = $("[name='" + name + "']");
		    var tip = tipname(name),
				tipObj = $("#" + tip);
		    //tcolor=tcolor ? tcolor : "";

		    //if(border  && !field.is(":radio") && !field.is(":checkbox")) field.css("border","1px solid "+border);
		    if (tipObj.length > 0) {
		        if (warnInfo) {
		            //if(tcolor) tipObj.css("color",tcolor);
		            tipObj.removeClass().addClass('validatorMsg ' + sclass);
		            tipObj.html(warnInfo);
		        }
		    } else {
		        if (warnInfo) {
		            warnInfo = "<span class='validatorMsg " + sclass + "' id='" + tip + "'> " + warnInfo + "</span>";
		            field.parent().append(warnInfo);
		        }
		    }
		},
            //单元素验证
		fieldCheck = function (item) {
		    var i = item, field = $("[name='" + i.name + "']", formObj[0]);
		    if (!field[0]) return;
		    if (field.hasClass('vError')) { checkRet = false; return; }
		    var warnMsg, matchRet = true, fv = $.trim(field.val());

		    if (field.is(":radio") || field.is(":checkbox")) {
		        matchRet = typeTest(i.type, i.name, i.other);
		    }

		    else if (fv == "") {
		        matchRet = false;
		        warnMsg = i.isNull || i.error;
		    }

		    else if (i.type) {
		        matchRet = i.other ? typeTest(i.type, fv, i.other) : typeTest(i.type, fv);
		    }

		    warnMsg = warnMsg ? warnMsg : (matchRet ? i.success : i.error);



		    if (matchRet) showRight(field, i.name, warnMsg);
		    else showError(field, i.name, warnMsg);
		},
            //元素组验证
		validate = function () {
		    checkRet = true;
		    $.each(items, function () { fieldCheck(this); });
		    if (checkRet == true && ajaxSubmit == true) {
		        ajaxForm();
		        return false;
		    } else {
		        return checkRet;
		    }
		};
            //单元素事件绑定
            $.each(items, function () {
                var field = $("[name='" + this.name + "']", formObj[0]);
                if (field.is(":hidden")) return;
                var obj = this, toCheck = function () { fieldCheck(obj); }, tofocus = function () { fieldfocus(obj); };
                if (field.is(":file") || field.is("select")) {
                    field.change(toCheck);
                } else {
                    field.blur(toCheck);
                }
                field.focus(tofocus);
            });
            //提交事件绑定

            if (isBindSubmit == true) {
                $(this).submit(validate);
            } else {
                $("#" + isBindSubmit).click(validate);
            }



        }
    });
})(jQuery);