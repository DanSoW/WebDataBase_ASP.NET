<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotExistData.aspx.cs" Inherits="WebDataBase.Error_RegisterTable.NotExistData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ошибка! Указанный регистрационный номер или паспортные данные не присутствуют в базе данных!"></asp:Label>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Вернуться на главную" />
        </p>
    </form>
</body>
</html>
