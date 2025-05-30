using System.Collections.Generic;
using UnityEngine;
public class Parabola : MonoBehaviour
{
    Player player;
    public void Start()
    {
        player = new Player();
    }
    public void Update()
    {
        Vector3 launchForce = player.transform.forward * player.forceStrength + Vector3.up * player.SAngleY;
        float mass = player.rb.mass;
    }
}
