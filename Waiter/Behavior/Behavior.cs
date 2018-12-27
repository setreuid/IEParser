using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEParser.Waiter.Behavior
{
    public interface Behavior
    {
        void SetWebBrowser(WebBrowser webBrowser);

        bool Check();
    }
}
