using IEParser.Waiter.Selector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEParser.Waiter.Behavior
{
    public class ElementUnVisible : Behavior
    {
        private Selector.Selector selector;


        public ElementUnVisible(Selector.Selector targetSelector)
        {
            this.selector = targetSelector;
        }


        public bool Check()
        {
            try
            {
                if (((CSS) selector).IsHidden())
                    return true;
                return false;
            }
            catch (Exception e1)
            {
                Trace.WriteLine(e1.Message);
                return false;
            }
        }


        public void SetWebBrowser(WebBrowser webBrowser)
        {
            this.selector.SetWebBrowser(webBrowser);
        }
    }
}
