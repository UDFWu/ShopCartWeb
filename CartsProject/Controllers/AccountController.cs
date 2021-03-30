using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CartsProject.Models;
using System.Data;

namespace CartsProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        MyDataBase db = new MyDataBase();

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Models.User post)
        {
            if (string.IsNullOrWhiteSpace(post.password1) || post.password1 != post.password2)
            {
                ViewBag.Msg = "請確認密碼輸入正確";
                return View(post);
            }
            else
            {
                if (db.AddUserData(post))
                {
                    Response.Redirect("~/Account/Login");
                    return new EmptyResult();
                }
                else
                {
                    ViewBag.Msg = "註冊失敗,該帳號已被使用";
                    return View(post);
                }
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User post)
        {
            string email = post.email;
            string password = post.password1;
            DataTable dt = db.CheckAccountAccessible(email, password);
            //驗證密碼
            if (dt.Rows.Count > 0)
            {
                
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                Session["UserId"] = dt.Rows[0]["Id"].ToString();
                Response.Redirect("~/Home/Index");
                return new EmptyResult();
            }
            else
            {
                ViewBag.Msg = "登入失敗,請確認帳號密碼正確";
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["UserName"] = "";
            Response.Redirect("~/Home/Index");
            return new EmptyResult();
        }
    }
}