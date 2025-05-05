using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteMemories.OverWitch.qianhan.Entities;
using InfiniteMemories.OverWitch.qianhan.MonoBehaviours;

namespace InfiniteMemories.OverWitch.qianhan.Objects.Behavuours
{
    /// <summary>
    /// 行为类，继承自组件类
    /// </summary>
    public class Behaviour : Component
    {
        public Behaviour(string name, string description, bool active, bool remove, Entity entity) : base(name, description, active, remove, entity)
        {
        }

        public bool enabled { get; set; } = true;
        public bool isActiveAndEnabled => enabled && !isRemove;
        public string name { get; set; } = "Behaviour";
        public string tag { get; set; } = "Untagged";
        public int layer { get; set; } = 0;
        public int instanceID { get; set; } = 0;
        public string fullName { get; set; } = "Behaviour";
        public string typeName { get; set; } = "Behaviour";
        /// <summary>
        /// 停止运行
        /// </summary>
        public virtual void Stop()
        {
            if (isRemove)
            {
                Console.WriteLine($"{name} 已经被移除，无法停止！");
                return;
            }
            if (enabled)
            {
                enabled = false;
                Console.WriteLine($"{name} 停止运行！");
            }
            else
            {
                Console.WriteLine($"{name} 已经停止运行！");
            }
        }
    }
}
