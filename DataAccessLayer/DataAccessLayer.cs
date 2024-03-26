using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using SocialMediaWebApp.Models;
using System.Web.Helpers;
public class DataAccessLayer
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public int RegisterUser(Users u)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("RegisterUser",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username",u.Username);
                cmd.Parameters.AddWithValue("password", u.Password);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@full_name", u.FullName);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }

        public int LoginUser(Users u)
        {
            int userId = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", u.Username);
                cmd.Parameters.AddWithValue("@password", u.Password);
                var result = cmd.ExecuteScalar();
                if(result != null && result != DBNull.Value)
                {
                    userId = Convert.ToInt32(result);
                }
            }
            return userId;
        }


        public int CreatePost(Post p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("CreatePost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user_id", p.UserId);
                command.Parameters.AddWithValue("@post_content", p.PostContent);
                int i = command.ExecuteNonQuery();
                return i;
            }
        }

        public DataTable GetHomePagePosts()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetHomePagePosts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetUserProfile(int userId)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUserProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public void DeletePostFromHomePage(int postId, int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeletePostFromHomePage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@post_id", postId);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.ExecuteNonQuery();
            }
        }
    }