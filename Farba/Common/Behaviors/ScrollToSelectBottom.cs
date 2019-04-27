namespace Farba.Common.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Threading;
    using System.Threading.Tasks;
    using System.Windows.Interactivity;
    using System.Windows.Controls;

    class ScrollToSelectBottom : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += OnSelection;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelection;
        }

        private void OnSelection(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if(listBox != null)
            {
                Action action = () =>
                {
                    var selected= listBox.SelectedItem;
                    if (selected != null)
                    {
                        listBox.ScrollIntoView(selected);
                    }
                };
                listBox.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
            }
        }
    }
}
