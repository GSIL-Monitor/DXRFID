<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Return_Handle.aspx.cs" Inherits="DXRFID.Return_Handle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--归还操作
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">归还操作</li>
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
                        <span class="am-icon-code"></span>借出信息查询
                    </div>
                </div>
                <div class="am-u-sm-12 am-u-md-9">
                    <div class="am-form am-form-horizontal">

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color:red">*</span>RFID / Radio Frequency Identification Devices</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="RFID" placeholder="输入RFID / Radio Frequency Identification Devices" runat="server" autofocus="autofocus" ><a href="#" runat="server" visible="false" id="Show_pic">查看设备详情</a>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-name" class="am-u-sm-3 am-form-label">资产编号 / Asset Number</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Asset_Number" readonly="readonly" runat="server" style="cursor:pointer"/>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-name" class="am-u-sm-3 am-form-label">名称 / Equipment Name</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="EquipmentName" readonly="readonly" runat="server" style="cursor:pointer"/>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-email" class="am-u-sm-3 am-form-label">数量 / Quantity</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Quantity" readonly="readonly" runat="server" style="cursor:pointer"/>
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-phone" class="am-u-sm-3 am-form-label">规格 / Specification</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Specification" readonly="readonly" runat="server" style="cursor:pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">品牌 / Brand</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Brand" readonly="readonly" runat="server" style="cursor:pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">借出人 / Lender</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Lender" readonly="readonly" runat="server" style="cursor:pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">借出时间 / Borrow DateTime</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Lending_Time" runat="server" readonly="readonly" style="cursor:pointer" />
                            </div>
                        </div>

                        <div class="am-form-group">
                            <label for="user-QQ" class="am-u-sm-3 am-form-label">借出周期(天) / Loan Period</label>
                            <div class="am-u-sm-9">
                                <input type="text" id="Loan_Period" runat="server" readonly="readonly" style="cursor:pointer" />

                            </div>
                        </div>

                        <div>
                            <label style="color: red; width: 100%; text-align: center" id="select_error" runat="server" ></label>
                        </div>
                        <div></div>

                        <div id="show_return_msg" runat="server" visible="false">
                            <div class="portlet-title">
                                <div class="caption font-green bold">
                                    <span class="am-icon-code"></span>填写归还表单
                                </div>
                            </div>

                            <div class="am-form-group">
                                <label for="user-QQ" class="am-u-sm-3 am-form-label"><span style="color:red">*</span>归还人 / Return People</label>
                                <div class="am-u-sm-9">
                                    <input type="text" id="Return_People" placeholder="输入归还人 / Return People" runat="server">
                                </div>
                            </div>

                            <div class="am-form-group">
                                <div class="am-u-sm-9 am-u-sm-push-3">
                                    <button type="submit" class="am-btn am-btn-primary" runat="server">提交数据</button>
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
