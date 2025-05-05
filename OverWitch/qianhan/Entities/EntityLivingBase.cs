using Assets.OverWitch.qianhan.Log.lang;
using EntityLivingBaseEvent;
using InfiniteMemories.OverWitch.Numbers.Assets.OverWitch.QianHan.Log.lang.logine;
using InfiniteMemories.OverWitch.qianhan.Entities.item;
using InfiniteMemories.OverWitch.qianhan.Events;
using InfiniteMemories.OverWitch.qianhan.Events.fml;
using InfiniteMemories.OverWitch.qianhan.Hooks;
using InfiniteMemories.OverWitch.qianhan.Util;
using OverWitch.QianHan.Log.network;

namespace InfiniteMemories.OverWitch.qianhan.Entities
{
    /// <summary>
    /// 生物基类
    /// </summary>
    public class EntityLivingBase:Entity
    {
        public LivingBaseManager livingBaseManager;
        Entity entity;
        protected Event Events;
        protected bool dead;
        protected bool isDodge;
        protected bool isSkill;

        public int Defense { get; private set; }
        public int Armores { get; private set; }
        public int Dodge { get; private set; }
        public static readonly DataParameter<float> Damage = new DataParameter<float>("damage");
        public EntityLivingBase():base() 
        {

        }
        public override void onEntityStart()
        {
            this.source=new DamageSource();
            entity = this;
            entity.isDead = false;
            base.onEntityStart();
        }
        /// <summary>
        /// 执行kill命令时
        /// </summary>
        public override void onKillCommands()
        {
            base.onKillCommands();
            this.attackEntityForm(DamageSource.OUT_OF_WORLD, float.MaxValue);
        }
        public class LivingBaseManager:EntityLivingBase
        {
            private float damage;
            private float MaxDamage;
            

            public override void hitEntity(Entity livingBase, DamageSource source, float damage)
            {
                base.hitEntity(livingBase, source, damage);
                livingBase = (EntityLivingBase)livingBase;
                if (livingBase.isDead)
                {
                    return;
                }
                livingBase.attackEntityForm(DamageSource.GENERIC, damage);
            }
            public void setMaxDamage(float value)
            {
                if (currentDamage <= 0)
                {
                    //Debug.Log("最大伤害值不能小于等于0");
                    return;
                }
                if (dataManagers == null) return;
                if (dataManagers != null)
                {
                    currentDamage = MaxDamage;
                    dataManagers.set<float>(Damage, MaxDamage);
                }
            }
            public float getDamage()
            {
                if(currentDamage<=0)
                {
                    return 0.0F;
                }
                if (dataManagers==null)
                {
                    return 0;
                }
                return dataManagers.get<float>(Damage);
            }
            public override bool attackEntityForm(DamageSource source, float value)
            {
                if (!ForgeHooks.onLivingAttack(this, source, value)) return false;
                EntityLivingBase livingBase = this;
                if (livingBase == null) return false;
                if(!Events.getGlobalMarkEvent())
                {
                    if(!livingBase.invulnerable||!livingBase.isDead)
                    {
                        float finalDamage = ApplyDamageReduction(source, value);
                        if(source.DeadlyDamage)
                        {
                            finalDamage = livingBase.getMaxHealth() * 0.65F;
                            source.setDeadlyDamageisArmor();
                        }
                        float currentHealth = livingBase.getHealth();
                        float newHealth = MathF.Max(currentHealth - finalDamage, 0.0F);
                        if(float.IsNaN(newHealth))
                        {
                            livingBase.setDeath();
                            livingBase.world.removeEntity(this);
                            Debug.LogWarning($"{this}的生命值为IsNaN，以诛杀");
                        }
                        livingBase.setHealth(newHealth);
                        if(newHealth<=0)
                        {
                            livingBase.setDeath();
                            this.onDeath(source);
                            return true;
                        }
                    }
                    return true;
                }
                return false;
            }

            public float ApplyDamageReduction(DamageSource source, float value)
            {
                throw new NotImplementedException();
            }

            public override void onDeath(DamageSource source)
            {
                if (!ForgeHooks.onLivingDeath(this, source)) return;
                //如果生物未死亡
                if (!this.isDead)
                {
                    //获取伤害来源实体
                    Entity entity = source.getTrueSource();
                    //获取当前攻击的实体对象
                    EntityLivingBase entityLivingBase = this.getAttackingEntity();
                    //如果当前生物不为空或生物不处于无敌状态
                    if (entityLivingBase != null || !this.isEntityAlive())
                    {
                        //设置生物为死亡
                        this.dead = true;
                        //创建生物死亡事件
                        LivingBaseDeathEvent deathEvent = new LivingBaseDeathEvent(this, source);
                        //发布生物死亡事件
                        EventBus.Publish(deathEvent);
                        //获取全局事件变量
                        if (deathEvent.getEvent())
                        {
                            //如果全局事件不是true
                            if (!deathEvent.getEvent())
                            {
                                //返回不执行
                                return;
                            }
                        }
                        //如果实体已经被确定为强制死亡
                        if (this.forceDead)
                        {
                            this.setDeath();
                        }
                        //如果不可见是false时
                        if (!isActive)
                        {
                            //将是否不可见设置为true
                            this.isActive = true;
                        }
                        this.isRecycle = true;
                        // 如果该生物有掉落物时
                        this.spawnItemEntity(entity);
                    }
                }
                // 如果无法标记为死亡则调用setDeath()方法强制标记为死亡状态
                this.setDeath();
            }
            /// <summary>
            /// 更新闪避逻辑
            /// </summary>
            private void UpdateDodge()
            {
                // 基础闪避逻辑，如果闪避值大于一定阈值，则设置为闪避成功
                float effectiveDodge = (float)Super.Clamped(Dodge, 0, 30);
                //不使用Unity的Random.Range方法而是我自定义的方法
                isDodge = effectiveDodge > Super.Clamps(0, 100);
            }
            //真实闪避
            public void OunDodge()
            {
                //如果是必中
                if (this.isSkill)
                {
                    this.setDamage(0.0F);//设置伤害值为0
                }
                float effectedDodge = (float)Super.Clamped(Dodge, 0, 50);
                //不使用Unity的Random.Range方法而是我自定义的方法
                isDodge = effectedDodge > Super.Clamps(1, 50);
            }
            //基础减免逻辑
            public float ApplyDamagedReduction(DamageSource source, float value)
            {
                float currentDamaged = value;
                UpdateDodge();
                if (isDodge && !isSkill)
                {
                    return 0;
                }
                Armors(currentDamaged, source);
                Defens(currentDamaged, source);
                return MathF.Max(currentDamaged, 0);
            }
            //盔甲逻辑，这只是个半成品
            public void Armors(float va1, DamageSource source)
            {
                if (source == null) return;
                if (source.isDamageAbsolute() || source.isUnblockable())
                {
                    return;
                }
                if (Armores > 0)
                {
                    damage = Armores < 1 ? damage * 0.99F : MathF.Max(damage - Armores, 0);
                }
            }
            //防御逻辑，只是一个半成品
            public void Defens( float va2, DamageSource damageSource)
            {
                if (damageSource == null) return;
                if (damageSource.isDamageAbsolute() || damageSource.isUnblockable())
                {
                    return;
                }
                if (Defense > 0)
                {
                    damage *= (1 - Super.Clamped(Defense / 100.0F, 0, 0.8F));
                }
            }
            public void spawnItemEntity(Entity entity)
            {
                Entity entity1 = (ItemEntity)entity;
                if (entity1 != null)
                {
                    if (entity1.currentVlaue == 0)
                    {
                        entity1.setDeath();
                    }
                    else
                    {
                        //Debug.LogError("你确定实体物品是实体的子类吗？");
                    }
                }
            }
            /// <summary>
            /// 枚举类型，表示随机值的范围
            /// </summary>
            public enum Left
            {
                None = 0,
                One = 1,
                Two = 2,
                Three = 3,
                Fourth = 4,
                Five = 5,
                Six = 6,
                Seven = 7,
                Eigh = 8,
                Nine = 9,
                Ten = 10,
            }
            /// <summary>
            /// 获取防御值
            /// </summary>
            public void getDefense()
            {
                if (Defense > 0)
                {
                    // 生成一个0到2之间的随机数并限制在[0, 2]的范围内
                    int randomLeftValue = (int)Super.Clamps((float)new System.Random().NextDouble() * 3, 2); // 生成 0 到 2 的随机值
                    Left randomLeft = (Left)randomLeftValue;

                    if (randomLeft == Left.One)
                    {
                        Armors(Armores, source);  // 执行 Armors 操作
                    }
                    else if (randomLeft == Left.Two)
                    {
                        Defens(Defense, source);  // 执行 Defens 操作
                    }
                }
            }
            public EntityLivingBase getAttackingEntity()
            {
                return (EntityLivingBase)this;
            }
            public void setDamage(float value)
            {
                float clamped=Super.Clamped(value, 0, MaxDamage);
                this.dataManagers.set<float>(Damage, clamped);
                this.damage = clamped;
            }
            public float getMaxDamage()
            {
                MaxDamage = dataManagers.get<float>(Damage);
                return MaxDamage;
            }
            public virtual void TakeDamage(float value,float amount)
            {
                //如果实体未被标记为无敌或者实体并不处于无敌状态时
                if (!this.getEntityAlive() || !entity.isDead)
                {
                    // 处理受到的伤害
                    float newHealth = MathF.Max(entity.getHealth() - amount, 0);
                    entity.setHealth(newHealth);
                    if (newHealth <= 0)
                    {
                        this.onDeath(source);
                    }
                }
                if (entity.isEntityAlive())
                {
                    return;
                }
            }
        }
    }
}
