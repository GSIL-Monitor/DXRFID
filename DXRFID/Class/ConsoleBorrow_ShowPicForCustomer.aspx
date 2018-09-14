<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsoleBorrow_ShowPicForCustomer.aspx.cs" Inherits="DXRFID.Class.ConsoleBorrow_ShowPicForCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OFT RFID 设备管理系统</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="Image_show" runat="server" /><br />
            <asp:Label ID="Label_name" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
