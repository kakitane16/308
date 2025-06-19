using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fan : MonoBehaviour
{
    public float Force = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // “–‚½‚Á‚½Object‚Ìƒ^ƒO‚ªPlayer‚Å‚Í‚È‚¢‚È‚çˆ—‚ğ‚µ‚È‚¢
        if (other.gameObject.tag != "Player") { return; }

        // Player‚ÌRigidbody‚ğæ“¾
        Rigidbody playerRb = other.attachedRigidbody;
        // î•ñ‚ª‚È‚¢‚È‚çˆ—‚ğ‚µ‚È‚¢
        if (playerRb == null) { return; }

        // ©•ª‚ÌŒü‚¢‚Ä‚¢‚é•ûŒü‚ğæ“¾
        Vector3 forward = transform.forward;
        // player‚ğ‚»‚Ì•ûŒü‚É”ò‚Î‚·
       // playerRb.velocity = Vector3.zero;
        playerRb.AddForce(forward.normalized * Force, ForceMode.VelocityChange);
    }
}
