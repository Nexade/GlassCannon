using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modding;
using UnityEngine;
using JetBrains.Annotations;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore.Utils;
using SFCore;
using UnityEngine.PlayerLoop;

namespace GlassCannonMod
{
    [UsedImplicitly]
    public class GlassCannonMod : Mod, ITogglableMod
    {
        public override string GetVersion() => "1.0";


        public override void Initialize()
        {
            ModHooks.HitInstanceHook += Hit;
            ModHooks.TakeDamageHook += Dam;
        }

        private int Dam(ref int hazardType, int damage)
        {
            damage = 20;
            return damage;
        }

        private HitInstance Hit(Fsm owner, HitInstance hit)
        {
            hit.DamageDealt = 1000;
            return hit;
        }

        public void Unload()
        {
            ModHooks.HitInstanceHook -= Hit;
            ModHooks.TakeDamageHook -= Dam;
        }
    }
}

