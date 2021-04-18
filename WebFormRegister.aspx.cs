using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataBase
{
	public partial class WebFormRegister : System.Web.UI.Page
	{
		public const int MAX_SIZE_DATE = 10;

		protected void Page_Load(object sender, EventArgs e)
		{

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
			if ((returnInt < 0) && (returnInt != (-3)))
			{
				Server.Transfer("Error_RegisterTable\\NotExistData.aspx", false);
				return;
			}
			else if (returnInt == (-3))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInTime.aspx", false);
				return;
			}
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
			if ((returnInt < 0) && (returnInt != (-3)))
			{
				Server.Transfer("Error_RegisterTable\\NotExistData.aspx", false);
				return;
			}
			else if (returnInt == (-3))
			{
				Server.Transfer("Error_RegisterTable\\ErrorInTime.aspx", false);
				return;
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			if ((!WebFormDefault.CheckTextBoxes(new List<TextBox>()
			{
				TextBox1, TextBox2, TextBox3, TextBox4
			})) || (!CheckOrientedData(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text))){
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}

			SqlDataSource1.Insert();
			GridView1.DataBind();
		}
	}
}