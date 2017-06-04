using Caliburn.Micro;

namespace GodHand.Client.ViewModels
{
    public interface ITabItem
    {
        string Header { get; set; }
        bool IsSelected { get; set; }
        Screen Content { get; }

    }
}
