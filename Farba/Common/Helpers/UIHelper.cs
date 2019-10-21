using System;
using System.Windows;
using System.Threading.Tasks;

namespace Farba.Common.Helpers
{
    internal static class UIHelper
    {
        public static void UpdateUI(Action action)
        {
            if ((action == null) || (Application.Current == null))
            {
                return;
            }

            var dispatcher = Application.Current.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                try
                {
                    dispatcher.Invoke(action);
                }
                catch (TaskCanceledException) { }
            }
        }
    }
}
