using Realtime_Test.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Realtime_Test.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {


            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CategoryConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [ID],[Name],[Description],[Alias] FROM [dbo].[Category] ", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listCate = reader.Cast<IDataRecord>()
                            .Select(x => new
                            {
                               // ID = (int)x["ID"],                   
                                Name = x["Name"],
                                Description= x["Description"] ,
                                Alias=  x["Alias"]
                            }).ToList();


                    return Json(new { listCate = listCate }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            CateHub.Show();
        }
    }
}