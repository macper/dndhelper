using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DnDHelper.Domain;
using DnDHelpers.GUI.SPA2.API.DTO;
using DnDHelpers.GUI.SPA2.API.DTO.Spells;
using ServiceStack.ServiceInterface;

namespace DnDHelpers.GUI.SPA2.API
{
    public class SpellsService : Service
    {
        public RepositorySet Repository { get; set; }

        public IEnumerable<DictionaryItem> Get( SpellTypesRequest spellTypesRequest )
        {
            return EnumsDictionary.SpellTypes.Select( s => new DictionaryItem
            {
                Name = s.Name,
                Value = s.Value.ToString()
            } );
        }

        public IEnumerable<DictionaryItem> Get( SpellSchoolsRequest request )
        {
            return EnumsDictionary.SpellShools.Select( s => new DictionaryItem
            {
                Name = s.Name,
                Value = s.Value.ToString()
            } );
        }

        public IEnumerable<DictionaryItem> Get( SpellRangesRequest request )
        {
            return EnumsDictionary.SpellRanges.Select( s => new DictionaryItem
            {
                Name = s.Name,
                Value = s.Value.ToString()
            } );
        }

        public IEnumerable<SpellsInfoResponse> Get( SpellsInfoRequest request )
        {
            var spellTypeParsed = request.Type == null ? SpellType.All : (SpellType)Enum.Parse( typeof( SpellType ), request.Type );
            return Repository.Get<SpellDefinition>().Elements
                .Where( e => ( string.IsNullOrEmpty( request.Name ) || e.Name.ToLower().StartsWith( request.Name.ToLower() ) ) &&
                ( spellTypeParsed == SpellType.All || e.SpellTypes.Contains( spellTypeParsed ) ) &&
                ( request.Level == "(wszystkie)" || string.IsNullOrEmpty( request.Level ) || int.Parse( request.Level ) == e.Level ) )
                .Select( s => new SpellsInfoResponse
                {
                    Id = s.Id.Value,
                    Name = s.Name,
                    Level = s.Level,
                    Type = string.Join( ",", s.SpellTypes )
                } ).OrderBy( o => o.Level ).ThenBy( t => t.Name );
        }

        public SpellDetails Get( SpellDetailsRequest request )
        {
            var details = new SpellDetails();
            details.DictRanges = Get( new SpellRangesRequest() );
            details.DictSchools = Get( new SpellSchoolsRequest() );
            details.DictTypes = Get( new SpellTypesRequest() );

            if( request.Id == Guid.Empty )
            {
                details.SpellInstance = new SpellInstance
                    {
                        Id = Guid.Empty,
                        Name = "(nowy czar)",
                        Type = new List<DictionaryItem>()
                    };
                return details;
            }
            var spell = Repository.Get<SpellDefinition>().SingleOrDefault( s => s.Id == request.Id );
            if( spell == null )
                throw new ApplicationException( "Spell not found" );

            details.SpellInstance = new SpellInstance
            {
                Id = spell.Id.Value,
                Description = spell.Description,
                Level = spell.Level,
                Name = spell.Name,
                Range = details.DictRanges.Single( r => r.Value == spell.Range.ToString() ),
                School = details.DictSchools.Single( r => r.Value == spell.SpellShool.ToString() ),
                Type = details.DictTypes.Where( t => spell.SpellTypes.Any( s => s.ToString() == t.Value ) )
            };

            return details;
        }

        public void Post(SpellInstance spell)
        {
            var repo = Repository.Get<SpellDefinition>();

            SpellDefinition toUpdate = null;
            if( spell.Id == Guid.Empty )
            {
                if( repo.Elements.Any( s => s.Name == spell.Name ) )
                {
                    throw new ApplicationException( " Czar o tej nazwie już istnieje" );
                }
                toUpdate = new SpellDefinition();
                repo.Elements.Add( toUpdate );
            }
            else
            {
                toUpdate = repo.SingleOrDefault( s => s.Id == spell.Id );
            }
            if( toUpdate == null )
                throw new ApplicationException( "Spell not found" );

            toUpdate.Name = spell.Name;
            toUpdate.Level = spell.Level;
            toUpdate.Range = (SpellRange)Enum.Parse( typeof(SpellRange), spell.Range.Value );
            toUpdate.SpellShool = (SpellShool)Enum.Parse( typeof( SpellShool ), spell.School.Value );
            toUpdate.Description = spell.Description;

            var typesList = new List<SpellType>();
            foreach( var type in spell.Type )
            {
                var t = (SpellType)Enum.Parse( typeof( SpellType ), type.Value );
                typesList.Add( t );
            }

            toUpdate.SpellTypes = typesList;
            repo.Commit( toUpdate, true );
        }

        public void Delete(SpellInstance spell)
        {
            var repo = Repository.Get<SpellDefinition>();
            var sp = repo.Elements.SingleOrDefault( s => s.Id == spell.Id );
            if( sp == null )
            {
                throw new ApplicationException( "Spell not found" );
            }

            repo.Elements.Remove( sp );
            repo.Commit( sp, false );
        }
    }
}