using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    /// <summary>
    /// Return an <see cref="Apache.Ibatis.Common.Utilities.Resources.IResource"/> handle for the
    /// specified resource.
    /// </summary>
    public interface IResourceLoader
    {
        /// <summary>
        /// Check if this loader accepts the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True or false</returns>
        bool Accept(Uri uri);

        /// <summary>
        /// Return an <see cref="Apache.Ibatis.Common.Utilities.Resources.IResource"/> handle for the
        /// specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// An appropriate <see cref="Apache.Ibatis.Common.Utilities.Resources.IResource"/> handle.
        /// </returns>
        IResource Create(Uri uri);
    }
}
