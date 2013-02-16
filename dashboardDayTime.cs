using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CoreUtilities;


namespace Worklog
{
    public partial class dashboardDayTime : UserControl
    {
		Action<string> RefreshPanels=null;
		public dashboardDayTime(Action<string> _RefreshPanels)
        {
			RefreshPanels=_RefreshPanels;
            InitializeComponent();
		// we set the date external so we don't have crash if things not initliazed
        }

		public void SetDate ()
		{
			currentDate = DateTime.Today;
		}

        public void _Init()
           
        {
            // so we have a revious value
            m_currentDate = DateTime.Today;
            currentDate = DateTime.Today;

        }

//        public delegate void delegateDateChanged(DateTime _date, DateTime _previous);
//        private event delegateDateChanged DateChanged;
//
//        protected virtual void OnDateChanged(DateTime _date, DateTime _previous)
//        {
//            if (DateChanged != null)
//            {
//                DateChanged(_date, _previous);
//            }
//            
//        }


        private DateTime m_currentDate = DateTime.Today;
        /// <summary>
        /// current selected date on calendar
        /// defaults to today
        /// </summary>
        public DateTime currentDate
        {
            get
			{
                return m_currentDate;
                // load fields from disc
                // CANNOT happen here... must figure out where I can trigger this
            }
            set
            {
                
                //SaveCurrentDiary();


               // OnDateChanged(value, m_currentDate);
                m_currentDate = value;
                

                Year.Text = currentDate.Year.ToString();
                Day.Text = currentDate.Day.ToString();
                Month.Text = currentDate.ToString("MMMM");
                DayOf.Text = currentDate.DayOfWeek.ToString();
                //everytime date is sent I need to build a new dataview
                //textBox5.Text = EventTable.GetDatesStats(m_currentDate);

				if (RefreshPanels != null)
				{
					RefreshPanels("");
				}
            }
        }
    
        private void bJumpToDate_Click(object sender, EventArgs e)
        {
            currentDate = DateTime.Today;
         
        }

        private void bDaysPrevious_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
           
        }

        private void bDaysNExt_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
           
        }

        /// <summary>
        /// Pick a date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            //completely create unique form?
            Form fDate = new Form();
            fDate.Height = 90;
            fDate.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            fDate.Text = "Select Date";
            DateTimePicker picker = new DateTimePicker();
            fDate.Controls.Add(picker);
            picker.Dock = DockStyle.Top;
            Button bOk = new Button();
            bOk.DialogResult = DialogResult.OK;
            fDate.Controls.Add(bOk);
            bOk.Dock = DockStyle.Fill;
            bOk.Text = "OK";

            if (fDate.ShowDialog() == DialogResult.OK)
            {
                currentDate = picker.Value.Date;
                
            }


        }

    }
}
