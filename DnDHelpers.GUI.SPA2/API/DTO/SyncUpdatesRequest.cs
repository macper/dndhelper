using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API.DTO
{
    [Route("/api/syncRepositories")]
    [Authenticate]
    [RequiredRole("Admin")]
    public class SyncUpdatesRequest
    {
    }
}