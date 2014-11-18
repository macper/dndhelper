using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class DamageBoneModel : ViewModelBase
    {
        private int _maxValue;
        public int MaxValue
        {
            get { return _maxValue; }
            set { 
                _maxValue = value;
                CommandHasExecuted("ValueChanged", OperationResult.Success());
                PropertyHasChanged("MaxValue");
            }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value;
                CommandHasExecuted("ValueChanged", OperationResult.Success());
                PropertyHasChanged("Count");
            }
        }

        private string _damageType;
        public string DamageType { get { return _damageType; } set {
            _damageType = value; 
            CommandHasExecuted("ValueChanged", OperationResult.Success()); 
            PropertyHasChanged("DamageType"); } }


    }
}
