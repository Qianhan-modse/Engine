using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Objects;
using InfiniteMemories.OverWitch.qianhan.Util;

namespace InfiniteMemories.OverWitch.qianhan.Item
{
    public class item
    {
        public long id;
        public string name;
        public int MaxDamage;
        public int MinDamage;
        public int currentDamage;
        private MainObject mains;
        private DamageSource source;
        public virtual int setDamage(int v)
        {
            if(this.currentDamage<=0)
            {
                MainObject.removeObjects();
            }
            return v;
        }
        public virtual void onItemUpdate()
        {

        }
        protected void removeItem(string name)
        {
            MainObject.removeObjectDestroy(name);   
        }
        public virtual void hitEntity(EntityLivingBase livingBase,EntityLivingBase target,float value)
        {
            if(livingBase != null && target != null)
            {
                if (livingBase.getHealth() > 0)
                {
                    livingBase.setHealth(livingBase.getHealth() - value);
                    if (livingBase.getHealth() <= 0)
                    {
                        livingBase.livingBaseManager.onDeath(source);
                    }
                }
            }
        }
    }
}
