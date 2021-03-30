using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CartsProject.Models
{
    public class MyDataBase
    {
        string constr = @"Data Source=DESKTOP-VPEKQQN\SQLEXPRESS;Initial Catalog=Carts;Integrated Security=True";

        public bool AddUserData(User data)
        {
            bool rtn = false;
            if (!CheckAccountValid(data.email))
            {
                return false;
            }
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    DateTime myDate = DateTime.Now;
                    conn.Open();
                    string id = myDate.ToString("yyyyMMddHHmmss");
                    string strSQL = @"INSERT INTO UserData (Id, Email, Password, UserName, PhoneNumber) VALUES (@Id, @Email, @Password, @UserName, @PhoneNumber)";
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Email", data.email);
                    cmd.Parameters.AddWithValue("@Password", data.password1);
                    cmd.Parameters.AddWithValue("@UserName", data.userName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", data.phoneNumber);
                    cmd.ExecuteNonQuery();
                    rtn = true;
                }
            }
            catch (Exception ex)
            {
                rtn = false;
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return rtn;
        }

        public bool AddOrder(Order data, string userId, string orderid)
        {
            bool rtn = false;
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                    string strSQL = @"INSERT INTO Orders (UserId, RecieverName, RecieverPhone, RecieverAddress) VALUES (@UserId, @RecieverName, @RecieverPhone, @RecieverAddress)";
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@RecieverName", data.RecieverName);
                    cmd.Parameters.AddWithValue("@RecieverPhone", data.RecieverPhone);
                    cmd.Parameters.AddWithValue("@RecieverAddress", data.RecieverAddress);
                    cmd.ExecuteNonQuery();
                    rtn = true;
                }
            }
            catch (Exception ex)
            {
                rtn = false;
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return rtn;
        }

        public bool AddOrderDetail(OrderDetail data, string orderid)
        {
            bool rtn = false;
            SqlConnection conn = new SqlConnection(constr);
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                    string strSQL = @"INSERT INTO OrderDetails (OrderId, Name, Price, Quantity) VALUES (@OrderId, @Name, @Price, @Quantity)";
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.Parameters.AddWithValue("@OrderId", Convert.ToInt32(orderid));
                    cmd.Parameters.AddWithValue("@Name", data.Name);
                    cmd.Parameters.AddWithValue("@Price", data.Price);
                    cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                    cmd.ExecuteNonQuery();
                    rtn = true;
                }
            }
            catch (Exception ex)
            {
                rtn = false;
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return rtn;
        }

        public DataTable Select_SQL(string selectSQL)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(constr);
            if (conn != null && conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(selectSQL, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            if (conn.State != ConnectionState.Closed) conn.Close();
            return (dt);
        }

        private bool CheckAccountValid(string email)
        {
            bool rtn = false;
            string qry = @"SELECT * FROM dbo.UserData WHERE Email = '" + email + "' ";
            DataTable dt = Select_SQL(qry);
            if (dt.Rows.Count > 0)
            {
                rtn = false;
            }
            else
            {
                rtn = true;
            }

            return rtn;
        }
        public DataTable CheckAccountAccessible(string email, string password)
        {
            string qry = @"SELECT * FROM UserData WHERE Email = '" + email + "' AND Password = '" + password + "' ";
            DataTable dt = Select_SQL(qry);
            return dt;
        }

    }
}