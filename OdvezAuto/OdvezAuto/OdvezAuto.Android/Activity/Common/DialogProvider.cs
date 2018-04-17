using System;
using Android.App;
using Android.Content;

namespace OdvezAuto.Droid.Activity.Common
{
    public class DialogProvider
    {
        public static void ShowOkDialogWithoutAction(Context context, string qoute, string title, EventHandler<DialogClickEventArgs> onOkClickAction)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetMessage(qoute)
                .SetTitle(title)
                .SetCancelable(false)
                .SetPositiveButton("OK", (o, eventArgs) =>
                {
                    onOkClickAction?.Invoke(o, eventArgs);
                });
            ;
            AlertDialog alert = builder.Create();
            alert.Show();
        }
    }
}