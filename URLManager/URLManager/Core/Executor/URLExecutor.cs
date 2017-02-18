using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using URLManager.Core.Executor.Base;
using URLManager.Core.Interfaces;
using URLManager.Core.Setter;
using static URLManager.Global.Globals;

namespace URLManager.Core.Executor
{
    /// <summary>
    /// URL과 같은 링크를 실행시키는 실행자입니다.
    /// </summary>
    [Serializable]
    public class URLExecutor : BaseExecutor
    {
        public URLExecutor(string urllink, string Name)
        {
            urllink = GetURLLink(urllink);
            _URLLink = urllink;
            this.Name = Name;
            _Icon = GetImageFromURL(urllink);
        }

        string _URLLink;
        public string URLLink
        {
            get { return _URLLink; }
            set { _URLLink = value; }
        }

        public override bool CanExecute
        {
            get
            {
                Uri uriResult;
                bool result = Uri.TryCreate(_URLLink, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

                return result;
            }
        }

        ImageSource _Icon;
        public override ImageSource Icon
        {
            get
            {
                return _Icon;
            }
        }

        //EnumSetter BrowserSetter = new EnumSetter(new List<Enum>());
        EnumSetter BrowserSetter = new EnumSetter(
            new int[] { (int)BrowserKind.InternetExplorer,
                        (int)BrowserKind.Chrome,
                        (int)BrowserKind.Firefox });

        public override bool Execute()
        {
            if (!CanExecute) return false;

            switch (((BrowserKind)BrowserSetter.SelectedEnum))
            {
                case BrowserKind.InternetExplorer:
                    Process.Start("iexplore", $"{_URLLink}");
                    break;
                case BrowserKind.Chrome:
                    Process.Start("chrome", $"{_URLLink} --new-window");
                    break;
                case BrowserKind.Firefox:
                    Process.Start("firefox", $"{_URLLink}");
                    break;
            }

            return true;
        }
    }
}
