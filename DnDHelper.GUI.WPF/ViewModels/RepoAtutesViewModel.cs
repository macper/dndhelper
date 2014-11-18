using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public  class RepoAtutesViewModel : ViewModelBase
    {
        private string _searchName;
        public bool SelectMode { get; set; }

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; PropertyHasChanged("SearchName"); FillList(); }
        }

        public ObservableCollection<AtutDefinition> Atutes { get; set; }

        private AtutDefinition _selectedAtut;
        public AtutDefinition SelectedAtut
        {
            get
            {
                return _selectedAtut;
            } 
            set
            {
                _selectedAtut = value;
                PropertyHasChanged("SelectedAtut");
            }
        }

        private Atut _selectedAtutInstance;
        public Atut AtutInstance
        {
            get { return _selectedAtutInstance; }
            set { _selectedAtutInstance = value; PropertyHasChanged("AtutInstance"); }
        }

        public bool IsAtutInstaceVisible
        {
            get { return AtutInstance != null && SelectMode; }
        }

        public ObservableCollection<Script> Scripts
        {
            get { return new ObservableCollection<Script>( _appFacade.Value.Repositories.Get<Script>().Elements.Where(e => e.ScriptContext == ScriptContext.Atut) ); }
        }

        public ICommand AddAtut { get; private set; }
        public ICommand RemoveAtut { get; private set; }
        public ICommand SelectAtut { get; private set; }
        public ICommand ConfirmSelect { get; private set; }
        public ICommand Commit { get; set; }
        public ICommand EditScripts { get; private set; }

        private readonly Lazy<IAppAPI> _appApi = new Lazy<IAppAPI>(ServiceContainer.GetInstance<IAppAPI>);
        private readonly Lazy<AppFacade> _appFacade = new Lazy<AppFacade>(ServiceContainer.GetInstance<AppFacade>);

        private readonly Character character;

        public RepoAtutesViewModel(bool selectMode = false, Character ch = null)
        {
            character = ch;
            SelectMode = selectMode;
            FillList();
            AddAtut = new Command((o) =>
                                        {
                                            SelectedAtut = new AtutDefinition() {Name = "(nowy atut)"};
                                        });
            RemoveAtut = new Command((o) =>
                                           {
                                               _appApi.Value.HandleOperationResult(_appFacade.Value.RemoveAtut(SelectedAtut));
                                               FillList();
                                               _appFacade.Value.AtutesChanged( SelectedAtut );
                                           }, this, () => SelectedAtut != null, "SelectedAtut");
            SelectAtut = new Command((o) =>
                                           {
                                               AtutInstance = SelectedAtut.CreateItem();
                                               PropertyHasChanged("IsAtutInstaceVisible");
                                           }, this, () => SelectedAtut != null, "SelectedAtut");
            ConfirmSelect = new Command((o) =>
                                            {
                                                var res = character.Controller.AddAtuteOriginal(AtutInstance);
                                                _appApi.Value.HandleOperationResult(res);
                                                CommandHasExecuted("CommitSelect", res);
                                            });
            Commit = new Command((o) =>
                                     {
                                         if (!Atutes.Any(a => a.Name == SelectedAtut.Name))
                                         {
                                             _appApi.Value.HandleOperationResult(_appFacade.Value.AddAtut(SelectedAtut));
                                         }
                                         else
                                         {
                                             _appFacade.Value.AtutesChanged( SelectedAtut );
                                         }
                                         _appApi.Value.ShowNotifyToUser("Zapisano pomyślnie");
                                     });

            EditScripts = new Command((o) => _appApi.Value.RedirectToViewModel(new ScriptEditorViewModel(ScriptContext.Atut), () => PropertyHasChanged("Scripts")));
        }

        private void FillList()
        {
            var appFacade = ServiceContainer.GetInstance<AppFacade>();
            Atutes = new ObservableCollection<AtutDefinition>(appFacade.RepoAtutes.Where(e => string.IsNullOrEmpty(_searchName) || e.Name.ToLower().StartsWith(_searchName.ToLower())));
            PropertyHasChanged("Atutes");
        }
    }
}
