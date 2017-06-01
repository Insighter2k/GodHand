using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodHand.Shared.Models;

namespace GodHand.Shared.IO
{
    public class Check
    {
        public static List<Tuple<string, string>> ForDirectories(List<TreeViewItem> a, List<TreeViewItem> b)
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            foreach (var item in a)
            {
                foreach (var item2 in b)
                {
                    if (item.Name == item2.Name)
                    {
                        list.Add(Tuple.Create(item.Fullpath, item2.Fullpath));

                        var targetSubDirs = (item as DirectoryItem).Items;
                        var patchSubDirs = (item2 as DirectoryItem).Items;

                        list.AddRange(ForDirectories(targetSubDirs, patchSubDirs));
                    }
                }
            }

            return list;
        }

        public static List<Tuple<string, string>> ForFiles(List<Tuple<string, string>> a)
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            foreach (var item in a)
            {
                var targetFiles = Read.FilesOfDirectory(item.Item1);
                var patchFiles = Read.FilesOfDirectory(item.Item2);

                foreach (var targetFile in targetFiles)
                {
                    foreach (var patchFile in patchFiles)
                    {
                        if (targetFile.Name == patchFile.Name.Replace(".ghp", ""))
                        {
                            list.Add(Tuple.Create(targetFile.Fullpath, patchFile.Fullpath));
                        }
                    }
                }
            }

            return list;
        }
    }
}
