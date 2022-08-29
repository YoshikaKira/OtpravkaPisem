using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _email;
        string _directory = "C:\\Users\\STUDENT\\Desktop\\1001.xlsx";
        ExcelGen _generator;
        void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
   
        public ViewModel()
        {
            Email = "";
            _generator = new ExcelGen();
        }
        public ButtonCommand DirectoryClick 
        {
            get
            {
                return new ButtonCommand
                (new Action(() =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel|*.xlsx";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        _directory = saveFileDialog.FileName;
                    }
                }));
            }
        }
        public ButtonCommand SendClick
        {
            get {
                return new ButtonCommand
                  (new Action(()=>
                  {
                      _generator.Generate(_directory);
                      Mail mail = new Mail("nekrashevitch.ihor@yandex.ru", "nekrashevitch.ihor!",                                                                                
                          "smtp.yandex.ru", 25);
                      mail.SendMail(_email,
                          "Отчет",
                          "Добрый день, отчет готов", _directory);      
                  }), 
                  
                  () => { return _email.Contains("@") && _email.Contains("."); });
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Notify("Email");
            }
        }
    }
}
