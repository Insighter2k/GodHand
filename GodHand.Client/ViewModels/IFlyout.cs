using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
