using UnityEngine;
using System.Collections.Generic;

// Each notification type should gets its own enum
public enum NotificationTypes {

	//Input
	oninputdone,			//Se ha realizado un input
	oninputfinished,		//Se ha terminado un input
	onjumppressed,			//Se aprieta el salto
	onreturntoground,		//Se aprieta la tecla de volver al suelo
	onshotpressed,

	onlevelfinished,

	TotalNotifications
};

// Notification delegate signature
public delegate void OnNotificationDelegate(Notification note);

public class NotificationCenter
{
    private static NotificationCenter instance;
	private List<OnNotificationDelegate>[] listeners =
		new List<OnNotificationDelegate>[(int)NotificationTypes.TotalNotifications];
	
    private NotificationCenter()
    {
        if (instance != null) {
			Debug.Log( "NotificationCenter instance is not null" );
            return;
        }

        instance = this;
    }
	
	~NotificationCenter()
	{
		instance = null;
	}

    public static NotificationCenter defaultCenter
    {
        get
        {
            if (instance == null) {
                new NotificationCenter();
			}

            return instance;
        }
    }
	
    public void addListener(OnNotificationDelegate newListenerDelegate, NotificationTypes type)
    {
        int typeInt = (int)type;

        // Create the listener List lazily
        if (listeners[typeInt] == null) {
			listeners[typeInt] = new List<OnNotificationDelegate>();
		}

        listeners[typeInt].Add(newListenerDelegate);
    }

    public void removeListener(OnNotificationDelegate listenerDelegate, NotificationTypes type)
    {
        int typeInt = (int)type;

        if (listeners[typeInt] == null) {
			return;
		}

        if (listeners[typeInt].Contains(listenerDelegate)) {
			listeners[typeInt].Remove(listenerDelegate);
		}

		// Clean up empty listener List
        if (listeners[typeInt].Count == 0) {
			listeners[typeInt] = null;
		}
    }
	
    public void postNotification(Notification note)
    {
        int typeInt = (int)note.type;

        if (listeners[typeInt] == null) {
			return;
		}

        foreach (OnNotificationDelegate delegateCall in listeners[typeInt]) {
			delegateCall(note);
        }
    }
}