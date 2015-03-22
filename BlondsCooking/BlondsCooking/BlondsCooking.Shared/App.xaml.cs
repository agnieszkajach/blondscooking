using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Networking.Connectivity;
using Windows.Networking.NetworkOperators;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BlondsCooking.Helpers;
using BlondsCooking.Synchronization;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using BlondsCooking.Views;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;

namespace BlondsCooking
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        public static string Path = ApplicationData.Current.LocalFolder.Path + "\\";
        public static string FileName = "recipes.xml";

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
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            //ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            //if (internetConnectionProfile != null)
            //{
            //    var mes = new MessageDialog("asd");
            //    await mes.ShowAsync();
            //}

            if (LocalSettingsHelper.CheckIfFirstLaunch())
            {
                await LocalContentHelper.CheckForLocalFile();   
                //if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                //{
                //    await AzureTableHelper.DownloadAllRecipes();
                //    await AzureStorageHelper.DownloadAllImagesFromAzure();
                //}
                //else
                //{
                //    var messageDialog = new MessageDialog("No internet connection has been found.");
                //    await messageDialog.ShowAsync();
                //}
            }
            else
            {
                await LocalContentHelper.CheckForLocalFile();
            }
            BackgroundExecutionManager.RequestAccessAsync();
            BackgroundTaskBuilder backgroundTaskBuilder = new BackgroundTaskBuilder
            {
                TaskEntryPoint = "BlondsCooking.Synchronization.BackgroundTask"
            };
            backgroundTaskBuilder.SetTrigger(new TimeTrigger(15, false));
            backgroundTaskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
            IBackgroundTaskRegistration task = backgroundTaskBuilder.Register();
            
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