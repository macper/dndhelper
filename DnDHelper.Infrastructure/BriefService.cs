using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.BriefService;
using DnDHelper.Domain;
using ServiceStack.ServiceClient.Web;

namespace DnDHelper.Infrastructure
{
    public class BriefService : IBriefService
    {
        private string _serviceURL;

        public BriefService(string url)
        {
            _serviceURL = url;
        }

        public void UpdateBriefs(BriefUpdate update)
        {
            var serviceClient = new JsonServiceClient(_serviceURL);
            serviceClient.Send(new BriefUpdateRequest
            {
                Characters = update.Characters,
                EnemyCharacters = update.EnemyCharacters,
                Time = DateTime.Now
            });
        }

        public void RemoveEnemies()
        {
            var serviceClient = new JsonServiceClient(_serviceURL);
            serviceClient.Send(new BriefUpdateRequest
            {
                Characters = new List<CharacterBrief>(),
                EnemyCharacters = new List<EnemyBrief>(),
                RemoveAllEnemies = true,
                Time = DateTime.Now
            });
        }
    }
}
