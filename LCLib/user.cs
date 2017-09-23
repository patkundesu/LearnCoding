using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCLib
{
    public class lc_user
    {
        private string u_username = "";
        private string u_password = "";
        private DateTime u_created = new DateTime();
        private progress u_progress = new progress();
        private int u_index = 0;
        private unit_stats u_fund = new unit_stats();
        private unit_stats u_java = new unit_stats();
        private unit_stats u_cpp = new unit_stats();
        private unit_stats u_html = new unit_stats();
        private unit_stats u_js = new unit_stats();
        private unit_stats u_php = new unit_stats();
        private bool u_ongoing = false;
        /// <summary>
        /// The username of the User
        /// </summary>
        public string Username
        {
            get { return u_username; }
            set { u_username = value; }
        }
        /// <summary>
        /// The password of the User
        /// </summary>

        public string Password
        {
            get { return u_password; }
            set { u_password = value; }
        }
        /// <summary>
        /// The date of creation of the User acount
        /// </summary>
        public DateTime Created
        {
            get { return u_created; }
            set { u_created = value; }
        }
        /// <summary>
        /// The current progress of the User
        /// </summary>
        public progress Progress
        {
            set { u_progress = value; }
            get { return u_progress; }
        }
        /// <summary>
        /// The Index of the User in the Users list
        /// </summary>
        public int Index
        {
            set { u_index = value; }
            get { return u_index; }
        }
        /// <summary>
        /// The Status of Fundamental Lesson
        /// </summary>
        public unit_stats FundamentalStats
        {
            set { u_fund = value; }
            get { return u_fund; }
        }
        /// <summary>
        /// The Status of Java Lesson
        /// </summary>
        public unit_stats JavaStats
        {
            set { u_java = value; }
            get { return u_java; }
        }
        /// <summary>
        /// The Status of C++ Lesson
        /// </summary>
        public unit_stats CppStats
        {
            set { u_cpp = value; }
            get { return u_cpp; }
        }
        /// <summary>
        /// The Status of HTML Lesson
        /// </summary>
        public unit_stats HTMLStats
        {
            set { u_html = value; }
            get { return u_html; }
        }
        /// <summary>
        /// The Status of JavaScript Lesson
        /// </summary>
        public unit_stats JavaScriptStats
        {
            set { u_js = value; }
            get { return u_js; }
        }
        /// <summary>
        /// The Status of PHP Lesson
        /// </summary>
        public unit_stats PHPStats
        {
            set { u_php = value; }
            get { return u_php; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool LessonOnGoing
        {
            set { u_ongoing = value; }
            get { return u_ongoing; }
        }
    }

    public class progress
    {
        private unit u_current_unit = new unit();
        private int u_current_lesson = 0;
        private int u_current_page = 0;
        /// <summary>
        /// The Current Unit of the Course
        /// </summary>
        public unit Current_Unit
        {
            get { return u_current_unit; }
            set { u_current_unit = value; }
        }
        /// <summary>
        /// The Current Lesson of the Unit
        /// </summary>
        public int Current_Lesson
        {
            get { return u_current_lesson; }
            set { u_current_lesson = value; }
        }
        /// <summary>
        /// The Current Page of the Lesson
        /// </summary>
        public int Current_Page
        {
            get { return u_current_page; }
            set { u_current_page = value; }
        }
    }

    public class unit_stats
    {
        private int clesson = 0;
        private int cpage = 0;
        private double percentage = 0.0;
        private bool finished = false;
        private List<test_stats> u_tests = new List<test_stats>();
        /// <summary>
        /// The Current Lesson of the Unit
        /// </summary>
        public int CurrentLesson
        {
            set { clesson = value; }
            get { return clesson; }
        }
        /// <summary>
        /// The Current Page of the Lesson
        /// </summary>
        public int CurrentPage
        {
            set { cpage = value; }
            get { return cpage; }
        }
        /// <summary>
        /// The Percentage finished of the Unit
        /// </summary>
        public double Percentage
        {
            set { percentage = value; }
            get { return percentage; }
        }
        /// <summary>
        /// Checks if the Unit is already finished
        /// </summary>
        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }
        /// <summary>
        /// The list of Test of the Unit
        /// </summary>
        public List<test_stats> Tests
        {
            set { u_tests = value; }
            get { return u_tests; }
        }
    }
    public class test_stats
    {
        private int t_number = 0;
        private int t_score = 0;
        private double t_percentage = 0.0;
        /// <summary>
        /// The lesson number of the Test
        /// </summary>
        public int TestNumber
        {
            set { t_number = value; }
            get { return t_number; }
        }
        /// <summary>
        /// The score in the Test
        /// </summary>
        public int Score
        {
            set { t_score = value; }
            get { return t_score; }
        }
        /// <summary>
        /// The percentage of the total score in the test
        /// </summary>
        public double Percentage
        {
            set { t_percentage = value; }
            get { return t_percentage; }
        }
    }
}
