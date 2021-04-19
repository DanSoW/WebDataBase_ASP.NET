<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormBook.aspx.cs" Inherits="WebDataBase.WebFormBook" %>

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
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Register_Number" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Register_Number" HeaderText="Регистрационный номер" ReadOnly="True" SortExpression="Register_Number" />
                    <asp:BoundField DataField="Count_Pages" HeaderText="Количество страниц" SortExpression="Count_Pages" />
                    <asp:BoundField DataField="Year_Publishing" HeaderText="Год публикации" SortExpression="Year_Publishing" />
                    <asp:BoundField DataField="Section" HeaderText="Раздел" SortExpression="Section" />
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DataBaseSQLConnectionString %>" DeleteCommand="DeleteBookData" DeleteCommandType="StoredProcedure" InsertCommand="WriteBookData" InsertCommandType="StoredProcedure" SelectCommand="ReadBookData" SelectCommandType="StoredProcedure" UpdateCommand="UpdateBookData" UpdateCommandType="StoredProcedure" OnDeleted="SqlDataSource1_Deleted" OnDeleting="SqlDataSource1_Deleting" OnInserted="SqlDataSource1_Inserted" OnInserting="SqlDataSource1_Inserting" OnUpdated="SqlDataSource1_Updated" OnUpdating="SqlDataSource1_Updating" OldValuesParameterFormatString="Register_Number">
                <DeleteParameters>
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                    <asp:Parameter Name="Register_Number" Type="Int64" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="RETURN_VALUE" Type="Int32" Direction="ReturnValue" />
                    <asp:ControlParameter ControlID="TextBox1" Name="Register_Number" PropertyName="Text" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox2" Name="Count_Pages" PropertyName="Text" Type="Int16" />
                    <asp:ControlParameter ControlID="TextBox3" Name="Year_Publishing" PropertyName="Text" Type="Int16" />
                    <asp:ControlParameter ControlID="TextBox4" Name="Section" PropertyName="Text" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:ControlParameter ControlID="TextBox1" Name="Register_Number" PropertyName="Text" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox2" Name="Count_Pages" PropertyName="Text" Type="Int16" />
                    <asp:ControlParameter ControlID="TextBox3" Name="Year_Publishing" PropertyName="Text" Type="Int16" />
                    <asp:ControlParameter ControlID="TextBox4" Name="Section" PropertyName="Text" Type="String" />
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

           <br /><br />
        <table>
            <tr><td>Регистрационный номер</td><td>Количество страниц</td><td>Год публикации</td><td>Раздел</td></tr>
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server" OnKeyPress="EnsureNumeric()"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox2" runat="server" OnKeyPress="EnsureNumeric()"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox3" runat="server" OnKeyPress="EnsureNumeric()"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="Button1" runat="server" Text="Добавить" Width="82px" OnClick="Button1_Click" />&nbsp;
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Правка" />
                </td>
            </tr>
        </table>
            <br /><br />
        <table>
            <tr>
                <td><asp:Button ID="Button2" runat="server" Text="Таблица Читателей" OnClick="Button2_Click" /></td>
                <td><asp:Button ID="Button3" runat="server" Text="Таблица Регистрации" OnClick="Button3_Click" /></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
