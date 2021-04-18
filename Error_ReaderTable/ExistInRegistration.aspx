<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExistInRegistration.aspx.cs" Inherits="WebDataBase.Error_ReaderTable.ExistInRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Ошибка! Данная запись уже зарегистрирована в таблице регистрации и для её удаления необходимо удалить её в таблице регистрации!"></asp:Label>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Вернуться на главную" OnClick="Button1_Click" />
        </p>
    </form>
</body>
</html>
