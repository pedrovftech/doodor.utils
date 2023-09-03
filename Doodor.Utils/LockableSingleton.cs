using System;

namespace Doodor.Utils
{
    public class LockableSingleton<TSingleton> where TSingleton : class
    {
        public static TSingleton Instance { get; private set; }
        public static bool InstanceLocked { get; private set; }

        public static void SetInstance(TSingleton instance)
        {
            if (InstanceLocked)
                throw new Exception("Instance is locked.");

            Instance = instance;
        }

        public static void LockInstance()
        {
            if (Instance == null)
                throw new Exception("Cannot lock a null instance.");

            InstanceLocked = true;
        }
    }
}