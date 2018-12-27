using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEParser.Waiter.Selector
{
    public interface Selector
    {
        /**
         * 프레임이 있는 홈페이지를 위한 HtmlDocument 반환 메소드
         * @return HtmlDocument
         */
        HtmlDocument GetDocument();


        /**
         * DOM 가져오기
         * @exception NullReferenceException
         */
        dynamic GetElements();


        /**
         * DOM 배열의 경우 갯수 가져오기
         * @exception NullReferenceException
         * @exception InvalidCastException
         */
        long GetCounts();


        /**
         * DOM 특정 값 가져오기
         * @param attributeName
         * @exception NullReferenceException
         */
        String GetAttribute(String attributeName);


        /**
         * DOM 특정 값 주입
         * @param attributeName
         * @param attributeValue
         */
        void SetAttribute(String attributeName, String attributeValue);


        /**
         * 파싱용 웹 브라우저 주입
         * @param  webBrowser
         * @return Selector
         */
        Selector SetWebBrowser(WebBrowser webBrowser);
    }
}
