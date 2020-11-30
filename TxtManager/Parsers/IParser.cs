using System;
using System.Collections.Generic;
using System.Text;

namespace TxtManager.Parsers
{
    interface IParser
    {
       Settings ParseSettings();
    }
}
