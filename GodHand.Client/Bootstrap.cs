using System.Windows;
using Caliburn.Micro;
using GodHand.Client.ViewModels;

namespace GodHand.Client
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}