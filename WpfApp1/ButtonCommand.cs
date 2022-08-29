﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    class ButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        Action _action;
        Func<bool> _func;
        public ButtonCommand(Action action, Func<bool> func = null)
        {
            _action = action;
            _func = func;
        }
        public bool CanExecute(object parameter)
        {
            return _func == null || _func() == true;
        }

        public void Execute(object parameter)
        {
           
            _action();
        }
    }
}
