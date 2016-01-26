using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteSample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SQLiteSample
{
    internal static class Database
    {
        private static string dbPath = string.Empty;
        private static string DbPath
        {
            get
            {
                if (string.IsNullOrEmpty(dbPath))
                {
                    dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite");
                }

                return dbPath;
            }
        }

        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            }
        }

        /// <summary>
        /// Create the database for students
        /// </summary>
        public static void CreateDatabaseForStudents()
        {
            // Create a new connection
            using (var db = DbConnection)
            {
                var c = db.CreateTable<Student>();
                var info = db.GetMapping(typeof(Student));
            }
        }

        /// <summary>
        /// Create the database for grades
        /// </summary>
        public static void CreateDatabaseForGrades()
        {
            using (var db = DbConnection)
            {
                var c = db.CreateTable<Student>();
                var info = db.GetMapping(typeof(Student));
            }
        }

        /// <summary>
        /// Retrive all the Students in the Database
        /// </summary>
        /// <returns></returns>
        #region Students
        public static List<Student> GetAllStudents()
        {
            List<Student> list;

            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                list = (from p in db.Table<Student>()
                        select p).ToList();
            }

            return list;
        }

        /// <summary>
        /// Retrieve a student by Id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student</returns>
        public static Student SearchStudentById(int id)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                Student m = (from p in db.Table<Student>()
                             where p.Id == id
                             select p).FirstOrDefault();
                return m;
            }
        }

        /// <summary>
        /// Retrieve a student by Email
        /// </summary>
        /// <param name="email">Student Email</param>
        /// <returns></returns>
        public static Student SearchStudentByEmail(string email)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                Student m = (from p in db.Table<Student>()
                             where p.Email == email
                             select p).FirstOrDefault();
                return m;
            }
        }

        /// <summary>
        /// With a Student Id == 0, will create a new student
        /// </summary>
        /// <param name="student"></param>
        public static void AddOrUpdateStudent(Student student)
        {
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {

                if (student.Id == 0)
                {
                    // New
                    db.Insert(student);
                }
                else
                {
                    // Update
                    db.Update(student);
                }
            }
        }

        /// <summary>
        /// Add many students
        /// </summary>
        /// <param name="list"></param>
        public static void AddAllStudents(IEnumerable<string> list)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                db.InsertAll(list);
            }
        }

        /// <summary>
        /// Delete the database content
        /// </summary>
        public static void DeleteAllDatabase()
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                db.DeleteAll<Student>();
            }
        }

        /// <summary>
        /// Delete all students in the database
        /// </summary>
        /// <param name="students"></param>
        public static void DeleteStudents(IEnumerable<Student> students)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                foreach (var item in students)
                {
                    db.Delete(item);
                }
            }
        }

        /// <summary>
        /// Delete a student by email
        /// </summary>
        /// <param name="email">Student Email</param>
        public static void DeleteStudentByEmail(string email)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                Student m = (from p in db.Table<Student>()
                             where p.Email == email
                             select p).FirstOrDefault();
                if (m != null)
                    db.Delete(m);
            }
        }

        /// <summary>
        /// Delete a student by id
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteStudentById(int id)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                Student m = (from p in db.Table<Student>()
                             where p.Id == id
                             select p).FirstOrDefault();
                if (m != null)
                    db.Delete(m);
            }
        }

        /// <summary>
        /// Get the number of students in the database
        /// </summary>
        /// <returns></returns>
        public static int CountStudents()
        {
            int count = 0;
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                count = (from p in db.Table<Student>()
                         select p).Count<Student>();
            }

            return count;
        }
        #endregion

        #region Grades

        public static List<Grade> GetStudentGrades(int studentId)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                List<Grade> m = (from p in db.Table<Grade>()
                             where p.StudentId == studentId
                             select p).ToList();
                return m;
            }
        }

        public static void AddOrUpdateGradeToStudent(Grade grade)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {

                if (grade.Id == 0)
                {
                    // New
                    db.Insert(grade);
                }
                else
                {
                    // Update
                    db.Update(grade);
                }
            }
        }

        public static void DeleteGrade(int id)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                Student m = (from p in db.Table<Student>()
                             where p.Id == id
                             select p).FirstOrDefault();
                if (m != null)
                    db.Delete(m);
            }
        }

        #endregion
    }
}
