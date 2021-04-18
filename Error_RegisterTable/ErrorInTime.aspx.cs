using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataBase.Error_RegisterTable
{
	public partial class ErrorInTime : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			Server.Transfer("..\\WebFormReader.aspx", false);
		}
	}
}