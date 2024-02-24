using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aioi
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


        }

        protected override void OnPreRender(EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString().ToLower();
            if (url.Contains("/default"))
            {
                liDefault.Attributes.Add("class", "nav-item active");
            }
            else if (url.Contains("/contact"))
            {
                liContact.Attributes.Add("class", "nav-item active");
            }
            else if (url.Contains("/customer") || url.Contains("/detail"))
            {
                liCustomer.Attributes.Add("class", "nav-item active");
            }
            else if (url.Contains("/about"))
            {
                liAbout.Attributes.Add("class", "nav-item active");
            }

        }

         
    }
}