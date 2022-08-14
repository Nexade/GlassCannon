using Modding;
using Modding.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace GlassCannonButBetterProbably
{
    public class GlassCannonButBetter : Mod, ITogglableMod
    {
        public static GlassCannonButBetter LoadedInstance { get; set; }
        public GlassCannonButBetter() : base("GlassCannonButBetter") { }
        public static int blue = 0;
        public override List<(string, string)> GetPreloadNames()
        {
            return base.GetPreloadNames();
        }
        public override string GetVersion()
        {
            return "1.6.0";
        }
        public override void Initialize()
        {
            if (GlassCannonButBetter.LoadedInstance != null) return;
            GlassCannonButBetter.LoadedInstance = this;
            Log("I'm in");
            On.HealthManager.Start += HealthManager_Start;
            On.HealthManager.TakeDamage += HealthManager_TakeDamage;
            ModHooks.SlashHitHook += ModHooks_SlashHitHook;
            ModHooks.BeforeAddHealthHook += ModHooks_BeforeAddHealthHook;
            ModHooks.BlueHealthHook += ModHooks_BlueHealthHook;
        }

        private void ModHooks_SlashHitHook(Collider2D otherCollider, GameObject slash)
        {
            if (otherCollider.gameObject.TryGetComponent(out HealthManager self))
            {
                if (self.hp > 1)
                {
                    self.hp = 1;
                }
            }
            if (PlayerData.instance.health > 1 || PlayerData.instance.healthBlue > 0)
            {
                HeroController.instance.TakeHealth(PlayerData.instance.health - 1);
                PlayerData.instance.healthBlue = 0;
            }
        }

        private void HealthManager_TakeDamage(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            orig.Invoke(self, hitInstance);
            if (self.hp > 1)
            {
                self.hp = 1;
            }
            if (PlayerData.instance.health > 1 || PlayerData.instance.healthBlue > 0)
            {
                HeroController.instance.TakeHealth(PlayerData.instance.health - 1);
                PlayerData.instance.healthBlue = 0;
            }
        }

        private int ModHooks_BlueHealthHook()
        {
            PlayerData.instance.healthBlue = 0;
            return 0;
        }

        private int ModHooks_BeforeAddHealthHook(int arg)
        {
            return 0;
        }

        private void HealthManager_Start(On.HealthManager.orig_Start orig, HealthManager self)
        {
            orig.Invoke(self);
            if (self.hp > 1)
            {
                self.hp = 1;
            }
            if (PlayerData.instance.health > 1 || PlayerData.instance.healthBlue > 0)
            {
                HeroController.instance.TakeHealth(PlayerData.instance.health - 1);
                PlayerData.instance.healthBlue = 0;
            }
        }

        public void Unload()
        {
            On.HealthManager.Start -= HealthManager_Start;
            ModHooks.BeforeAddHealthHook -= ModHooks_BeforeAddHealthHook;
            ModHooks.BlueHealthHook -= ModHooks_BlueHealthHook;
            On.HealthManager.TakeDamage -= HealthManager_TakeDamage;
            ModHooks.SlashHitHook -= ModHooks_SlashHitHook;
            GlassCannonButBetter.LoadedInstance = null;
        }
    }
}
