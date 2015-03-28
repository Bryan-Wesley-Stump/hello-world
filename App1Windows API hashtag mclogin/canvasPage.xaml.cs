using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238


namespace App1Windows_API_hashtag_mclogin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class canvasPage : Page, INotifyPropertyChanged
    {
        public bool didHoldSoMove = false;
        public canvasPage()
        {
            this.InitializeComponent();
        }

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
    }
}
