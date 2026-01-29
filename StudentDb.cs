using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using StudentManagement.Models;


namespace StudentManagement
{
    public class StudentDb
    {
        string cs = ConfigurationManager.ConnectionStrings["StudentDbConnection"].ConnectionString;

        public List<Student> StudentsDetais()
        {
            List<Student> Stulist = new List<Student>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "Select * From Students";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Student student = new Student();
                    student.StudentId = Convert.ToInt32(dr.GetValue(0).ToString());
                    student.Name = dr.GetValue(1).ToString();
                    student.Email = dr.GetValue(2).ToString();
                    student.Age = Convert.ToInt32(dr.GetValue(3).ToString());
                    student.Gender = dr.GetValue(4).ToString();
                    student.Course = dr.GetValue(5).ToString();
                    Stulist.Add(student);
                }

                return Stulist;
            }
        }

        // INSERT Student
        public bool AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Students
                                 (Name, Email, Age, Gender, Course)
                                 VALUES
                                 (@Name, @Email, @Age, @Gender, @Course)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Course", student.Course);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }
        public bool EditStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Students
                                SET Name = @Name,Email = @Email,Age = @Age,Gender = @Gender,Course=@Course
                                Where StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Course", student.Course);
                con.Open();
                int row = cmd.ExecuteNonQuery();
                return row > 0;
            }
        }
        public bool DeleteStudent(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"DELETE FROM Students
                                WHERE StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", id);
                con.Open();
                int row = cmd.ExecuteNonQuery();
                return row > 0;
            }
        }
    }
}
