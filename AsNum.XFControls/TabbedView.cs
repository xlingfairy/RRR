using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class TabbedView : ContentView {

        #region itemsSource
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(TabbedView),
                null,
                propertyChanged: ItemsSourceChanged);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabs();
            tv.UpdateChildren();

            if (newValue is INotifyCollectionChanged) {
                var newCollection = (INotifyCollectionChanged)newValue;
                tv.InitCollection(newCollection);
            }
        }
        #endregion

        #region TabTemplate
        public static readonly BindableProperty TabTemplateProperty =
            BindableProperty.Create("TabTemplate",
                typeof(DataTemplate),
                typeof(TabbedView),
                null,
                propertyChanged: TabTemplateChanged);

        public DataTemplate TabTemplate {
            get {
                return (DataTemplate)GetValue(TabTemplateProperty);
            }
            set {
                SetValue(TabTemplateProperty, value);
            }
        }

        private static void TabTemplateChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabs();
        }
        #endregion

        #region ItemTemplate
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(TabbedView),
                null);

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion

        #region itemTemplateSelector
        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabbedView),
                null);

        public DataTemplateSelector ItemTemplateSelector {
            get {
                return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            }
            set {
                SetValue(ItemTemplateSelectorProperty, value);
            }
        }
        #endregion


        #region TabTemplateSelector
        public static readonly BindableProperty TabTemplateSelectorProperty =
            BindableProperty.Create("TabTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabbedView),
                null);

        public DataTemplateSelector TabTemplateSelector {
            get {
                return (DataTemplateSelector)GetValue(TabTemplateSelectorProperty);
            }
            set {
                SetValue(TabTemplateSelectorProperty, value);
            }
        }
        #endregion

        #region selectedItem
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(TabbedView),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        public object SelectedItem {
            get {
                return (object)GetValue(SelectedItemProperty);
            }
            set {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {

        }
        #endregion

        #region tabPosition
        public static readonly BindableProperty TabPositionProperty =
            BindableProperty.Create("TabPosition",
                typeof(TabPositions),
                typeof(TabbedView),
                TabPositions.Top,
                propertyChanged: TabPositionChanged);

        public TabPositions TabPosition {
            get {
                return (TabPositions)(this.GetValue(TabPositionProperty));
            }
            set {
                this.SetValue(TabPositionProperty, value);
            }
        }

        private static void TabPositionChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabPosition();
        }
        #endregion





        private Grid ChildrenContainer = null;
        private ScrollView TabScroller = null;
        private StackLayout TabContainer = null;

        public TabbedView() {
            var grid = new Grid();
            this.Content = grid;

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            this.ChildrenContainer = new Grid();
            grid.Children.Add(this.ChildrenContainer);

            this.TabScroller = new ScrollView();
            grid.Children.Add(this.TabScroller);

            this.TabContainer = new StackLayout();
            this.TabScroller.Content = this.TabContainer;

            this.UpdateTabPosition();
            this.UpdateChildrenPosition();

        }

        private void UpdateTabPosition() {
            int row = 0, col = 0, colSpan = 1, rowSpan = 1;
            ScrollOrientation orientation = ScrollOrientation.Horizontal;
            StackOrientation orientation2 = StackOrientation.Horizontal;
            switch (this.TabPosition) {
                case TabPositions.Top:
                    row = 0;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 1;
                    orientation = ScrollOrientation.Horizontal;
                    orientation2 = StackOrientation.Horizontal;
                    break;
                case TabPositions.Bottom:
                    row = 2;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 1;
                    orientation = ScrollOrientation.Horizontal;
                    orientation2 = StackOrientation.Horizontal;
                    break;
                case TabPositions.Left:
                    row = 0;
                    col = 0;
                    rowSpan = 3;
                    colSpan = 1;
                    orientation = ScrollOrientation.Vertical;
                    orientation2 = StackOrientation.Vertical;
                    break;
                case TabPositions.Right:
                    row = 0;
                    col = 2;
                    rowSpan = 3;
                    colSpan = 1;
                    orientation = ScrollOrientation.Vertical;
                    orientation2 = StackOrientation.Vertical;
                    break;
            }

            this.TabScroller.Orientation = orientation;
            this.TabContainer.Orientation = orientation2;

            Grid.SetRow(this.TabScroller, row);
            Grid.SetColumn(this.TabScroller, col);
            Grid.SetRowSpan(this.TabScroller, rowSpan);
            Grid.SetColumnSpan(this.TabScroller, colSpan);
        }

        private void UpdateChildrenPosition() {
            int row = 0, col = 0, colSpan = 0, rowSpan = 0;

            switch (this.TabPosition) {
                case TabPositions.Top:
                    row = 1;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 2;
                    break;
                case TabPositions.Bottom:
                    row = 0;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 2;
                    break;
                case TabPositions.Left:
                    row = 0;
                    col = 1;
                    rowSpan = 3;
                    colSpan = 2;
                    break;
                case TabPositions.Right:
                    row = 0;
                    col = 0;
                    rowSpan = 3;
                    colSpan = 2;
                    break;
            }
            Grid.SetRow(this.ChildrenContainer, row);
            Grid.SetColumn(this.ChildrenContainer, col);
            Grid.SetRowSpan(this.ChildrenContainer, rowSpan);
            Grid.SetColumnSpan(this.ChildrenContainer, colSpan);
        }

        public enum TabPositions {
            Top,
            Bottom,
            Left,
            Right
        }


        private void InitCollection(INotifyCollectionChanged collection) {
            if (collection != null)
                collection.CollectionChanged += Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    this.InsertChildren(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveChildren(e.OldItems, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.UpdateTabs();
                    this.UpdateChildren();
                    break;
            }
        }

        private void UpdateChildren() {
            this.ChildrenContainer.Children.Clear();

            if (this.ItemsSource != null)
                foreach (var d in this.ItemsSource) {
                    View view = this.GetView(d);
                    this.ChildrenContainer.Children.Add(view);
                }
        }

        private void UpdateTabs() {
            this.TabContainer.Children.Clear();
            if (this.ItemsSource != null)
                foreach (var d in this.ItemsSource) {
                    View tabView = this.GetTabView(d);
                    this.TabContainer.Children.Add(tabView);
                }
        }

        private void InsertChildren(IEnumerable datas, int startIdx = 0) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                var view = this.GetView(d);
                var tabView = this.GetTabView(d);

                this.ChildrenContainer.Children.Insert(startIdx, view);
                this.TabContainer.Children.Insert(startIdx, tabView);
                startIdx++;
            }
        }

        private void RemoveChildren(IList datas, int startIdx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.ChildrenContainer.Children.RemoveAt(startIdx);
                this.TabContainer.Children.RemoveAt(startIdx);
                startIdx++;
            }
        }

        private View GetView(object data) {
            View view = null;
            if (this.ItemTemplate != null || this.ItemTemplateSelector != null) {
                if (this.ItemTemplateSelector != null)
                    view = (View)this.ItemTemplateSelector.SelectTemplate(data, null).CreateContent();
                else if (this.ItemTemplate != null)
                    view = (View)this.ItemTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                }
            }

            if (view == null)
                view = new Label() { Text = "111" };

            this.ChildrenContainer.Children.Add(view);

            return view;
        }

        private View GetTabView(object data) {
            View view = null;

            if (this.TabTemplate != null || this.TabTemplateSelector != null) {
                if (this.TabTemplateSelector != null)
                    view = (View)this.TabTemplateSelector.SelectTemplate(data, null).CreateContent();
                else if (this.TabTemplate != null)
                    view = (View)this.TabTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                }
            }

            if (view == null)
                view = new Label() { Text = "Tab" };

            this.TabContainer.Children.Add(view);

            return view;
        }
    }
}
