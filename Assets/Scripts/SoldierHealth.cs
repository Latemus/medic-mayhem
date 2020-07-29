 using UnityEngine; 
 using System.Collections;
 
 public class SoldierHealth : MonoBehaviour { 
    public int curHealth = 5;
    public GameObject becomesAfterDying;

    private Vector3 dyingPosition;

    void FixedUpdate()
    {
        dyingPosition = transform.position;
    }

    // TODO: There is a bug somewhere in this function, that if 2 bullets hit the base
    // at the same time, the ResetGame function gets called twice earning 2 points for
    // the winning team. Luckily this is rare and non-fatal bug only affecting the 
    // score keeping. 
    public void AddDamage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            int faceUpOrDown = Random.Range (0, 2);
            GameObject new_soldier = Instantiate(becomesAfterDying, transform.position,
            Quaternion.Euler(new Vector3(90 + faceUpOrDown*180, 0, 0)));

            if(gameObject.name == "GreenBase" || gameObject.name == "TanBase")
            {
                int winning_team; 
                if (gameObject.CompareTag("Green"))
                {
                    winning_team = 1;
                }
                else 
                {
                    winning_team = 0;
                }
                GameStateController.instance.ResetGame(winning_team);
            }
            Destroy(gameObject);
        }
    }
}