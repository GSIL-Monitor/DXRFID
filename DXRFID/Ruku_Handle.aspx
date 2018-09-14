<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ruku_Handle.aspx.cs" Inherits="DXRFID.Ruku_Handle" MasterPageFile="~/Header.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Main">
    <div class="tpl-portlet-components">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--入库操作
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">入库操作</li>
            </ol>
        </div>

        <%--表单主要内容--%>


        <div class="tpl-portlet-components" runat="server" id="no_show" visible="false">
            <asp:Label ID="NO_data" runat="server" Width="100%" Style="text-align: center; color: #FF0000"></asp:Label>
        </div>

        <div class="tpl-block" runat="server" id="show">
            <div class="portlet-title">
                <div class="caption font-green bold">
                    <span class="am-icon-code"></span>填写入库表单
                </div>
            </div>
            <div class="am-g tpl-amazeui-form">
                <div class="am-u-sm-12 am-u-md-9">
                    <div class="am-form am-form-horizontal" runat="server">
                        <div class="am-form-group">
                            <label for="user-name" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>资产编号 / Asset Number</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Asset_Number" placeholder="输入资产编号 / Asset Number" runat="server" autofocus="autofocus" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>RFID / Radio Frequency Identification Devices</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="RFID" placeholder="输入RFID / Radio Frequency Identification Devices" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>名称 / Equipment Name</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Equipment_Name" placeholder="输入名称 / Equipment Name" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-email" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>数量 / Quantity</label>
                            <div class="am-u-sm-9">
                                <input type="text" onkeyup="value=value.replace(/[^\d]/g,'')" id="Quantity" placeholder="输入数量 / Quantity" runat="server" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-phone" class="am-u-sm-3 am-form-label">规格 / Specification</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Specification" placeholder="输入规格 / Specification" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">品牌 / Brand</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Brand" placeholder="输入品牌 / Brand" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>存放地点 / Storage Place</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Storage_Place" placeholder="输入存放地点 / Storage Place" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">保管人 / Keeper</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Keeper" placeholder="输入保管人 / Keeper" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-intro" class="am-u-sm-3 am-form-label">设备图片描述 / Description</label>
                            <div class="am-u-sm-9" id="upload_file" runat="server">
                                <dx:ASPxUploadControl ID="Upload" runat="server" Width="100%" Height="20px" ShowProgressPanel="True" ShowUploadButton="True" NullText="点击浏览图片文件..." OnFileUploadComplete="Upload_FileUploadComplete" Theme="iOS" UploadMode="Advanced">
                                    <ValidationSettings AllowedFileExtensions=".jpg, .jpeg, .jpe, .gif, .ico, .png,.jfif" MaxFileCount="1" MaxFileSize="20971520">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="function(s, e) {
	                                                                                        Upload.PerformCallback();
                                                                                        }" />
                                    <AdvancedModeSettings EnableDragAndDrop="True">
                                    </AdvancedModeSettings>
                                </dx:ASPxUploadControl>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <div class="am-u-sm-9 am-u-sm-push-3">
                                <button type="submit" class="am-btn am-btn-primary" runat="server">提交数据</button>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <div class="am-u-sm-9">
                                <label style="color: red; width: 100%; text-align: center" id="tijiao_tishi" runat="server"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>



