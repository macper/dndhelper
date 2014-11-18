using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DnDHelper.UpdateServiceAPI;

namespace DnDHelper.GUI.WPF
{
    public class UpdateServiceProxy : ClientBase<IUpdateService>, IUpdateService
    {

        public SyncResponse Synchronize( SyncRequest request )
        {
            return Channel.Synchronize( request );
        }
    }
}
