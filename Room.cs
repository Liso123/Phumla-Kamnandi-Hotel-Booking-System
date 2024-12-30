using System;

public class Room 
{
	private string _roomID;
	private Price _price;
	private bool _availability;

	public Room()
	{
	}

	public string roomID
	{ get { return _roomID; }
	  set { _roomID = value; }
	}
	public double price
	{ get { return _price; }
	  set { _price = value; }
	}
	public bool availability
	{ get { return _availability; }
	  set {  _availability = value; }
	}
	
}
