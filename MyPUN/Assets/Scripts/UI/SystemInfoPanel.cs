using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemInfoPanel : MonoBehaviour {

    [SerializeField]
    private Text content;

    private static SystemInfoPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  static void AddMessage(string cont)
    {
        Instance.content.text += "\n";
        Instance.content.text += cont;
    }

}
