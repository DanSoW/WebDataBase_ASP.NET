using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataBase
{
	public partial class WebFormDefault : System.Web.UI.Page
	{
		public const int MAX_SIZE_PASS = 10;
		public const int MAX_SIZE_HOME_ADDR = 80;
		public const int MAX_SIZE_FIO = 50;

		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void SqlDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] param = new DbParameter[2]{
				e.Command.Parameters["@Password_Data"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			param[0].ParameterName = "@Password_Data";
			param[1].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < param.Length; i++)
				e.Command.Parameters.Add(param[i]);
		}

		protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] deleteParam = new DbParameter[3]{
				e.Command.Parameters["@Password_Data"],
				e.Command.Parameters["@Home_Address"],
				e.Command.Parameters["@Full_Name"]
			};

			deleteParam[0].ParameterName = "@Password_Data";
			deleteParam[1].ParameterName = "@Home_Address";
			deleteParam[2].ParameterName = "@Full_Name";

			e.Command.Parameters.Clear();
			for(int i = 0; i < deleteParam.Length; i++)
				e.Command.Parameters.Add(deleteParam[i]);
		}

		protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] param = new DbParameter[4]{
				e.Command.Parameters["@Password_Data"],
				e.Command.Parameters["@Home_Address"],
				e.Command.Parameters["@Full_Name"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			param[0].ParameterName = "@Password_Data";
			param[1].ParameterName = "@Home_Address";
			param[2].ParameterName = "@Full_Name";
			param[3].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < param.Length; i++)
				e.Command.Parameters.Add(param[i]);
		}

		private bool CheckOrientedData(String pass, String home, String fio)
		{
			if ((home.Length == 0)
				|| (fio.Length == 0)
				|| (home.Trim(' ').Length == 0)
				|| (fio.Trim(' ').Length == 0)
				|| (home.Length > MAX_SIZE_HOME_ADDR)
				|| (fio.Length > MAX_SIZE_FIO)
				|| (!passwordDataValidate(pass)))
				return false;
			try
			{
				long.Parse(pass);
			}
			catch (Exception) { return false; }

			return true;
		}

		public static bool CheckTextBoxes(List<TextBox> txb)
		{
			if (txb.Count == 0)
				return false;

			foreach (var i in txb)
			{
				if (i.Text.Length == 0)
					return false;
			}

			return true;
		}

		public static bool passwordDataValidate(String psw)
		{
			if ((psw.Length != MAX_SIZE_PASS) || (psw[0] == '0'))
				return false;
			bool flag = true;
			foreach (var i in psw)
				if (!Char.IsDigit(i))
				{
					flag = false;
					break;
				}

			return flag;
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			if((!CheckTextBoxes(new List<TextBox>() { this.TextBox1,
			this.TextBox2, this.TextBox3}))
			|| (!CheckOrientedData(this.TextBox1.Text, this.TextBox2.Text, this.TextBox3.Text)))
			{
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}

			SqlDataSource1.Insert();
			GridView1.DataBind();
		}

		protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			if(Int32.Parse(returnValue.Value.ToString()) < 0)
			{
				Server.Transfer("Error_ReaderTable\\ExistThisRegister.aspx", false);
				return;
			}
		}

		protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			if (Int32.Parse(returnValue.Value.ToString()) < 0)
			{
				Server.Transfer("Error_ReaderTable\\ExistInRegistration.aspx", false);
				return;
			}
		}

		protected void Button2_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormBook.aspx", false);
		}

		protected void Button3_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormRegister.aspx", false);
		}
	}
}