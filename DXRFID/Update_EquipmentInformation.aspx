<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Update_EquipmentInformation.aspx.cs" Inherits="DXRFID.Update_EquipmentInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--设备信息维护
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">设备信息维护</li>
            </ol>
        </div>

        <%--表单主要内容--%>


        <div class="tpl-portlet-components" runat="server" id="no_show" visible="false">
            <asp:Label ID="NO_data" runat="server" Width="100%" Style="text-align: center; color: #FF0000"></asp:Label>
        </div>

        <div class="tpl-block" runat="server" id="show">

            <div class="am-g tpl-amazeui-form">

                <div class="portlet-title">
                    <div class="caption font-green bold">
                        <span class="am-icon-code"></span>填写设备信息维护表单
                    </div>
                </div>
                <div class="am-u-sm-12 am-u-md-9">
                    <div class="am-form am-form-horizontal" value="点此重新上传图片 原图片名:">

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>RFID / Radio Frequency Identification Devices</label>
                            <div class="am-u-sm-9 am-u-sm-push-3">
                            </div>
                            <div class="am-u-sm-9">
                                <input type="text" id="RFID" placeholder="输入RFID / Radio Frequency Identification Devices" runat="server" autofocus="autofocus" onserverchange="RFID_ServerChange" /><a href="#" runat="server" visible="false" id="Show_pic">查看设备详情</a>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <div class="am-u-sm-9 am-u-sm-push-3">
                                <label style="color: red; width: 100%; text-align: center" id="RFID_tishi" runat="server"></label>
                            </div>
                        </div>


                        <div class="am-form-group">
                            <label for="user-name" class="am-u-sm-3 am-form-label">资产编号 / Asset Number</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Asset_Number" readonly="readonly" runat="server" style="cursor: pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-name" class="am-u-sm-3 am-form-label">名称 / Equipment Name</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="EquipmentName" readonly="readonly" runat="server" style="cursor: pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-email" class="am-u-sm-3 am-form-label">数量 / Quantity</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Quantity" readonly="readonly" runat="server" style="cursor: pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-phone" class="am-u-sm-3 am-form-label">规格 / Specification</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Specification" readonly="readonly" runat="server" style="cursor: pointer">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">品牌 / Brand</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Brand" readonly="readonly" runat="server" style="cursor: pointer">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>存放地点 / Storage Place</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Storage_Place" runat="server">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>保管人 / Keeper</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Keeper" runat="server" >
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">设备图片描述 / Description</label>
                            <div class="am-u-sm-9">
                                <dx:ASPxUploadControl ID="Upload" runat="server" Visible="false" Width="100%" Height="20px" ShowProgressPanel="True" ShowUploadButton="True" NullText="点击浏览图片文件..." OnFileUploadComplete="Upload_FileUploadComplete" Theme="iOS" UploadMode="Advanced">
                                    <ValidationSettings AllowedFileExtensions=".jpg, .jpeg, .jpe, .gif, .ico, .png,.jfif" MaxFileCount="1" MaxFileSize="20971520">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="function(s, e) {
	                                                                                        Upload.PerformCallback();
                                                                                        }" />
                                    <RemoveButton ImagePosition="Bottom">
                                    </RemoveButton>
                                    <UploadButton>
                                        <Image IconID="export_exportfile_16x16">
                                        </Image>
                                    </UploadButton>
                                </dx:ASPxUploadControl>
                                <input type="button" id="Description" runat="server" onserverclick="Description_ServerClick" style="border: 1px solid #CCCCCC; background-color: #FFFFFF; height: 37px; width: 100%;text-align:left;color:#666666" >
                            </div>
                        </div>

                        <div class="am-form-group">
                            <div class="am-u-sm-9 am-u-sm-push-3">
                                <button type="button" id="submit" class="am-btn am-btn-primary" runat="server" onserverclick="Submit_ServerClick">确认修改</button>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <div class="am-u-sm-9 am-u-sm-push-3">
                                <label style="color: red; width: 100%; text-align: center" id="tijiao_tishi" runat="server"></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
