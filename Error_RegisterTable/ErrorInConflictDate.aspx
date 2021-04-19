<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorInConflictDate.aspx.cs" Inherits="WebDataBase.Error_RegisterTable.ErrorInConflictDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ошибка! Нельзя добавлять или изменять запись таким образом, чтобы дата выдачи и возврата книги пересекалась или полностью совпадала с уже зарегистрированными датами, так как книга в это время отсутствует в библиотеке!"></asp:Label>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Вернуться на главную" />
        </p>
    </form>
</body>
</html>
