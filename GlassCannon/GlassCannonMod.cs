using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modding;
using UnityEngine;
namespace GlassCannonMod
{
  
    public class GlassCannonMod : Mod, ITogglableMod
    {
        public override string GetVersion() => "1.1";


        public override void Initialize()
        {
            ModHooks.HitInstanceHook += Hit;
            ModHooks.TakeDamageHook += Dam;
        }

        private HitInstance Hit(HutongGames.PlayMaker.Fsm owner, HitInstance hit)
        {
            hit.DamageDealt = 1000;
            return hit;
        }

        private int Dam(ref int hazardType, int damage)
        {
            if(damage>0)
            {
                damage = 20;
            }
            return damage;
        }

        public void Unload()
        {
            ModHooks.HitInstanceHook -= Hit;
            ModHooks.TakeDamageHook -= Dam;
        }
    }
}

