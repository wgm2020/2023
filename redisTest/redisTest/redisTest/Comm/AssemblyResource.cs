using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    public class AssemblyResource
    {
        private readonly Uri uri = null;
        private readonly string resourceName = string.Empty;
        private readonly string fullResourceName = string.Empty;
        private readonly string resourceAssemblyName = string.Empty;
        private readonly Assembly assembly = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyResource"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public AssemblyResource(Uri uri)
        {
            this.uri = uri;
            string[] info = GetResourceNameWithoutProtocol(uri).Split('/');

            if (info.Length != 3)
            {
                throw new Exception(
                    "Invalid resource for '" + uri.OriginalString + "'. It has to be in " +
                        "'assembly:<assemblyName>/<namespace>/<resourceName>' format. The uri is case sensitive.");
            }

            resourceAssemblyName = info[0];

            assembly = Assembly.Load(resourceAssemblyName);

            if (assembly == null)
            {
                throw new Exception("Unable to load assembly [" + resourceAssemblyName + "]");
            }

            fullResourceName = String.Format("{0}.{1}.{2}", info[0], info[1], info[2]);
            resourceName = String.Format("{0}.{1}", info[1], info[2]);
        }

        public Stream GetAssemblyResource()
        {
            Stream rs = null;
            if (assembly != null)
            {
                rs = assembly.GetManifestResourceStream(fullResourceName);
            }
            return rs;
        }

        protected string GetResourceNameWithoutProtocol(Uri uriParam)
        {
            int index = uriParam.Scheme.Length + Uri.SchemeDelimiter.Length;

            if (index == -1)
            {
                return uriParam.AbsoluteUri;
            }
            else
            {
                return uriParam.OriginalString.Substring(index);
            }
        }
    }
}
