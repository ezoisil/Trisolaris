using System;
using System.Collections;
using System.Collections.Generic;
using Trisolaris.Core;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Projectile : MonoBehaviour
    {
    
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        [SerializeField] float destructionTime = 15f;
        private Health target = null;
        float damage = 0;
       


        void Start()
        {
            transform.LookAt(GetAimLocation());
            StartCoroutine(DestroyInTime(destructionTime));
        }

        void Update()
        {
            if (target == null) { Destroy(gameObject); }

            if (isHoming && !target.IsDead()) 
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
            
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() != target) return; 
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            Destroy(gameObject);     
        }

        IEnumerator DestroyInTime(float destructionTime)
        {
            yield return new WaitForSeconds(destructionTime);
        }
    }
}
