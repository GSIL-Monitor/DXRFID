<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DXRFID.Account.ChangePassword" %>

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
    <script type="text/javascript" src="../assets/js/jquery.min.js"></script>
</head>

<body data-type="login">
    <div class="am-g myapp-login">
        <div class="myapp-login-logo-block  tpl-login-max">
            <div class="myapp-login-logo-text">
                <div class="myapp-login-logo-text" id="title" runat="server">
                    OFT RFID夹具管理系统--密码修改 <%--<i class="am-icon-skyatlas"></i>--%>
                </div>
            </div>

            <div class="login-font"> 
                <a href="Login.aspx">
                    <i style="text-decoration:underline">You Can Click Here To Log In System</i>
                </a>
            </div>
            <div class="am-u-sm-10 login-am-center">
                <form class="am-form" runat="server">
                    <fieldset>
                        <div class="am-form-group">
                            <label style="color: white">*工号 / Opid</label>
                            <input type="text" id="opid" runat="server" placeholder="输入工号 / Opid" autofocus="autofocus" />
                        </div>
                        <p></p>
                        <div class="am-form-group" id="forget" runat="server">
                            <div class="am-form-group">
                                <label style="color: white">*旧密码 / Verification Code</label>
                                <input type="password" id="old_pwd" runat="server" placeholder="输入旧密码" />                                
                            </div>
                            <label style="color:red; width:100%; text-align: center" id="check_error" runat="server"></label>
                            <p>
                                <button type="submit" class="am-btn am-btn-default" runat="server" onserverclick="Check_ServerClick">修改密码</button>
                            </p>
                        </div>
                        <div class="am-form-group" runat="server" id="change" visible ="false">
                            <div class="am-form-group">
                                <label style="color: white">*新密码 / New Password</label>
                                <input type="password" id="new_pwd" runat="server" placeholder="输入密码 / Password" autofocus="autofocus" />
                            </div>
                            <label style="color: darkorange; text-align: center" id="Label1" runat="server"></label>
                            <div class="am-form-group">
                                <label style="color: white">*确认密码 / Comfirm Password</label>
                                <input type="password" id="comfirm_pwd" runat="server" placeholder="输入确认密码 / Comfirm Password" />
                            </div>
                            <p>
                                <button type="button" class="am-btn am-btn-default" runat="server" onserverclick="Update_ServerClick">确认修改</button>
                            </p>
                        </div>
                        <label style="color:red; width:100%; text-align: center" id="error" runat="server"></label>
                    </fieldset>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
