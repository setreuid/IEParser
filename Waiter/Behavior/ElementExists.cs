using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEParser.Waiter.Behavior
{
    public class ElementExists : Behavior
    {
        private Selector.Selector selector;


        public ElementExists(Selector.Selector targetSelector)
        {
            this.selector = targetSelector;
        }


        /**
         * DOM 객체가 존재하는지 테스트
         */
        public bool Check()
        {
            try
            {
                if (selector.GetCounts() > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetWebBrowser(WebBrowser webBrowser)
        {
            this.selector.SetWebBrowser(webBrowser);
        }
    }
}
