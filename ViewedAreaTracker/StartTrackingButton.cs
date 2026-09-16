using ArcGIS.Desktop.Framework.Contracts;

using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace ViewedAreaTracker
{
    internal class StartTrackingButton : Button
    {
        protected override void OnClick()
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                "Tracking has started!",
                "Viewed Area Tracker");

            ViewTracker.Start();
        }
    }
}