using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public class MoreAdvancedQueueManager : IQueueManager
    {
        public void RepositorySaveRequest<T>( Repository<T> repository ) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
