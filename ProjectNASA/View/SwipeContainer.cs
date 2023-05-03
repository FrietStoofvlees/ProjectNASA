using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.View
{
    public class SwipeContainer : ContentView
    {
        int pageId = 0;

        public SwipeContainer()
        {
            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Left));
            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Right));

            Swipe += OnSwiped;
        }

        public event EventHandler<SwipedEventArgs> Swipe;

        SwipeGestureRecognizer GetSwipeGestureRecognizer(SwipeDirection direction)
        {
            SwipeGestureRecognizer swipe = new SwipeGestureRecognizer { Direction = direction };
            swipe.Swiped += (sender, e) => Swipe?.Invoke(this, e);
            return swipe;
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            var currentShell = Shell.Current.CurrentItem.CurrentItem;
            string currentPage = currentShell.Route;

            currentPage = currentPage.Replace("IMPL_", "");

            if (Enum.TryParse<TabPages>(currentPage, out var tabPage))
            {
                pageId = (int)tabPage;
            }

            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    if (pageId == 0)
                        pageId = 2;
                    else 
                        pageId--;
                    ((AppShell)Application.Current.MainPage).SwitchtoTab((TabPages)pageId);
                    break;
                case SwipeDirection.Left:
                    if (pageId == 2)
                        pageId = 0;
                    else 
                        pageId++;
                    ((AppShell)Application.Current.MainPage).SwitchtoTab((TabPages)pageId);
                    break;
                default:
                    break;
            }
        }
    }
}
