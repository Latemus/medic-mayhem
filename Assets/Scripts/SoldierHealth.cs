 using UnityEngine; 
 using System.Collections;
 
 public class SoldierHealth : MonoBehaviour { 
 public int maxHealth = 5; 
 public int curHealth = 5;
 
 //initialization 
 void Start () { } 
 
 // Update 
 void Update () { 
     if (curHealth <1){ 
         Destroy(gameObject); 
     } 
 } 
 private void OnTriggerEnter(Collider col) { 
     Debug.Log("Bullet tag is: "+col.gameObject.tag);
     if (col.transform.gameObject.tag == "bullet"){ 
        curHealth -= 1;
        Destroy(col.transform.gameObject);
     } 
 }
}