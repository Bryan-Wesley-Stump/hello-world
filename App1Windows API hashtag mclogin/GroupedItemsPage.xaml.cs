﻿using App1Windows_API_hashtag_mclogin.Common;
using App1Windows_API_hashtag_mclogin.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace App1Windows_API_hashtag_mclogin
{
    public class LoadFilesHelper
    {
        public ObservableCollection<ImageSource> ReturnImageSourceCollection()
        {
            var didid = new ObservableCollection<ImageSource>();

            //split here

            //to
            //load file folder all with some crap make json

            //split here
            //doWhateverConsumerAPI ddd = new doWhateverConsumerAPI();
            //ddd.doAClassThing();
            return didid;
        }
    }
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : Page, INotifyPropertyChanged
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public GroupedItemsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            //doWhateverConsumerAPI ddd = new doWhateverConsumerAPI();
            //ddd.doAClassThing();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroups = await OnBase.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetailPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool reallyStupidBoolean;
            bool stupidBoolean = bool.TryParse(EditModeToggle.IsChecked.ToString(), out reallyStupidBoolean);

            if (isTggingEnbled)
            {
                var clck = e.ClickedItem as SampleDataItem;
                StringBuilder sb = new StringBuilder();
                foreach (var itemd in theGridViewThing.SelectedItems)
                {
                    sb.Append(itemd.ToString());
                }
                clck.ListTags.Add(sb.ToString()); //update data source
                //clck.Title = sb.ToString();
                //clck.OnPropertyChanged("Title");
            }
            else if (reallyStupidBoolean && stupidBoolean)
            {


                this.SampleItem1 = e.ClickedItem as SampleDataItem;

                //then don't? i guess
            }
            else
            {
                // Navigate to the appropriate destination page, configuring the new page
                // by passing required information as a navigation parameter
                var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
                this.Frame.Navigate(typeof(ItemDetailPage), itemId);
            }
        }
        public bool isTggingEnbled = false;
        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public bool didHoldSoMove = false;

        private void TagGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (!didHoldSoMove) return;
            var newCanLeft = e.Cumulative.Translation.X + _initCanLeft;
            var newCanTop = e.Cumulative.Translation.Y + _initCanTop;
            var did = sender as UIElement;
            Canvas.SetLeft(did, newCanLeft);
            Canvas.SetTop(did, newCanTop);
        }

        double _initCanLeft;
        double _initCanTop;


        private void TagGrid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            didHoldSoMove = true;
            _initCanLeft = (double)(sender as UIElement).GetValue(Canvas.LeftProperty);
            _initCanTop = (double)(sender as UIElement).GetValue(Canvas.TopProperty);
        }

        private void TagGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            didHoldSoMove = false;
        }

        private void TagGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void TagGrid_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private ObservableCollection<string> _tags = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string callerName = null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(callerName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tags.Add(WhatTextB.Text);
        }

        private ObservableCollection<object> _objectItems1 = new ObservableCollection<object>();

        public ObservableCollection<object> ObjectItems123
        {
            get { return _objectItems1; }
            set
            {
                _objectItems1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImageSource> _images1 = new ObservableCollection<ImageSource>();

        public ObservableCollection<ImageSource> Images11
        {
            get { return _images1; }
            set
            {
                _images1 = value;
                OnPropertyChanged();
            }
        }


        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Queue<object> brains1 = new Queue<object>();
            Stack<object> brainsremoved = new Stack<object>();

            if (e.AddedItems.Any())
            {
                ItemSelected();
                Speciecifics();
                Specifics();
                //and then...
                //do sequence
                foreach (var addedItem in e.AddedItems)
                {
                    brains1.Enqueue(addedItem);
                    if (addedItem is ISelecteable)
                    {
                        (addedItem as ISelecteable).HasBeenSelected();
                    }
                }
            }
            if (e.RemovedItems.Any())
            {
                foreach (var remov in e.RemovedItems)
                {
                    ItemRemoved();
                    brainsremoved.Push(remov);
                    if (remov is ISelecteable)
                    {
                        (remov as ISelecteable).HasBeenDeselected();
                    }
                }
            }

            var gview = sender as GridView;
            if (gview.SelectedItems.Count > 0)
            {
                isTggingEnbled = true;
            }
            else
            {
                isTggingEnbled = false;
            }
        }

        private void ItemRemoved()
        {
            //nothing remd/?
        }

        private void Specifics()
        {
        }

        private void Speciecifics()
        {
        }

        public void ItemSelected()
        {
            mistakeprone();
            mistake2(); //todo comment select

        }

        public interface ISelecteable
        {
            bool IsSelected();
            bool HasBeenSelected();
            bool HasBeenDeselected();
            bool HasBeenNegelectedlected();
        }

        private void mistake2()
        {
            //throw new NotImplementedException();
        }

        private void mistakeprone()
        {

            //throw new NotImplementedException();
        }

        public void unselectItemed()
        {

        }

        private SampleDataItem _seampleItem;

        public SampleDataItem SampleItem1
        {
            get { return _seampleItem; }
            set
            {
                _seampleItem = value;
                OnPropertyChanged();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dkdk = new OnBase();
            dkdk.ReStructureTagGroups();

        }
    }
}