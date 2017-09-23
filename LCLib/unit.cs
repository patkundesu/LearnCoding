using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCLib
{
    public class page
    {
        private int i_id = 0;
        private string i_content = "N/A";
        private string i_type = "text";
        private string i_lang = "N/A";
        private string i_class = "N/A";
        private i_codes i_codes = new i_codes();
        private System.Drawing.Image i_img;
        private i_links i_links = new i_links();

        public int ID
        {
            set { i_id = value; }
            get { return i_id; }
        }
        public string Content
        {
            set { i_content = value; }
            get { return i_content; }
        }
        public string PageType
        {
            set { i_type = value; }
            get { return i_type; }
        }
        public string MainLanguage
        {
            set { i_lang = value; }
            get { return i_lang; }
        }
        public string ClassName
        {
            set { i_class = value; }
            get { return i_class; }
        }
        public i_codes Codes
        {
            set { i_codes = value; }
            get { return i_codes; }
        }
        public System.Drawing.Image Image
        {
            set { i_img = value; }
            get { return i_img; }
        }
        public i_links Links
        {
            set { i_links = value; }
            get { return i_links; }
        }
    }
    public class i_codes
    {
        private string i_java = "N/A";
        private string i_cpp = "N/A";
        private string i_html = "N/A";
        private string i_js = "N/A";
        private string i_php = "N/A";
        public string Java
        {
            set { i_java = value; }
            get { return i_java; }
        }
        public string Cpp
        {
            set { i_cpp = value; }
            get { return i_cpp; }
        }
        public string HTML
        {
            set { i_html = value; }
            get { return i_html; }
        }
        public string JavaScript
        {
            set { i_js = value; }
            get { return i_js; }
        }
        public string PHP
        {
            set { i_php = value; }
            get { return i_php; }
        }
    }
    public class i_links
    {
        private string i_vid = "N/A";
        private string i_src = "N/A";
        public string Video
        {
            set { i_vid = value; }
            get { return i_vid; }
        }
        public string Source
        {
            set { i_src = value; }
            get { return i_src; }
        }
    }
    public class lesson
    {
        private int l_no = 0;
        private string l_topic= "";
        private string l_duration = "";
        private List<page> l_infos = new List<page>();
        private test l_test = new test();
        public int Number
        {
            set { l_no = value; }
            get { return l_no; }
        }
        public string Topic
        {
            set { l_topic = value; }
            get { return l_topic; }
        }
        public string Duration
        {
            set { l_duration = value; }
            get { return l_duration; }
        }
        public List<page> Pages
        {
            set { l_infos = value; }
            get { return l_infos; }
        }
        public test Test
        {
            set { l_test = value; }
            get { return l_test; }
        }
    }
    public class unit
    {
        private string u_pl = "";
        private string u_title = "";
        private int u_no = 0;
        private List<lesson> u_lesson = new List<lesson>();
        public string Language
        {
            set { u_pl = value; }
            get { return u_pl; }
        }
        public string Title
        {
            set { u_title = value; }
            get { return u_title; }
        }
        public int Number
        {
            set { u_no = value; }
            get { return u_no; }
        }
        public List<lesson> Lessons
        {
            set { u_lesson = value; }
            get { return u_lesson; }
        }
    }
}
