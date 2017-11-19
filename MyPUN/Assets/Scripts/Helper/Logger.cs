using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger {

	public static void Write(object obj)
    {
        Debug.Log(obj);
        SystemInfoPanel.AddMessage(obj.ToString());
    }


}
