using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    public class RepositoryInfo
    {
        public string RepoName { get; set; }
        public Guid Id { get; set; }
        public string ChangeType { get; set; }

    }
}