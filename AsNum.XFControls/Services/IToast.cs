using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.XFControls.Services {
    public interface IToast {
        void Show(string msg, bool longShow = false);
    }
}
