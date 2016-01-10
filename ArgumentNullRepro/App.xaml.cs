using Prism.Windows;
using System;
using Windows.ApplicationModel.Activation;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using Prism.Windows.AppModel;

namespace ArgumentNullRepro
{
    sealed partial class App : PrismApplication
    {
        public App()
        {
            // Package locale is set to "tr-tr" in Package.appxmanifest,
            // Since Prism localizations are not available for Turkish, Resource loader throws an exception.
            // And we just get an argument null exception here bacause PrismApplication.cctor is initializing a DebugLogger object which tries to load a resuource.
            InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);

            return Task.CompletedTask;
        }

        protected override INavigationService CreateNavigationService(IFrameFacade rootFrame, ISessionStateService sessionStateService)
        {
            return new FrameNavigationService(
                rootFrame,
                s => Type.GetType($"ArgumentNullRepro.{s}Page"),
                sessionStateService
            );
        }

    }
}
