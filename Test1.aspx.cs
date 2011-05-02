using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BingMapTest_Test1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ClientScript.IsStartupScriptRegistered("alert"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "function SetSize() {var zmLevel = parseInt(document.getElementById('" + TextBox1.ClientID + "').value);alert(zmLevel);map.setView({ zoom:zmLevel });}", true);
        }
    }

    private void setZoom(String newZoom)
    {
        /*string script = "<script type=\"text/javascript\">";
        script += "SetZoom(";
        script += newZoom;
        script += ");";
        script += "</script>";
        if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "setzoom_script"))
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "setzoom_script", script);*/

        if (!ClientScript.IsClientScriptBlockRegistered("setSize"))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                "setSize", "SetZoom();", true);
        }
    }


    public void Button1_Click(object sender, EventArgs e)
    {
        setZoom(TextBox1.Text);
    }

}