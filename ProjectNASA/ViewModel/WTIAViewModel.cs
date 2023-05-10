using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.ViewModel
{
    public partial class WTIAViewModel : BaseViewModel
    {
        readonly IConnectivity connectivity;
        readonly IWTIAService wTIAService;

        [ObservableProperty]
        ISS iss;

        public WTIAViewModel(IWTIAService wTIAService, IConnectivity connectivity)
        {
            this.wTIAService = wTIAService;
            this.connectivity = connectivity;
        }




    }
}
