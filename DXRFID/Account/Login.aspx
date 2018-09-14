<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DXRFID.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>OFT RFID夹具管理系统</title>
    <meta name="keywords" content="index" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <link rel="icon" type="image/png" href="../assets/i/favicon.png" />
    <link rel="apple-touch-icon-precomposed" href="../assets/i/app-icon72x72@2x.png" />
    <meta name="apple-mobile-web-app-title" content="Amaze UI" />
    <link rel="stylesheet" href="../assets/css/admin.css" />
    <link rel="stylesheet" href="../assets/css/amazeui.min.css" />
    <link rel="stylesheet" href="../assets/css/app.css" />
    <script src="../assets/js/jquery.min.js"></script>
</head>

<body data-type="login">
    <div class="am-g myapp-login">
        <div class="myapp-login-logo-block  tpl-login-max">
            <div class="myapp-login-logo-text">
                <div class="myapp-login-logo-text">
                    OFT RFID夹具管理系统--用户登录
                    <%--<i class="am-icon-skyatlas"></i>--%>
                </div>
            </div>

            <div class="am-u-sm-10 login-am-center">
                <form class="am-form" runat="server">
                    <fieldset>
                        <div class="am-form-group">
                            <input type="text" id="opid" runat="server" placeholder="输入工号" autofocus="autofocus" />
                        </div>
                        <div class="am-form-group">
                            <input type="password" id="pwd" runat="server" placeholder="输入密码" />
                        </div>
                        
                        <p>
                            <button type="submit" class="am-btn am-btn-default" runat="server">登录</button>
                        </p><label style="color: darkorange; text-align: center" id="error" runat="server"></label>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>

</body>
</html>
