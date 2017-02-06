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

        public override bool IsLocalFile
        {
            get { return false; }
        }

        EnumSetter BrowserSetter = new EnumSetter(
            new Enum[] { BrowserKind.InternetExplorer,
                         BrowserKind.Chrome,
                         BrowserKind.Firefox });

        public override iSetter[] SettingItems
        {
            get { return new iSetter[] { BrowserSetter }; }
        }



        public override bool Execute()
        {
            if (!CanExecute) return false;

            switch (((BrowserKind)BrowserSetter.RealValue))
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

        public override bool OpenContainFolder()
        {
            return false;
        }
    }
}
