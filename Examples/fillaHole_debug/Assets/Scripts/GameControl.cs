using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
	    DontDestroyOnLoad(this);
	    Screen.SetResolution(300,400,false);
    }
}
