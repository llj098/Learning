using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

public partial class MD5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = "FetionCmd(llj)";

        var v1 = FormsAuthentication.HashPasswordForStoringInConfigFile(content,"md5");
        var b1 = System.Text.Encoding.UTF8.GetBytes(v1);

        Response.Write((v1) + "<br />");
        Response.Write(BitConverter.ToString(b1) + "<br />");
        Response.Write(Convert.ToBase64String(b1) + "<br />");

        var md5 = new MD5CryptoServiceProvider();
        var md5bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
		 
        Response.Write(BitConverter.ToString(md5bytes) + "<br />");
        foreach (var item in md5bytes)
        {
            Response.Write(item.ToString()+":"+item.ToString("x")+",");
        }
        Response.Write("<br />"+Convert.ToBase64String(md5bytes) + "<br />");
    }
}
