using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI_MVC.BL;
using UI_MVC.BL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IMedicalVisitManager mgrMed = new MedicalVisitManager();
        ISubscriptionManager mgrSub = new SubscriptionManager();
        IMemberManager mgrMem = new MemberManager();
        public ActionResult Index()
        {
            IEnumerable<Subscription> subs = mgrSub.getSubscriptionsForYear(DateTime.Now.Year);
            IEnumerable<MedicalVisit> meds = mgrMed.getMedicalVisits();
            IEnumerable<Member> members = mgrMem.GetFullMembers();

            int totalMembers = subs.Count();
            int firstAppartenance = subs.Count(s => s.MembershipType == MembershipTypes.PremiereAppartenance);
            int secAppartenance = subs.Count(s => s.MembershipType == MembershipTypes.SecondeAppartenance);
            int effectifs = subs.Count(s => s.MemberState == MemberStates.Effectif);
            int adherents = subs.Count(s => s.MemberState == MemberStates.Adherent);

            int medNotInOrder = 0;
            int ecgNotInOrder = 0;
            int numberOfCFPS = 0;

            foreach (var mem in members)
            {
                if(mem.Subscriptions != null)
                {
                    foreach(var sub in mem.Subscriptions)
                    {
                        if (sub.ExpirationDate.Year.Equals(DateTime.Now.Year) || (sub.DatePayed.Month >= 9 && sub.DatePayed.Year == DateTime.Now.Year)){
                            if(mem.MedicalVisits.Count() == 0 )
                            {
                                medNotInOrder++;
                            }
                            foreach(var med in mem.MedicalVisits)
                            {
                                if(med.ExpirationDate.CompareTo(DateTime.Now) < 0)
                                {
                                    medNotInOrder++;
                                }
                            }
                                if (mem.Ecgs.Count() == 0)
                                {
                                    ecgNotInOrder++;
                                }
                                foreach (var ecg in mem.Ecgs)
                                {
                                    if (ecg.ExpirationDate.CompareTo(DateTime.Now) < 0)
                                    {
                                        ecgNotInOrder++;
                                    }
                                }
                                if(mem.CFPSs.Count() != 0)
                                {
                                    foreach (var cfps in mem.CFPSs)
                                    {
                                        if(cfps.ExpirationDate.CompareTo(DateTime.Now) > 0)
                                        {
                                            numberOfCFPS++;
                                        }
                                    }
                                }

                            }
                    }
                }
            }


            ViewBag.TotalMembers = totalMembers;
            ViewBag.FirstAppartenance = firstAppartenance;
            ViewBag.SecAppartenance = secAppartenance;

            ViewBag.MedNotInOrder = medNotInOrder;
            ViewBag.EcgNotInOrder = ecgNotInOrder;

            ViewBag.Effectifs = effectifs;
            ViewBag.Adherents = adherents;

            ViewBag.NumberOfCFPS = numberOfCFPS;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SendMail()
        {
            IEnumerable<Member> members = mgrMem.GetFullMembers();
            List<string> emails = new List<string>();

            foreach (var item in members)
            {
                if(item.Subscriptions != null)
                {
                    foreach(var sub in item.Subscriptions)
                    {
                        if(sub.ExpirationDate.CompareTo(DateTime.Now) < 0)
                        {
                            emails.Add(item.Email);
                        }
                    }
                }
            }



            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            List<Member> member = mgrMem.GetExportToExcel(DateTime.Now.Year);
            List<memberToExport> membersToExport = new List<memberToExport>();
            foreach(var mem in member)
            {
                memberToExport memberToAdd = new memberToExport();
                memberToAdd.Address = mem.Address;
                memberToAdd.Birthdate = mem.Birthdate;
                memberToAdd.City = mem.City;
                memberToAdd.CMASNumber = mem.CMASNumber;
                memberToAdd.Country = mem.Country;
                memberToAdd.Email = mem.Email;
                memberToAdd.FirstName = mem.FirstName;
                memberToAdd.Gender = mem.Gender;
                memberToAdd.HouseNumber = mem.HouseNumber;
                memberToAdd.LastName = mem.LastName;
                memberToAdd.LIFRASNumber = mem.LIFRASNumber;
                memberToAdd.MailBox = mem.MailBox;
                memberToAdd.MemberId = mem.MemberId;
                memberToAdd.Phone = mem.Phone;
                memberToAdd.Zip = mem.Zip;

                foreach (var ice in mem.ICEs)
                {
                    memberToAdd.LastNameIce = ice.LastName;
                    memberToAdd.FirstNameIce = ice.FirstName;
                    memberToAdd.PhoneIce = ice.Phone;
                    memberToAdd.DescriptionIce = ice.Description;
                }

                membersToExport.Add(memberToAdd);
            }
            gv.DataSource = membersToExport;

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Members.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
    }

    internal class memberToExport
    {
        public int MemberId { get; set; }
       
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string HouseNumber { get; set; }

        public int? MailBox { get; set; }

        public int Zip { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public Gender Gender { get; set; }

        public int? CMASNumber { get; set; }

        public int? LIFRASNumber { get; set; }

        public string FirstNameIce { get; set; }

        public string LastNameIce { get; set; }

        public string DescriptionIce { get; set; }

        public string PhoneIce { get; set; }
    }
}