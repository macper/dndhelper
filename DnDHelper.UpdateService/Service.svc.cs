using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DnDHelper.UpdateServiceAPI;

namespace DnDHelper.UpdateService
{
    public class UpdateService : IUpdateService
    {
        public SyncResponse Synchronize( SyncRequest request )
        {
            var response = new SyncResponse();

            try
            {
                var toSend = new List<EntityUpdateInfo>();

                using( var context = new DataContext() )
                {
                    foreach( var repo in context.Repositories.ToList() )
                    {
                        var serverEntities = repo.DnDEntities.Where( e => e.LastChange > request.SyncTime ).ToList();
                        var syncEntities = request.Updates.Where( u => u.RepositoryName == repo.Name );

                        foreach( var syncEntity in syncEntities )
                        {
                            var serverEntity = serverEntities.SingleOrDefault( s => s.Id == syncEntity.Id );
                            if( serverEntity != null )
                            {
                                if( serverEntity.LastChange < syncEntity.LastChange )
                                {
                                    if( syncEntity.UpdateInsert )
                                    {
                                        serverEntity.LastChange = syncEntity.LastChange;
                                        serverEntity.Content = syncEntity.Content;
                                    }
                                    else
                                    {
                                        repo.DnDEntities.Remove( serverEntity );
                                    }
                                }
                                else
                                {
                                    toSend.Add( new EntityUpdateInfo
                                    {
                                        Id = serverEntity.Id,
                                        LastChange = serverEntity.LastChange,
                                        Content = serverEntity.Content,
                                        RepositoryName = repo.Name,
                                        UpdateInsert = true
                                    } );
                                }
                            }
                            else
                            {
                                var entityToUpdate = context.Entities.SingleOrDefault( e => e.Id == syncEntity.Id );
                                if( entityToUpdate == null && syncEntity.UpdateInsert )
                                {
                                    entityToUpdate = new DnDEntity
                                    {
                                        DnDRepository = repo,
                                        Id = syncEntity.Id
                                    };
                                    repo.DnDEntities.Add( entityToUpdate );
                                }
                                if( syncEntity.UpdateInsert )
                                {
                                    entityToUpdate.Content = syncEntity.Content;
                                }
                                else
                                {
                                    entityToUpdate.Deleted = true;
                                }
                                entityToUpdate.LastChange = syncEntity.LastChange;
                            }
                        }

                        foreach( var newServerEntity in serverEntities.Where( s => !syncEntities.Any( e => e.Id == s.Id ) ) )
                        {
                            toSend.Add( new EntityUpdateInfo
                            {
                                Id = newServerEntity.Id,
                                UpdateInsert = newServerEntity.Deleted == null,
                                LastChange = newServerEntity.LastChange,
                                Content = newServerEntity.Content,
                                RepositoryName = repo.Name
                            } );
                        }
                    }
                    context.SaveChanges();
                }

                response.Updates = toSend;
                response.Success = true;
            }
            catch( Exception exc )
            {
                response.Error = exc.ToString();
                response.Success = false;
            }

            return response;
        }
    }
}
