using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
