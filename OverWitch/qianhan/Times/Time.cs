using InfiniteMemories.OverWitch.qianhan.annotations.meta;
using InfiniteMemories.OverWitch.qianhan.annotations;
using InfiniteMemories.OverWitch.qianhan.Enums;
using InfiniteMemories.OverWitch.qianhan.Start;

namespace InfiniteMemories.OverWitch.qianhan.Times
{
    [StaticAccessor("getTimeManager()", StaticAccessorType.Dot)]
    public static class Time
    {
        public static float TimeSinceStartup => time;
        public static float DeltaTime => deltaTime;
        public static float UnscaledDeltaTime => rawDelta;
        public static float TimeScale
        {
            get => timeScale;
            set => timeScale = MathF.Max(0f, value);
        }

        public static float UnscaledTime => unscaledTime;
        public static float FixedDeltaTime { get; set; } = 0.02f;
        public static float FixedTime => fixedTime;
        public static int FrameCount => frameCount;
        public static bool InFixedTimeStep { get; private set; } = false;

        public static float time;
        public static float deltaTime;
        public static float unscaledTime;
        public static float rawDelta;
        public static float timeScale = 1f;
        public static float fixedTime;
        public static float accumulator;
        public static int frameCount;

        // 每帧调用
        [Chinese(ChinesePhase.Update)]
        public static void Update(float delta)
        {
            rawDelta = delta;
            deltaTime = delta * timeScale;
            time += deltaTime;
            unscaledTime += delta;
            frameCount++;

            accumulator += deltaTime;
            while (accumulator >= FixedDeltaTime)
            {
                InFixedTimeStep = true;
                fixedTime += FixedDeltaTime;
                accumulator -= FixedDeltaTime;
                InFixedTimeStep = false;
                // 此处你可以触发 FixedUpdate 的委托或调度器
            }
        }

        public static void Reset()
        {
            time = 0f;
            deltaTime = 0f;
            unscaledTime = 0f;
            fixedTime = 0f;
            accumulator = 0f;
            frameCount = 0;
        }
    }

    public static class TimeManager
    {
        public static float time { get; private set; }
        public static float deltaTime { get; private set; }
        public static float timeScale { get; set; } = 1f;

        // 更新方法，每帧调用
        [Chinese(ChinesePhase.Update)]
        public static void Update(float delta)
        {
            deltaTime = delta * timeScale;
            time += deltaTime;
        }
    }
    public static class TimeScheduler
    {
        private class Task
        {
            public float Delay;
            public float Elapsed;
            public int RepeatCount; // -1 = 无限
            public float Interval;
            public required Action Callback;
            public bool UseUnscaled;
            public bool IsRepeating;
        }

        private static readonly List<Task> tasks = new();
        private static readonly List<Task> temp = new(); // 用于避免遍历时修改集合

        public static void Schedule(float delay, Action callback, bool useUnscaledTime = false)
        {
            if (callback == null || delay < 0f) return;
            tasks.Add(new Task
            {
                Delay = delay,
                Callback = callback,
                UseUnscaled = useUnscaledTime,
                RepeatCount = 1
            });
        }

        public static void ScheduleRepeat(float interval, Action callback, int repeatCount = -1, bool useUnscaledTime = false)
        {
            if (callback == null || interval <= 0f) return;
            tasks.Add(new Task
            {
                Delay = interval,
                Interval = interval,
                Callback = callback,
                UseUnscaled = useUnscaledTime,
                IsRepeating = true,
                RepeatCount = repeatCount
            });
        }
        [Chinese(ChinesePhase.onUpdate)]
        public static void Update(float delta)
        {
            if (tasks.Count == 0) return;
            temp.Clear();
            temp.AddRange(tasks);

            foreach (var task in temp)
            {
                float dt = task.UseUnscaled ? Time.UnscaledDeltaTime : Time.DeltaTime;
                task.Elapsed += dt;

                if (task.Elapsed >= task.Delay)
                {
                    task.Callback?.Invoke();
                    task.Elapsed = 0f;

                    if (task.IsRepeating)
                    {
                        if (task.RepeatCount > 0)
                        {
                            task.RepeatCount--;
                            if (task.RepeatCount == 0)
                                tasks.Remove(task);
                        }
                    }
                    else
                    {
                        tasks.Remove(task);
                    }
                }
            }
        }
        [DestoryEnum(Destory.Return)]
        public static void Clear()
        {
            tasks.Clear();
        }
    }
}
