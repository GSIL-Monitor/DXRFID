<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Borrow_Handle.aspx.cs" Inherits="DXRFID.Borrow_Handle" MasterPageFile="~/Header.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Main">
    <div class="tpl-portlet-components">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--借出操作
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">借出操作</li>
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
                        <span class="am-icon-code"></span>设备信息查询
                    </div>
                </div>
                <div class="am-u-sm-12 am-u-md-9">
                    <div class="am-form am-form-horizontal">

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>RFID / Radio Frequency Identification Devices</label>
                            <div class="am-u-sm-9 am-u-sm-push-3">
                            </div>
                            <div class="am-u-sm-9">
                                <input type="text" id="RFID" placeholder="输入RFID / Radio Frequency Identification Devices" runat="server" autofocus="autofocus" /><a href="#" runat="server" visible="false" id="Show_pic">查看设备详情</a>
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
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">存放地点 / Storage Place</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Storage_Place" readonly="readonly" runat="server" style="cursor: pointer">
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">保管人 / Keeper</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Keeper" readonly="readonly" runat="server" style="cursor: pointer">
                            </div>
                        </div>

                        <div>
                            <label style="color: red; width: 100%; text-align: center" id="select_error" runat="server"></label>
                        </div>
                        <div></div>

                        <div id="show_borrow_msg" runat="server" visible="false">
                            <div class="portlet-title">
                                <div class="caption font-green bold">
                                    <span class="am-icon-code">填写借出表单</span>
                                </div>
                            </div>

                            <div class="am-form-group">
                                <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>借出人 / Lender</label>
                                <div class="am-u-sm-9">
                                    <input type="text" id="Lender" placeholder="输入借出人 / Lender" runat="server">
                                </div>
                            </div>

                            <div class="am-form-group">
                                <label for="user-QQ" class="am-u-sm-3 am-form-label">借出原因 / Lending Reasons</label>
                                <div class="am-u-sm-9">
                                    <input type="text" id="Lending_Reasons" placeholder="输入借出原因 / Lending Reasons" runat="server">
                                </div>
                            </div>

                            <div class="am-form-group">
                                <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color: red">*</span>借出周期(天) / Loan Period</label>
                                <div class="am-u-sm-9">
                                    <input type="text" id="Loan_Period" onkeyup="value=value.replace(/[^\d]/g,'')" placeholder="输入借出周期 / Loan Period" runat="server">
                                </div>
                            </div>

                            <div class="am-form-group">
                                <div class="am-u-sm-9 am-u-sm-push-3">
                                    <button type="submit" class="am-btn am-btn-primary">提交数据</button>
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

    </div>
</asp:Content>
