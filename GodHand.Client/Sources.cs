using System.Collections.ObjectModel;

namespace GodHand.Client
{
    public class Sources
    {
        public static Shared.Models.Settings Settings { get; set; }
        public static ObservableCollection<Shared.Models.ProjectSettings> Projects { get; set; }

        
    }
}
