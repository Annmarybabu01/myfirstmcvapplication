using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Myfirstmvcapplication.Models;
using System.Security.Cryptography.X509Certificates;

namespace Myfirstmvcapplication.DAL
{
    public class SignUpDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["SignUpConnection"].ToString();
        public List<SignUpModel> GetAllDetails()
        {
            List<SignUpModel> detailList = new List<SignUpModel>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SignUpSelect";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtSignUp = new DataTable();
                connection.Open();
                sqlDA.Fill(dtSignUp);
                foreach (DataRow dr in dtSignUp.Rows) {
                    detailList.Add(new SignUpModel
                    {
                        id = Convert.ToInt32(dr["id"]),
                        firstName = dr["firstName"].ToString(),
                        lastNmae = dr["lastNmae"].ToString(),
                        phoneNumber = dr["phoneNumber"].ToString(),
                        email = dr["email"].ToString(),
                        password = dr["password"].ToString()
                    });
                }
            }
            return detailList;
        }
        public bool InsertSignUp(SignUpModel signupentry)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SignUpInsert",connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstName",signupentry.firstName);
                cmd.Parameters.AddWithValue("@lastNmae",signupentry.lastNmae);
                cmd.Parameters.AddWithValue("@phoneNumber",signupentry.phoneNumber);
                cmd.Parameters.AddWithValue("@email",signupentry.email);
                cmd.Parameters.AddWithValue("@password",signupentry.password);
                connection.Open();
                id=cmd.ExecuteNonQuery();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<SignUpModel> GetSignUpById(int id)
        {
            List<SignUpModel> detailList = new List<SignUpModel>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetElementById";
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
                DataTable dtSignUp = new DataTable();
                connection.Open();
                sqlDA.Fill(dtSignUp);
                foreach (DataRow dr in dtSignUp.Rows)
                {
                    detailList.Add(new SignUpModel
                    {
                        id = Convert.ToInt32(dr["id"]),
                        firstName = dr["firstName"].ToString(),
                        lastNmae = dr["lastNmae"].ToString(),
                        phoneNumber = dr["phoneNumber"].ToString(),
                        email = dr["email"].ToString(),
                        password = dr["password"].ToString()
                    });
                }
            }
            return detailList;
        }
        //
        public bool UpdateSignUp(SignUpModel signupentry)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SignUpUpdate", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", signupentry.id);
                cmd.Parameters.AddWithValue("@firstName", signupentry.firstName);
                cmd.Parameters.AddWithValue("@lastNmae", signupentry.lastNmae);
                cmd.Parameters.AddWithValue("@phoneNumber", signupentry.phoneNumber);
                cmd.Parameters.AddWithValue("@email", signupentry.email);
                cmd.Parameters.AddWithValue("@password", signupentry.password);
                connection.Open();
                i = cmd.ExecuteNonQuery();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string DeleteSignUp(int id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd= new SqlCommand("SignUpDelete", connection);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.Add("@returnMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                connection.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@returnMessage"].Value.ToString();
                connection.Close();
            }
            return result;
        }
    }
}