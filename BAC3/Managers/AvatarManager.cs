using IPA.Utilities;
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

        public void LoadAvatarData()
        {
            Plugin.Logger.Debug("Setting visual avatar data");
            AvatarDataModel[] trs = Resources.FindObjectsOfTypeAll<AvatarDataModel>();
            Plugin.Logger.Debug($"There are {trs.Length} AvatarDataModels");

            AvatarData data = trs[0].avatarData;
            AvatarPartsModel partsModel = ReflectionUtil.GetField<AvatarPartsModel, AvatarDataModel>(trs[0], "_avatarPartsModel");
            AvatarVisualController visualController = AvatarPrefab.GetComponent<AvatarVisualController>();

            AvatarMeshPartSO avatarMeshPartSO = partsModel.headTopCollection.GetById(data.headTopId) ?? partsModel.headTopCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_headTopMeshFilter").mesh = avatarMeshPartSO.mesh;
        }
    }
}
