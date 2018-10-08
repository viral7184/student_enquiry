using student_enquiry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using student_enquiry.Controllers;
using student_enquiry.Models;
using System.Data.OleDb;
using System.Globalization;

namespace student_enquiry.Controllers
{
    public class HomeController : Controller
    {
        dbs_studentEntities db = new dbs_studentEntities();
        public ActionResult enquiry()
        {
          
            ViewBag.enquiry = db.enquiries.Where(i => i.IS_DELETED == false).ToList();
            ViewBag.course = db.courses.Where(i => i.IS_DELETED == false).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult enquiry(enquiry data)
        {
            try
            {
                if (data.ID > 0)
                {
                    var enquiry = db.enquiries.Where(i => i.ID == data.ID).SingleOrDefault();
                    enquiry.NAME = data.NAME;
                    enquiry.EMAIL = data.EMAIL;
                    enquiry.NUMBER = data.NUMBER;
                    enquiry.REFERENCE = data.REFERENCE;
                    enquiry.GENDER = data.GENDER;
                    enquiry.DOB = data.DOB;
                    if (data.COURSE_ENQUIRY != null)
                    {
                        enquiry.COURSE_ENQUIRY = data.COURSE_ENQUIRY;
                    }
                    if (data.IN_TIME != null)
                    {
                        enquiry.IN_TIME = data.IN_TIME;
                    }
                    if (data.OUT_TIME != null)
                    {
                        enquiry.OUT_TIME = data.OUT_TIME;
                    }
                    if (data.JOINING_DATE != null)
                    {
                        enquiry.JOINING_DATE = data.JOINING_DATE;
                    }
                    if (data.END_DATE != null)
                    {
                        enquiry.END_DATE = data.END_DATE;
                    }
                    enquiry.TESTIMONIAL = data.TESTIMONIAL;
                    if(data.FEES!=null)
                    {
                        enquiry.FEES = data.FEES;
                        var course = db.courses.Where(i => i.ID == data.COURSE_ENQUIRY).FirstOrDefault();
                        var coursefee = Convert.ToInt16(course.FEES);
                        var datafee = Convert.ToInt16(data.FEES);
                        var remainingfee = coursefee - datafee;
                        enquiry.REMAINING_FEES = remainingfee.ToString();
                    }
                    db.SaveChanges();
                }
                else
                {
                    data.IS_DELETED = false;
                    var course = db.courses.Where(i => i.ID == data.COURSE_ENQUIRY && i.ID != 5).FirstOrDefault();
                    if(course !=null)
                    { 
                    var coursefee = Convert.ToInt32(course.FEES);
                    var datafee = Convert.ToInt32(data.FEES);
                    var remainingfee= coursefee - datafee;
                    data.REMAINING_FEES = remainingfee.ToString();
                    }
                    var enquiry = db.enquiries.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return RedirectToAction("enquiry");
        }
        public JsonResult getenquiry(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.enquiries.Where(i => i.ID == id).Select(i => new
                {
                    i.ID,
                    i.NAME,
                    i.EMAIL,
                    i.NUMBER,
                    i.REFERENCE,
                    i.GENDER,
                    i.DOB,
                    courseid = i.cours.ID,
                    course = i.cours.COURSE_NAME,
                    i.IN_TIME,
                    i.OUT_TIME,
                    i.JOINING_DATE,
                    i.END_DATE,
                    i.TESTIMONIAL,
                    i.FEES,
                    i.REMAINING_FEES
                }).FirstOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }




        public JsonResult delete(int id, string course_del, string enquiry_del,string callslist_del)
        {
            try
            {
                if (course_del != null)
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var notification = db.courses.Where(i => i.ID == id).SingleOrDefault();
                    notification.IS_DELETED = true;
                    db.SaveChanges();
                    return Json(new { data = notification }, JsonRequestBehavior.AllowGet);

                }
                if (enquiry_del != null)
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var notification = db.enquiries.Where(i => i.ID == id).SingleOrDefault();
                    notification.IS_DELETED = true;
                    db.SaveChanges();
                    return Json(new { data = notification }, JsonRequestBehavior.AllowGet);
                }
                if (callslist_del != null)
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var notification = db.todaylists.Where(i => i.ID == id).SingleOrDefault();
                    notification.IS_DELETED = true;
                    db.SaveChanges();
                    return Json(new { data = notification }, JsonRequestBehavior.AllowGet);
                }

                return Json("");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult course()
        {

            ViewBag.course = db.courses.Where(i => i.IS_DELETED == false).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult course(cours data)
        {
            try
            {
                if (data.ID > 0)
                {
                    var enquiry = db.courses.Where(i => i.ID == data.ID).SingleOrDefault();
                    enquiry.COURSE_NAME = data.COURSE_NAME;
                    enquiry.FEES = data.FEES;
                    db.SaveChanges();
                }
                else
                {
                    data.IS_DELETED = false;
                    var enquiry = db.courses.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return RedirectToAction("course");
        }
        public JsonResult getcourse(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.courses.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult callslist()
        {

            ViewBag.calls = db.todaylists.Where(i => i.IS_DELETED == false).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult callslist(todaylist data)
        {
            try
            {
                if (data.ID > 0)
                {
                    var call = db.todaylists.Where(i => i.ID == data.ID).SingleOrDefault();
                    call.DATE = data.DATE;
                    call.TOTAL_LEAD = data.TOTAL_LEAD;
                    call.TOTAL_CALLS = data.TOTAL_CALLS;
                    call.GENUINE = data.GENUINE;
                    call.FAKE = data.FAKE;
                    call.INTERESTED = data.INTERESTED;
                    call.IN_PROGRESS = data.IN_PROGRESS;
                    call.CONVERTED = data.CONVERTED;
                    call.RECEIVECALLS = data.RECEIVECALLS;
                    call.UNRECEIVECALLS = data.UNRECEIVECALLS;
                    db.SaveChanges();
                }
               else
                { 
                    data.IS_DELETED = false;
                   
                    var calls = db.todaylists.Add(data);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return RedirectToAction("callslist");
        }
        public JsonResult getcallslist(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.todaylists.Where(i => i.ID == id).SingleOrDefault();
                return Json(new { data = confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult uploadexcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadexcel(HttpPostedFileBase excelfile,enquiry data)
        {
            try
            {   

                if (excelfile == null || excelfile.ContentLength == 0)
                {
                    return View("uploadexcel");
                }
                else
                {
                    if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                    {

                        string path = Server.MapPath("~/ExcelFiles/" + excelfile.FileName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);

                        excelfile.SaveAs(path);
                        Enterdata(path);
                        return RedirectToAction("enquiry");
                    }
                    else
                    {
                        return RedirectToAction("enquiry");
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }          
        }

        public void Enterdata(string path)
        {
            try
            {
                //  string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=Yes;'";
                string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                enquiry stud = new enquiry();

                using (OleDbConnection connection = new OleDbConnection(con))
                {

                    connection.Open();
                    OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
                    List<dynamic> duplicate = new List<dynamic>();
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //  stud.NAME = dr[1].ToString();
                            stud.EMAIL = dr[2].ToString();
                            if (db.enquiries.Any(k => k.EMAIL == stud.EMAIL))
                            {
                                var data = db.enquiries.Where(i => i.EMAIL == stud.EMAIL).FirstOrDefault();
                                duplicate.Add(data);
                            }
                            else
                            {
                                stud.END_DATE =Convert.ToDateTime(dr[0]);
                                stud.NAME = dr[1].ToString();
                                stud.EMAIL = dr[2].ToString();
                                if (dr[3].ToString() != null && dr[3].ToString() != "")
                                {
                                    stud.NUMBER = dr[3].ToString();
                                }
                                if (dr[4].ToString() != null && dr[4].ToString() != "")
                                {
                                    stud.GENDER = dr[4].ToString();
                                }
                                else
                                {
                                    stud.GENDER = "Male";
                                }
                                if (dr[5].ToString() != null && dr[5].ToString() != "")
                                {
                                    stud.DOB = Convert.ToDateTime(dr[5]);
                                }
                                stud.REFERENCE = dr[6].ToString();
                                if (dr[7].ToString() != null && dr[7].ToString() != "")
                                {
                                    stud.COURSE_ENQUIRY = 5;
                                }
                                else
                                {
                                    stud.COURSE_ENQUIRY =5;
                                }
                               if (dr[8].ToString() != null && dr[8].ToString() != "")
                                { 
                                    stud.JOINING_DATE = Convert.ToDateTime(dr[8]);
                                }
                                string stringTime = dr[9].ToString();
                                TimeSpan time = TimeSpan.Parse(stringTime);
                                //stud.IN_TIME = time;

                                //string Time = dr[9].ToString();
                                //string time2 = Time.ToString('hh:mm:ss tt');
                                //stud.IN_TIME = time2;

                                //string stringTime2 = dr[10].ToString();                               
                                //stud.OUT_TIME = stringTime2;

                                stud.TESTIMONIAL = dr[11].ToString();
                                stud.FEES = dr[12].ToString();
                                stud.REMAINING_FEES = dr[13].ToString();                              
                                stud.IS_DELETED = false;
                                db.enquiries.Add(stud);
                                db.SaveChanges();
                            }

                        }
                        ViewBag.dup = duplicate;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public JsonResult getdatacourse()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var confi = db.courses.Select(i => new {
                    id = i.ID,
                    coursename = i.COURSE_NAME,
                    fee = i.FEES
                }).ToList();
                return Json(new { confi }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}