using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CustomNotificationSystem.Common
{
    public class PageBase : Page
    {
        private const string StateKey = "State";

        private readonly NavigationHelper _navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get
            {
                return _navigationHelper;
            }
        }


        private StackPanel _panel;
        private TextBlock _textBlock;

        public PageBase()
        {

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += NavigationHelperLoadState;
            _navigationHelper.SaveState += NavigationHelperSaveState;

            //instantiate and create StackPanel and TextBlock
            //you can put anything you want in the panel
            _panel = new StackPanel()
            {
                Background = new SolidColorBrush(Colors.Blue),
                Visibility = Visibility.Collapsed,
            };

            _textBlock = new TextBlock()
            {
                FontSize = 20,
                Margin = new Thickness(39, 10, 10, 10),
                TextAlignment = TextAlignment.Center
            };
            _panel.Children.Add(_textBlock);
        }

        /// <summary>
        /// Gets a list DependencyObject from the Visual Tree
        /// </summary>
        /// <typeparam name="T">the type of the desired object</typeparam>
        /// <param name="results">List of children</param>
        /// <param name="startNode">the DependencyObject to start the search with</param>
        public static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                var current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()) == typeof(T) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }



        /// <summary>
        /// global property to bind the notification text against
        /// </summary>
        public static readonly DependencyProperty AppNotificationTextProperty = DependencyProperty.Register(
            "AppNotificationText", typeof (string), typeof (PageBase), new PropertyMetadata(string.Empty, (s, e) =>
            {
                var current = s as PageBase;

                if (current == null)
                {
                    return;
                }

                current.CheckifNotificationMessageIsNeeded(s);
            }));

        /// <summary>
        /// gets or sets the AppNotificationText
        /// </summary>
        public string AppNotificationText
        {
            get { return (string)GetValue(AppNotificationTextProperty); }
            set { SetValue(AppNotificationTextProperty, value); }}


        /// <summary>
        /// handles the visibility of the notification
        /// </summary>
        /// <param name="currentDependencyObject">the primary depenedency object to start with</param>
        private void CheckifNotificationMessageIsNeeded(DependencyObject currentDependencyObject)
        {
            if (currentDependencyObject == null) return;

            var children = new List<DependencyObject>();
            FindChildren(children, currentDependencyObject);
            if (children.Count == 0) return;

            var rootGrid = (Grid)children.FirstOrDefault(i => i.GetType() == typeof(Grid));

            if (rootGrid != null)

                if (!string.IsNullOrEmpty(AppNotificationText))
                {
                    if (!rootGrid.Children.Contains(_panel))
                    {
                        rootGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(_panel.ActualHeight, GridUnitType.Auto)});
                        _panel.SetValue(Grid.RowProperty, rootGrid.RowDefinitions.Count);

                        rootGrid.Children.Add(_panel);
                    }

                    _textBlock.Text = AppNotificationText;
                    _panel.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrEmpty(AppNotificationText))
                {
                    _textBlock.Text = string.Empty;
                    _panel.Visibility = Visibility.Collapsed;
                }
        }




        protected virtual void LoadState(object state)
        {
        }

        protected void NavigationHelperLoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null
                && e.PageState.ContainsKey(StateKey))
            {
                LoadState(e.PageState[StateKey]);
            }
        }

        protected void NavigationHelperSaveState(object sender, SaveStateEventArgs e)
        {
            if (e.PageState == null)
            {
                throw new InvalidOperationException("PageState is null");
            }

            if (e.PageState.ContainsKey(StateKey))
            {
                e.PageState.Remove(StateKey);
            }

            var state = SaveState();

            if (state != null)
            {
                e.PageState.Add(StateKey, state);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected virtual object SaveState()
        {
            return null;
        }


    }
}
