using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_MVC.BL;
using UI_MVC.BL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.Controllers
{
    public class MemberFunctionController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        // GET: MemberFunction
        public ActionResult Index()
        {
            return View();
        }

        // GET: MemberFunction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberFunction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberFunction/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                MemberFunction function = new MemberFunction
                {
                    Function = m.MemberFunction.Function
                };
                member.MemberFunctions = new List<MemberFunction>();
                member.MemberFunctions.Add(function);
                mgrMbr.ChangeMember(member);
                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: MemberFunction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MemberFunction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberFunction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberFunction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrMbr.RemoveFunction(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                 return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
