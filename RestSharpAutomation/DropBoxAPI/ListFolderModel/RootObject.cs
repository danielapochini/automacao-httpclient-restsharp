using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpAutomation.DropBoxAPI.ListFolderModel
{
    public class RootObject
    {
        public List<Entry> Entries { get; set; }
        public string Cursor { get; set; }
        public bool HasMore { get; set; }
    }
}
