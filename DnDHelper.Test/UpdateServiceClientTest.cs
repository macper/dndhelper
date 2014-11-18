using System.Collections.Generic;
using System.Linq;
using DnDHelper.Domain;
using DnDHelper.UpdateServiceAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceStack.Text;

namespace DnDHelper.Test
{
    [TestClass]
    public class UpdateServiceClientTest 
    {
        private IUpdateClient _updateClient;

        public void InitTest()
        {
            _updateClient = new UpdateClient();
        }

        //[TestMethod]
        //public void BriefSerializeTest()
        //{
        //    var request = new BriefInfoRequest
        //    {
        //        EnemyMembers = new List<ChangeInfo>(),
        //        PartyMembers = new List<ChangeInfo>()
        //    };
        //    var serialized = JsonSerializer.SerializeToString(request);
        //}

    }
}