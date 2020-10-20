using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Realestate_portal.Models;

namespace Realestate_portal.Controllers
{
    public class NotesController : Controller
    {
        private Realstate_agentsEntities db = new Realstate_agentsEntities();
   
        private clsGeneral generalClass = new clsGeneral();

        // GET: Notes
        public ActionResult Index(int? ID_Customer, int? ID_Propery, string module, int broker=0)
        {
            if (generalClass.checkSession())
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;

                //HEADER
                //ACTIVE PAGES
                ViewData["Menu"] = "CRM";
                ViewData["Page"] = "Notes";
                ViewBag.menunameid = "";
                ViewBag.submenunameid = "";
                List<string> s = new List<string>(activeuser.Department.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstDepartments = JsonConvert.SerializeObject(s);
                List<string> r = new List<string>(activeuser.Roles.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstRoles = JsonConvert.SerializeObject(r);
                //NOTIFICATIONS
                DateTime now = DateTime.Today;
                List<Sys_Notifications> lstAlerts = (from a in db.Sys_Notifications where (a.ID_user == activeuser.ID_User && a.Active == true) select a).OrderByDescending(x => x.Date).Take(4).ToList();
                ViewBag.notifications = lstAlerts;
                ViewBag.userID = activeuser.ID_User;
                ViewBag.userName = activeuser.Name + " " + activeuser.LastName;

             


                ViewBag.rol = "";

                //Filtros SA

                var lstCompanies = (from a in db.Sys_Company select a).ToList();
                ViewBag.lstCompanies = lstCompanies;

                List<Tb_Notes> lstNotes = new List<Tb_Notes>();

                ViewBag.module = module;
                ViewBag.Customer = ID_Customer;
                ViewBag.Process = ID_Propery;
                if (r.Contains("Agent"))
                {
                    ViewBag.rol = "Agent";
                    if (module == "Customers")
                    {
                        lstNotes = db.Tb_Notes.Where(c => c.ID_Customer == ID_Customer && c.ID_User==activeuser.ID_User).ToList();
                        
                    }
                    else
                    {
                        lstNotes = db.Tb_Notes.Where(c => c.ID_Property==ID_Propery && c.ID_User == activeuser.ID_User).ToList();
                    }
                }
                else
                {
                    ViewBag.rol = "Admin";
                    if (module == "Customers")
                    {
                        lstNotes = db.Tb_Notes.Where(c => c.ID_Customer == ID_Customer).ToList();
                    }
                    else
                    {
                        ViewBag.rol = "SA";
                        lstNotes = db.Tb_Notes.Where(c => c.ID_Property == ID_Propery).ToList();
                    }


                    if (broker == 0)
                    {
                    }
                    else
                    {
                        ViewBag.rol = "SA";

                    }

                }
                ViewBag.selbroker = broker;
                var propertiesprojectedgains = (from f in db.Tb_Process where (f.ID_User == activeuser.ID_User && f.Stage == "UNDER CONTRACT") select f).ToList();
                var propertiesgains = (from f in db.Tb_Process where (f.ID_User == activeuser.ID_User && f.Stage == "CLOSED") select f).ToList();
                var totalproperties = (from f in db.Tb_Process where (f.ID_User == activeuser.ID_User) select f).Count();

                decimal totalprojectedgains = 0;
                decimal totalgains = 0;
                if (propertiesprojectedgains.Count > 0) { totalprojectedgains = propertiesprojectedgains.Select(c => c.Commission_amount).Sum(); }
                if (propertiesgains.Count > 0) { totalgains = propertiesgains.Select(c => c.Commission_amount).Sum(); }

                ViewBag.totalcustomers = totalproperties;
                ViewBag.totalgainsprojected = totalprojectedgains;
                ViewBag.totalgains = totalgains;

                return View(lstNotes);

            }
            else
            {

                return RedirectToAction("Login", "Portal", new { access = false });

            }


          
           
    
        }
        [HttpPost]
        public ActionResult Addnote(string content, int? customer, int? process)
        {

            if (customer == null) { customer = 0; }
            if (process == null) { process = 0; }
            try
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;
                Tb_Notes newnote = new Tb_Notes();
                newnote.Date = DateTime.UtcNow;
                newnote.ID_Customer = Convert.ToInt32(customer);
                newnote.ID_Property = Convert.ToInt32(process);
                newnote.ID_User = activeuser.ID_User;
                newnote.Text = content;

                db.Tb_Notes.Add(newnote);
                db.SaveChanges();

                var result = "SUCCESS";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }
        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (generalClass.checkSession())
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;

                //HEADER
                //ACTIVE PAGES
                ViewData["Menu"] = "CRM";
                ViewData["Page"] = "Notes";
                ViewBag.menunameid = "";
                ViewBag.submenunameid = "";
                List<string> s = new List<string>(activeuser.Department.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstDepartments = JsonConvert.SerializeObject(s);
                List<string> r = new List<string>(activeuser.Roles.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstRoles = JsonConvert.SerializeObject(r);
                //NOTIFICATIONS
                DateTime now = DateTime.Today;
                List<Sys_Notifications> lstAlerts = (from a in db.Sys_Notifications where (a.ID_user == activeuser.ID_User && a.Active == true) select a).OrderByDescending(x => x.Date).Take(4).ToList();
                ViewBag.notifications = lstAlerts;
                ViewBag.userID = activeuser.ID_User;
                ViewBag.userName = activeuser.Name + " " + activeuser.LastName;


                ViewBag.rol = "";

     
                Tb_Notes tb_Notes = db.Tb_Notes.Find(id);

                if (r.Contains("Agent"))
                {
                    ViewBag.rol = "Agent";

                }
                else
                {
                    ViewBag.rol = "Admin";


                }
                return View(tb_Notes);

            }
            else
            {

                return RedirectToAction("Login", "Portal", new { access = false });

            }
            
        }

        // GET: Notes/Create
        public ActionResult Create(int broker=0)
        {
            if (generalClass.checkSession())
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;

                //HEADER
                //ACTIVE PAGES
                ViewData["Menu"] = "CRM";
                ViewData["Page"] = "Notes";
                ViewBag.menunameid = "";
                ViewBag.submenunameid = "";
                List<string> s = new List<string>(activeuser.Department.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstDepartments = JsonConvert.SerializeObject(s);
                List<string> r = new List<string>(activeuser.Roles.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstRoles = JsonConvert.SerializeObject(r);
                //NOTIFICATIONS
                DateTime now = DateTime.Today;
                List<Sys_Notifications> lstAlerts = (from a in db.Sys_Notifications where (a.ID_user == activeuser.ID_User && a.Active == true) select a).OrderByDescending(x => x.Date).Take(4).ToList();
                ViewBag.notifications = lstAlerts;
                ViewBag.userID = activeuser.ID_User;
                ViewBag.userName = activeuser.Name + " " + activeuser.LastName;


                ViewBag.rol = "";

                //Filtros SA

                var lstCompanies = (from a in db.Sys_Company select a).ToList();
                ViewBag.lstCompanies = lstCompanies;


                if (r.Contains("Agent"))
                {
                    ViewBag.rol = "Agent";

                }
                else
                {
                    if (r.Contains("SA") && broker == 0)
                    {
                        ViewBag.rol = "SA";
                        ViewBag.userdata = (from usd in db.Sys_Users where (usd.ID_Company == activeuser.ID_Company) select usd).FirstOrDefault();
                        var brokersel = (from b in db.Sys_Users where (b.ID_Company == activeuser.ID_Company && b.Roles.Contains("Admin")) select b).FirstOrDefault();
                        RedirectToAction("Dashboard", "Portal", new { broker = brokersel.ID_Company });
                    }
                    else
                    {
                        ViewBag.rol = "Admin";

                        if (broker == 0)
                        {
                        }
                        else
                        {
                            ViewBag.rol = "SA";

                        }
                    }
              

                }
                ViewBag.selbroker = broker;
                return View();
                
            }
            else
            {

                return RedirectToAction("Login", "Portal", new { access = false });

            }
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_note,Text,Date,ID_Property,ID_Customer,ID_User")] Tb_Notes tb_Notes)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Notes.Add(tb_Notes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Notes);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id, int? ID_Customer, int? ID_Propery, string module, int broker=0)
        {
            if (generalClass.checkSession())
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;

                //HEADER
                //ACTIVE PAGES
                ViewData["Menu"] = "CRM";
                ViewData["Page"] = "Notes";
                ViewBag.menunameid = "";
                ViewBag.submenunameid = "";
                List<string> s = new List<string>(activeuser.Department.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstDepartments = JsonConvert.SerializeObject(s);
                List<string> r = new List<string>(activeuser.Roles.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstRoles = JsonConvert.SerializeObject(r);
                //NOTIFICATIONS
                DateTime now = DateTime.Today;
                List<Sys_Notifications> lstAlerts = (from a in db.Sys_Notifications where (a.ID_user == activeuser.ID_User && a.Active == true) select a).OrderByDescending(x => x.Date).Take(4).ToList();
                ViewBag.notifications = lstAlerts;
                ViewBag.userID = activeuser.ID_User;
                ViewBag.userName = activeuser.Name + " " + activeuser.LastName;


                ViewBag.rol = "";


                //Filtros SA

                var lstCompanies = (from a in db.Sys_Company select a).ToList();
                ViewBag.lstCompanies = lstCompanies;

                ViewBag.module = module;
                ViewBag.Customer = ID_Customer;
                ViewBag.Process = ID_Propery;
                Tb_Notes tb_Notes = db.Tb_Notes.Find(id);

                if (r.Contains("Agent"))
                {
                    ViewBag.rol = "Agent";

                }
                else
                {
                    if (r.Contains("SA") && broker == 0)
                    {
                        ViewBag.rol = "SA";
                        ViewBag.userdata = (from usd in db.Sys_Users where (usd.ID_Company == activeuser.ID_Company) select usd).FirstOrDefault();
                        var brokersel = (from b in db.Sys_Users where (b.ID_Company == activeuser.ID_Company && b.Roles.Contains("Admin")) select b).FirstOrDefault();
                        RedirectToAction("Dashboard", "Portal", new { broker = brokersel.ID_Company });
                    }
                    else
                    {
                        ViewBag.rol = "Admin";
                        if (broker == 0)
                        {
                        }
                        else
                        {
                            ViewBag.rol = "SA";

                        }
                    }
                   


                }
                ViewBag.selbroker = broker;
                return View(tb_Notes);

            }
            else
            {

                return RedirectToAction("Login", "Portal", new { access = false });

            }

        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_note,Text,Date,ID_Property,ID_Customer,ID_User")] Tb_Notes tb_Notes, int? ID_Customer, int? ID_Propery, string module)
        {

            try
            {
              

                db.Entry(tb_Notes).State = EntityState.Modified;
                db.SaveChanges();
                TempData["exito"] = "Data saved successfully.";
                return RedirectToAction("Edit", "Notes", new { id = tb_Notes.ID_note, ID_Customer = ID_Customer, ID_Propery = ID_Propery, module = module });
            }
            catch (Exception ex)
            {
                TempData["advertencia"] = "Something went wrong." + ex.Message;
                return RedirectToAction("Edit", "Notes", new { id = tb_Notes.ID_note, ID_Customer = ID_Customer, ID_Propery = ID_Propery, module = module });
            }

        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id, int? ID_Customer, int? ID_Propery, string module, int broker=0)
        {
            if (generalClass.checkSession())
            {
                Sys_Users activeuser = Session["activeUser"] as Sys_Users;

                //HEADER
                //ACTIVE PAGES
                ViewData["Menu"] = "CRM";
                ViewData["Page"] = "Notes";
                ViewBag.menunameid = "";
                ViewBag.submenunameid = "";
                List<string> s = new List<string>(activeuser.Department.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstDepartments = JsonConvert.SerializeObject(s);
                List<string> r = new List<string>(activeuser.Roles.Split(new string[] { "," }, StringSplitOptions.None));
                ViewBag.lstRoles = JsonConvert.SerializeObject(r);
                //NOTIFICATIONS
                DateTime now = DateTime.Today;
                List<Sys_Notifications> lstAlerts = (from a in db.Sys_Notifications where (a.ID_user == activeuser.ID_User && a.Active == true) select a).OrderByDescending(x => x.Date).Take(4).ToList();
                ViewBag.notifications = lstAlerts;
                ViewBag.userID = activeuser.ID_User;
                ViewBag.userName = activeuser.Name + " " + activeuser.LastName;


                ViewBag.rol = "";
                ViewBag.module = module;
                ViewBag.Customer = ID_Customer;
                ViewBag.Process = ID_Propery;

                //Filtros SA

                var lstCompanies = (from a in db.Sys_Company select a).ToList();
                ViewBag.lstCompanies = lstCompanies;


                Tb_Notes tb_Notes = db.Tb_Notes.Find(id);

                if (r.Contains("Agent"))
                {
                    ViewBag.rol = "Agent";

                }
                else
                {
                    if (r.Contains("SA") && broker == 0)
                    {
                        ViewBag.rol = "SA";
                        ViewBag.userdata = (from usd in db.Sys_Users where (usd.ID_Company == activeuser.ID_Company) select usd).FirstOrDefault();
                        var brokersel = (from b in db.Sys_Users where (b.ID_Company == activeuser.ID_Company && b.Roles.Contains("Admin")) select b).FirstOrDefault();
                        RedirectToAction("Dashboard", "Portal", new { broker = brokersel.ID_Company });
                    }
                    else
                    {
                        ViewBag.rol = "Admin";
                        if (broker == 0)
                        {
                        }
                        else
                        {
                            ViewBag.rol = "SA";

                        }
                    }
                 


                }
                ViewBag.selbroker = broker;
                return View(tb_Notes);

            }
            else
            {

                return RedirectToAction("Login", "Portal", new { access = false });

            }

        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? ID_Customer, int? ID_Propery, string module)
        {
            Tb_Notes tb_Notes = db.Tb_Notes.Find(id);
            db.Tb_Notes.Remove(tb_Notes);
            db.SaveChanges();
            return RedirectToAction("Index","Notes", new { ID_Customer = ID_Customer, ID_Propery=ID_Propery, module = module });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
