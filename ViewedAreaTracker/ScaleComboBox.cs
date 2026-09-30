using ArcGIS.Desktop.Framework.Contracts;
using System;
using System.Globalization;

namespace ViewedAreaTracker
{
    internal class ScaleComboBox : ComboBox
    {
        public static double MaximumScale { get; private set; }
            = double.MaxValue;


        public ScaleComboBox()
        {
            Add(new ComboBoxItem("All scales"));

            Add(new ComboBoxItem("1:1,000"));
            Add(new ComboBoxItem("1:2,000"));
            Add(new ComboBoxItem("1:5,000"));

            Add(new ComboBoxItem("1:10,000"));
            Add(new ComboBoxItem("1:20,000"));
            Add(new ComboBoxItem("1:50,000"));
            Add(new ComboBoxItem("1:70,000"));

            Add(new ComboBoxItem("1:100,000"));
            Add(new ComboBoxItem("1:200,000"));
            Add(new ComboBoxItem("1:300,000"));
            Add(new ComboBoxItem("1:400,000"));
            Add(new ComboBoxItem("1:500,000"));

            Add(new ComboBoxItem("1:750,000"));

            Add(new ComboBoxItem("1:1,000,000"));
            Add(new ComboBoxItem("1:1,500,000"));
            Add(new ComboBoxItem("1:2,000,000"));
            Add(new ComboBoxItem("1:3,000,000"));
            Add(new ComboBoxItem("1:5,000,000"));
            Add(new ComboBoxItem("1:10,000,000"));


            SelectedItem =
                ItemCollection[0];
        }


        protected override void OnSelectionChange(
            ComboBoxItem item)
        {
            if (item == null)
                return;


            // All scales = no upper limit
            if (item.Text == "All scales")
            {
                MaximumScale =
                    double.MaxValue;

                return;
            }

            string scaleText =
                item.Text
                    .Replace("1:", "")
                    .Replace(",", "");


            if (double.TryParse(
                    scaleText,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out double scale))
            {
                MaximumScale =
                    scale;
            }
            else
            {
                MaximumScale =
                    double.MaxValue;
            }
        }
    }
}