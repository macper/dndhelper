using System;
using System.Collections.Generic;
using System.Linq;
using DnDHelper.UpdateServiceAPI;

namespace DnDHelper.Domain
{
    public class UpdateClient : IUpdateClient
    {
        private static readonly ILogger Logger = ServiceContainer.GetInstance<ILogger>(new KeyValuePair<string, object>(Const.LoggerName, typeof(UpdateClient).Name));
        private static Dictionary<string, int> _serverVersions = new Dictionary<string, int>();

        public OperationResult Synchronize()
        {
            try
            {
                var repos = ServiceContainer.GetInstance<RepositorySet>();
                var service = ServiceContainer.GetInstance<IUpdateService>();
                var serializer = ServiceContainer.GetInstance<IEntitySerializer>();

                var request = new SyncRequest
                {
                    SyncTime = ChangeTracker.Instance.LastSync
                };

                var toSend = new List<EntityUpdateInfo>();
                foreach( var repoChange in ChangeTracker.Instance.RepositoryChanges )
                {
                    foreach( var entityChange in repoChange.EntityChanges.Where( e => e.TimeStamp > ChangeTracker.Instance.LastSync ) )
                    {
                        Logger.Debug( string.Format( "To Send: {0} {1} {2}", repoChange.Name, entityChange.Id, entityChange.IsUpdateInsert ) );
                        toSend.Add( new EntityUpdateInfo
                        {
                            Id = entityChange.Id,
                            LastChange = entityChange.TimeStamp,
                            RepositoryName = repoChange.Name,
                            UpdateInsert = entityChange.IsUpdateInsert,
                            Content = entityChange.IsUpdateInsert ? serializer.Serialize(repos.GetByName(repoChange.Name).GetElements().Single(s => s.Id == entityChange.Id)) : null
                        } );
                    }
                }
                request.Updates = toSend;
                Logger.Debug( toSend.Count + " entities has changed" );

                var response = service.Synchronize( request );
                if( !response.Success )
                {
                    return OperationResult.Error( response.Error );
                }

                Logger.Debug( "UpdateService - OK" );

                foreach( var toChange in response.Updates )
                {
                    Logger.Debug( string.Format( "To update: {0} Id: {1} {2}", toChange.RepositoryName, toChange.Id, toChange.UpdateInsert ) );
                    var repo = repos.GetByName( toChange.RepositoryName );
                    if (repo == null)
                        continue;

                    var element = repo.GetElements().SingleOrDefault( s => s.Id == toChange.Id );
                    repo.RemoveElement( element );
                    if( toChange.UpdateInsert )
                    {
                        var elToUpdate = serializer.Deserialize<BaseEntity>( toChange.Content );
                        repo.AddElement( elToUpdate );
                    }
                    repo.Commit(null, true);
                }

                ChangeTracker.Instance.LastSync = DateTime.Now.Ticks;
                ChangeTracker.Instance.SaveChanges();

                var res = OperationResult.Success();
                res.Message = response.Updates.Count() + " updates received from server";
                return res;
            }
            catch( Exception exc )
            {
                Logger.Error( exc.Message, exc );
                return OperationResult.Error( exc.Message );
            }
        }


        public OperationResult InitialPopulate()
        {
            try
            {
                var repos = ServiceContainer.GetInstance<RepositorySet>();
                var service = ServiceContainer.GetInstance<IUpdateService>();
                var serializer = ServiceContainer.GetInstance<IEntitySerializer>();

                var syncTime = DateTime.Now.Ticks;

                foreach( var repo in repos.GetAll() )
                {
                    var elements = repo.GetElements().OrderBy( e => e.Name ).ToList();
                    var counter = 0;
                    var thisElements = elements.Skip( counter ).Take( 20 );
                    while( thisElements.Count() > 0 )
                    {
                        var request = new SyncRequest()
                        {
                            SyncTime = syncTime
                        };
                        request.Updates = thisElements.Select( t => new EntityUpdateInfo
                        {
                            Id = t.Id.Value,
                            UpdateInsert = true,
                            LastChange = syncTime,
                            RepositoryName = repo.Name,
                            Content = serializer.Serialize( t )
                        } );

                        Logger.Debug( string.Format( "Sending {0} elements of {1}...", request.Updates.Count(), repo.Name ) );

                        var response = service.Synchronize( request );

                        if( !response.Success )
                        {
                            throw new ApplicationException( "Error during sync: " + response.Error );
                        }

                        counter += 20;
                        thisElements = elements.Skip( counter ).Take( 20 );
                    }
                }
            }
            catch( Exception exc )
            {
                return OperationResult.Error( exc.Message );
            }
            return OperationResult.Success();
        }
    }
}