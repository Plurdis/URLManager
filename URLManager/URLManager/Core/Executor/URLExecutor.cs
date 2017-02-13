using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLManager.Core.Executor.Base;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter;

namespace URLManager.Core.Executor
{
    /// <summary>
    /// URL과 같은 링크를 실행시키는 실행자입니다.
    /// </summary>
    class URLExecutor : BaseExecutor
    {
        public URLExecutor(string urllink)
        {
            URLLink = urllink;
        }


        string URLLink;

        public override bool CanExecute
        {
            get
            {
                Uri uriResult;
                bool result = Uri.TryCreate(URLLink, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

                return result;
            }
        }
        

        EnumSetter BrowserSetter = new EnumSetter(
            new Enum[] { BrowserKind.InternetExplorer,
                         BrowserKind.Chrome,
                         BrowserKind.Firefox });
        
        public override bool Execute()
        {
            if (!CanExecute) return false;

            switch (((BrowserKind)BrowserSetter.SelectedEnum))
            {
                case BrowserKind.InternetExplorer:
                    Process.Start("iexplore", $"{URLLink}");
                    break;
                case BrowserKind.Chrome:
                    Process.Start("chrome", $"{URLLink} --new-window");
                    break;
                case BrowserKind.Firefox:
                    Process.Start("firefox", $"{URLLink}");
                    break;
            }

            return true;
        }
    }
}
