﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormRegister.aspx.cs" Inherits="WebDataBase.WebFormRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="Book_Register_Number" HeaderText="Book_Register_Number" SortExpression="Book_Register_Number" ReadOnly="True" />
                    <asp:BoundField DataField="Reader_Password_Data" HeaderText="Reader_Password_Data" SortExpression="Reader_Password_Data" ReadOnly="True" />
                    <asp:BoundField DataField="Date_Issue" HeaderText="Date_Issue" SortExpression="Date_Issue" />
                    <asp:BoundField DataField="Date_Return" HeaderText="Date_Return" SortExpression="Date_Return" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DataBaseSQLConnectionString %>" DeleteCommand="DeleteRegisterData" DeleteCommandType="StoredProcedure" InsertCommand="WriteRegisterData" InsertCommandType="StoredProcedure" SelectCommand="ReadRegisterData" SelectCommandType="StoredProcedure" UpdateCommand="UpdateRegisterData" UpdateCommandType="StoredProcedure" OnDeleting="SqlDataSource1_Deleting" OnInserting="SqlDataSource1_Inserting" OnUpdated="SqlDataSource1_Updated" OnUpdating="SqlDataSource1_Updating" OldValuesParameterFormatString="ID" OnInserted="SqlDataSource1_Inserted">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ID" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox1" Name="Book_Register_Number" PropertyName="Text" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox2" Name="Reader_Password_Data" PropertyName="Text" Type="Int64" />
                    <asp:ControlParameter ControlID="TextBox3" DbType="Date" Name="Date_Issue" PropertyName="Text" />
                    <asp:ControlParameter ControlID="TextBox4" DbType="Date" Name="Date_Return" PropertyName="Text" />
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                    <asp:Parameter Name="Book_Register_Number" Type="Int64" />
                    <asp:Parameter Name="Reader_Password_Data" Type="Int64" />
                    <asp:Parameter DbType="Date" Name="Date_Issue" />
                    <asp:Parameter DbType="Date" Name="Date_Return" />
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
             <br /><br />
        <table>
            <tr><td>Регистрационный номер</td><td>Паспортные данные</td><td>Дата выдачи</td><td>Дата возврата</td></tr>
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="Button1" runat="server" Text="Добавить" Width="82px" OnClick="Button1_Click" style="height: 29px" /></td>
            </tr>
        </table>
            <br /><br />
        <table>
            <tr>
                <td><asp:Button ID="Button2" runat="server" Text="Таблица Книг" OnClick="Button2_Click"/></td>
                <td><asp:Button ID="Button3" runat="server" Text="Таблица Читателей" OnClick="Button3_Click" /></td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
