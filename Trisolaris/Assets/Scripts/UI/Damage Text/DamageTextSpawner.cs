using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
       

        public void Spawn(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab,transform);
            instance.GetComponent<DamageText>().SetValue(damage);
        }

       
    }
}
