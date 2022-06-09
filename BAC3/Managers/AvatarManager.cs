using IPA.Utilities;
using System.Linq;
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
            var visualController = AvatarPrefab.GetComponent<AvatarVisualController>();
            LoadAvatarData(ref visualController);
            s_instance = this;
        }

        public void LoadAvatarData(ref AvatarVisualController visualController)
        {
            Plugin.Logger.Debug("Setting visual avatar data");
            AvatarDataModel[] trs = Resources.FindObjectsOfTypeAll<AvatarDataModel>();
            Plugin.Logger.Debug($"There are {trs.Length} AvatarDataModels");

            // match visuals === begin ===
            AvatarDataModel dataModel = trs.First((x) => x.avatarData != null);
            AvatarData data = dataModel.avatarData;
            AvatarPartsModel partsModel = ReflectionUtil.GetField<AvatarPartsModel, AvatarDataModel>(trs[0], "_avatarPartsModel");

            // we set this so an exception isnt thrown when UpdateAvatarVisual() invokes UpdateAvatarColors()
            FieldAccessor<AvatarVisualController, AvatarPartsModel>.Set(visualController, "_avatarPartsModel", partsModel);
            visualController.UpdateAvatarVisual(data);

            Plugin.Logger.Debug("Successfully set all avatar sprites and meshes!");

            // TODO: Colours

            Plugin.Logger.Debug("Successfully set all avatar colors!");
        }
    }
}
