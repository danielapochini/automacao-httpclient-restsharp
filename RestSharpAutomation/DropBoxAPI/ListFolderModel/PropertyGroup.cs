using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpAutomation.DropBoxAPI.ListFolderModel
{
    public class PropertyGroup
    {
        public string TemplateId { get; set; }
        public List<Field> Fields { get; set; }
    }
}
