using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {

     
        // GET: Employee
        public ActionResult Index()
        {
            IgnoreBadCertificates();
            IEnumerable<MvcEmployeeModel> employeeModels;
            HttpResponseMessage responseMessage = GlobalVariables.webApiClient.GetAsync("Employees").Result;
            employeeModels = responseMessage.Content.ReadAsAsync<IEnumerable<MvcEmployeeModel>>().Result;
            return View(employeeModels);
        }


        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public ActionResult AddOrEdit(int id=0)
        {
           if(id == 0)
            {
                return View(new MvcEmployeeModel());


            }
            else
            {

                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Employees/"+ id.ToString()).Result;

                return View(response.Content.ReadAsAsync<MvcEmployeeModel>().Result);
            }
            
        }

        [HttpPost]
        public ActionResult AddOrEdit(MvcEmployeeModel employeeModel)
        {

            if (employeeModel.EmployeeID == 0)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("Employees", employeeModel).Result;

                TempData["SuccessMessage"] = "Saved Successfully";
            
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("Employees/"+ employeeModel.EmployeeID, employeeModel).Result;

                TempData["SuccessMessage"] = "Updated Successfully";

            }

            return RedirectToAction("Index");
        }



        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = GlobalVariables.webApiClient.DeleteAsync("Employees/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "delete Successfully";
            return RedirectToAction("Index");

        }

    }
}