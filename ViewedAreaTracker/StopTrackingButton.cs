using ArcGIS.Desktop.Framework.Contracts;

namespace ViewedAreaTracker
{
    internal class StopTrackingButton : Button
    {
        protected override void OnClick()
        {
            ViewTracker.Stop();
        }
    }
}
