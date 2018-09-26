<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Pandian_Admin.aspx.cs" Inherits="DXRFID.Pandian_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components" style="position:absolute;height:100%;width:90%">
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--盘点信息维护
        </div>

        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">盘点信息维护</li>
            </ol>
        </div>

        <div class="tpl-portlet-components" runat="server" id="no_show" visible="false">
            <asp:Label ID="NO_data" runat="server" Width="100%" Style="text-align: center; color: #FF0000"></asp:Label>
        </div>

        <div class="tpl-portlet-components" style="height: 90%">
            <div class="tpl-block" runat="server" id="show">
                <div class="am-g">
                    <div class="am-u-sm-12 am-u-md-9">
                        <button type="button" class="am-btn am-btn-primary" runat="server" onserverclick="Submit_ServerClick">清除所有绑定数据</button>
                        <label style="color: red;" id="tijiao_tishi" runat="server"></label>
                    </div>
                    <br />
                    <div class="am-g">
                        <div class="am-u-sm-12">
                            <dx:ASPxGridView ID="ASPxGridView_show" runat="server" AutoGenerateColumns="False" Font-Names="微软雅黑" Theme="iOS" DataSourceID="SqlDataSource_relieve" KeyFieldName="区域RFID标记" Width="100%" OnRowInserting="ASPxGridView_show_RowInserting">
                                <SettingsAdaptivity>
                                    <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                                </SettingsAdaptivity>

                                <SettingsDataSecurity AllowDelete="False" />

                                <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                                <Columns>
                                    <dx:GridViewCommandColumn ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Name="操作">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="区域RFID标记" VisibleIndex="1">
                                        <HeaderStyle ForeColor="#999999" />
                                        <CellStyle ForeColor="#999999">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="区域名称" VisibleIndex="2">
                                        <EditItemTemplate>
                                            <dx:ASPxComboBox ID="ASPxComboBox_StoragePlace" runat="server" DataSourceID="SqlDataSource_StoragePlace" Theme="iOS" NullText="请选择区域名称" ValueField="StoragePlace" TextField="StoragePlace" AutoPostBack="false" ItemStyle-ForeColor="Gray" Width="400px"></dx:ASPxComboBox>
                                        </EditItemTemplate>
                                        <HeaderStyle ForeColor="#999999" />
                                        <CellStyle ForeColor="#999999">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="区域盘点负责人" VisibleIndex="3">
                                        <HeaderStyle ForeColor="#999999" />
                                        <CellStyle ForeColor="#999999">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn FieldName="预计盘点开始时间" VisibleIndex="4" PropertiesDateEdit-Width="400px" PropertiesDateEdit-CalendarProperties-DayStyle-ForeColor="Gray">
                                        <PropertiesDateEdit Width="400px">
                                            <CalendarProperties>
                                                <DayStyle ForeColor="Gray"></DayStyle>
                                            </CalendarProperties>
                                        </PropertiesDateEdit>

                                        <HeaderStyle ForeColor="#999999" />
                                        <CellStyle ForeColor="#999999">
                                        </CellStyle>
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                                <Styles>
                                    <Header Border-BorderStyle="None">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                            <asp:SqlDataSource ID="SqlDataSource_relieve" runat="server" ConnectionString="<%$ ConnectionStrings:RFIDConnectionString %>" SelectCommand="SELECT [StoragePlace_RFID] 区域RFID标记, [StoragePlace_Name] 区域名称, [TakeStock_Leader] 区域盘点负责人, [TakeStockStartTime] 预计盘点开始时间 FROM [TakeStock_Admin]" UpdateCommand="UPDATE [dbo].[TakeStock_Admin]
                                               SET [StoragePlace_Name] = @区域名称
                                                  ,[TakeStock_Leader] = @区域盘点负责人
                                                  ,[TakeStockStartTime] = @预计盘点开始时间
                                             WHERE [StoragePlace_RFID]=@区域RFID标记;UPDATE [dbo].[EquipmentInformation]
   SET  [CurrentTakeStock_Person] = '无'
 WHERE [StoragePlace] = @区域名称"
                                InsertCommand="INSERT INTO [dbo].[TakeStock_Admin] ([StoragePlace_RFID],[StoragePlace_Name],[TakeStock_Leader],[TakeStockStartTime]) VALUES(@区域RFID标记,@区域名称,@区域盘点负责人,@预计盘点开始时间);UPDATE [dbo].[EquipmentInformation]
   SET  [CurrentTakeStock_Person] = '无'
 WHERE [StoragePlace] = @区域名称">
                                <InsertParameters>
                                    <asp:Parameter Name="区域RFID标记" />
                                    <asp:Parameter Name="区域名称" />
                                    <asp:Parameter Name="区域盘点负责人" />
                                    <asp:Parameter Name="预计盘点开始时间" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="区域名称" />
                                    <asp:Parameter Name="区域盘点负责人" />
                                    <asp:Parameter Name="预计盘点开始时间" />
                                    <asp:Parameter Name="区域RFID标记" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource_StoragePlace" runat="server" SelectCommand="SELECT distinct [StoragePlace] FROM [RFID].[dbo].[EquipmentInformation] order by [StoragePlace]" ConnectionString='<%$ ConnectionStrings:RFIDConnectionString %>'></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
