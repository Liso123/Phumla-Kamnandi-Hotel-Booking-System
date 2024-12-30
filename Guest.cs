using System;

public class Guest : Person 
{
	private string _id;
	private string _email;
	private int _phone;


	public Guest()
	{

	}
	
	public string Id
	{ get { return _id; }
	  set {  _id = value; }
	}
	public string Email
	{ get { return _email; }
	  set {  _email = value; }
	}
	public int Phone
	{ get { return _phone; }
	set { _phone = value; }
	}
}
