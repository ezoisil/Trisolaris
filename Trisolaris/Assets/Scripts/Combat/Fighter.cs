using System;
using System.Collections;
using System.Collections.Generic;
using Trisolaris.Attributes;
using Trisolaris.Core;
using Trisolaris.Movement;
using Trisolaris.Saving;
using Trisolaris.Stats;
using UnityEngine;

namespace Trisolaris.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] WeaponConfig defaultWeaponConfig = null;

        
        Health target;
        Mover mover;
        float timeSinceLastAttack;
        Animator animator;
        WeaponConfig currentWeaponConfig = null;
        Weapon currentWeapon;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            currentWeaponConfig = defaultWeaponConfig;
            currentWeapon = SetupDefaultWeapon();

        }

        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeaponConfig);
        }

        private void Start()
        {
            EquipWeapon(currentWeaponConfig);
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon = AttachWeapon(weapon);
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
        public Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            if(currentWeapon != null)
            {
                currentWeapon.OnHit();
            }

            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);

            }
        }
        // This is an animation event
        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeaponConfig.GetRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health healthToTest = combatTarget.GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead() ;
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            WeaponConfig weapon = Resources.Load<WeaponConfig>((string)state);
            currentWeaponConfig = weapon;
        }

        public Health GetTarget()
        {
            return target;
        }

       
    }
}
