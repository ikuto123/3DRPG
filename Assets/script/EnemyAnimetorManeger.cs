using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimetorManeger : MonoBehaviour
{
    public EnemyContollor_Z enemyContollor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WeaponON()
    {
        enemyContollor.WeaponColl.enabled = true;
        //enemyContollor.rb.AddRelativeForce(Vector3.forward * enemyContollor.rb.mass * 10, ForceMode.Impulse);
        Debug.Log("攻撃しました");
        
    }

    public void WeaponOFF()
    {
        enemyContollor.WeaponColl.enabled = false;
        enemyContollor.animator.SetBool("attack0", false);
        enemyContollor.animator.SetBool("attack1", false);
        enemyContollor.animator.SetBool("attack2", false);
        Debug.Log("攻撃終わりました");
    }

   public void MagicON()
    {
        var _ma = Instantiate(enemyContollor.Magic, enemyContollor.MagicPos);
        _ma.transform.localPosition = Vector3.zero;
        _ma.transform.localEulerAngles = new Vector3(90,0,0);
        _ma.transform.parent = null;
        _ma.SetActive(true);
        Destroy(_ma , 3);
        Debug.Log("魔法を使用しました");
    }
}
