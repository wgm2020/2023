using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    /// <summary>
    /// The central abstraction for iBATIS.NET's access to resources.
    /// </summary>
	public interface IResource : IDisposable
    {

        /// <summary>
        /// Returns the <see cref="System.Uri"/> handle for this resource.
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// Returns a <see cref="System.IO.FileInfo"/> handle for this resource.
        /// </summary>
        /// <remarks>
        /// <p>
        /// For safety, always check the value of the
        /// <see cref="System.Uri.IsFile "/> property prior to
        /// accessing this property; resources that cannot be exposed as 
        /// a <see cref="System.IO.FileInfo"/> will typically return
        /// <see langword="false"/> from a call to this property.
        /// </p>
        /// </remarks>
        /// <value>
        /// The <see cref="System.IO.FileInfo"/> handle for this resource.
        /// </value>
        /// <exception cref="System.IO.IOException">
        /// If the resource is not available on a filesystem, or cannot be
        /// exposed as a <see cref="System.IO.FileInfo"/> handle.
        /// </exception>
        FileInfo FileInfo { get; }

        /// <summary>
        /// Return an <see cref="System.IO.Stream"/> for this resource.
        /// </summary>
        /// <remarks>
        /// <note type="caution">
        /// Clients of this interface must be aware that every access of this
        /// property will create a <i>fresh</i> <see cref="System.IO.Stream"/>;
        /// it is the responsibility of the calling code to close any such
        /// <see cref="System.IO.Stream"/>.
        /// </note>
        /// </remarks>
        /// <value>
        /// An <see cref="System.IO.Stream"/>.
        /// </value>
        /// <exception cref="System.IO.IOException">
        /// If the stream could not be opened.
        /// </exception>
        Stream Stream { get; }

        /// <summary>
        /// Returns a description for this resource.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The description is typically used for diagnostics and other such
        /// logging when working with the resource.
        /// </p>
        /// <p>
        /// Implementations are also encouraged to return this value from their
        /// <see cref="System.Object.ToString()"/> method.
        /// </p>
        /// </remarks>
        /// <value>
        /// A description for this resource.
        /// </value>
        string Description { get; }

        /// <summary>
        /// Creates a resource relative to this resource.
        /// </summary>
        /// <param name="relativePath">
        /// The path (always resolved as relative to this resource).
        /// </param>
        /// <returns>
        /// The relative resource.
        /// </returns>
        /// <exception cref="System.IO.IOException">
        /// If the relative resource could not be created from the supplied
        /// path.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// If the resource does not support the notion of a relative path.
        /// </exception>
        IResource CreateRelative(string relativePath);
    }
}
