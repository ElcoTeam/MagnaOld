/**
	 * 扩展的基本校验规则，
	 */
	$.extend($.fn.validatebox.defaults.rules, { 
	    minLength : { // 判断最小长度 
	        validator : function(value, param) { 
	            value = $.trim(value); //去空格 
	            return value.length >= param[0]; 
	        }, 
	        message : '最少输入 {0} 个字符。'
	    }, 
	    length:{validator:function(value,param){ 
	        var len=$.trim(value).length; 
	            return len>=param[0]&&len<=param[1]; 
	        }, 
	        message: '输入大小不正确'
	    },
	    maxmin : {
	        validator: function (value) {// 验证0到1之间
	            return /^[0-1]\d*$/i.test(value);
	        },
	        message: '输入数字0代表包含，1代表不包含'
	    },
	    phone : {// 验证电话号码 
	        validator : function(value) { 
	            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value); 
	        }, 
	        message : '格式不正确,请使用下面格式:020-88888888'
	    }, 
	    mobile : {// 验证手机号码 
	        validator : function(value) { 
	            return /^(13|15|18)\d{9}$/i.test(value); 
	        }, 
	        message : '手机号码格式不正确'
	    }, 
	    idcard : {// 验证身份证 
	        validator : function(value) { 
	            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value); 
	        }, 
	        message : '身份证号码格式不正确'
	    }, 
	    intOrFloat : {// 验证整数或小数 
	        validator : function(value) { 
	            return /^\d+(\.\d+)?$/i.test(value); 
	        }, 
	        message : '请输入数字，并确保格式正确'
	    }, 
	    currency : {// 验证货币 
	        validator : function(value) { 
	            return /^\d+(\.\d+)?$/i.test(value); 
	        }, 
	        message : '货币格式不正确'
	    }, 
	    qq : {// 验证QQ,从10000开始 
	        validator : function(value) { 
	            return /^[1-9]\d{4,9}$/i.test(value); 
	        }, 
	        message : 'QQ号码格式不正确'
	    }, 
	    integer : {// 验证整数 
	        validator : function(value) { 
	            return /^[+]?[1-9]+\d*$/i.test(value); 
	        }, 
	        message : '请输入整数'
	    },     
	    chinese : {// 验证中文 
	        validator : function(value) { 
	            return /^[\u0391-\uFFE5]+$/i.test(value); 
	        }, 
	        message : '请输入中文'
	    }, 
	    english : {// 验证英语 
	        validator : function(value) { 
	            return /^[A-Za-z]+$/i.test(value); 
	        }, 
	        message : '请输入英文'
	    }, 
	    unnormal : {// 验证是否包含空格和非法字符 
	        validator : function(value) { 
	            return /.+/i.test(value); 
	        }, 
	        message : '输入值不能为空和包含其他非法字符'
	    }, 
	    account: {//param的值为[]中值
	        validator: function (value, param) {
	            if (value.length < param[0] || value.length > param[1]) {
	                $.fn.validatebox.defaults.rules.account.message = '用户名长度必须在' + param[0] + '至' + param[1] + '范围';
	                return false;
	            } else {
	                if (!/^[\w]+$/.test(value)) {
	                    $.fn.validatebox.defaults.rules.account.message = '用户名只能数字、字母、下划线组成.';
	                    return false;
	                } else {
	                    return true;
	                }
	            }
	        }, message: ''
	    },
	    username : {// 验证用户名 
	        validator : function(value) { 
	            return /^[a-zA-Z][a-zA-Z0-9_]{5,15}$/i.test(value); 
	        }, 
	        message : '用户名不合法（字母开头，允许6-16字节，允许字母数字下划线）'
	    }, 
	    faxno : {// 验证传真 
	        validator : function(value) { 
//	            return /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/i.test(value); 
	            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value); 
	        }, 
	        message : '传真号码不正确'
	    }, 
	    zip : {// 验证邮政编码 
	        validator : function(value) { 
	            return /^[1-9]\d{5}$/i.test(value); 
	        }, 
	        message : '邮政编码格式不正确'
	    },
	    orgCode: {// 验证组织机构代码
	        validator : function(value) { 
	            return /[0-9]{8}-[a-zA-Z0-9]/i.test(value); 
	        }, 
	        message : '组织机构代码格式不正确，正确的格式如12345678-1'
	    },
	    ip : {// 验证IP地址 
	        validator : function(value) { 
	            return /d+.d+.d+.d+/i.test(value); 
	        }, 
	        message : 'IP地址格式不正确'
	    }, 
	    name : {// 验证姓名，可以是中文或英文 
	            validator : function(value) { 
	                return /^[\u0391-\uFFE5]+$/i.test(value)|/^\w+[\w\s]+\w+$/i.test(value); 
	            }, 
	            message : '请输入姓名'
	    }, 
	    carNo:{ 
	        validator : function(value){ 
	            return /^[\u4E00-\u9FA5][\da-zA-Z]{6}$/.test(value); 
	        }, 
	        message : '车牌号码无效（例：粤J12350）'
	    }, 
	    carenergin:{ 
	        validator : function(value){ 
	            return /^[a-zA-Z0-9]{16}$/.test(value); 
	        }, 
	        message : '发动机型号无效(例：FG6H012345654584)'
	    }, 
	    email:{ 
	        validator : function(value){ 
	        return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value); 
	    }, 
	    message : '请输入有效的电子邮件账号(例：abc@126.com)'   
	    }, 
	    msn:{ 
	        validator : function(value){ 
	        return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value); 
	    }, 
	    message : '请输入有效的msn账号(例：abc@hotnail(msn/live).com)'
	    },
	    same:{ 
	        validator : function(value, param){ 
	            if($("#"+param[0]).val() != "" && value != ""){ 
	                return $("#"+param[0]).val() == value; 
	            }else{ 
	                return true; 
	            } 
	        }, 
	        message : '两次输入的密码不一致！'   
	    },
	    warnmintime : { // 判断告警的值只能一级一级的增加，最小值
	        validator : function(value, param) { 
	            value = $.trim(value); //去空格 
	            if( value !="")
	            for(var i=0;i<param.length; i++){
	                $(param[i]).val();
	                if($(param[i]).combobox('getValue')){
	                    var temp=$.trim($(param[i]).combobox('getValue'));
	                    if(temp !="" && !isNaN(temp) && parseInt(value) <= parseInt(temp))
	                        return false;
	                   }
	            }
	            return true;
	        }, 
	        message : '不能小于当前告警的前一级的告警时间'
	    },
	    warnmaxtime : { // 判断告警的值只能一级一级的增加，最大值
	        validator : function(value, param) { 
	            value = $.trim(value); //去空格 
	            if( value !="")
	            for(var i=0;i<param.length; i++){
	                $(param[i]).val();
	                if($(param[i]).combobox('getValue')){
	                    var temp=$.trim($(param[i]).combobox('getValue'));
	                    if(temp !="" && !isNaN(temp) && parseInt(value) >= parseInt(temp))
	                        return false;
	                   }
	            }
	            return true;
	        }, 
	        message : '不能大于当前告警的后一级的告警时间'
	    },
	    compareDate: {
	        validator: function (value, param) {
	        return dateCompare($(param[0]).datetimebox('getValue'), value); //注意easyui 时间控制获取值的方式
	        },
	        message: '开始日期不能大于结束日期'
	        }, 
		    mac : {// 验证物理地址
		        validator : function(value) { 
		            return /^([A-Z0-9]{2})-([A-Z0-9]{2})-([A-Z0-9]{2})-([A-Z0-9]{2})-([A-Z0-9]{2})-([A-Z0-9]{2})$/i.test(value); 
		        }, 
		        message : '物理地址格式不正确'
		    }, 
		    port : {// 验证端口
		        validator : function(value) { 
		            return /^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-5]{2}[0-3][0-5])$/i.test(value); 
		        }, 
		        message : '端口输入不正确'
		    },
		    devicePriority : {// 堆叠优先级
		    		validator : function(value) { 
		            return /^(1|2|3|4|5|6|7|8|9|10|11|12|13|14|15)$/i.test(value); 
		        }, 
		        message : '请输入1-15内的数字'
		    },
		    netmask : {// 验证IP地址 
		        validator : function(value) { 
		            return /[0-255].[0-255].[0-255].[0-255]/i.test(value); 
		        }, 
		        message : '子网掩码地址格式不正确'
		    },
		    date: {
		        validator: function (value) {
		            //格式yyyy-mm-dd hh:mm:ss
		           return /(\d{4}-\d{2}-\d{2}\d{2}:\d{2}:\d{2})|(\d{2}-\d{2}\d{2}:\d{2}:\d{2})|(\d{2}:\d{2}:\d{2})/.test(value);
		        },
		        message: '请输入正确日期格式，格式为：yyyy-mm-dd hh:mm:ss！'
		    },
		    sqlinjection: {
		        validator: function (value) {
		        	var array = ['`','~','!','@','#','$','%','^','&','*','(',')','-','+','_','=','[',']','{','}',':',';','"','\'','<','>',',','.','?','/','|','\\',
		        	             '｀','～','！','＠','＃','＄','％','＾','＆','＊','（','）','－','＋','＿','＝','［','］','｛','｝','｜','＼','：','；','＇','＂','，','．','＜','＞','？','／',
		        	             '∑','㏒','㏑','∏','+','-',
		        	             'doucment','javascript','function','NULL','null','sqlconn.Open()'];
		        	var result;
		        	var flag;
		        	for(var i=0;i<array.length;i++){
		        		result = value.indexOf(array[i]);
		        		if(result == -1){
		        			flag = true;
		        		}else{
		        			flag = false;
		        			break;
		        		}
		        	}
		        	return flag;
		        },
		        message: '含有非法字符，请检查!'
		    },
		    domainName:{
		    	validator: function (value) {
		           return /^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$/.test(value);
		        },
		        message: '域名格式不正确！'
		    },
		    md: {
			    	validator: function(value, param){
				    	startTime = $(param[0]).datetimebox('getValue');
				    	/*var d1 = $.fn.datebox.defaults.parser(startTime);
				    	var d2 = $.fn.datebox.defaults.parser(value);*/
				    	varify=value>startTime;
				    	return varify;
			    	},
			    	message: '结束时间要大于开始时间！'
		    	},
		   ips : {// 验证IP地址 
		        validator : function(value) { 
		            return /[0-999].[0-999].[0-999].[0-999]/i.test(value); 
		        }, 
		        message : 'IP地址格式不正确'
		    },
		    ipxm : {// 验证IP地址 
		        validator : function(value) { 
		        	var guize =  /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
		            return guize.test(value); 
		        }, 
		        message : 'IP地址格式不正确'
		    },
		    mon: {
		    	validator: function(value){
		    		var startMon = $("#startMon").find("option : selected").val();
		    		var endMon = $("#endMon").find("option : selected").val();
		    		varify = startMon > endMon;
			    	return varify;
		    	},
		    	message: '结束时间要大于开始时间！'
	    	},
	    	times : {
	    		validator : function(value) { 
		            return /^([01]\d|2[01234]):([0-5]\d|60)$/i.test(value); 
		        }, 
		        message : '时间格式不正确'
	    	},
	    	pwd : {
	    	    validator: function (value) {
	    	        return /^(?![a-zA-Z]+$)(?![A-Z0-9]+$)(?![A-Z\W_]+$)(?![a-z0-9]+$)(?![a-z\W_]+$)(?![0-9\W_]+$)[a-zA-Z0-9\W_]{8,}$/.test(value);
	    	    },
	    	    message: '密码长度最少8位,大写字母，小写字母，数字，特殊符号必须四选三'
	    	}
	});


   