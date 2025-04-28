using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //space‚ª„‚³‚ê‚½Œã‘äÀ‚Ì“–‚½‚è”»’è‚àÁ‚¦‚Ä‚Ù‚µ‚¢‚½‚ß
        //Destroy‚ÅÁ‚µ‚Ä‚¢‚é
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject);
        }
    }
}
