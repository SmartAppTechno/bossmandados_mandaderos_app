package md5f0962ec36cc241549a26a9e18d4eb9dd;


public class UsuarioTestWrapper
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Common.DBItems.UsuarioTestWrapper, Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UsuarioTestWrapper.class, __md_methods);
	}


	public UsuarioTestWrapper ()
	{
		super ();
		if (getClass () == UsuarioTestWrapper.class)
			mono.android.TypeManager.Activate ("Common.DBItems.UsuarioTestWrapper, Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
