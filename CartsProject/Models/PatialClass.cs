using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CartsProject.Models
{
    public class PatialClass
    {
    }
    //定義Models.Order的部分類別
    public partial class Order
    {
        //取得訂單中的 使用者暱稱
        public string GetUrderName()
        {
            
            Models.MyDataBase db = new Models.MyDataBase();
            //查詢目前網站使用者暱稱符合UserName的UserId
            string qry = @"SELECT * FROM dbo.UserData WHERE Id = '" + UserId + "' ";
            DataTable dt = db.Select_SQL(qry);
            var result = dt.Rows[0]["UserName"].ToString();
                //回傳 結果 至Index()的View
              
            return result;
        }

    }
}