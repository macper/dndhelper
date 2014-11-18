using System;
using DnDHelper.GUI.WPF.ViewModels;

namespace DnDHelper.GUI.WPF
{
    public class ViewModelHandler
    {
        private readonly Func<ViewModelBase, bool> _handle;

        public ViewModelHandler(Func<ViewModelBase, bool> handler)
        {
            _handle = handler;
        }

        public void Handle(ViewModelBase model, Action onSuccess)
        {
            if (_handle(model) && onSuccess != null)
            {
                onSuccess();
            }
        }
    }
}