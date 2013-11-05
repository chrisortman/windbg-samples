using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace HealthCareDotGov.Web
{
    public partial class Quote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //calDOB.SelectedDate = new DateTime(1980,1,1);
                lstBirthYear.DataSource = Enumerable.Range(1960, 60);
                lstBirthYear.DataBind();
            }
        }

        protected void GetQuote(object sender, EventArgs e)
        {
            var client = ChannelFactory<IInsuranceQuoteService>.CreateChannel(
                new BasicHttpBinding(), new EndpointAddress("http://localhost:8088/ins"));
            BeginCall:
            try
            {
                var findPolicyRequest = new FindPolicyRequest()
                {
                    PersonSSN = txtSSN.Text,
                    DateOfBirth = new DateTime(Convert.ToInt32(lstBirthYear.SelectedValue), 1, 1),
                    Gender = "M",
                    PolicyType = lstPolicyType.SelectedItem.Text,
                };
                var policies = client.FindPolicies(findPolicyRequest);
                resultsGrid.DataSource = policies;
                resultsGrid.DataBind();
                ResultsMsg.InnerText = String.Format("Showing {0} results for age {1} years and policy type {2}",
                    policies.Length,
                    DateTime.Now.Year - findPolicyRequest.DateOfBirth.Year, findPolicyRequest.PolicyType);
            }
            catch (Exception ex)
            {
                //this service was buggy during testing, retry command on failure
                goto BeginCall;
            }
        }
    }
}