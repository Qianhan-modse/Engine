using InfiniteMemories.OverWitch.qianhan.Start;
using InfiniteMemories.OverWitch.qianhan.Times;
using InfiniteMemories.OverWitch.qianhan.Worlds;

namespace InfiniteMemories.OverWitch.qianhan.Entities.item
{
    /// <summary>
    /// 表示掉落物的实体
    /// </summary>
    public class ItemEntity : Entity
    {
        public new string name;  // 实体名称
        private float lifetime;  // 掉落物的生命周期
        private float maxLifetime = 300f;  // 最大生存时间，例如 30 秒
        private bool dead;  // 是否已死亡
        private World World;  // 世界对象

        public ItemEntity(string name, long id, double posX, double posY, double posZ, World world) : base(name, id, posX, posY, posZ, world)
        {
            this.name = name;  // 设置掉落物名称
            this.lifetime = 0f;  // 初始化生命周期
            this.maxLifetime = 300;  // 设置最大生存时间
            this.dead = false;  // 初始化死亡状态
            this.World = world;  // 设置世界对象
        }

        public override void onEntityStart()
        {
            dead = false;
            lifetime = 0f;
            name = string.Empty;
        }

        // 更新方法，检查掉落物的生存时间
        [Chinese(ChinesePhase.Update)]
        private void onItemUpdate()
        {
            if (dead)
            {
                // 如果物品已死亡，移除实体
                World.removeEntity(this);  // 使用空安全操作符
            }
            else
            {
                // 增加生存时间
                lifetime += Time.deltaTime;  // 使用时间增量来增加生存时间

                // 如果超过最大生存时间，标记为死亡
                if (lifetime > maxLifetime)
                {
                    dead = true;
                    World.removeEntity(this);  // 移除实体
                }
            }
        }

        // 如果物品已死亡，移除
        public void checkAndRemoveIfDead()
        {
            if (dead)
            {
                World.removeEntity(this);  // 移除实体
            }
        }
    }
}
