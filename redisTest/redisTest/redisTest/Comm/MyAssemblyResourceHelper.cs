using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    public static class MyAssemblyResourceHelper
    {
        public static Stream GetAssemblyStream(string uri)
        {
            Stream rs = null;
            try
            {
                AssemblyResource loader = new AssemblyResource(new Uri(uri));
                rs = loader.GetAssemblyResource();
            }
            catch (Exception)
            {

            }
            return rs;
        }
    }
}
