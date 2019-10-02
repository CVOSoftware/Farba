using System.Windows.Forms;

namespace Farba.Helpers
{
    internal static class DialogWindowHelper
    {
        public static string FileDialog(FileFilter filter)
        {
            using(var dialog = new OpenFileDialog())
            {
                dialog.Filter = GetFilterString(filter);
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }
            return string.Empty;
        }

        private static string GetFilterString(FileFilter filter)
        {
            switch(filter)
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
