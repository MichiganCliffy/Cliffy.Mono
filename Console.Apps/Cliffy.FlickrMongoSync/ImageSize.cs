using System;

namespace Cliffy.FlickrMongoSync
{
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