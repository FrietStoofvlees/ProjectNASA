using System.Collections.ObjectModel;

namespace ProjectNASA
{
    public static class Extensions
    {
        public static void OrderByDate(this ObservableCollection<Apod> collection)
        {
            if (collection.Count <= 1)
                return;

            ObservableCollection<Apod> temp = new(collection.OrderByDescending(apod => apod.Date));

            collection.Clear();
            foreach (Apod apod in temp)
            {
                collection.Add(apod);
            }
        }
    }
}
