using MahApps.Metro.Controls;

namespace GodHand.Client.ViewModels
{
    public interface IFlyout
    {
        string Header { get; set; }
        bool IsOpen { get; set; }
        Position Position { get; set; }
    }
}
