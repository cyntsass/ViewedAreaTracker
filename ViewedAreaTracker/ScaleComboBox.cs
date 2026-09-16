using ArcGIS.Desktop.Framework.Contracts;

namespace ViewedAreaTracker
{
    internal class ScaleComboBox : ComboBox
    {
        public static double MaximumScale { get; private set; } = double.MaxValue;

        public ScaleComboBox()
        {
            Add(new ComboBoxItem("All scales"));
            Add(new ComboBoxItem("1:1,000"));
            Add(new ComboBoxItem("1:2,000"));
            Add(new ComboBoxItem("1:5,000"));
            Add(new ComboBoxItem("1:10,000"));
            Add(new ComboBoxItem("1:20,000"));
            Add(new ComboBoxItem("1:50,000"));
            Add(new ComboBoxItem("1:100,000"));

            SelectedItem = ItemCollection[0];
        }

        protected override void OnSelectionChange(ComboBoxItem item)
        {
            if (item == null)
                return;

            switch (item.Text)
            {
                case "1:1,000":
                    MaximumScale = 1000;
                    break;

                case "1:2,000":
                    MaximumScale = 2000;
                    break;

                case "1:5,000":
                    MaximumScale = 5000;
                    break;

                case "1:10,000":
                    MaximumScale = 10000;
                    break;

                case "1:20,000":
                    MaximumScale = 20000;
                    break;

                case "1:50,000":
                    MaximumScale = 50000;
                    break;

                case "1:100,000":
                    MaximumScale = 100000;
                    break;

                default:
                    MaximumScale = double.MaxValue;
                    break;
            }
        }
    }
}