using InfiniteMemories.OverWitch.Numbers.Assets.OverWitch.QianHan.Log.lang.logine;
using InfiniteMemories.OverWitch.qianhan.Util;
using InfiniteMemories.OverWitch.qianhan.Worlds;
using OverWitch.QianHan.Log.network;

namespace InfiniteMemories.OverWitch.qianhan.Entities
{
    /// <summary>
    /// 实体基础类,所有实体必须继承
    /// </summary>
    public class Entity
    {
        public World world;
        public double posX;
        public double posY;
        public double posZ;
        public bool isDead;
        internal bool forceDead;
        public bool isKey;
        public bool isClearDebuff;
        public bool isRemoved;
        public bool isActive;
        protected bool isAlive;
        private bool isEntity;
        public bool isUpdate;
        public bool isAi;
        protected bool invulnerable;
        public bool isFallingObject;
        public bool isInRemovingProcess;
        public bool isRecycle;
        protected float MaxHealth;
        protected float MinHealth;
        protected float currentHealth;
        public DataManager dataManagers;
        public DataParameter<float> MAXHEALTH = new DataParameter<float>("Max_Health");
        public DataParameter<float> CURRENTHEALTH = new DataParameter<float>("Current_Health");
        public float currentDamage;
        public DamageSource source;
        private int max;
        private int min;
        public string Name;
        public long id;
        internal int currentVlaue;

        public Entity()
        {
            source = new DamageSource();
            Name = "Entity";
        }

        public Entity(string name, long id, double posX, double posY, double posZ, World world)
        {
            Name = name;
            this.id = id;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.world = world;
        }

        /// <summary>
        /// 当实体被激活时
        /// </summary>
        public virtual void onEntityAwake() 
        {
            max = 5;
            min = 3;
            if(dataManagers==null)
            {
                dataManagers = new DataManager();
            }
        }
        public virtual void hitEntity(Entity entity,DamageSource source,float value)
        {
            if(this.getHealth()==0)
            {
                return;
            }
        }
        /// <summary>
        /// 当实体开始处理时
        /// </summary>
        public virtual void onEntityStart()
        {
            dataManagers = new DataManager();
            MAXHEALTH = new DataParameter<float>("Max_Health");
            CURRENTHEALTH = new DataParameter<float>("Current_Health");
        }
        /// <summary>
        /// 当实体更新时
        /// </summary>
        public virtual void onEntityUpdate()
        {
            if(isKey)
            {
                MaxHealth = dataManagers.get<float>(MAXHEALTH);
                currentHealth = dataManagers.get<float>(CURRENTHEALTH);
                min++;
                //启动while循环保证只执行一次
                while (min < max)
                {
                    dataManagers.get<float>(MAXHEALTH);
                    dataManagers.get<float>(CURRENTHEALTH);
                    isKey = false;
                    break;
                }
            }
        }
        //设置死亡
        public virtual void setDeath()
        {
            if (this.invulnerable)
            {
                invulnerable = false;
            }
            this.isDead = true;
            this.forceDead = true;
            this.isRecycle = true;
        }
        /// <summary>
        /// 当实体死亡时调用
        /// </summary>
        public virtual void onEntityDeath()
        {
            this.isDead = true;
        }
        /// <summary>
        /// 当实体复活时调用
        /// </summary>
        public virtual void onEntityLiving()
        {
            this.isDead = false;
        }
        /// <summary>
        /// 当实体被击杀时调用
        /// </summary>
        public virtual void onKillEntity()
        {
            this.isDead = true;
            this.forceDead = true;
            this.isRecycle = true;
        }
        /// <summary>
        /// 更好的补充生命值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float Health(float value)
        {
            setHealth(value);
            return getHealth();
        }
        /// <summary>
        /// 实体死亡时调用
        /// </summary>
        /// <param name="source"></param>
        public virtual void onDeath(DamageSource source)
        {

        }
        /// <summary>
        /// 生命值恢复逻辑
        /// </summary>
        /// <param name="amount"></param>
        public virtual void RegenHealth(float amount)
        {
            if (currentHealth <= 0)
            {
                return;
            }
            else
            {
                currentHealth = Super.Min(currentHealth + amount, MaxHealth);
                dataManagers.set<float>(CURRENTHEALTH, currentHealth);
            }
        }
        /// <summary>
        /// 获取生命值
        /// </summary>
        /// <returns></returns>

        public float getHealth()
        {
            if (dataManagers == null)
            {
                onEntityAwake();
                return 0;
            }
            //return dataManager.get<float>(CURRENTHEALTH);
            return currentHealth;
        }
        /// <summary>
        /// 设置最大生命值
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public void setMaxHealth(float value)
        {
            if (float.IsNaN(value))
            {
                setDeath();
                throw new ArgumentException($"当前目标生命值为NaN,以及强制击杀{this}");
            }
            if (value <= 0) { throw new ArgumentException("最大生命值必须大于0"); }
            dataManagers.set<float>(MAXHEALTH, value);
            if (this.getHealth() > value) setHealth(value);
            else
            {
                setHealth(this.getHealth());
            }
        }
        /// <summary>
        /// 攻击实体
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool attackEntityForm(DamageSource source, float value)
        {
            return false;
        }
        /// <summary>
        /// 当执行kill命令时
        /// </summary>
        public virtual void onKillCommands()
        {
            isDead = true;
        }
        /// <summary>
        /// 判断实体是否处于无敌状态
        /// </summary>
        /// <returns></returns>
        public virtual bool isEntityAlive()
        {
            return !this.invulnerable;
        }
        /// <summary>
        /// 设置实体无敌状态为true/false
        /// </summary>
        /// <param name="v"></param>
        public void setEntityAlive(bool v)
        {
            this.invulnerable = v;
        }
        /// <summary>
        /// 获取实体无敌状态
        /// </summary>
        /// <returns></returns>
        public bool getEntityAlive()
        {
            return invulnerable;
        }
        /// <summary>
        /// 设置生命值
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public void setHealth(float value)
        {
            if (float.IsNaN((float)value))
            {
                setDeath();
                throw new ArgumentException($"生命值为NaN，无法设置,以强制击杀目标{this}");
            }
            if (dataManagers == null)
            {
                onEntityAwake();
                return;
            }
            float clampedValue = Super.Clamped(value, 0, this.getMaxHealth());
            dataManagers.set<float>(CURRENTHEALTH, clampedValue);
            currentHealth = clampedValue;
        }
        /// <summary>
        /// 获取最大生命值上限
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public float getMaxHealth()
        {
            if (float.IsNaN(MaxHealth))
            {
                setDeath();
                throw new ArgumentException($"当前目标生命值为NaN,以及强制击杀{this}");
            }
            if (dataManagers != null)
            {
                //Debug.Log("dataManager不为null，已经获取到实体的最大生命值");
                //MaxHealth = dataManager.get<float>(MAXHEALTH);
                if (MaxHealth < 0)
                {
                    throw new ArgumentException("最大生命值为0，无法获取已经为零或者小于零的生命值");
                }
                MaxHealth = dataManagers.get<float>(MAXHEALTH);
                return MaxHealth;
            }
            return MaxHealth;
        }
        public Entity getEntity()
        {
            this.isEntity = true;
            return this;
        }
    }
}
