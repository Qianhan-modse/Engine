using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteMemories.OverWitch.qianhan.Entities;

namespace InfiniteMemories.OverWitch.qianhan.Util
{
    public class DamageSource
    {
        public static DamageSource ANVIL = new DamageSource("anvil");
        public static DamageSource CRAMMING = (new DamageSource("cramming")).setDamageBypassesArmor();
        public static DamageSource DROWN = (new DamageSource("drown")).setDamageBypassesArmor();
        public static DamageSource FALL = (new DamageSource("fall")).setDamageBypassesArmor();
        public static DamageSource FALLING_BLOCK = new DamageSource("fallingBlock");
        public static DamageSource FIREWORKS = (new DamageSource("fireworks")).setExplosion();
        public static DamageSource FLY_INTO_WALL = (new DamageSource("flyIntoWall")).setDamageBypassesArmor();
        public static DamageSource GENERAL = new DamageSource("general");
        public static DamageSource GENERIC = (new DamageSource("generic")).setDamageBypassesArmor();
        public static DamageSource HOT_FLOOR = (new DamageSource("hotFloor")).setFireDamage();

        //伤害来源或者伤害源
        public static DamageSource DEADLY = (new DamageSource("deadly")).setDeadlyDamageisArmor();
        public static DamageSource IN_FIRE = (new DamageSource("inFire")).setFireDamage();
        public static DamageSource IN_WALL = (new DamageSource("inWall")).setDamageBypassesArmor();
        public static DamageSource LAVA = (new DamageSource("lava")).setFireDamage();
        public static DamageSource LIGHTNING_BOLT = new DamageSource("lightningBolt");
        public static DamageSource MAGIC = (new DamageSource("magic")).setDamageBypassesArmor().setDamageIsMagicDamage();
        public static DamageSource ON_FIRE = (new DamageSource("onFire")).setDamageBypassesArmor().setFireDamage();
        public static DamageSource OUT_OF_WORLD = (new DamageSource("outOfWorld")).setDamageBypassesArmor();
        public static DamageSource SKILL = (new DamageSource("skil").setExplosion().setDifficultyScaled());
        public static DamageSource WITHER = (new DamageSource("wither")).setDamageBypassesArmor();
        public bool attackDamage;//是否是攻击伤害
        public string damageType;//伤害类型，以this连用表示当前伤害类型
        private Entity entity;
        //private bool isDamageAllowedInCreativeMode;//是否在操作模式受伤，这个引用自minecraft;
        private bool damageIsAbsolute;//伤害是否是绝对值
        private bool difficultyScaled;//是否随难度变幻伤害
        private bool explosion;//是否为爆炸伤害
        private bool fireDamage;//是否是火焰伤害
        private float hungerDamage = 0.1F;//饥饿伤害值
        private bool IsUnblockable;//是否不可拦截
        private bool magicDamage;//是否为法术伤害
        private bool projectile;//是否是弹射物伤害
        private bool breakThrough;//击破
        private bool superSmash;//超击破
        public bool DeadlyDamage;//致命伤害
        public DamageSource(string v)
        {
            this.damageType = v;
        }
        public DamageSource() { }
        /// <summary>
        /// 判断是否是致命伤害
        /// </summary>
        /// <returns></returns>
        public bool isDeadlyDamage()
        {
            return DeadlyDamage;
        }
        /// <summary>
        /// 判断是否为超击破
        /// </summary>
        /// <returns></returns>
        public bool isSuperSmash()
        {
            return this.superSmash;
        }
        /// <summary>
        /// 判断是否是击破
        /// </summary>
        /// <returns></returns>
        public bool isBreakThrough()
        {
            return this.breakThrough;
        }
        /// <summary>
        /// 设置伤害为超击破伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setSuperSmashDamage()
        {
            this.superSmash = true;
            return this;
        }
        /// <summary>
        /// 设置伤害为击破伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setBreakThroughDamage()
        {
            this.breakThrough = true;
            return this;
        }
        /// <summary>
        /// 设置伤害无视护甲
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageBypassesArmor()
        {
            this.IsUnblockable = true;
            return this;
        }
        /// <summary>
        /// 获取伤害类型
        /// </summary>
        /// <returns></returns>
        public string getDamageType()
        {
            return this.damageType;
        }
        /// <summary>
        /// 判断伤害是否为绝对伤害
        /// </summary>
        /// <returns></returns>
        public bool isDamageAbsolute()
        {
            return this.damageIsAbsolute;
        }
        /// <summary>
        /// 设置致命伤害无视护甲
        /// </summary>
        /// <returns></returns>
        public DamageSource setDeadlyDamageisArmor()
        {
            this.DeadlyDamage = true;
            this.damageIsAbsolute = true;
            return this;
        }
        /// <summary>
        /// 设置伤害为致命的
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageisDeadlyDamage()
        {
            this.IsUnblockable = true;
            this.damageIsAbsolute = true;
            this.DeadlyDamage = true;
            return this;
        }
        /// <summary>
        /// 获取饥饿伤害
        /// </summary>
        /// <returns></returns>
        public float getHungerDamage()
        {
            return this.hungerDamage;
        }
        /// <summary>
        /// 判断伤害是否为火焰伤害
        /// </summary>
        /// <returns></returns>
        public bool isFireDamage()
        {
            return this.fireDamage;
        }
        /// <summary>
        /// 判断伤害是否为弹射物伤害
        /// </summary>
        /// <returns></returns>
        public bool isProjectile()
        {
            return this.projectile;
        }
        /// <summary>
        /// 判断伤害是否为不可防御
        /// </summary>
        /// <returns></returns>
        public bool isUnblockable()
        {
            return this.IsUnblockable; ;
        }
        /// <summary>
        /// 设置伤害为绝对的
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageIsAbsolute()
        {
            this.damageIsAbsolute = true;
            this.hungerDamage = 0.0F;
            return this;
        }
        /// <summary>
        /// 设置为魔法伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageIsMagicDamage()
        {
            this.magicDamage = true;
            entity.currentDamage = 300.0F;
            return this;
        }
        /// <summary>
        /// 设置伤害随难度改变
        /// </summary>
        /// <returns></returns>
        public DamageSource setDifficultyScaled()
        {
            this.difficultyScaled = true;
            return this;
        }
        /// <summary>
        /// 设置为爆炸伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setExplosion()
        {
            this.explosion = true;
            return this;
        }
        /// <summary>
        /// 设置为火焰伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setFireDamage()
        {
            this.fireDamage = true;
            return this;
        }

        /// <summary>
        /// 设置为虚空伤害
        /// </summary>
        /// <returns></returns>

        public DamageSource setVoidDamage()
        {
            this.difficultyScaled = false;
            entity.currentDamage = float.MaxValue;
            return this;
        }

        internal Entity getTrueSource()
        {
            throw new NotImplementedException();
        }
    }
}
