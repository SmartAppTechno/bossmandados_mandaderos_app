package md5f0df50de2868be123f4a356284f07269;


public class MapInvoker
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BossMandadero.MapInvoker, BossMandadero, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MapInvoker.class, __md_methods);
	}


	public MapInvoker ()
	{
		super ();
		if (getClass () == MapInvoker.class)
			mono.android.TypeManager.Activate ("BossMandadero.MapInvoker, BossMandadero, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MapInvoker (android.app.Activity p0, int p1)
	{
		super ();
		if (getClass () == MapInvoker.class)
			mono.android.TypeManager.Activate ("BossMandadero.MapInvoker, BossMandadero, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.App.Activity, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:BossMandadero.MapType, BossMandadero, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}

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
