using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SocialMedia.Models;

public class DataAccessLayer
{
    string connectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

    public int RegisterUser(User user)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("RegisterUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@full_name", user.FullName);
            return command.ExecuteNonQuery();
        }
    }

    public int LoginUser(User user)
    {
        int userId = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("LoginUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                userId = Convert.ToInt32(result);
            }
        }
        return userId;
    }



    public User GetUserById(int userId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User] WHERE user_id = @userId", connection);
            command.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                // User found, create a User object and return it
                return new User
                {
                    UserId = (int)reader["user_id"],
                    Username = (string)reader["username"],
                    Password = (string)reader["password"],
                    Email = (string)reader["email"],
                    FullName = (string)reader["full_name"],
                    RegistrationDate = (DateTime)reader["registration_date"]
                };
            }
            else
            {
                // User not found, return null
                return null;
            }
        }
    }



    public int CreatePost(Post post)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("CreatePost", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user_id", post.UserId);
            command.Parameters.AddWithValue("@post_content", post.PostContent);
            return command.ExecuteNonQuery();
        }
    }

    public DataTable GetHomePagePosts()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetHomePagePosts", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }

    public DataTable GetUserProfile(int userId)
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetUserProfile", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user_id", userId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }

    public void DeletePostFromHomePage(int postId, int userId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("DeletePostFromHomePage", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@post_id", postId);
            command.Parameters.AddWithValue("@user_id", userId);
            command.ExecuteNonQuery();
        }
    }


}
