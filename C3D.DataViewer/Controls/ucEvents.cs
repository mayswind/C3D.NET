using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucEvents : UserControl
    {
        public ucEvents(C3DFile file)
        {
            InitializeComponent();

            C3DHeaderEvent[] events = file.Header.GetAllHeaderEvents();
            if (events != null && events.Length > 0)
            {
                for (Int32 i = 0; i < events.Length; i++)
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] { events[i].EventName, events[i].EventTime.ToString(), events[i].IsDisplay.ToString() }));
                }
            }
        }
    }
}