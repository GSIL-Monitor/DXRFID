﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="Borrow_Find.aspx.cs" Inherits="DXRFID.Borrow_Find" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function datetime() {
            var obj = document.getElementById("Main_select_key");
            var elem_date = document.getElementById("Main_key_values_datetime");
            var elem = document.getElementById("Main_key_values");
            if (obj.options[obj.selectedIndex].value.indexOf("时间") != -1) {
                elem_date.style.display = 'block';
                elem.style.display = 'none';
            }
            else {
                elem_date.style.display = 'none';
                elem.style.display = 'block'
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="tpl-portlet-components" style="position:absolute;width:auto;height:auto">

        <%--表单头--%>
        <div class="tpl-content-page-title">
            OFT RFID 管理系统--借出历史信息查询
        </div>
        <div>
            <ol class="am-breadcrumb">
                <li><a href="Default.aspx" class="am-icon-home">首页</a></li>
                <li class="am-active">借出历史信息查询</li>
            </ol>
        </div>

        <div class="tpl-portlet-components" style="height:100%;width:100%">
            <div class="tpl-block">
                <div class="am-g" style="width:1400px">
                    <div class="am-u-md-6">
                        <div class="am-btn-group am-btn-group-xs">
                            <button type="button" class="am-btn am-btn-default am-btn-warning" runat="server" onserverclick="Download_ServerClick"><span class="am-icon-archive"></span>下载</button>

                        </div>
                        <div class="am-btn-group am-btn-group-xs">
                            <button type="button" class="am-btn am-btn-default am-btn-danger" runat="server" onserverclick="Refrash_ServerClick"><span class="am-icon-trash-o"></span>刷新</button>
                        </div>
                    </div>
                    <div class="am-u-sm-12 am-u-md-3">
                        <div class="am-form-group">
                            <select data-am-selected="{btnSize: 'sm'}" id="select_key" runat="server" onchange="datetime();">
                                <option value="所有类别" selected="selected">所有类别</option>
                                <option value="资产编码">资产编码</option>
                                <option value="RFID">RFID</option>
                                <option value="名称">名称</option>
                                <option value="规格">规格</option>
                                <option value="品牌">品牌</option>
                                <option value="保管人">保管人</option>
                                <option value="存放地点">存放地点</option>
                                <option value="借出人">借出人</option>
                                <option value="借出时间">借出时间</option>
                                <option value="借出周期">借出周期（天）</option>
                                <option value="借出操作人">借出操作人</option>
                            </select>
                        </div>
                    </div>
                    <div class="am-u-sm-12 am-u-md-3">
                        <div class="am-input-group am-input-group-sm">
                             <input type="text"  id="key_values_datetime" placeholder="选择查询日期" runat="server" onclick="WdatePicker({ skin: 'whyGreen', dateFmt: 'yyyy-MM-dd' });" style="display:none" />
                            <input type="text"  id="key_values" placeholder="输入查询关键字" runat="server"  />
                            <span class="am-input-group-btn">
                                <button class="am-btn  am-btn-default am-btn-success tpl-am-btn-success am-icon-search" type="button" runat="server" onserverclick="Key_ServerClick"></button>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="am-g">
                    <div class="am-u-sm-12">
                        <dx:ASPxGridView ID="ASPxGridView_show" runat="server" AutoGenerateColumns="False" Font-Names="微软雅黑" Theme="iOS" DataSourceID="SqlDataSource_show" Width="100%" OnCustomColumnDisplayText="ASPxGridView_show_CustomColumnDisplayText">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>


                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="资产编码" Name="资产编码" VisibleIndex="0">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RFID" Name="RFID" VisibleIndex="1">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="名称" Name="名称" VisibleIndex="2">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="规格" Name="规格" VisibleIndex="3">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="品牌" Name="品牌" VisibleIndex="4">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="数量" Name="数量" VisibleIndex="5">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="保管人" Name="保管人" VisibleIndex="6">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="存放地点" Name="存放地点" VisibleIndex="7">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="借出时间" Name="借出时间" VisibleIndex="9">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="借出人" Name="借出人" VisibleIndex="8">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="借出周期" Name="借出周期" VisibleIndex="10">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="借出原因" Name="借出原因" VisibleIndex="11">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="借出操作人" Name="借出操作人" VisibleIndex="12">
                                    <HeaderStyle ForeColor="#999999" />
                                    <CellStyle ForeColor="#999999">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <Header Border-BorderStyle="None">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                        <asp:SqlDataSource ID="SqlDataSource_show" runat="server" ConnectionString="<%$ ConnectionStrings:RFIDConnectionString %>" SelectCommand="SELECT [AssetNumber] 资产编码,a.[RFID],[EquipmentName] 名称,[Specification] 规格,[Brand] 品牌,[Quantity] 数量,[Keeper] 保管人,[StoragePlace] 存放地点,b.[Borrow_Person] 借出人,
b.[Borrow_Datetime] 借出时间,b.[Borrow_Cycle] 借出周期,b.[Borrow_Reason] 借出原因,CONCAT(b.[Operator],'(',(SELECT [name] FROM [dbo].[userinfo] WHERE opid=b.[Operator]),')') 借出操作人 FROM [dbo].[EquipmentInformation]
a inner join [dbo].[BorrowInformation] b on a.[RFID] = b.[RFID] order by RFID"></asp:SqlDataSource>
                    </div>

                </div>
            </div>
            <div class="tpl-alert"></div>
        </div>
    </div>
</asp:Content>
