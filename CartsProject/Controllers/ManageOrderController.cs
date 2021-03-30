using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace CartsProject.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //取得Order中所有資料
                var result = (from s in db.Orders
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult Details(int id)
        {
            
            using (Models.CartsEntities1 db = new Models.CartsEntities1())
            {
                //取得OrderId為傳入id的所有商品列表
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0)
                {   //如果商品數目為零，代表該訂單異常(無商品)，則導回商品列表
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.OrderId = id.ToString();
                    return View(result);
                }
            }
        }

        public ActionResult SerachByUserName(string UserName)
        {

            Models.MyDataBase db = new Models.MyDataBase();
            //查詢目前網站使用者暱稱符合UserName的UserId
            string qry = @"SELECT * FROM dbo.UserData WHERE UserName = '" + UserName + "' ";
            DataTable dt = db.Select_SQL(qry);

            //如果有存在UserId
            if (dt.Rows.Count > 0)
            {   //則將此UserId的所有訂單找出
                qry = @"SELECT * FROM dbo.Order WHERE Id = '" + dt.Rows[0]["Id"].ToString() + "' ";
                DataTable dt2 = db.Select_SQL(qry);
                var result = dt2.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("Id")).ToList();

                //回傳 結果 至Index()的View
                return View("Index", result);

            }
            else
            {   //回傳 空結果 至Index()的View
                return View("Index", new List<Models.Order>());
            }
        }


    }
}