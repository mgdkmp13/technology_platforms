using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using Rainbow.Diff;
using System.DirectoryServices;


namespace lab10
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSorted;
        private PropertyDescriptor sortProperty;
        private ListSortDirection sortDirection;

        public SortableBindingList() : base() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection dir)
        {
            if (Items is List<T> itemsList)
            {
                if (prop.PropertyType.GetInterfaces().Any(i => i == typeof(IComparable) || (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IComparable<>))))
                {
                    var comparer = new PropertyComparer<T>(prop, dir);
                    itemsList.Sort(comparer);
                    isSorted = true;
                    sortProperty = prop;
                    sortDirection = dir;
                }
                else
                {
                    throw new NotSupportedException("Error: No implementation of IComparable.");
                }
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            if (Items is List<T> itemsList)
            {
                foreach (var item in itemsList)
                {
                    var property = TypeDescriptor.GetProperties(typeof(T)).Find(prop.Name, true);
                    if (property != null)
                    {
                        var value = property.GetValue(item);
                        if (value != null && value.ToString().Contains(key.ToString()))
                            return itemsList.IndexOf(item);
                    }
                }
            }
            return -1;
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
            sortProperty = null;
            sortDirection = ListSortDirection.Ascending;
        }



        private class PropertyComparer<TItem> : IComparer<TItem>
        {
            private PropertyDescriptor _property;
            private ListSortDirection _direction;

            public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
            {
                _property = property;
                _direction = direction;
            }

            public int Compare(TItem x, TItem y)
            {
                object xValue = _property.GetValue(x);
                object yValue = _property.GetValue(y);

                int result = Comparer<object>.Default.Compare(xValue, yValue);
                return _direction == ListSortDirection.Ascending ? result : -result;
            }
        }

        public void SortBy(string property, ListSortDirection direction)
        {
            ApplySortCore(TypeDescriptor.GetProperties(typeof(T)).Find(property, true), direction);
        }


        public int Find(string property, object key)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(property, true);
            if (prop != null)
            {
                return FindCore(prop, key);
            }
            return -1;
        }

    }
}
