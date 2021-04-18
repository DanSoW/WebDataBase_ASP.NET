using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataBase
{
	public partial class WebFormBook : System.Web.UI.Page
	{
		private const int MAX_SIZE_REGNUM = 10;
		private const int MAX_SIZE_PAGE = 10;
		private const int MAX_SIZE_SECTION = 150;
		private const int MAX_SIZE_YEAR = 4;

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		private bool CheckOrientedData(String reg, String pages, String year, String section)
		{
			if ((pages.Length == 0)
				|| (year.Trim(' ').Length == 0)
				|| (section.Trim(' ').Length == 0)
				|| (pages.Length > MAX_SIZE_PAGE)
				|| (year.Length > MAX_SIZE_YEAR)
				|| (section.Length > MAX_SIZE_SECTION)
				|| (!registerNumberValidate(reg)))
				return false;
			try
			{
				long.Parse(reg);
				if((short.Parse(pages) <= 0)
					|| (short.Parse(year) <= 0))
				{
					return false;
				}
			}
			catch (Exception) { return false; }

			return true;
		}

		public static bool registerNumberValidate(String psw)
		{
			if ((psw.Length != MAX_SIZE_REGNUM) || (psw[0] == '0'))
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


		protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] deleteParam = new DbParameter[5]{
				e.Command.Parameters["@Register_Number"],
				e.Command.Parameters["@Count_Pages"],
				e.Command.Parameters["@Year_Publishing"],
				e.Command.Parameters["@Section"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			deleteParam[0].ParameterName = "@Register_Number";
			deleteParam[1].ParameterName = "@Count_Pages";
			deleteParam[2].ParameterName = "@Year_Publishing";
			deleteParam[3].ParameterName = "@Section";
			deleteParam[4].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < deleteParam.Length; i++)
				e.Command.Parameters.Add(deleteParam[i]);
		}

		protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			int returnInt = Int32.Parse(returnValue.Value.ToString());
			if (returnInt == (-3))
			{
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}else if(returnInt < 0)
			{
				Server.Transfer("Error_BookTable\\ErrorUpdateTime.aspx", false);
				return;
			}
		}

		protected void SqlDataSource1_Deleting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] deleteParam = new DbParameter[2]{
				e.Command.Parameters["@Register_Number"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			deleteParam[0].ParameterName = "@Register_Number";
			deleteParam[1].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < deleteParam.Length; i++)
				e.Command.Parameters.Add(deleteParam[i]);
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

		protected void Button1_Click(object sender, EventArgs e)
		{
			if((!WebFormDefault.CheckTextBoxes(new List<TextBox>()
			{
				TextBox1, TextBox2, TextBox3, TextBox4
			})) || (!CheckOrientedData(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text))){
				Server.Transfer("Error_ReaderTable\\NotCorrectInputData.aspx", false);
				return;
			}

			SqlDataSource1.Insert();
			GridView1.DataBind();
		}

		protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
		{
			DbParameter returnValue = e.Command.Parameters["@RETURN_VALUE"];
			int returnInt = Int32.Parse(returnValue.Value.ToString());

			if (returnInt == (-1))
			{
				Server.Transfer("Error_ReaderTable\\ExistInRegistration.aspx", false);
				return;
			}else if(returnInt == (-2))
			{
				Server.Transfer("Error_BookTable\\ErrorUpdateTime.aspx", false);
				return;
			}
		}

		protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e)
		{
			DbParameter[] deleteParam = new DbParameter[5]{
				e.Command.Parameters["@Register_Number"],
				e.Command.Parameters["@Count_Pages"],
				e.Command.Parameters["@Year_Publishing"],
				e.Command.Parameters["@Section"],
				e.Command.Parameters["@RETURN_VALUE"]
			};

			deleteParam[0].ParameterName = "@Register_Number";
			deleteParam[1].ParameterName = "@Count_Pages";
			deleteParam[2].ParameterName = "@Year_Publishing";
			deleteParam[3].ParameterName = "@Section";
			deleteParam[4].ParameterName = "@RETURN_VALUE";

			e.Command.Parameters.Clear();
			for (int i = 0; i < deleteParam.Length; i++)
				e.Command.Parameters.Add(deleteParam[i]);
		}

		protected void Button2_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormReader.aspx", false);
		}

		protected void Button3_Click(object sender, EventArgs e)
		{
			Server.Transfer("WebFormRegister.aspx", false);
		}
	}
}