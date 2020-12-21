﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.BaseModels
{
    public class ViewModelStammdatan : ViewModelBasis
    {
        public ICommand SaveCommand { get; protected set; }

        protected bool CanExecuteSaveCommand(String arg)
        {
            return ValidationErrors.Count == 0;
        }

        protected virtual void ExecuteSaveCommand(String arg)
        {
            throw new NotImplementedException();
        }
    }
}