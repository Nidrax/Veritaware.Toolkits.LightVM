using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veritaware.Toolkits.LightVM.WpfMock.IServices
{
    internal interface INamesService
    {
        string Get();
        ICollection<string> Get(int count);
    }
}
