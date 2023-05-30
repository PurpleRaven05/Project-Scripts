using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager:MonoBehaviour
{
    
    public EnemyBaseProjectile CreateProjectile(){
        UnityEngine.Debug.Log("Invoca");
        GameObject projectileGo = Instantiate(Resources.Load("Projectile") as GameObject);
        return projectileGo.GetComponent<EnemyBaseProjectile>();
    }
}
