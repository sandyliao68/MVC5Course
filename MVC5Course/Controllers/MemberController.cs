﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    public class MemberController : Controller
    {
        [AllowAnonymous]
        // GET: Member
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login)
        {
            if(CheckLogin(login.Email,login.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(login.Email, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "您輸入的帳號或密碼錯誤");
            return View();
        }

        private bool CheckLogin(string username,string password)
        {
            return (username == "SANDY@gmail.com" && password=="1234");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}