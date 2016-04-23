using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class ActivityComparer : IComparer<ActivityObjectBase>
    {
        private readonly bool _sortAscending;
        private readonly string _columnToSortOn;

        public ActivityComparer(bool sortAscending, string columnToSortOn)
        {
            _sortAscending = sortAscending;
            _columnToSortOn = columnToSortOn;
        }

        public int Compare(ActivityObjectBase x, ActivityObjectBase y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return ApplySortDirection(-1);
            if (y == null) return ApplySortDirection(1);

            switch (_columnToSortOn)
            {
                case "Name":
                    return ApplySortDirection(SortByName(x, y));

                //case "ShortName":
                //    return ApplySortDirection(SortByShortName(x, y));

                default:
                    throw new ArgumentOutOfRangeException(
                        string.Format("Can't sort on column {0}",
                        _columnToSortOn));
            }
        }

        //private int SortByShortName(ActivityObjectBase x, ActivityObjectBase y)
        //{
        //    return x.ShortName.CompareTo(y.ShortName);
        //}

        private int SortByName(ActivityObjectBase x, ActivityObjectBase y)
        {
            return x.Name.CompareTo(y.Name);
            //var lastNameResult = x.LastName.CompareTo(y.LastName);
            //if (lastNameResult != 0)
            //    return lastNameResult;
            //return x.FirstName.CompareTo(y.FirstName);
        }

        private int ApplySortDirection(int result)
        {
            return _sortAscending ? result : (result * -1);
        }
    }
}
