using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels.MainVM
{
    public partial class MainViewModel
    {
        private void SubToLogsViewEvent()
        {
            LogsView.SelectTourLogsEvent += (_, logs) =>
            {
                // TODO
            };
        }
    }
}
