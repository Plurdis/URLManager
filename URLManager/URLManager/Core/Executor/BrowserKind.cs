using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLManager.Core.Executor
{
    enum BrowserKind
    {
        [Description("Google Chrome")]
        Chrome,
        [Description("Internet Explorer")]
        InternetExplorer,
        [Description("Mozllia FireFox")]
        Firefox
    }
}
