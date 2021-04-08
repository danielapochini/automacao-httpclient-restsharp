using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpAutomation.DropBoxAPI.ListFolderModel
{
    public class FileLockInfo
    {
        public bool IsLockholder { get; set; }
        public string LockholderName { get; set; }
        public DateTime Created { get; set; }
    }
}
