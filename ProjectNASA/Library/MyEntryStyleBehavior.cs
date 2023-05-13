using Maui.FixesAndWorkarounds;

//https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/behaviors#consume-a-net-maui-behavior-with-a-style

namespace ProjectNASA.Library
{
    public class MyEntryStyleBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty AttachBehaviorProperty =
            BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(MyEntryStyleBehavior), false, propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            if (view is not Entry entry)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                entry.Behaviors.Add(new TapToCloseBehavior());
            }
            else
            {
                Behavior toRemove = entry.Behaviors.FirstOrDefault(b => b is TapToCloseBehavior);
                if (toRemove != null)
                {
                    entry.Behaviors.Remove(toRemove);
                }
            }
        }
    }
}
