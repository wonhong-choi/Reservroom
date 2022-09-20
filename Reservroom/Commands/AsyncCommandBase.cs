using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExcuting;
        public bool IsExcuting
        {
            get => _isExcuting;
            set
            {
                _isExcuting = value;
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !IsExcuting && base.CanExecute(parameter);
        }



        public override async void Execute(object parameter)
        {
            IsExcuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExcuting = false;
            }
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
