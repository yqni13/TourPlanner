using System;
using WpfSubViewExample.SearchEngine.Abstract;
using WpfSubViewExample.ViewModels.Abstract;

namespace WpfSubViewExample.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        private string _resultText;

        public string ResultText 
        {
            get { return _resultText; }
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        public void DisplayResults(string resultText)
        {
            this.ResultText = resultText;
        }
    }
}