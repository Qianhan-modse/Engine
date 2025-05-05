using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.Objects;

namespace InfiniteMemories.OverWitch.qianhan.Worlds
{
    public class World
    {
        public void removeEntity(Entity entity)
        {
            if(entity!=null)
            {
                MainObject.removeObjectAll(entity.id);
            }
        }
    }
}
