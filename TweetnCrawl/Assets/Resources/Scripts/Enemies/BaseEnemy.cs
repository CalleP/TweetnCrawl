using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour {


    public int Health = 1;
    protected int Speed;
    
    
    


	
	// Update is called once per frame
	void Update () {
	
	}


    protected void Kill()
    {
        ///play death animation
        Destroy(gameObject);
    }



    public void TakeDamage(int amount) 
    {
        Health = Health - amount;
        if (Health <= 0)
        {
            Kill();
        }
    }



    protected void Move()
    { }

}
