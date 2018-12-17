<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="DXRFID.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function clear()
        {
            var elem = document.getElementById("Main_key_values");
            elem.innerText = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components" style="position: absolute;height:auto;width:85%">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--用户信息管理
        </div>

        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">用户信息管理</li>
            </ol>
        </div>

        <div class="tpl-portlet-components" runat="server" id="no_show" visible="false">
            <asp:Label ID="NO_data" runat="server" Width="100%" Style="text-align: center; color: #FF0000"></asp:Label>
        </div>

        <div class="tpl-portlet-components" style="height: 100%" id="show" runat="server">
            <div class="tpl-block">
                <div class="am-g">
                    <div class="am-u-md-6">
                        <div class="am-btn-group am-btn-group-xs">
                            <button type="button" class="am-btn am-btn-default am-btn-warning" runat="server" onserverclick="Download_ServerClick"><span class="am-icon-archive"></span>下载</button>

                        </div>
                        <div class="am-btn-group am-btn-group-xs">
                            <button type="button" class="am-btn am-btn-default am-btn-danger" runat="server" onserverclick="Refrash_ServerClick"><span class="am-icon-trash-o"></span>刷新</button>
                        </div>
                    </div>
                    <div class="am-u-sm-12 am-u-md-3">
                        <select data-am-selected="{btnSize: 'sm'}" id="select_key" runat="server">
                            <option value="所有类别" selected="selected">所有类别</option>
                            <option value="工号">工号</option>
                            <option value="姓名">姓名</option>
                            <option value="部门">部门</option>
                        </select>
                    </div>
                    <div>
                        <div class="am-input-group am-input-group-sm">
                            <input type="text" id="key_values" placeholder="输入查询关键字" runat="server" />
                            <span class="am-input-group-btn">
                                <button class="am-btn  am-btn-default am-btn-success tpl-am-btn-success am-icon-search" type="button" runat="server" onserverclick="Key_ServerClick"></button>
                            </span>
                        </div>
                    </div>
                </div>
                <br />
                <div class="am-g">
                    <div class="am-u-sm-12" id="show_table">
                        <dx:ASPxGridView ID="ASPxGridView_show" runat="server" AutoGenerateColumns="False" Font-Names="微软雅黑" Theme="iOS" DataSourceID="SqlDataSource_show" Width="100%" KeyFieldName="工号" OnRowUpdating="ASPxGridView_show_RowUpdating">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>


                            <SettingsDataSecurity AllowInsert="False" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="工号" Name="工号" VisibleIndex="1">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="姓名" Name="姓名" VisibleIndex="2">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="部门" Name="部门" VisibleIndex="3">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="邮箱" Name="邮箱" VisibleIndex="4">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="权限" Name="权限" VisibleIndex="5">
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="ASPxComboBox_premission" runat="server" EnableTheming="True" SelectedIndex="1" Theme="iOS">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="普通用户" Value="Normal" />
                                                <dx:ListEditItem Text="管理员" Value="Root" />
                                                <dx:ListEditItem Text="超级管理员" Value="SuperRoot" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:SqlDataSource ID="SqlDataSource_show" runat="server" ConnectionString="<%$ ConnectionStrings:RFIDConnectionString %>" DeleteCommand="DELETE FROM [dbo].[userinfo] WHERE opid=@工号" UpdateCommand="UPDATE [dbo].[userinfo] SET [function] = @部门 ,[premission] = @权限,e_mail=@邮箱 WHERE opid=@工号">
                            <DeleteParameters>
                                <asp:Parameter Name="工号"></asp:Parameter>
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="工号" />
                                <asp:Parameter Name="部门" />
                                <asp:Parameter Name="权限" />
                                <asp:Parameter Name="邮箱" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>

                </div>
            </div>
            <div class="tpl-alert"></div>
        </div>
    </div>
</asp:Content>
