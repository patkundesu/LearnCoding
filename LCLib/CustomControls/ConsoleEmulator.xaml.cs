using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
namespace LCLib.CustomControls
{
    /// <summary>
    /// Interaction logic for ConsoleEmulator.xaml
    /// </summary>
    /// 
    public delegate void ProcessEventHandler(object sender, ProcessEventArgs args);
    public partial class ConsoleEmulator : UserControl
    {
        /// <summary>
        /// Line Numbers and Such
        /// </summary>
        private ProcessStartInfo procInf;
        private Process proc;
        private BackgroundWorker bgWorker;
        private BackgroundWorker errWorker;

        private bool procStart = false;

        private int inputStart = -1;

        private StreamWriter inputWriter;
        private TextReader outputReader;
        private TextReader errorReader;

        public event ProcessEventHandler OnInput;
        public event ProcessEventHandler OnOutput;
        public event ProcessEventHandler OnError;
        public event ProcessEventHandler OnExit;
        
        public ConsoleEmulator()
        {
            InitializeComponent();

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

            errWorker = new BackgroundWorker();
            errWorker.WorkerReportsProgress = true;
            errWorker.WorkerSupportsCancellation = true;
            errWorker.DoWork += new DoWorkEventHandler(errWorker_DoWork);
            errWorker.ProgressChanged += new ProgressChangedEventHandler(errWorker_ProgressChanged);
            errWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(errWorker_RunWorkerCompleted);
        }
        public void StartProcess(string filename, string args, string workingDir)
        {
            procInf = new ProcessStartInfo(filename, args);

            if (workingDir != "")
                procInf.WorkingDirectory = workingDir;

            procInf.UseShellExecute = false;
            procInf.ErrorDialog = false;
            procInf.CreateNoWindow = true;

            procInf.RedirectStandardError = true;
            procInf.RedirectStandardInput = true;
            procInf.RedirectStandardOutput = true;

            proc = new Process();
            proc.EnableRaisingEvents = true;
            proc.StartInfo = procInf;
            proc.Exited += new EventHandler(proc_Exit);
            procStart = proc.Start();

            inputWriter = proc.StandardInput;
            outputReader = TextReader.Synchronized(proc.StandardOutput);
            errorReader = TextReader.Synchronized(proc.StandardError);

            bgWorker.RunWorkerAsync();
            errWorker.RunWorkerAsync();
        }
        public void StopProcess()
        {
            if (!procStart)
                return;
            proc.Kill();
        }
        /// <summary>
        /// Background Worker Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bgWorker.CancellationPending)
            {
                int count = 0;
                char[] buffer = new char[1024];
                do
                {
                    StringBuilder str_b = new StringBuilder();
                    count = outputReader.Read(buffer, 0, 1024);
                    str_b.Append(buffer, 0, count);
                    bgWorker.ReportProgress(0, str_b.ToString());
                } while (count > 0);

                System.Threading.Thread.Sleep(200);
            }
        }

        public void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
            {
                WriteOutput(e.UserState as string);
                FireProcessOutput(new ProcessEventArgs());
            }
        }
        public void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FireProcessExit(new ProcessEventArgs());
        }
        /// <summary>
        /// Error Worker Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void errWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0;
            char[] buffer = new char[1024];
            do
            {
                StringBuilder str_b = new StringBuilder();
                count = errorReader.Read(buffer, 0, 1024);
                str_b.Append(buffer, 0, count);
                errWorker.ReportProgress(0, str_b.ToString());
            } while (count > 0);

            System.Threading.Thread.Sleep(200);
            FireProcessError(new ProcessEventArgs());
        }
        public void errWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
                WriteOutput(e.UserState as string);
        }
        public void errWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FireProcessExit(new ProcessEventArgs());
        }
        /// <summary>
        /// process events
        /// </summary>
        public void proc_Exit(object sender, EventArgs e)
        {
            procStart = !proc.HasExited;
            bgWorker.CancelAsync();
            errWorker.CancelAsync();
            inputWriter = null;
            outputReader = null;
            errorReader = null;
            proc = null;
        }
        public void WriteOutput(string output)
        {
            Text += output;
            inputStart = txt_console.Text.Length;
            txt_console.SelectionStart = inputStart;
        }
        public void WriteInput(string input)
        {
            if (procStart)
            {
                inputWriter.WriteLine(input);
                inputWriter.Flush();
            }
            FireProcessInput(new ProcessEventArgs(input));
        }
        public void ClearConsole()
        {
            if (!procStart)
            {
                txt_console.Text = "";
                inputStart = -1;
            }
        }
        private void txt_console_KeyDown(object sender, KeyEventArgs e)
        {
            bool read_only = (txt_console.SelectionStart < inputStart);
            if (!procStart || read_only)
                e.Handled = true;
            if (e.Key == Key.Return)
            {
                string input = txt_console.Text.Substring(inputStart, txt_console.Text.Length - inputStart);
                txt_console.Text += "\n";
                WriteInput(input);
            }
        }

        public string Text
        {
            get { return txt_console.Text; }
            set { txt_console.Text = value; }
        }

        public void FireProcessInput(ProcessEventArgs args)
        {
            var theEvent = OnInput;
            if (theEvent != null)
                theEvent(this, args);
        }
        public void FireProcessOutput(ProcessEventArgs args)
        {
            var theEvent = OnOutput;
            if (theEvent != null)
                theEvent(this, args);
        }
        public void FireProcessError(ProcessEventArgs args)
        {
            var theEvent = OnError;
            if (theEvent != null)
                theEvent(this, args);
        }
        public void FireProcessExit(ProcessEventArgs args)
        {
            var theEvent = OnExit;
            if (theEvent != null)
                theEvent(this, args);
        }

        private void txt_console_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool read_only = (txt_console.SelectionStart < inputStart);
            if (!procStart || read_only || (read_only && (e.Key == Key.Back || e.Key == Key.Space)))
                e.Handled = true;
            
        }
    }
    /// <summary>
    /// The ProcessEventArgs are arguments for a console event.
    /// </summary>
    /// 
    public class ProcessEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleEventArgs"/> class.
        /// </summary>
        public ProcessEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleEventArgs"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public ProcessEventArgs(string content)
        {
            //  Set the content and code.
            Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleEventArgs"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public ProcessEventArgs(int code)
        {
            //  Set the content and code.
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleEventArgs"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="code">The code.</param>
        public ProcessEventArgs(string content, int code)
        {
            //  Set the content and code.
            Content = content;
            Code = code;
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int? Code
        {
            get;
            private set;
        }
    }
}
