using System;
using System.Collections.Generic;
using ArcGIS.Core.Events;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;

namespace ViewedAreaTracker
{
    internal static class ViewTracker
    {
        private static SubscriptionToken _cameraToken;
        private static bool _recording = false;

        public static void Start()
        {
            if (_recording)
                return;

            _cameraToken =
                MapViewCameraChangedEvent.Subscribe(OnCameraChanged);

            _recording = true;
        }

        public static void Stop()
        {
            if (!_recording)
                return;

            if (_cameraToken != null)
                MapViewCameraChangedEvent.Unsubscribe(_cameraToken);

            _cameraToken = null;
            _recording = false;

            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                "Tracking stopped.",
                "Viewed Area Tracker");
        }

        private static async void OnCameraChanged(
            MapViewCameraChangedEventArgs args)
        {
            if (!_recording)
                return;

            MapView mapView = MapView.Active;

            if (mapView == null)
                return;

            Envelope extent = mapView.Extent;

            if (extent == null)
                return;

            double scale = mapView.Camera.Scale;
            if (scale > ScaleComboBox.MaximumScale)
                return;

            await QueuedTask.Run(() =>
            {
                var layers = mapView.Map.FindLayers("Viewed_Areas");

                if (layers.Count == 0)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                        "Could not find a layer named Viewed_Areas.",
                        "Viewed Area Tracker");
                    return;
                }

                FeatureLayer layer = layers[0] as FeatureLayer;

                if (layer == null)
                    return;

                Polygon polygon =
                    PolygonBuilderEx.CreatePolygon(extent);

                var attributes =
                    new Dictionary<string, object>
                    {
                        { "ViewedTime", DateTime.Now },
                        { "Scale", scale }
                    };

                var operation = new EditOperation
                {
                    Name = "Record viewed extent"
                };

                operation.Create(
                    layer,
                    polygon,
                    attributes);

                if (!operation.IsEmpty)
                {
                    bool success = operation.Execute();

                    if (!success)
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                            operation.ErrorMessage,
                            "Tracking error");
                    }
                    else
                    {
                        mapView.Map.ClearSelection();
                    }
                }
            });
        }
    }
}