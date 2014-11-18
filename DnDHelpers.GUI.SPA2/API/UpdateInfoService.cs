using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelpers.GUI.SPA2.API.DTO;
using ServiceStack.ServiceInterface;
using System.Linq;

namespace DnDHelpers.GUI.SPA2.API
{
    public class UpdateInfoService : Service
    {
        public IUpdateClient UpdateService { get; set; }

        public RepositorySet RepositorySet { get; set; }

        public SyncInfoResponse Get(SyncInfoRequest request)
        {
            var resp = new SyncInfoResponse
            {
                LastSyncTime = ChangeTracker.Instance.LastSync == 0 ? "(brak)" : new DateTime(ChangeTracker.Instance.LastSync).ToString(),
            };

            var list = new List<RepositoryInfo>();
            var repos = ChangeTracker.Instance.RepositoryChanges.Where(r => r.EntityChanges.Any(e => e.TimeStamp > ChangeTracker.Instance.LastSync));
            foreach (var r in repos)
            {
                r.EntityChanges.Where(e => e.TimeStamp > ChangeTracker.Instance.LastSync).ToList().ForEach(f => list.Add(new RepositoryInfo
                {
                    Id = f.Id,
                    RepoName = r.Name,
                    ChangeType = f.IsUpdateInsert ? "Update/Insert" : "Delete"
                }));
            }
            resp.Changes = list;
            return resp;
        }

    }
}