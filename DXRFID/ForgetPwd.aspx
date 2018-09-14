<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="ForgetPwd.aspx.cs" Inherits="DXRFID.ForgetPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 设备管理系统--重置用户密码
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">重置用户密码</li>
            </ol>
        </div>

        <div class="tpl-portlet-components" runat="server" id="no_show" visible="false">
            <asp:Label ID="NO_data" runat="server" Width="100%" Style="text-align: center; color: #FF0000"></asp:Label>
        </div>

        <div class="tpl-block" runat="server" id="show">
            <div class="portlet-title">
                <div class="caption font-green bold">
                    <span class="am-icon-code"></span>重置人员密码
                </div>
            </div>
            <div class="am-g tpl-amazeui-form">
                <div class="am-u-sm-12 am-u-md-9">
                    <div class="am-form am-form-horizontal" runat="server">
                        <div class="am-form-group">
                            <label class="am-u-sm-3 am-form-label"><span style="color: red">*</span>工号 / Opid</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="opid" runat="server" placeholder="输入工号 / Opid" autofocus="autofocus" />
                            </div>
                        </div>
                        <div class="am-form-group">
                            <div class="am-u-sm-9 am-u-sm-push-3">
                                <button type="submit" class="am-btn am-btn-primary" runat="server" onserverclick="ChangePWD_ServerClick">重置密码</button>
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
