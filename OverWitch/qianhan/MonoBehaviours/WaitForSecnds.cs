using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteMemories.OverWitch.qianhan.MonoBehaviours
{
    public class WaitForSeconds : IYieldInstruction
    {
        private float waitTime;
        private float elapsedTime;

        public WaitForSeconds(float seconds)
        {
            waitTime = seconds;
            elapsedTime = 0f;
        }

        public bool KeepWaiting(float deltaTime)
        {
            elapsedTime += deltaTime;
            return elapsedTime < waitTime;
        }
    }
}
