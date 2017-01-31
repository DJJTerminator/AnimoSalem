using UnityEngine;
using System.Collections;

public class IndestructableScript : MonoBehaviour {
    public GameObject canvas;
    public GameObject battle;  //this stands for the canvas, battlesystem

    // Use this for initialization
    void Awake () {
		DontDestroyOnLoad(transform.gameObject);

        if (FindObjectsOfType(GetType()).Length > 2)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            DataStorage.battleSystem = GameObject.Find("All Canvases/BattleSystem");
            DataStorage.canvas = canvas;
            DataStorage.battleSystem = battle;
            canvas.SetActive(true);
            battle.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
