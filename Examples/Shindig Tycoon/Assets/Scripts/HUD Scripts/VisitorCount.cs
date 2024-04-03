using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisitorCount : MonoBehaviour

{
    public TextMeshProUGUI visitorCountText;
    // Start is called before the first frame update

    void Update()
    {
        visitorCountText.text = NPCManager.Instance.GetVisitorCount().ToString();
    }
}
