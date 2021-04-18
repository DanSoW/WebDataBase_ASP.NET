<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorUpdateTime.aspx.cs" Inherits="WebDataBase.Error_BookTable.ErrorUpdateTime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ошибка! Дата публикации книги должна быть раньше установленного на сервере времени и не быть позднее дат, зарегистрированных в таблице регистрации!"></asp:Label>
        </div>
    </form>
</body>
</html>
