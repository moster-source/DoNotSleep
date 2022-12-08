/*
 * Program to prevent windows to turn off screen or go to standby
 * tested on windows 10
 * should be working on all windows
 * 
 * it seems program is working without need to call function repetedly
 * so timer not needed
 * 
 * 
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DoNotSleep.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DoNotSleep
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
        // timer initialisation
        //public static System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();

            /* //it seems timer not needed
            t.Interval = 50000; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            
            // for now not needed
            t.Enabled = false;
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            PreventSleep();
             
        }

        void PreventSleep()
        {   
            //some testing "EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS" worked too
            // Prevent Idle-to-Sleep (monitor not affected) (see note above)
            //SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            //SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
           
                    //SystemSounds.Beep.Play();
                    //PreventSleep();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //destroy everything and return normal working standby

            //t.Stop();
            //t.Dispose();
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            //MessageBoxButtons buttons = MessageBoxButtons.OK;
            //DialogResult result;
            //result = MessageBox.Show("Closing", "Disposing timer and closing program", buttons);
        }

        
    }

 
}
