using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCLib
{
    public class question
    {
        private int q_id = 0;
        private string q_given = "";
        private string q_type = "";
        private string q_lang = "";
        private string q_class = "";
        private string q_code = "";
        private int q_tries = 0;
        private List<string> q_choices = new List<string>();
        private string q_ans = "";
        private q_misc q_misc = new q_misc();
        public int ID
        {
            set { q_id = value; }
            get { return q_id; }
        }
        public string Given
        {
            set { q_given = value; }
            get { return q_given; }
        }
        public string Type
        {
            set { q_type = value; }
            get { return q_type; }
        }
        public string Language
        {
            set { q_lang = value; }
            get { return q_lang; }
        }
        public string ClassName
        {
            set { q_class = value; }
            get { return q_class; }
        }
        public string Code
        {
            set { q_code = value; }
            get { return q_code; }
        }
        public int Tries
        {
            set { q_tries = value; }
            get { return q_tries; }
        }
        public List<string> Choices
        {
            set { q_choices = value; }
            get { return q_choices; }
        }
        public string Answer
        {
            set { q_ans = value; }
            get { return q_ans; }
        }
        public q_misc Misc
        {
            set { q_misc = value; }
            get { return q_misc; }
        }
    }
    public class q_misc
    {
        private int q_eslimit = 0;
        private List<string> q_idright = new List<string>();
        private List<string> q_cdin = new List<string>();
        private List<string> q_cdout = new List<string>();
        public int EssayLimit
        {
            set { q_eslimit = value; }
            get { return q_eslimit; }
        }
        public List<string> IdentificationKeywords
        {
            set { q_idright = value; }
            get { return q_idright; }
        }
        public List<string> CodingInputs
        {
            set { q_cdin = value; }
            get { return q_cdin; }
        }
        public List<string> CodingOutputs
        {
            set { q_cdout = value; }
            get { return q_cdout; }
        }
    }
    public class test
    {
        private int t_number = 0;
        private string t_instruction = "";
        private List<question> a_questions = new List<question>();

        public int Number
        {
            set { t_number = value; }
            get { return t_number; }
        }
        public string Instruction
        {
            set { t_instruction = value; }
            get { return t_instruction; }
        }
        public List<question> Questions
        {
            set { a_questions = value; }
            get { return a_questions; }
        }

    }
}
