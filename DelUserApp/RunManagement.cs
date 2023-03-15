using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Documents;

namespace DelUserApp
{
    public class RunManagement
    {
        private static RunManagement _instance = null;
        private static ObservableCollection<Run> _myRuns { get; set; }
        
        protected RunManagement()
        {
            _myRuns = new ObservableCollection<Run>();
        }

        public static RunManagement GetInstance()
        {
            if (_instance == null)
                _instance = new RunManagement();
            return _instance;
        }

        public void AddRun(Run newRun)
        {
            _myRuns.Add(newRun);
        }

        public ObservableCollection<Run> GetRuns ()
        {
            return _myRuns;
        }
    }
}
