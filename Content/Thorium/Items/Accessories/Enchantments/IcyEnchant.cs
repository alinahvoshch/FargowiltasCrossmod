﻿using Microsoft.Xna.Framework;
using FargowiltasCrossmod.Content.Thorium.Buffs;
using FargowiltasCrossmod.Content.Thorium.Items.Accessories.Enchantments;
using FargowiltasCrossmod.Content.Thorium.NPCs;
using FargowiltasCrossmod.Content.Thorium.Projectiles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static FargowiltasCrossmod.Core.ModCompatibility;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace FargowiltasCrossmod.Content.Thorium.Items.Accessories.Enchantments
{
    [ExtendsFromMod("ThoriumMod")]
    public class IcyEnchant : BaseEnchant
    {
        protected override Color nameColor => Color.DarkBlue;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.IcyEnch = true;
            DLCPlayer.IcyEnchItem = Item;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe()
        //        .Register();
        //}
    }
}

namespace FargowiltasCrossmod.Content.Thorium.Projectiles
{
    public class IcyEnchGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true; 
        public override bool AppliesToEntity(Projectile proj, bool lateInstantiation)
        {
            return proj.friendly && !proj.sentry && !proj.minion && proj.type != ProjectileID.NorthPoleSnowflake;
        }

        public int icySnowflackTimer;

        public override void PostAI(Projectile projectile)
        {
            if (projectile.active && projectile.damage > 0 && projectile.velocity.LengthSquared() > 1f && (Main.player[projectile.owner]).GetModPlayer<CrossplayerThorium>().IcyEnch)
            {
                if (++icySnowflackTimer > 30)
                {
                    icySnowflackTimer = 0;
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(0, Main.rand.Next(5)), ProjectileID.NorthPoleSnowflake, projectile.damage / 10, 0.4f, projectile.owner);
                }
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.active && projectile.damage > 0 && (Main.player[projectile.owner]).GetModPlayer<CrossplayerThorium>().IcyEnch)
            {
                modifiers.FinalDamage *= 0.8f;
            }
        }
    }
}
