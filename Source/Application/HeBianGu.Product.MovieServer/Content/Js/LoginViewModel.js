var loginViewModel = function () {
    var self = this;

    self.submit = function () {

        if (isCheck()) {

            //var params = ["app_id=android", "username="+ $.trim($("#username").val()), "password=" + $.trim($("#password").val())];
            $.ajax({
                type: "POST",
                async: false,
                url: "/Login/UserLogin",//v1/member/login
                dataType: "json",
                //data: { "sign": MD5(BuildSign(params)), "username": $.trim($("#username").val()), "password": $.trim($("#password").val()) },
                data: { "method": "v1/member/login", "username": $.trim($("#username").val()), "password": $.trim($("#password").val()) },
                success: function (json) {//debugger

                    if (json.code == 100006) {
                        //var open_id = json.data.user_id;
                        //if (bindCheck(open_id, "hsy")) {
                        //    window.location.href = "index.html" + "?openid=" + open_id + "&from=hsy";
                        //} else {
                        //    wechatbind($.trim($("#username").val()), open_id, "hsy");
                        //}

                        var url = "";
                        if (GetQuery("backurl") != null && GetQuery("backurl") != "") {
                            url = unescape(GetQuery("backurl"));
                        }
                        else {
                            url = "/";
                        }
                        window.location.href = url;
                    }
                    else {
                        layerAlert(json.message);
                    }
                },
                error: function (i, j, k) {

                    //layerAlert(json.message);
                }
            });

        }
        else {
            return false;
        }
    }



}
//var sumit = function () {

//    if(IsCheck())
//    {
//        return true;
//    }

//    return false;
//}

var IsCheck = function () {
    var isright = true;
    var flag;

    try {
        if ($.trim($("#username").val()) == "") {

            flag = 0;
            throw ("请输入手机号");
        }

        if ($.trim($("#password").val()) == "") {

            flag = 1;
            throw ("请输入密码");
        }
    }
    catch (e) {

        if (flag == 0) {
            $("#username").focus();
        }
        else {
            $("#password").focus();
        }
        layerAlert(e);
        isright = false;

    }

    return isright;
}