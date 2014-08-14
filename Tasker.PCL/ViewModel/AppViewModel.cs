using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Tasker.PCL.Model;

namespace Tasker.PCL.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        public AppContext Context { get; set; }

        public AppViewModel(AppContext context)
        {
            Context = context;
        }
    }
}
