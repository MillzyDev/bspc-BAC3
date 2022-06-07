using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BAC3.Managers
{
    class AvatarManager : IInitializable
    {
        private static AvatarManager s_instance = null;
        public static AvatarManager Instance
        {
            get => s_instance;
        }

        public GameObject AvatarPrefab { get; private set; }

        public void Initialize()
        {
            AvatarPrefab = ObjectUtils.FindObject<AvatarPoseController>("PlayerAvatar", false);
            s_instance = this;
        }
    }
}
