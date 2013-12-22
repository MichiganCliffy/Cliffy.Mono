using System;
using System.Collections;
using System.Collections.Generic;

namespace Cliffy.Common
{
	public class PhotographDateSort : IComparer, IComparer<Photograph>
	{
		private int mSortDirection = 1;

		public PhotographDateSort() : base() { }

		public PhotographDateSort(SortDirection direction)
		{
			this.mSortDirection = (int)direction;
		}

		int IComparer.Compare(object x, object y)
		{
			Photograph imageX = (Photograph)x;
			Photograph imageY = (Photograph)y;

			if ((imageX == null) && (imageY == null))
			{
				return 0;
			}
			else if ((imageX == null) && (imageY != null))
			{
				return (this.mSortDirection == 0) ? -1 : 1;
			}
			else if ((imageX != null) && (imageY == null))
			{
				return (this.mSortDirection == 0) ? 1 : -1;
			}
			else
			{
				return (this.mSortDirection == 0) ?
					imageX.DateSaved.CompareTo(imageY.DateSaved) :
					imageY.DateSaved.CompareTo(imageX.DateSaved);
			}
		}

		public int Compare(Photograph x, Photograph y)
		{
			return (this.mSortDirection == 0) ?
				x.DateSaved.CompareTo(y.DateSaved) :
				y.DateSaved.CompareTo(x.DateSaved);
		}
	}
}