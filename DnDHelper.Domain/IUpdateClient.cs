using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using DnDHelper.UpdateServiceAPI;

namespace DnDHelper.Domain
{
    public interface IUpdateClient
    {
        OperationResult Synchronize();
        OperationResult InitialPopulate();
    }
}