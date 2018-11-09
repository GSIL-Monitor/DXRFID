<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateHeadPic.aspx.cs" Inherits="DXRFID.Class.UpdateHeadPic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>OFT RFID 夹具管理系统</title>
    <meta name="description" content="这是一个 index 页面">
    <meta name="keywords" content="index">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="renderer" content="webkit">
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <link rel="icon" type="image/png" href="assets/i/favicon.png">
    <link rel="apple-touch-icon-precomposed" href="assets/i/app-icon72x72@2x.png">
    <meta name="apple-mobile-web-app-title" content="Amaze UI" />
    <link rel="stylesheet" href="assets/css/amazeui.min.css" />
    <link rel="stylesheet" href="assets/css/admin.css">
    <link rel="stylesheet" href="assets/css/app.css">
    <script type="text/javascript" src="assets/js/echarts.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tpl-portlet-components">

            <%--表单头--%>
            <div class="tpl-block" runat="server" id="show">
                <div class="am-g tpl-amazeui-form">
                    <div class="am-u-sm-12 am-u-md-9">
                        <div class="am-form am-form-horizontal" runat="server">

                            <div class="am-form-group">
                                <label for="user-intro" class="am-u-sm-3 am-form-label">请选择头像文件：</label>
                                <div class="am-u-sm-9" id="upload_file" runat="server">
                                    <dx:ASPxUploadControl ID="Upload" runat="server" Width="100%" Height="20px" ShowProgressPanel="True" ShowUploadButton="True" NullText="点击浏览图片文件..." OnFileUploadComplete="Upload_FileUploadComplete" Theme="iOS" UploadMode="Advanced">
                                        <ValidationSettings AllowedFileExtensions=".jpg,.png" MaxFileCount="1" MaxFileSize="20971520">
                                        </ValidationSettings>
                                        <ClientSideEvents FileUploadComplete="function(s, e) {
	                                                                                        Upload.PerformCallback();
                                                                                        }" />
                                        <AdvancedModeSettings EnableDragAndDrop="True">
                                        </AdvancedModeSettings>
                                    </dx:ASPxUploadControl>
                                </div>
                                 <label style="color: red; width: 100%; text-align: center" id="tijiao_tishi" runat="server"></label>
                            </div>
                            <div>
                                <asp:Image ID="Image_show" runat="server" Style="text-align: center" /><br />
                            </div>
                           <%-- <div class="am-form-group">
                                <div class="am-u-sm-9">
                                    <label style="color: red; width: 100%; text-align: center" id="tijiao_tishi" runat="server"></label>
                                </div>
                            </div>--%>
                            

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
