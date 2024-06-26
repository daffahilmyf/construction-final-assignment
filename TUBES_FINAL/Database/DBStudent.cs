﻿namespace TUBES_FINAL.Database
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TUBES_FINAL.Model;

    public static class DBStudent
    {
       
        private static List<StudentModel> studentList;
        private static StudentModel studentData;
        private static string queryString;

        public static List<StudentModel> GetAllStudent()
        {
            try
            {
                studentList = new List<StudentModel>();
                queryString = "SELECT * FROM mahasiswa";

                DBConn.Connection.Open();
                DBConn.Command = new MySqlCommand(queryString, DBConn.Connection);
                DBConn.Reader = DBConn.Command.ExecuteReader();

                while (DBConn.Reader.Read())
                {
                    string id = DBConn.Reader["id_mahasiswa"].ToString();
                    string nama = DBConn.Reader["nama_mahasiswa"].ToString();
                    string email = DBConn.Reader["email_mahasiswa"].ToString();
                    string password = DBConn.Reader["password_mahasiswa"].ToString();
                    string nim = DBConn.Reader["nim_mahasiswa"].ToString();
                    string tahun = DBConn.Reader["tahun_mahasiswa"].ToString();

                    studentList.Add(
                        new StudentModel(
                            id, nama, email, password, nim, tahun
                        )
                    );

                }

                DBConn.Reader.Close();
                DBConn.Connection.Close();

                return studentList;

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }

        public static StudentModel GetStudentByNIM(string NIM)
        {
            try
            {
                queryString = $"SELECT * FROM mahasiswa WHERE nim_mahasiswa = '{NIM}'";

                DBConn.Connection.Open();
                DBConn.Command = new MySqlCommand(queryString, DBConn.Connection);
                DBConn.Reader = DBConn.Command.ExecuteReader();

                while (DBConn.Reader.Read())
                {
                    string id = DBConn.Reader["id_mahasiswa"].ToString();
                    string nama = DBConn.Reader["nama_mahasiswa"].ToString();
                    string email = DBConn.Reader["email_mahasiswa"].ToString();
                    string password = DBConn.Reader["password_mahasiswa"].ToString();
                    string nim = DBConn.Reader["nim_mahasiswa"].ToString();
                    string tahun = DBConn.Reader["tahun_mahasiswa"].ToString();

                    studentData = new StudentModel(id, nama, email, password, nim, tahun);

                }

                DBConn.Reader.Close();
                DBConn.Connection.Close();
                return studentData;

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }

        }
        public static void InsertStudent(StudentModel student)
        {
            try
            {
                queryString = $"INSERT INTO mahasiswa (nama_mahasiswa, nim_mahasiswa, email_mahasiswa, password_mahasiswa, tahun_mahasiswa)" +
                    $" VALUES ('{student.PersonName}', '{student.StudentNIM}', '{student.PersonEmail}', '{student.PersonPassword}', '{student.StudentYear}')";
                DBConn.Connection.Open();
                DBConn.Command = new MySqlCommand(queryString, DBConn.Connection);
                DBConn.Command.ExecuteNonQuery();
                DBConn.Connection.Close();

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }

            Console.WriteLine($"Insert data {student.StudentNIM} success");

        }

        public static void UpdateStudent(string index, StudentModel student)
        {
            try
            {
                queryString =   $"UPDATE mahasiswa SET nama_mahasiswa = '{student.PersonName}', " +
                                $"email_mahasiswa = '{student.PersonEmail}', " +
                                $"password_mahasiswa = '{student.PersonPassword}' " +
                                $"WHERE `nim_mahasiswa` = {index};";

                DBConn.Connection.Open();
                DBConn.Command = new MySqlCommand(queryString, DBConn.Connection);
                DBConn.Command.ExecuteNonQuery();
                DBConn.Connection.Close();
                Console.WriteLine($"Update data {index} success");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
        }
    }
}
