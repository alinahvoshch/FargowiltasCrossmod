﻿
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.Projectiles.Boss;
using FargowiltasCrossmod.Core;
using FargowiltasSouls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FargowiltasCrossmod.Content.Calamity.Bosses.Perforators
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class PerfsEternity : EModeCalBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<PerforatorHive>());
        public override bool InstancePerEntity => true;
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            base.HitEffect(npc, hit);
        }
        public override void SetDefaults(NPC entity)
        {
            if (!WorldSavingSystem.EternityMode) return;
            base.SetDefaults(entity);
            entity.lifeMax = 7000;
            if (BossRushEvent.BossRushActive)
            {
                entity.lifeMax = 5000000;
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
        {
            base.ApplyDifficultyAndPlayerScaling(npc, numPlayers, balance, bossAdjustment);
        }
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            base.OnHitPlayer(npc, target, hurtInfo);
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            
            legpos = npc.Center;
            prevlegpos = npc.Center;
            legpos2 = npc.Center;
            prevlegpos2 = npc.Center;

            legpos3 = npc.Center;
            prevlegpos3 = npc.Center;
            legpos4 = npc.Center;
            prevlegpos4 = npc.Center;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);
        }
        Vector2 legpos = Vector2.Zero;
        Vector2 prevlegpos = Vector2.Zero;
        Vector2 legpos2 = Vector2.Zero;
        Vector2 prevlegpos2 = Vector2.Zero;

        Vector2 legpos3 = Vector2.Zero;
        Vector2 prevlegpos3 = Vector2.Zero;
        Vector2 legpos4 = Vector2.Zero;
        Vector2 prevlegpos4 = Vector2.Zero;

        int legtime = 0;
        int legtime2 = 15;
        float legprog = 0;
        float legprog2 = 0;

        int legtime3 = 30;
        int legtime4 = 45;
        float legprog3 = 0;
        float legprog4 = 0;
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!WorldSavingSystem.EternityMode) return true;
            Asset<Texture2D> leg = TextureAssets.Chains[0];
            spriteBatch.Draw(leg.Value, npc.Center - Main.screenPosition, null, drawColor, MathHelper.ToRadians(120), new Vector2(leg.Width() / 2, 0), new Vector2(1, 10), SpriteEffects.None, 0);
            spriteBatch.Draw(leg.Value, npc.Center - Main.screenPosition, null, drawColor, MathHelper.ToRadians(-120), new Vector2(leg.Width() / 2, 0), new Vector2(1, 10), SpriteEffects.None, 0);
            spriteBatch.Draw(leg.Value, npc.Center - Main.screenPosition, null, new Color(drawColor.R - 120, drawColor.G - 100, drawColor.B - 100), MathHelper.ToRadians(110), new Vector2(leg.Width() / 2, 0), new Vector2(1, 10), SpriteEffects.None, 0);
            spriteBatch.Draw(leg.Value, npc.Center - Main.screenPosition, null, new Color(drawColor.R - 120, drawColor.G - 100, drawColor.B - 100), MathHelper.ToRadians(-110), new Vector2(leg.Width() / 2, 0), new Vector2(1, 10), SpriteEffects.None, 0);

            Vector2 start3 = npc.Center + new Vector2(260, -90);
            Vector2 current3 = Vector2.Lerp(prevlegpos3, legpos3, 1 - (float)Math.Cos((legprog3 * Math.PI) / 2));
            float prog3 = Math.Abs(legprog3 - 0.5f) * 2;
            if (prevlegpos3.Distance(legpos3) < 100) prog3 = 1;
            spriteBatch.Draw(leg.Value, start3 - Main.screenPosition, null, new Color(drawColor.R - 120, drawColor.G - 100, drawColor.B - 100),
                (start3).AngleTo(current3) - MathHelper.PiOver2,
                new Vector2(leg.Width() / 2, 0), new Vector2(1, MathHelper.Lerp(start3.Distance(current3) / 50, start3.Distance(current3) / 28, prog3)), SpriteEffects.None, 0);

            Vector2 start4 = npc.Center + new Vector2(-260, -90);
            Vector2 current4 = Vector2.Lerp(prevlegpos4, legpos4, 1 - (float)Math.Cos((legprog4 * Math.PI) / 2));
            float prog4 = Math.Abs(legprog4 - 0.5f) * 2;
            if (prevlegpos4.Distance(legpos4) < 100) prog4 = 1;
            spriteBatch.Draw(leg.Value, start4 - Main.screenPosition, null, new Color(drawColor.R - 120, drawColor.G - 100, drawColor.B - 100),
                (start4).AngleTo(current4) - MathHelper.PiOver2,
                new Vector2(leg.Width() / 2, 0), new Vector2(1, MathHelper.Lerp(start4.Distance(current4) / 50, start4.Distance(current4) / 28, prog4)), SpriteEffects.None, 0);

            Vector2 start = npc.Center + new Vector2(240, -140);
            Vector2 current = Vector2.Lerp(prevlegpos, legpos, 1 - (float)Math.Cos((legprog * Math.PI) / 2));
            float prog = Math.Abs(legprog - 0.5f)*2;
            if (prevlegpos.Distance(legpos) < 100) prog = 1;
            spriteBatch.Draw(leg.Value, start - Main.screenPosition, null, drawColor,
                (start).AngleTo(current) - MathHelper.PiOver2,
                new Vector2(leg.Width() / 2, 0), new Vector2(1, MathHelper.Lerp(start.Distance(current) / 50, start.Distance(current)/28, prog)), SpriteEffects.None, 0);

            Vector2 start2 = npc.Center + new Vector2(-240, -140);
            Vector2 current2 = Vector2.Lerp(prevlegpos2, legpos2, 1 - (float)Math.Cos((legprog2 * Math.PI) / 2));
            float prog2 = Math.Abs(legprog2 - 0.5f) * 2;
            if (prevlegpos2.Distance(legpos2) < 100) prog2 = 1;
            spriteBatch.Draw(leg.Value, start2 - Main.screenPosition, null, drawColor,
                (start2).AngleTo(current2) - MathHelper.PiOver2,
                new Vector2(leg.Width() / 2, 0), new Vector2(1, MathHelper.Lerp(start2.Distance(current2) / 50, start2.Distance(current2) / 28, prog2)), SpriteEffects.None, 0);

            

            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override void DrawBehind(NPC npc, int index)
        {
            base.DrawBehind(npc, index);
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            base.FindFrame(npc, frameHeight);
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return HitPlayer ? base.CanHitPlayer(npc, target, ref cooldownSlot) : false;
        }
        public int lastAttack = 0;
        public int[] wormCycle = new int[] { 5, 5, 6, 5, 7 };
        public int attackCounter = -2;
        public bool HitPlayer = true;

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            for (int i = 0; i < wormCycle.Length; i++)
            {
                binaryWriter.Write7BitEncodedInt(wormCycle[i]);
            }
            binaryWriter.Write7BitEncodedInt(lastAttack);
            binaryWriter.Write7BitEncodedInt(attackCounter);
            binaryWriter.Write(HitPlayer);
        }
        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            for (int i = 0; i < wormCycle.Length; i++)
            {
                wormCycle[i] = binaryReader.Read7BitEncodedInt();
            }
            lastAttack = binaryReader.Read7BitEncodedInt();
            attackCounter = binaryReader.Read7BitEncodedInt();
            HitPlayer = binaryReader.ReadBoolean();
        }
        public override bool SafePreAI(NPC npc)
        {
            if (!WorldSavingSystem.EternityMode) return true;
            
            if (npc.target < 0 || Main.player[npc.target] == null || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest();
                NetSync(npc);
            }
            if (npc.target < 0 || Main.player[npc.target] == null || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.velocity.Y += 1;
                return false;
            }
            Player target = Main.player[npc.target];
            Vector2 toTarget = (target.Center - npc.Center).SafeNormalize(Vector2.Zero);
            //npc.velocity = Vector2.Lerp(npc.velocity, (target.Center - npc.Center + new Vector2(0, -300)).SafeNormalize(Vector2.Zero) * 10, 0.03f);
            //Movement
            
            //Dust.NewDustPerfect(legpos, DustID.TerraBlade);
            //Right leg movement
            legtime++;
            if (legtime > 60)
            {
                prevlegpos = legpos;
                legpos = new Vector2(npc.Center.X + 300, FindGround((int)npc.Center.X + 300, (int)npc.Center.Y));
                legprog = 0;
                legtime = 0;
            }
            if (legprog < 1)
            legprog += 0.05f;
            legtime2++;
            //Left leg movement
            if (legtime2 > 60)
            {
                prevlegpos2 = legpos2;
                legpos2=  new Vector2(npc.Center.X + -300, FindGround((int)npc.Center.X + -300, (int)npc.Center.Y));
                legprog2 = 0;
                legtime2 = 0;
            }
            if (legprog2 < 1)
                legprog2 += 0.05f;

            //Right leg movement
            legtime3++;
            if (legtime3 > 60)
            {
                prevlegpos3 = legpos3;
                legpos3 = new Vector2(npc.Center.X + 400, FindGround((int)npc.Center.X + 400, (int)npc.Center.Y));
                legprog3 = 0;
                legtime3 = 0;
            }
            if (legprog3 < 1)
                legprog3 += 0.05f;
            legtime4++;
            //Left leg movement
            if (legtime4 > 60)
            {
                prevlegpos4 = legpos4;
                legpos4 = new Vector2(npc.Center.X + -400, FindGround((int)npc.Center.X + -400, (int)npc.Center.Y));
                legprog4 = 0;
                legtime4 = 0;
            }
            if (legprog4 < 1)
                legprog4 += 0.05f;
            if (npc.ai[0] == 0)
            {
                Movement();
                npc.ai[1]++;
                int time = 150;
                if (npc.GetLifePercent() <= 0.6f) time = 100;
                if (npc.ai[1] >= time)
                {
                    npc.ai[0] = ChooseAttack();
                    npc.ai[1] = 0;
                    NetSync(npc);
                }
            }
            npc.rotation = MathHelper.ToRadians(npc.velocity.X * 2);
            if (npc.ai[0] == 1)
            {
                //Balls();
                Spikes();
                Movement();
            }
            if (npc.ai[0] == 2)
            {
                Slam();
            }
            if (npc.ai[0] == 3)
            {
                
                //Slam();
                Ichor();
                Movement();
                
            }
            if (npc.ai[0] == 4)
            {
                Balls();
            }
            if (npc.ai[0] == 5)
            {
                SmallWorm();
            }
            if (npc.ai[0] == 6)
            {
                MediumWorm();
            }
            if (npc.ai[0] == 7)
            {
                LargeWorm();
            }
            CalamityGlobalNPC.perfHive = npc.whoAmI;
            int ChooseAttack()
            {
                attackCounter++;
                if (attackCounter % 3 == 0)
                {
                    int theyworm = wormCycle[attackCounter / 3];
                    if (attackCounter >= 12)
                    {
                        attackCounter = 0;
                    }
                    if (theyworm > 5 && npc.GetLifePercent() > 0.7f) theyworm = 5;
                    if (theyworm > 6 && npc.GetLifePercent() > 0.4f) theyworm = 5;
                    return theyworm;
                    
                }
                List<int> possibilities = new List<int>() { 1 };
                if (npc.GetLifePercent() <= 0.95f){
                    possibilities.Add(2);
                }
                if (npc.GetLifePercent() <= 0.75f)
                {
                    possibilities.Add(3);
                }
                if (npc.GetLifePercent() <= 0.5f)
                {
                    possibilities.Add(4);
                }
                int attack = possibilities[Main.rand.Next(0, possibilities.Count)];
                int escape = 0;
                while (attack == lastAttack && escape < 10)
                {
                    escape++;
                    attack = possibilities[Main.rand.Next(0, possibilities.Count)];
                }
                lastAttack = attack;
                NetSync(npc);
                return attack;
                
            }
            void Movement()
            {
                
                if (target.Center.X > npc.Center.X)
                {
                    npc.velocity.X += 0.1f;
                    if (npc.velocity.X < 0)
                    {
                        npc.velocity.X += 0.2f;
                    }
                }
                else if (target.Center.X < npc.Center.X)
                {
                    npc.velocity.X -= 0.1f;
                    if (npc.velocity.X > 0)
                    {
                        npc.velocity.X -= 0.2f;
                    }
                }
                if (Math.Abs(npc.Center.Y - FindGround((int)npc.Center.X, (int)npc.Center.Y)) > 350)
                {
                    npc.velocity.Y += 0.2f;
                    if (npc.velocity.Y < 0)
                    {
                        npc.velocity.Y += 0.2f;
                    }
                }
                else
                {
                    npc.velocity.Y -= 0.2f;
                    if (npc.velocity.Y > 0)
                    {
                        npc.velocity.Y -= 0.2f;
                    }
                }
            }
            void LargeWorm()
            {
                if (NPC.AnyNPCs(ModContent.NPCType<LargePerforatorHead>()))
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    ChooseAttack();
                    
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<LargePerforatorHead>());
                    SoundEngine.PlaySound(SoundID.NPCDeath23, npc.Center);
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    NetSync(npc);
                }
            }
            void MediumWorm()
            {
                if (NPC.AnyNPCs(ModContent.NPCType<PerforatorHeadMedium>()))
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    ChooseAttack();
                    
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<PerforatorHeadMedium>());
                    SoundEngine.PlaySound(SoundID.NPCDeath23, npc.Center);
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    NetSync(npc);
                }
            }
            void SmallWorm()
            {
                Movement();
                npc.ai[1]++;
                int time = 30;
                if (npc.GetLifePercent() <= 0.7f) time += 30;
                if (npc.GetLifePercent() <= 0.25f) time += 30;
                if (npc.ai[1] % 30 == 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<PerforatorHeadSmall>());
                }
                if (npc.ai[1] >= time)
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    NetSync(npc);
                }
            }
            void Balls()
            {
                
                npc.velocity *= 0.95f;
                npc.ai[1] += 1;
                if (npc.ai[2] == 0 && npc.ai[1] >= 30 && npc.ai[1] <= 90)
                {
                    int i = (int)npc.ai[1] - 60;
                    if (npc.ai[1] % 3f == 0)
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit20, npc.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center + new Vector2(0, -100).RotatedBy(MathHelper.ToRadians(i * 3)), new Vector2(0, -8).RotatedBy(MathHelper.ToRadians(i*3)), ModContent.ProjectileType<ToothBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.damage), 0);
                    }
                    
                }
                if (npc.ai[1] > 90)
                {
                    npc.velocity.Y = 10;
                    npc.velocity.X = 0;
                }
                if (npc.ai[1] >= 120 || WorldGen.SolidTile(npc.Center.ToTileCoordinates()))
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    npc.ai[2] = 0;
                    NetSync(npc);
                }

            }
            void Ichor()
            {
                
                npc.velocity /= 1.05f;
                if (npc.ai[2] == 0 && npc.ai[1] == 0)
                {
                    npc.ai[1] = 0.5f;
                }
                if (npc.ai[2] % 2 == 0)
                {
                    npc.ai[1] += 0.05f;
                    if (npc.ai[1] >= 1)
                    {
                        npc.ai[2]++;
                    } 
                }
                if (npc.ai[2] % 2 != 0)
                {
                    npc.ai[1] -= 0.05f;
                    if (npc.ai[1] <= 0)
                    {
                        npc.ai[2]++;
                    }
                }
                if (npc.ai[2] > 3 && Main.rand.NextBool(9))
                {
                    Vector2 off = new Vector2(0, -70).RotatedBy(Main.rand.NextFloat(-0.9f, 0.9f));
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center + off, (npc.Center + off).AngleFrom(npc.Center).ToRotationVector2() * 6, ModContent.ProjectileType<IchorBlob>(), FargoSoulsUtil.ScaledProjectileDamage(npc.damage), 0);
                }
                
                npc.rotation = Utils.AngleLerp(-0.5f, 0.5f, -(float)(Math.Cos(Math.PI * npc.ai[1]) - 1) / 2);
                if (npc.ai[2] >= 8)
                {
                    npc.ai[2] = 0;
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    NetSync(npc);
                }
            }
            void Spikes()
            {
                npc.ai[1]++;
                if (npc.ai[1] < 60 && npc.ai[1] % 10 == 0)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit20, npc.Center);
                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDustDirect(npc.Center, 0, 0, DustID.CrimsonPlants, 0, 2, Scale: 1.5f);
                    }
                }
                if (npc.ai[1] > 60 && npc.ai[1] % 5 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item17 with { MaxInstances = 10}, npc.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, new Vector2(0, -3).RotatedBy(Main.rand.NextFloat(-0.5f, 0.5f)), ModContent.ProjectileType<BloodGeyser>(), FargoSoulsUtil.ScaledProjectileDamage(npc.damage), 0);
                }
                if (npc.ai[1] >= 120)
                {
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                    npc.ai[2] = 0;
                    NetSync(npc);
                }
            }
            void Slam()
            {
                npc.ai[1]++;
                if (npc.ai[1] == 1 && npc.ai[2] == 0)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath23, npc.Center);
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDustDirect(npc.Center, 0, 0, DustID.CrimsonPlants, 0, 2, Scale:1.5f);
                    }
                    
                }
                if (npc.ai[1] < 30 && npc.ai[2] == 0)
                {
                    npc.velocity /= 1.1f;
                    npc.Center = Vector2.Lerp(npc.Center, target.Center + new Vector2(target.velocity.X * 40, -400), 0.04f);
                    HitPlayer = false;
                    NetSync(npc);
                }
                if (npc.ai[2] == 0 && npc.ai[1] > 30)
                {
                    npc.velocity.Y = 15;
                    npc.velocity.X = 0;
                    HitPlayer = true;
                }
                if (WorldGen.SolidTile((npc.Center + new Vector2(0, 20)).ToTileCoordinates()) && npc.ai[2] == 0)
                {
                    npc.velocity.Y = 0;
                    npc.ai[2] = 1;
                    npc.ai[1] = 0;
                }
                if (npc.ai[2] == 1)
                {
                    npc.velocity.Y -= 0.1f;
                    if (npc.ai[1] >= 30)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        NetSync(npc);
                    }
                }
            }
            int FindGround(int x, int y)
            {
                int escape = 0;
                
                while (escape < 100 && !WorldGen.SolidTile(new Vector2(x, y).ToTileCoordinates()))
                {
                    y += 8;
                    escape++;
                    
                }
                return y;
            }
            return false;
        }
    }
}
