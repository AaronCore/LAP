using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using LAP.HttpClient.Enum;

namespace WebForm.Example
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = HttpContext.Current;
                Task.Run(async () => await MyLogger.AddStatisticLog(context, StatisticAction.页面访问));
            }
        }
    }
}