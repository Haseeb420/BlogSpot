using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BlogSpot.Models
{
    public class DBHandlerModel
    {

        #region forLogin of User and Admin
        public static bool UserLogin(LoginModel loginData)
        {
            bool loginStatus = false;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Users where Email=@email and Password=@password";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("email", loginData.Email);
            SqlParameter p2 = new SqlParameter("password", loginData.Password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    loginStatus = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return loginStatus;
        }

        #endregion

        #region to get user Id when new user registered
        public static int GetUserID(string email, string password)
        {
            int user_id = default;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select id from Users where Email=@email and Password=@password";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("email", email);
            SqlParameter p2 = new SqlParameter("password", password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    user_id = dr.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return user_id;
        }

        #endregion

        #region to get data of logged In User
        public static UserModel GetUserData(LoginModel loginData)
        {
            UserModel LoggedInUser = new UserModel();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Users where Email=@email and Password=@password";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("email", loginData.Email);
            SqlParameter p2 = new SqlParameter("password", loginData.Password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LoggedInUser.UserID = dr.GetInt32(0);
                    LoggedInUser.Rank = dr.GetInt32(1);
                    LoggedInUser.Name = dr.GetString(2);
                    LoggedInUser.DateOfBrith = dr.GetDateTime(3);
                    LoggedInUser.Email = dr.GetString(4);
                    LoggedInUser.Gender = dr.GetString(5);
                    LoggedInUser.Password = dr.GetString(6);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return LoggedInUser;
        }


        #endregion


        #region for admin to view all users
        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Users where Rank=2 ";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    users.Add(new UserModel
                    {
                        UserID = dr.GetInt32(0),
                        Rank = dr.GetInt32(1),
                        Name = dr.GetString(2),
                        DateOfBrith = dr.GetDateTime(3),
                        Email = dr.GetString(4),
                        Gender = dr.GetString(5),
                        Password = dr.GetString(6)
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        #endregion

        #region sign up db code
        public static bool AddNewUser(UserModel newUserData)
        {
            bool newUserStatus = false;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO USERS(RANK,NAME,DOB,EMAIL,GENDER,PASSWORD) VALUES (@rank,@name,@dob,@email,@gender,@password)";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("rank", 2);
            SqlParameter p2 = new SqlParameter("name", newUserData.Name);
            SqlParameter p3 = new SqlParameter("dob", newUserData.DateOfBrith);
            SqlParameter p4 = new SqlParameter("email", newUserData.Email);
            SqlParameter p5 = new SqlParameter("gender", newUserData.Gender);
            SqlParameter p6 = new SqlParameter("password", newUserData.Password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            try
            {
                conn.Open();
                int effected = cmd.ExecuteNonQuery();
                if (effected == 1)
                {
                    newUserStatus = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return newUserStatus;
        }


        #endregion

        #region to add new blog created by user
        public static bool AddNewBlog(BlogModel blog)
        {
            bool newBlogStatus = false;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO Blogs(AuthorName,Title,Content) VALUES (@author,@title,@content)";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("author", blog.AuthorName);
            SqlParameter p3 = new SqlParameter("title", blog.Title);
            SqlParameter p4 = new SqlParameter("content", blog.Content);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);

            try
            {
                conn.Open();
                int effected = cmd.ExecuteNonQuery();
                if (effected == 1)
                {
                    newBlogStatus = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return newBlogStatus;
        }

        #endregion


        #region to view all blogs created by specific user
        public static List<BlogModel> getBlogsByUsers(string authorName)
        {
            List<BlogModel> blogs = new List<BlogModel>();
            // BlogModel blog = new BlogModel();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Blogs where AuthorName=@authorName";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("authorName", authorName);
            cmd.Parameters.Add(p1);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    blogs.Add(new BlogModel
                    {
                        Blog_ID = dr.GetInt32(0),
                        AuthorName = dr.GetString(1),
                        PublicationDate = dr.GetDateTime(2),
                        Title = dr.GetString(3),
                        Content = dr.GetString(4)
                    });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            return blogs;
        }

        #endregion

        #region to get all view in desending order

        public static List<BlogModel> GetAllBlogs()
        {
            List<BlogModel> blogs = new List<BlogModel>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "Select * from Blogs order by Date desc;";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    blogs.Add(new BlogModel
                    {
                        Blog_ID = dr.GetInt32(0),
                        AuthorName = dr.GetString(1),
                        PublicationDate = dr.GetDateTime(2),
                        Title = dr.GetString(3),
                        Content = dr.GetString(4)
                    });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            return blogs;
        }
        #endregion

        #region for admin to delete a specific Useer

        public static bool deleteUserById(int user_id)
        {
            bool deleteUserFlag = false;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BlogSpot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "delete Users where Id=@user_id;";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlParameter p1 = new SqlParameter("user_id", user_id);
            cmd.Parameters.Add(p1);
            try
            {
                conn.Open();
                int effected = cmd.ExecuteNonQuery();
                if (effected == 1)
                {
                    deleteUserFlag = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            return deleteUserFlag;
        }
        #endregion

    }
}
