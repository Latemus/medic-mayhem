 using UnityEngine; 
 using System.Collections;
 
 public class SoldierHealth : MonoBehaviour { 
    public int curHealth = 5;

    public GameObject becomesAfterDying;

    private Vector3 dyingPosition;

    void Update()
    {
        dyingPosition = transform.position;
    }

    void OnDestroy()
    {
        int faceUpOrDown = Random.Range (0, 2);
        GameObject new_soldier = Instantiate(becomesAfterDying, dyingPosition,
            Quaternion.Euler(new Vector3(90 + faceUpOrDown*180, 0, 0)));
    }
    
}