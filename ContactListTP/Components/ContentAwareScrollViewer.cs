using System.Windows;
using System.Windows.Controls;

namespace ContactListTP.Components
{
    public class ContentAwareScrollViewer : ScrollViewer
    {
        public static readonly RoutedEvent ContentChangedRoutedEvent = EventManager.RegisterRoutedEvent(nameof(RoutedContentChanged), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ContentAwareScrollViewer));

        static ContentAwareScrollViewer()
        {
            ContentProperty.OverrideMetadata(typeof(ContentAwareScrollViewer), new FrameworkPropertyMetadata(OnContentChanged));
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (ContentAwareScrollViewer) d;
            if (viewer.ContentChangedEvent != null)
            {
                var args = new DependencyPropertyChangedEventArgs(ContentProperty, e.OldValue, e.NewValue);
                viewer.ContentChangedEvent(viewer, args);
            }

            viewer.RaiseEvent(new RoutedEventArgs(ContentChangedRoutedEvent));
        }

        public event DependencyPropertyChangedEventHandler ContentChangedEvent;

        public event RoutedEventHandler RoutedContentChanged
        {
            add => AddHandler(ContentChangedRoutedEvent, value);
            remove => RemoveHandler(ContentChangedRoutedEvent, value);
        }
    }
}