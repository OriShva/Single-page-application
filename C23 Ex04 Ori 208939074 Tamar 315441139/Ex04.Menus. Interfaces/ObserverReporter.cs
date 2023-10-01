using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    internal class ObserverReporter<T>
    {
        private readonly List<T> r_Observers;

        internal ObserverReporter()
        {
            r_Observers = new List<T>();
        }

        internal void AddObserver(T i_Observer)
        {
            r_Observers.Add(i_Observer);
        }

        internal void RemoveObserver(T i_Observer)
        {
            r_Observers.Remove(i_Observer);
        }

        public void NotifyObservers(Menus.Interfaces.MenuItem i_Item)
        {
            foreach (T observer in r_Observers)
            {
                (observer as IMenuClickObserver).ReportClicked(i_Item);
            }
        }
    }
}