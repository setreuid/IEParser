using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEParser
{
    /**
     * .NET WINFORM 용 IE PARSER
     * 
     * @author 조태호(setreuid@naver.com)
     * @date   2018. 12. 26.
     * 
     * 기본제공되는 .NET WINFORM WEBBROWSER 객체를 사용하여 웹 파싱 처리를 하는데 목적을 가진다.
     * 
     * 이 모듈에서 사용하는 명칭과 설명은 다음과 같다.
     * 1. Parser  : 파싱 클래스 명칭
     * 2. Waiter  : 파싱 작업을 위해서 해당 HTML DOM 객체가 존재하거나
     *              사용 가능 여부를 확인하기 위한 클래스
     * 3. Watcher : Waiter 객체의 행동를 수행하기 위해 주기적으로 체크하는 클래스
     */
    public class Parser
    {
        // 파싱용 브라우저
        private System.Windows.Forms.WebBrowser webBrowser;

        private Watcher.Watcher watcher;
        
        private Mutex taskMutex;

        
        /**
         * 생성자
         * @param targetBrowser 파싱용 웹 브라우저
         */
        public Parser(System.Windows.Forms.WebBrowser targetBrowser)
        {
            this.webBrowser = targetBrowser;
            this.taskMutex = new Mutex(false, "Task");
            this.watcher = new Watcher.Watcher(targetBrowser, taskMutex);

            this.webBrowser.ScriptErrorsSuppressed = true;
        }


        /**
         * HTML DOM 객체 추적 작업 추가
         * @param waiter
         */
        public void AddWaiter(Waiter.Waiter waiter) {
            waiter.SetWebBrowser(this.webBrowser);
            this.watcher.Add(waiter);
        }


        /**
         * JS 스크립트 실행
         * @param jsString
         */
        public void ExecuteJS(String jsString)
        {
            ExecuteJS(jsString, webBrowser.Document);
        }


        /**
         * JS 스크립트 실행
         * @param jsString
         * @param frameIndex
         */
        public void ExecuteJS(String jsString, int frameIndex)
        {
            ExecuteJS(jsString, webBrowser.Document.Window.Frames[frameIndex].Document);
        }


        /**
         * JS 스크립트 실행
         * @param jsString
         * @param document
         */
        public void ExecuteJS(String jsString, HtmlDocument document)
        {
            this.webBrowser.Invoke(new Action(() =>
            {
                String command = String.Format(@"
                    (function() {{
                        window.requestAnimationFrame(function() {{
                            {0}
                        }});
                    }})
                ", jsString);

                document.InvokeScript("execScript", new Object[] {
                        String.Format(@"(function() {{ 
                            if (document.readyState != 'loading') {{
                                {0}();
                            }} else if (document.addEventListener) {{
                                document.addEventListener('DOMContentLoaded', {0});
                            }} else {{
                                document.attachEvent('onreadystatechange', function() {{
                                  if (document.readyState != 'loading')
                                    {0}();
                                }});
                            }}
                        }})()", command),
                        "JavaScript" });
            }));
        }
    }
}
