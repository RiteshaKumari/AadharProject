﻿using Aadhar1.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Web.ModelBinding;



namespace Aadhar1.Controllers
{

    public class HomeController : Controller
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        string str = @"Data Source=RITESHA\SQLEXPRESS;Initial Catalog=Aadhar1;Integrated Security=True;MultipleActiveResultSets=True";



        Utilitycs cs = new Utilitycs();

        public ActionResult Index()
        {
            return View();
        }
        //-------------signup
       // [ValidateAntiForgeryToken()]
        [Route("signup")]
        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public ActionResult signup(signup up)
        {
            if (ModelState.IsValid) { 
                if (up.ID is null)
                {
                    try
                    {
                        SqlParameter[] parameters = new SqlParameter[]
                        {

                      new SqlParameter("@Email", up.Email),

                      new SqlParameter("@Pass", up.Pass),

                        };
                        var isValid = (int)cs.func_ExecuteScalar("login", parameters);
                        if (isValid > 0)
                        {
                            ModelState.Clear();

                            TempData["message"] = "Data Already Exists.";
                            return RedirectToAction("signup", "home");
                        }
                        else
                        {
                            SqlParameter[] parameters2 = new SqlParameter[]
                          {

                       new SqlParameter("@Email", up.Email),

                          };
                            var isvalid2 = (int)cs.func_ExecuteScalar("check_data", parameters2);
                            if (isvalid2 > 0)
                            {
                                TempData["message"] = "Your Email already registered with us.";
                                return RedirectToAction("signup", "home");
                            }
                            else
                            {

                                SqlParameter[] parameters1 = new SqlParameter[]
                           {
                       new SqlParameter("@Firstname", up.Firstname),
                       new SqlParameter("@Email", up.Email),
                       new SqlParameter("@Mobile", up.Mobile),
                       new SqlParameter("@Pass", up.Pass),
                       new SqlParameter("@ConPass", up.ConPass)
                           };
                                var isValid1 = (int)cs.func_ExecuteScalar("add_data", parameters1);
                                if (isValid1 > 0)
                                {
                                    ModelState.Clear();
                                    TempData["message"] = "Data Saved.";
                                    return RedirectToAction("signup", "home");
                                }
                                else
                                {
                                    ModelState.Clear();
                                    TempData["message"] = "Something went wrong !";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["errormessage"] = ex.Message;
                        return View();
                    }
                }

            }
            return View(up);
          
            
        }


        [Route("Signin")]
        public ActionResult Signin()
        {


            //if (Request.Cookies["value1"].Value == "" || Request.Cookies["value1"] == null)
            //{
            //    return RedirectToAction("signin");
            //}
            //else if (Request.Cookies["value1"].Value == "T")
            //{
            //    return RedirectToAction("welcome");
            //}
            //else
            //{

            //    return RedirectToAction("signin");
            //}
           
             return View();


        }


        [HttpPost]
        [Route("Signin")]
        public ActionResult Signin(signin user)
        {

            //---------------fetching data from database
            
          
                SqlParameter[] parameters1 = new SqlParameter[]
                    {
                   new SqlParameter("@Email", user.Email),
                   new SqlParameter("@Pass", user.Pass),
                    };
                var isValid = (int)cs.func_ExecuteScalar("login", parameters1);

                if (isValid > 0)
                {
                //HttpCookie Cookie = new HttpCookie("imp");

                //if (user.remember == true)
                //{
                //    TempData["remb"] = "hello";
                //    Cookie["value1"] = "T";
                //    Cookie.Expires = DateTime.Now.AddDays(10);
                //    HttpContext.Response.Cookies.Add(Cookie);
                //}
                //else
                //{
                //    TempData["remb"] = "";
                //    Cookie.Expires = DateTime.Now.AddDays(-1);
                //    HttpContext.Response.Cookies.Add(Cookie);

                //}
                ModelState.Clear();
                    TempData["mes"] = "done";
                    ViewBag.Messagelog = "Successfully Login !";
                    return RedirectToAction("adharDetail", "home");
                
                }
                else
                {
                    ViewBag.Messagelog = "Something went wrong !";
                    return View();
                }
         
           
        }


        [Route("emailValidation")]
        public ActionResult emailValidation()
        {
            return View();
        }

        private void sendpassword(String password, String email)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("riteshak246@gmail.com", "usuezfqufanygyei");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Forget Password (ADHAR Project)";
            msg.Body = "Your Password is: " + password + "\n\n Thanks & Regards \n ADHAR Project Team";
            msg.To.Add(email);
            msg.From = new MailAddress("ADHAR Project" + email);
            try
            {
                smtp.Send(msg);
            }
            catch
            {
                throw;
            }
        }


        [HttpPost]
        [Route("emailValidation")]
        public ActionResult emailValidation(emailValidation em)
        {
            try
            {
                if (em.Email != null || em.Email == "")
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("getPassForget", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", em.Email);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.Read())
                    {
                        if (sdr.HasRows)
                        {
                            
                            String Password = sdr.GetValue(0).ToString();
                            sendpassword(Password, em.Email);
                            TempData["verifyEmail"] = "Your Password sent to your email.";
                           
                        }

                        else
                        {
                            ModelState.Clear();
                            TempData["message1"] = "Enter Valid Email.";
                            return RedirectToAction("email_update", "home");
                        }

                    }
                    else
                    {
                        ModelState.Clear();
                        TempData["message1"] = "Something went wrong.";
                        return RedirectToAction("email_update", "home");
                    }
                }
                else
                {
                    TempData["message1"] = "Enter email first ! ";
                    return RedirectToAction("email_update", "home");
                }

            }
            catch (Exception ex)
            {

                TempData["message1"] = ex.Message;
                return RedirectToAction("email_update", "home");
            }
            return View();
        }

       

        //--------update data
        [Route("email_update")]
        public ActionResult email_update()
        {
            //if (@TempData["mes"] == "done")
            //{
    
            return View("email_update");
            //}
            //else
            //{
            //    return View("signin");
            //}
        }

        [HttpPost]

        [Route("email_update")]
        public ActionResult email_update(email em)
        {
            try
            {
                if (em.Email != null || em.Email == "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("email_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", em.Email);

                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    if (sdr1.Read())
                    {
                        if (sdr1.HasRows)
                        {
                            TempData["id"] = sdr1.GetValue(0).ToString();

                            Random random = new Random();
                            int value = random.Next(1001, 9999);
                            TempData["otpGenerated"] = value;

                             String from, pass, to, messageBody;

                            MailMessage message = new MailMessage();
                            to = em.Email;
                            from = "riteshak246@gmail.com";
                            pass = "usuezfqufanygyei";
                            messageBody = "Your reset code is " + value;
                            message.To.Add(to);
                            message.From = new MailAddress(from);
                            message.Body = messageBody;
                            message.Subject = "Validation Code";
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                            smtp.EnableSsl = true;
                            smtp.Port = 587;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Credentials = new NetworkCredential(from, pass);

                            try
                            {
                                smtp.Send(message);
                                //MessageBox.Show("Code Send Successfully");
                                TempData["otpmess"] = "Code Send Successfully";
                                return RedirectToAction("VerifyOTP");
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                                TempData["otpmess"] = ex.Message;
                                return View();
                            }

                        }

                        else
                        {
                            ModelState.Clear();
                            TempData["message1"] = "Enter Valid Email.";
                            return RedirectToAction("email_update", "home");
                        }

                    }
                    else
                    {
                        ModelState.Clear();
                        TempData["message1"] = "Something went wrong.";
                        return RedirectToAction("email_update", "home");
                    }
                }
                else
                {
                    TempData["message1"] = "Enter email first ! ";
                    return RedirectToAction("email_update", "home");
                }

            }
            catch (Exception ex)
            {

                TempData["message1"] = ex.Message;
                return RedirectToAction("email_update", "home");
            }

        }


        [Route("VerifyOTP")]
        public ActionResult VerifyOTP()
        {

            return View();
        }

        [HttpPost]

        [Route("VerifyOTP")]
        public ActionResult VerifyOTP(VerifyOTP user)
        {
            //TempData["userOTP"] = user.OTP;
            int ID = Convert.ToInt32(TempData["id"]);
            int a = Convert.ToInt32(TempData["otpGenerated"]);
            if (user.OTP == Convert.ToString(a))
            {
                TempData["otpGenerated"] = null;
                return RedirectToAction("updatepage", new RouteValueDictionary(new { Controller = "Home", Action = "updatepage", ID }));

            }
            else
            {
                TempData["otpMess"] = "Invalid OTP";
                return View();
            }

        }


        [Route("updatepage")]
        public ActionResult updatepage(int? ID)
        {
           // TempData.ContainsKey("id") -- return boolean value
            updatepage gn = new updatepage();
            DataTable dataTable = new DataTable();
            using (SqlConnection con1 = new SqlConnection(str))
            {
                con1.Open();
                string q = "select * from signup where ID =" + ID;
                SqlDataAdapter da = new SqlDataAdapter(q, con1);
                da.Fill(dataTable);
            }
            if (dataTable.Rows.Count == 1)
            {
                gn.ID= ID;
                gn.Firstname = dataTable.Rows[0][1].ToString();
                gn.Email = dataTable.Rows[0][2].ToString();
                gn.Mobile = dataTable.Rows[0][3].ToString();
                gn.Pass = dataTable.Rows[0][4].ToString();
                gn.ConPass = dataTable.Rows[0][5].ToString();
                return View(gn);
            }
            else
            {
                TempData["message1"] = "Something went wrong !";
                return View("email_update");
            }
        }

        [HttpPost]
        [Route("updatepage")]
        public ActionResult updatepage(updatepage up)
        {
            if (ModelState.IsValid) { 
                try
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@ID",up.ID),
                       new SqlParameter("@Firstname", up.Firstname),
                        new SqlParameter("@Email", up.Email),
                       new SqlParameter("@Mobile", up.Mobile),
                       new SqlParameter("@Pass", up.Pass),
                       new SqlParameter("@ConPass", up.ConPass)

                    //--------applying validation for email as above---------
                };
                SqlParameter[] parameters2 = new SqlParameter[]
                    {

                       new SqlParameter("@Email", up.Email),

                    };
                var isvalid2 = (int)cs.func_ExecuteScalar("check_data", parameters2);

                if (isvalid2 > 0)
                {
                    var isValid = (int)cs.func_ExecuteScalar("update_data", parameters);
                    if (isValid > 0)
                    {
                        ModelState.Clear();

                        TempData["message"] = "Your Details have Updated.";
                        return RedirectToAction("updatepage", "home");
                        
                    }
                    else
                    {
                        TempData["alert"] = "Something Went Wrong !";
                        return RedirectToAction("updatepage", "home");
                    }

                }
                else
                {
                    return RedirectToAction("updatepage", "home");
                }

              
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                    return RedirectToAction("updatepage", "home");
                }

            }
            return RedirectToAction("updatepage", "home");
        }

        //----------------delete data--------------

        //---------------simple email procedure is used for deleting--------------
        [Route("email")]
        public ActionResult email()
        {
            //if (@TempData["mes"] == "done")
            //{
            return View("email");
            //}
            //else
            //{
            //    return View("signin");
            //}
        }
        [HttpPost]
        [Route("email")]
        public ActionResult email(email em)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {

                  new SqlParameter("@Email", em.Email),

                };
                var isValid = (int)cs.func_ExecuteScalar("email", parameters);
                if (isValid > 0)
                {
                    SqlParameter[] parameters1 = new SqlParameter[]
                                {

                                   new SqlParameter("@Email", em.Email),

                                };


                    var isValid1 = (int)cs.func_ExecuteScalar("delete_data", parameters1);
                    if (isValid1 > 0)
                    {
                        ModelState.Clear();
                        TempData["message1"] = "Your Detail have deleted.";
                        return RedirectToAction("email", "home");
                    }
                    else
                    {

                        TempData["message1"] = "Something went wrong !";

                    }
                }

                else
                {
                    ModelState.Clear();
                    TempData["message1"] = "Register first.";

                }
            }
            catch (Exception ex)
            {

                TempData["message1"] = ex.Message;

            }
            return RedirectToAction("email", "home");
        }

        [Route("welcome")]
        public ActionResult welcome(string Mobile)
        {
           
            if (TempData["mes"] == "done")
            {
                adharDetails gn = new adharDetails();
                DataTable dataTable = new DataTable();
                using (SqlConnection con1 = new SqlConnection(str))
                {
                    con1.Open();
                    string q = "select * from aadharDetailTable where Mobile =" + Mobile;
                    SqlDataAdapter da = new SqlDataAdapter(q, con1);
                    da.Fill(dataTable);
                }
                if (dataTable.Rows.Count == 1)
                {
                   
                    gn.FullName = dataTable.Rows[0][0].ToString();
                    gn.DOF = dataTable.Rows[0][1].ToString();
                    gn.StreetAdd = dataTable.Rows[0][3].ToString();
                    gn.ZIP = dataTable.Rows[0][4].ToString();
                    gn.City = dataTable.Rows[0][5].ToString();
                    gn.Gender = dataTable.Rows[0][6].ToString();
                    gn.Image = "/Content/image/"+ dataTable.Rows[0][8].ToString();
                    TempData["imagepath"] = gn.Image;
                    return View(gn);
                }
               
                else
                {
                    TempData["message"] = "Something went wrong !";
                    return View("adharDetail");
                }
                //return View();
            }
            else
            {
                return View("signin");
            }
        }

        [HttpPost]
        [Route("welcome")]
        public ActionResult welcome(welcome wel)
        {
          
                return View();
         
        }

        

        [Route("adharDetail")]
        public ActionResult adharDetail(int? ID)
        {
          
            if (TempData["mes"] == "done")
            {
                return View();
            }
            else
            {
                return View("signin");
            }
        }

        [HttpPost]
        [Route("adharDetail")]
       
        public ActionResult adharDetail(adharDetails aadharD, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string imagepath;
                    string filename;
                    adharDetails fs = new adharDetails();
                    //fs.filesize = 550;
                    string us = fs.UploadUserFile(Image);
                    if (us != null)
                    {
                        ViewBag.ResultErrorMessage = fs.ErrorMessage;
                    }
                    if (Image != null && Image.ContentLength > 0)
                    {
                        filename = Path.GetFileName(Image.FileName);
                        TempData["imgName"] = filename;
                        //string imagepath = Path.Combine(Server.MapPath("~/content/image/"), filename);
                        imagepath = Server.MapPath("~/Content/image/" + filename);
                        Image.SaveAs(imagepath);

                    }
                    else
                    {
                        //TempData["message"] = "Please upload your Image!";
                    }
                    SqlParameter[] parameters = new SqlParameter[]
                     {


                       new SqlParameter("@FullName", aadharD.FullName),
                       new SqlParameter("@DOF", aadharD.DOF),
                       //new SqlParameter("@Email", aadharD.Email),
                       new SqlParameter("@StreetAdd", aadharD.StreetAdd),
                       new SqlParameter("@ZIP", aadharD.ZIP),
                       new SqlParameter("@City", aadharD.City),
                        new SqlParameter("@Mobile", aadharD.Mobile),
                       new SqlParameter("@Gender", aadharD.Gender),
                       new SqlParameter("@Image", TempData["imgName"])

                     };
                    SqlParameter[] parameters1 = new SqlParameter[]
                 {


                        new SqlParameter("@Mobile", aadharD.Mobile)


                 };

                    var isValid = (int)cs.func_ExecuteScalar("check_mobile", parameters1);
                    if (isValid > 0)
                    {
                        ModelState.Clear();

                        TempData["message"] = "Mobile number Already Exists.";
                        return RedirectToAction("adharDetail", "home");
                    }
                    else
                    {
                        var isValid1 = (int)cs.func_ExecuteScalar("aadharDetail", parameters);
                        if (isValid1 > 0)
                        {
                            ModelState.Clear();
                            TempData["mes"] = "done";
                            TempData["message"] = "Details Saved.";

                            return RedirectToAction("welcome", new RouteValueDictionary(new { Controller = "Home", Action = "welcome", aadharD.Mobile }));


                        }
                        else
                        {
                            ModelState.Clear();
                            TempData["message"] = "Something went wrong !";
                        }

                    }
                }

                catch (Exception ex)
                {
                    TempData["errormessage"] = ex.Message;
                    return View();
                }
            }
                return View();
            
          
        }
    }
}
