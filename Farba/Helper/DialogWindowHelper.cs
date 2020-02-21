using System.Windows.Forms;

namespace Farba.Helper
{
    internal static class DialogWindowHelper
    {
        public static string[] FileDialog(FileFilter filter, bool multiSelect = false)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = multiSelect;
                dialog.Filter = GetFilterString(filter);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileNames;
                }
            }

            return default;
        }

        public static string FolderBrowseDialog()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }

            return default;
        }

        public static void MessageInfoDialog(string info)
        {
            var caption = "Export";
            var button = MessageBoxButtons.OK;
            var icon = MessageBoxIcon.Information;

            MessageBox.Show(info, caption, button, icon);
        }

        private static string GetFilterString(FileFilter filter)
        {
            switch (filter)
            {
                case FileFilter.Images:
                    return "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                default:
                    return string.Empty;
            }
        }
    }

    internal enum FileFilter
    {
        Images
    }
}