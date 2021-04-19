using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataBase
{
	public partial class WebFormRegister : System.Web.UI.Page
	{
		public const int MAX_SIZE_DATE = 10;
		static HttpCookie httpCookie = new HttpCookie("IDCookie", (-1).ToString());

		protected void Page_Load(object sender, EventArgs e)
		{
			Request.Cookies.Add(httpCookie);
		}

		private bool CheckOrientedData(String reg, String pwd, String dateIssue, String dateReturn)
		{
			if ((!WebFormDefault.passwordDataValidate(pwd))
				|| (!WebFormBook.registerNumberValidate(reg))
				|| (!checkDateEntry(dateIssue))
				|| (!checkDateEntry(dateReturn)))
			{
				return false;
			}

			try
			{
				long.Parse(reg);
				long.Parse(pwd);
			}
			catch (Exception) { return false; }

			return true;
		}

		private bool checkDateEntry(String date)
		{
			if (date.Length != MAX_SIZE_DATE)
				return false;

			if (date.Count((char i) => (i == '.')) != 2)
			{
				return false;
			}

			foreach (var i in date)
				if ((!Char.IsDigit(i)) && (i != '.'))
					return false;

			string[] dateSplit = date.Split(new char[] { '.' });
			if ((dateSplit[2].Length != 4) || (dateSplit[1].Length != 2) || (dateSplit[0].Length != 2))
				return false;

			try
			{
				int value = int.Parse(dateSplit[0]);
				if ((value <= 0) || (value > 31))
					return false;
				value = int.Parse(dateSplit[1]);
				if ((value <= 0) || (value > 12))
					return false;
				value = int.Parse(dateSplit[2]);
				if ((value <= 0) || (value > 9999))
					return false;
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		protected void Button3_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormReader.aspx", false);
		}

		protected void Button2_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormBook.aspx", false);
		}

		protected void SqlDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] param = new DbParameter[2]{
				e.Command.Parameters["@ID"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			param[0].ParameterName = "@ID";
			param[1].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < param.Length; i++)
				e.Command.Parameters.Add(param[i]);
		}

		protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] param = new DbParameter[6]{
				e.Command.Parameters["@ID"],
				e.Command.Parameters["@Book_Register_Number"],
				e.Command.Parameters["@Reader_Password_Data"],
				e.Command.Parameters["@Date_Issue"],
				e.Command.Parameters["@Date_Return"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			param[0].ParameterName = "@ID";
			param[1].ParameterName = "@Book_Register_Number";
			param[1].Direction = System.Data.ParameterDirection.Input;
			param[1].DbType = System.Data.DbType.Int64;
			param[1].Value = Int64.Parse(GridView1.Rows[Int32.Parse(param[0].Value.ToString())-1].Cells[1].Text);

			param[2].ParameterName = "@Reader_Password_Data";
			param[2].Direction = System.Data.ParameterDirection.Input;
			param[2].DbType = System.Data.DbType.Int64;
			param[2].Value = Int64.Parse(GridView1.Rows[Int32.Parse(param[0].Value.ToString()) - 1].Cells[2].Text);

			param[3].ParameterName = "@Date_Issue";
			param[4].ParameterName = "@Date_Return";
			param[5].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < param.Length; i++)
				e.Command.Parameters.Add(param[i]);
		}

		protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			int returnInt = Int32.Parse(returnValue.Value.ToString());
			if ((returnInt < 0) && (returnInt != (-3)) && (returnInt != (-5)))
			{
				Server.Transfer("Error_RegisterTable\\NotExistData.aspx", false);
				return;
			}
			else if (returnInt == (-3))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInTime.aspx", false);
				return;
			}else if(returnInt == (-5))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInConflictDate.aspx", false);
				return;
			}

			httpCookie.Value = (-1).ToString();
		}

		protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] param = new DbParameter[6]{
				e.Command.Parameters["@ID"],
				e.Command.Parameters["@Book_Register_Number"],
				e.Command.Parameters["@Reader_Password_Data"],
				e.Command.Parameters["@Date_Issue"],
				e.Command.Parameters["@Date_Return"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			param[0].ParameterName = "@ID";
			param[0].DbType = System.Data.DbType.Int64;
			param[0].Direction = System.Data.ParameterDirection.Input;
			if(GridView1.Rows.Count <= 0)
			{
				param[0].Value = 1;
			}
			else
			{
				param[0].Value = (Int64.Parse(GridView1.Rows[GridView1.Rows.Count - 1].Cells[0].Text) + 1);
			}

			param[1].ParameterName = "@Book_Register_Number";
			param[2].ParameterName = "@Reader_Password_Data";
			param[3].ParameterName = "@Date_Issue";
			param[4].ParameterName = "@Date_Return";
			param[5].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < param.Length; i++)
				e.Command.Parameters.Add(param[i]);
		}

		protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			int returnInt = Int32.Parse(returnValue.Value.ToString());
			if ((returnInt < 0) && (returnInt != (-3)) && (returnInt != (-5)))
			{
				Server.Transfer("Error_RegisterTable\\NotExistData.aspx", false);
				return;
			}
			else if (returnInt == (-3))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInTime.aspx", false);
				return;
			}
			else if (returnInt == (-5))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInConflictDate.aspx", false);
				return;
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			TextBox3.Text = TextBox3.Text.Trim();
			TextBox4.Text = TextBox4.Text.Trim();

			string reg = DropDownList1.SelectedValue.Trim();
			string pas = DropDownList2.SelectedValue.Trim();

			if ((!CheckOrientedData(reg, pas, TextBox3.Text, TextBox4.Text))){
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}

			SqlDataSource1.Insert();
			GridView1.DataBind();
		}

		private int numberBorrowedBooks(string passwordNumber)
		{
			int count = 0;
			for (int i = 0; i < GridView1.Rows.Count; i++)
			{
				if (GridView1.Rows[i].Cells[2].Text.Equals(passwordNumber))
					count++;
			}

			return count;
		}

		int compare(DataElement o1, DataElement o2)
		{
			string[] dateString = o1.dataIssue.Split(new char[] { '.' });
			DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));

			dateString = o2.dataIssue.Split(new char[] { '.' });
			DateTime date2 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));

			return date1.CompareTo(date2);
		}

		protected void Button4_Click(object sender, EventArgs e)
		{
			TextBox5.Text = TextBox5.Text.Trim();
			if (TextBox5.Text.Length == 0)
			{
				return;
			}

			for(int i = 0; i < TextBox5.Text.Length; i++)
			{
				if (!Char.IsDigit(TextBox5.Text[i]))
					return;
			}

			DataTable dataTable = new DataTable();
			DataTable dataGridTable = new DataTable();
			String[] headersTable = new string[6]{
			"ФИО",
			"Паспортные данные",
			"Регистрационный номер",
			"Дата выдачи",
			"Дата возврата",
			"Количество взятых книг" };
			for(int i = 0; i < headersTable.Length-1; i++)
			{
				dataTable.Columns.Add(headersTable[i]);
			}

			for (int i = 0; i < headersTable.Length; i++)
			{
				dataGridTable.Columns.Add(headersTable[i]);
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.TaskOneT", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@monthValue", int.Parse(TextBox5.Text));
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String fullName = sqlReader.GetValue(0).ToString();
							String pwd = sqlReader.GetValue(1).ToString();
							String reg = sqlReader.GetValue(2).ToString();

							while (pwd.Length != 10)
								pwd = ("0" + pwd);

							while (reg.Length != 10)
								reg = ("0" + reg);

							dataTable.Rows.Add(fullName, pwd, reg,
								sqlReader.GetValue(3).ToString().Split(new char[] { ' ' })[0],
								sqlReader.GetValue(4).ToString().Split(new char[] { ' ' })[0]);
						}
					}
					sqlReader.Close();
				}

				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					using (SqlCommand command2 = new SqlCommand("dbo.DefineBookCounter", sqlConnection))
					{
						command2.CommandType = System.Data.CommandType.StoredProcedure;

						SqlParameter valueReturn = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
						valueReturn.Direction = ParameterDirection.ReturnValue;

						command2.Parameters.Add(valueReturn);
						command2.Parameters.AddWithValue("@Reader_Password_Data", long.Parse(dataTable.Rows[i].ItemArray[1].ToString()));
						command2.ExecuteScalar();

						dataGridTable.Rows.Add(
							dataTable.Rows[i].ItemArray[0].ToString(),
							dataTable.Rows[i].ItemArray[1].ToString(),
							dataTable.Rows[i].ItemArray[2].ToString(),
							dataTable.Rows[i].ItemArray[3].ToString(),
							dataTable.Rows[i].ItemArray[4].ToString(),
							Convert.ToString(valueReturn.Value));
					}
				}

				sqlConnection.Close();
			}

			GridView2.DataSource = dataGridTable;
			GridView2.DataBind();
		}

		protected void Button5_Click(object sender, EventArgs e)
		{
			TextBox6.Text = TextBox6.Text.Trim();
			if (TextBox6.Text.Length != WebFormDefault.MAX_SIZE_PASS)
			{
				return;
			}

			for (int i = 0; i < TextBox6.Text.Length; i++)
			{
				if (!Char.IsDigit(TextBox6.Text[i]))
					return;
			}

			List<DataElement> elements = new List<DataElement>();

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.TaskTwot", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@Reader_Password_Data", long.Parse(TextBox6.Text));
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String reg = sqlReader.GetValue(0).ToString();

							while (reg.Length != 10)
								reg = ("0" + reg);

							elements.Add(new DataElement(reg,
								sqlReader.GetValue(1).ToString().Split(new char[] { ' ' })[0],
							int.Parse(sqlReader.GetValue(2).ToString()),
							sqlReader.GetValue(3).ToString()));
						}
					}
					sqlReader.Close();
				}

				sqlConnection.Close();
			}

			elements.Sort(new Comparison<DataElement>(compare));

			DataTable dataTable = new DataTable();
			String[] headersTable = new string[]
			{
				"Регистрационный номер",
				"Дата выдачи",
				"Количество страниц",
				"Раздел"
			};

			for(int i = 0; i < headersTable.Length; i++)
			{
				dataTable.Columns.Add(headersTable[i]);
			}

			for (int i = 0; i < elements.Count; i++)
			{
				dataTable.Rows.Add(
					elements[i].register,
					elements[i].dataIssue,
					elements[i].pages,
					elements[i].section);
			}

			GridView3.DataSource = dataTable;
			GridView3.DataBind();
		}

		protected void Button6_Click(object sender, EventArgs e)
		{
			string filename = File1.PostedFile.FileName;
			if (filename.Length == 0)
				return;

			DataTable dataTable = new DataTable();
			String[] headersTable = new string[6]{
			"ФИО",
			"Паспортные данные",
			"Регистрационный номер",
			"Дата выдачи",
			"Дата возврата",
			"Количество взятых книг" };
			for (int i = 0; i < headersTable.Length; i++)
			{
				dataTable.Columns.Add(headersTable[i]);
			}

			Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(filename);
			Microsoft.Office.Interop.Excel._Worksheet excelSheet = excelBook.Sheets[1];
			Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

			int rows = excelRange.Rows.Count;
			int cols = excelRange.Columns.Count;
			List<List<string>> maping = new List<List<string>>();
			for (int i = 1; i <= rows; i++)
			{
				maping.Add(new List<string>());
				for (int j = 1; j <= cols; j++)
				{
					if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
						maping[(i - 1)].Add(excelRange.Cells[i, j].Value2.ToString());
				}
			}

			try
			{
				excelBook.Close();
				excelApp.Quit();
			}catch (Exception) { }

			for (int i = 1; i < maping.Count; i++)
			{
				dataTable.Rows.Add(
					maping[i][0],
					maping[i][1],
					maping[i][2],
					maping[i][3],
					maping[i][4],
					maping[i][5]);
			}

			GridView2.DataSource = dataTable;
			GridView2.DataBind();
			System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
		}

		protected void Button7_Click(object sender, EventArgs e)
		{
			string filename = File2.PostedFile.FileName;
			if (filename.Length == 0)
				filename = "FileData";

			if (GridView2.Rows.Count == 0)
			{
				return;
			}

			Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
			excelApp.Workbooks.Add();
			Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.ActiveSheet;

			workSheet.Cells[1, "A"] = "ФИО";
			workSheet.Cells[1, "B"] = "Паспортные данные";
			workSheet.Cells[1, "C"] = "Регистрационный номер книги";
			workSheet.Cells[1, "D"] = "Дата выдачи";
			workSheet.Cells[1, "E"] = "Дата возврата";
			workSheet.Cells[1, "F"] = "Количество выданных книг";

			for (int i = 0; i < GridView2.Rows.Count; i++)
			{
				workSheet.Cells[(i + 2), "A"] = GridView2.Rows[i].Cells[0].Text;
				workSheet.Cells[(i + 2), "B"] = GridView2.Rows[i].Cells[1].Text;
				workSheet.Cells[(i + 2), "C"] = GridView2.Rows[i].Cells[2].Text;
				workSheet.Cells[(i + 2), "D"] = GridView2.Rows[i].Cells[3].Text;
				workSheet.Cells[(i + 2), "E"] = GridView2.Rows[i].Cells[4].Text;
				workSheet.Cells[(i + 2), "F"] = GridView2.Rows[i].Cells[5].Text;
			}

			try
			{
				workSheet.SaveAs(filename);
			}
			catch (Exception) { }

			try
			{
				excelApp.Quit();
			}
			catch (Exception) { }

			System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
		}

		protected void Button8_Click(object sender, EventArgs e)
		{
			if (int.Parse(httpCookie.Value) < 0)
			{
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}

			SqlDataSource1.Update();
			GridView1.DataBind();
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList1.SelectedValue = GridView1.SelectedRow.Cells[1].Text;
			DropDownList2.SelectedValue = GridView1.SelectedRow.Cells[2].Text;
			TextBox3.Text = GridView1.SelectedRow.Cells[3].Text.Split(' ')[0];
			TextBox4.Text = GridView1.SelectedRow.Cells[4].Text.Split(' ')[0];
			httpCookie.Value = (GridView1.SelectedRow.RowIndex + 1).ToString();
		}
	}

	struct DataElement
	{
		public string register;
		public string dataIssue;
		public int pages;
		public string section;

		public DataElement(string reg, string data, int pages, string section)
		{
			this.register = reg;
			this.dataIssue = data;
			this.pages = pages;
			this.section = section;
		}

		public override string ToString()
		{
			return register + ", " + dataIssue + ", " + pages.ToString()
				+ ", " + section;
		}
	}
}