using System.Collections;
using UnityEngine;

public class HoleScript : MonoBehaviour {
    public KeyCode holeKey;
    
    private bool _filled;
    private bool _interactable;

    private SpriteRenderer _sr;
    
    // Start is called before the first frame update
    void Start() {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.black;
        _filled = true;
        _interactable = false;

        // Start at a random time
        float startTime = Random.Range(3.0f, 5.0f);
        InvokeRepeating("HoleControl", startTime, 1);
    }

    // Update is called once per frame
    void Update(){
        if(_interactable){
            if (Input.GetKey(holeKey)) {
                _interactable = false;
                StopCoroutine("Open");
                Score();
                Fill();
            }
        }
    }

    private void HoleControl() {
        // Am I a filled hole
        if (_filled) { 
            // If so, what should we do with you
            int doWeOpen = Random.Range(0, 10);
            
            // Am I spawning?
            if (doWeOpen < 2) {
                // Open that hole!
                StartCoroutine("Open");
            }
        }
    }

    private IEnumerator Open() {
        _sr.color = Color.white;
        _filled = false;
        _interactable = true;
        yield return new WaitForSeconds(2);
        // Are we still unfilled?
        if (!_filled) {
            // Oh no! You took too long
            _interactable = false;
            _sr.color = Color.red;
            AudioControl.S.PlayMiss();
            ScoreScript.S.DecreaseScore();
            yield return new WaitForSeconds(1);
            Fill();
        }

        yield return null;
    }

    private void Score() {
        AudioControl.S.PlayFill();
        ScoreScript.S.IncreaseScore();
    }

    private void Fill() {
        _sr.color = Color.black;
        _interactable = false;
        _filled = true;
    }
} 
