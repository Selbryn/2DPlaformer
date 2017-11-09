using UnityEngine;

// Standard notification class.  For specific needs subclass
public class Notification
{
    public NotificationTypes type;
    public object userInfo;

    public Notification(NotificationTypes type)
    {
        this.type = type;
    }

    public Notification(NotificationTypes type, object userInfo)
    {
        this.type = type;
        this.userInfo = userInfo;
    }

	public NotificationTypes Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}
}

//Typedefs
public class BoolNotification : Notification
{
	public bool varBool;
	
	public BoolNotification(NotificationTypes type, bool varBool) : base(type)
	{
		this.varBool = varBool;
	}
}

public class IntNotification : Notification
{
	public int varInt;
	
	public IntNotification(NotificationTypes type, int varInt) : base(type)
	{
		this.varInt = varInt;
	}
}

public class FloatNotification : Notification
{
	public float varFloat;
	
	public FloatNotification(NotificationTypes type, float varFloat) : base(type)
	{
		this.varFloat = varFloat;
	}
}

public class Vector3Notification : Notification
{
	public Vector3 varVector3;

	public Vector3Notification(NotificationTypes type, Vector3 varVector3) : base(type)
	{
		this.varVector3 = varVector3;
	}
}

public class StringNotification : Notification
{
	public string varString;
	
	public StringNotification(NotificationTypes type, string varString) : base(type)
	{
		this.varString = varString;
	}
}