<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormReader.aspx.cs" Inherits="WebDataBase.WebFormDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script> 
        function EnsureNumeric()
        {
            var key = window.event.keyCode; 
            if ((key < 48) || (key > 57)) 
                window.event.returnValue = false; 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" aria-live="off">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Password_Data" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Password_Data" HeaderText="Паспортные данные" ReadOnly="True" SortExpression="Password_Data" />
                    <asp:BoundField DataField="Home_Address" HeaderText="Домашний адрес" SortExpression="Home_Address" />
                    <asp:BoundField DataField="Full_Name" HeaderText="ФИО" SortExpression="Full_Name" />
                    <asp:CommandField ShowEditButton="True"  />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DataBaseSQLConnectionString %>" DeleteCommand="DeleteReaderData" DeleteCommandType="StoredProcedure" InsertCommand="WriteReaderData" InsertCommandType="StoredProcedure" SelectCommand="ReadReaderData" SelectCommandType="StoredProcedure" UpdateCommand="UpdateReaderData" UpdateCommandType="StoredProcedure" OldValuesParameterFormatString="Password_Data" OnDeleting="SqlDataSource1_Deleting" OnUpdating="SqlDataSource1_Updating" OnInserting="SqlDataSource1_Inserting" OnDeleted="SqlDataSource1_Deleted" OnInserted="SqlDataSource1_Inserted">
                <DeleteParameters>
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                    <asp:Parameter Name="Password_Data" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:ControlParameter ControlID="TextBox1" Name="Password_Data" PropertyName="Text" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox2" Name="Home_Address" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="TextBox3" Name="Full_Name" PropertyName="Text" Type="String" />
                    <asp:Parameter Name="RETURN_VALUE" Type="Int32" Direction="ReturnValue" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Password_Data" Type="Int64" />
                    <asp:Parameter Name="Home_Address" Type="String" />
                    <asp:Parameter Name="Full_Name" Type="String" />
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br /><br />
        <table>
            <tr><td>Паспортные данные</td><td>Домашний адрес</td><td>ФИО</td></tr>
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server" OnKeyPress="EnsureNumeric()"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="Button1" runat="server" Text="Добавить" Width="82px" OnClick="Button1_Click" />&nbsp; </td>
            </tr>
        </table>
        <br /><br />
        <table>
            <tr>
                <td><asp:Button ID="Button2" runat="server" Text="Таблица Книг" OnClick="Button2_Click" /></td>
                <td><asp:Button ID="Button3" runat="server" Text="Таблица Регистрации" OnClick="Button3_Click" /></td>
            </tr>
        </table>
            <br />
        </div>
    </form>
</body>
</html>
