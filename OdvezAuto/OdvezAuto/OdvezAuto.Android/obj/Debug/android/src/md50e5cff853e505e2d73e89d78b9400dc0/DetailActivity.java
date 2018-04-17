package md50e5cff853e505e2d73e89d78b9400dc0;


public class DetailActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("OdvezAuto.Droid.Activity.CarDetail.DetailActivity, OdvezAuto.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DetailActivity.class, __md_methods);
	}


	public DetailActivity ()
	{
		super ();
		if (getClass () == DetailActivity.class)
			mono.android.TypeManager.Activate ("OdvezAuto.Droid.Activity.CarDetail.DetailActivity, OdvezAuto.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
