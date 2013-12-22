using System;
using System.Collections.Generic;
using System.Text;

namespace Cliffy.Common
{
    /// <summary>
    /// Enumerates the various EIF event types
    /// <para>TraceLog: Events application trace message for assistance in debugging, Configurable.</para>
    /// <para>AuditLog: Events audit messages.Always enabled in deployments, use sparingly.</para>
    /// <para>AdminLog: Events administrative messages Always enabled in deployments, use sparingly.</para>
    /// <para>ErrorLog: Events errors.  Always enabled in deployments, use sparingly.</para>
    /// </summary>
    public enum LogType
    {
        TraceLog = 1, AuditLog, ErrorLog
    }

    /// <summary>
    /// Defines sort directions.
    /// </summary>
    public enum SortDirection
    {
        Ascending = 1,
        Descending = -1
    }

	/// <summary>
	/// Defines possible image sizes.
	/// </summary>
	public enum ImageSize
	{
		/// <summary>
		/// A small thumbnail image to show on list pages.
		/// </summary>
		Thumbnail,

		/// <summary>
		/// A small(er) version of the image. Larger than a thumbnail, but not very big.
		/// </summary>
		Small,

		/// <summary>
		/// A medium sized version of the image.
		/// </summary>
		Medium,

		/// <summary>
		/// A large sized version of the image.
		/// </summary>
		Large,

		/// <summary>
		/// The original image.
		/// </summary>
		Original
	}
}