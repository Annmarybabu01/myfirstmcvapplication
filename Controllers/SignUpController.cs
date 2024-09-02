using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Myfirstmvcapplication.DAL;
using Myfirstmvcapplication.Models;

namespace Myfirstmvcapplication.Controllers
{
    public class SignUpController : Controller
    {
        SignUpDAL objSignUpdetail=new SignUpDAL();
        // GET: SignUp
        public ActionResult Index()
        {
            var signUpList= objSignUpdetail.GetAllDetails();
            if(signUpList.Count== 0)
            {
                TempData["infoMessage"] = "currently no signup data is available";
            }
            return View(signUpList);
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignUp/Create
        [HttpPost]
        public ActionResult Create(SignUpModel signupentry,SignUpDAL signUpDAL)
        {
            bool IsInserted = false;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    IsInserted = signUpDAL.InsertSignUp(signupentry);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "SIGN UP SUCCESSFULLY";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "SIGN UP FAILED";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int id, SignUpDAL signUpDAL)
        {
            var signUpId = signUpDAL.GetSignUpById(id).FirstOrDefault();
            if(signUpId == null)
            {
                TempData["InfoMessage"] = "selected id is not present";
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: SignUp/Edit/5
        [HttpPost]
        public ActionResult Edit(SignUpModel signupentry, SignUpDAL signUpDAL)
        {
            bool IsUpdated = false;
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    IsUpdated = signUpDAL.UpdateSignUp(signupentry);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "UPDATE SUCCESSFULLY";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "UPDATION FAILED";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int id, SignUpDAL signUpDAL)
        {
            try
            {
                var signUpId = signUpDAL.GetSignUpById(id).FirstOrDefault();
                if (signUpId == null)
                {
                    TempData["InfoMessage"] = "selected id is not present";
                    return RedirectToAction("Index");
                }
                return View(signUpId);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: SignUp/Delete/5
        [HttpPost]
        public ActionResult Delete(SignUpModel signupentry, SignUpDAL signUpDAL,int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = signUpDAL.GetSignUpById(id).ToString();
                if (result.Contains("deleted"))
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
