using System;
using System.Collections;
using System.Collections.Generic;
using Trisolaris.Core;
using Trisolaris.Movement;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;


        Health target;
        Mover mover;
        float timeSinceLastAttack;
        Animator animator;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SpawnWeapon();
        }

      
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;


            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
        private void SpawnWeapon()
        {
            if (weapon == null) return;
            weapon.Spawn(handTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will tritgger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttacking");
            animator.SetTrigger("attack");
        }

        // This is an animation event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weapon.GetDamage());
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            Debug.Log("attack!");          
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttacking");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health healthToTest = combatTarget.GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead() ;
        }
    }
}
