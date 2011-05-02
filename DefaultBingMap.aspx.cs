using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BingMapTest_DefaultBingMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void Button1_click(object sender, EventArgs e)
    {
        this.MyAjaxMapControl1.setZoom(TextBox1.Text);
    }
}