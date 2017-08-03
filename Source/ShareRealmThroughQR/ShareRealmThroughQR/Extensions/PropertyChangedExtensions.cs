using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ShareRealmThroughQR.Extensions
{
    public static class PropertyChangedExtensions
    {
        public static void WhenPropertyChanged<T>(this T obj, string property,
            Action<T> action) where T : INotifyPropertyChanged
        {
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == property)
                    action((T)sender);
            };
        }

        public static void WhenPropertyChanged<T>(this T obj, string property,
            Predicate<T> predicate, Action<T> action) where T : INotifyPropertyChanged
        {
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == property && predicate((T)sender))
                    action((T)sender);
            };
        }

        public static void WhenCollectionChanged<T>(this T obj,
            Func<T, NotifyCollectionChangedEventArgs, bool> predicate, Action<T> action)
            where T : INotifyCollectionChanged
        {
            obj.CollectionChanged += (sender, e) =>
            {
                if (predicate((T)sender, e))
                    action((T)sender);
            };
        }
    }
}
