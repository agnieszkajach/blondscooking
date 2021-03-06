﻿using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Networking.Connectivity;
using Windows.Networking.NetworkOperators;
#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using BlondsCooking.Helpers;
using BlondsCooking.Synchronization;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using BlondsCooking.Views;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using BlondsCooking.Common;
using GalaSoft.MvvmLight.Views;
using BlondsCooking.Services;
using GalaSoft.MvvmLight.Messaging;

namespace BlondsCooking
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        public static string Path = ApplicationData.Current.LocalFolder.Path + "\\";
        public static BackgroundAccessStatus BackgroundAccessStatus;
        public static string FileName = "recipes.xml";
        public static bool InternetNeeded = false;
        public static string FinishedDownloading = "visible";

#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif
        }
#if WINDOWS_PHONE_APP
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
#endif
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            ConnectionHelper connectionHelper = new ConnectionHelper();
            UpdateCheckingInBackground updateCheckingInBackground = new UpdateCheckingInBackground();
            var isConnected = connectionHelper.IsConnectedToInternet();
            if (LocalSettingsHelper.CheckIfFirstLaunch())
            {
                if (isConnected)
                {
                    BackgroundDataDownloader backgroundDataDownloader = new BackgroundDataDownloader();
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction) => backgroundDataDownloader.Run()), WorkItemPriority.High);                
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction) => updateCheckingInBackground.Run()), WorkItemPriority.Low);
                }
                else
                {
                    InternetNeeded = true;
                }
            }
            else
            {
                FinishedDownloading = "collapsed";
                LocalContentHelper localContentHelper = new LocalContentHelper();
                await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction) => localContentHelper.RunLocalContent()), WorkItemPriority.High);
                await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction) => updateCheckingInBackground.Run()), WorkItemPriority.Normal);
            }

                              
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
            if (InternetNeeded)
            {
                Services.IDialogService dialogService = new Services.DialogService();
                await dialogService.ShowMessage(
                    "Hey, you need to connect to Internet to get all that delicious stuff ♥");
            }
           
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }

    }
}