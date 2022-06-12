using IPA.Utilities;
using System.Linq;
using UnityEngine;
using Zenject;

namespace BAC3.Managers
{
    class AvatarManager : IInitializable
    {
        private readonly AvatarDataModel _avatarDataModel;

        public AvatarManager(AvatarDataModel avatarDataModel)
        {
            _avatarDataModel = avatarDataModel;
        }

        public GameObject AvatarPrefab { get; private set; }

        public void Initialize()
        {
            AvatarPrefab = ObjectUtils.FindObject<AvatarTweenController>("AnimatedAvatar", false);
            var visualController = AvatarPrefab.GetComponentInChildren<AvatarVisualController>();
            LoadAvatarData(ref visualController);
        }

        public void LoadAvatarData(ref AvatarVisualController visualController)
        {
            Plugin.Logger.Debug("Setting visual avatar data");

            // match visuals === begin ===
            AvatarData data = _avatarDataModel.avatarData;
            AvatarPartsModel partsModel = ReflectionUtil.GetField<AvatarPartsModel, AvatarDataModel>(trs[0], "_avatarPartsModel");

            // we set this so an exception isnt thrown when UpdateAvatarVisual() invokes UpdateAvatarColors()
            FieldAccessor<AvatarVisualController, AvatarPartsModel>.Set(visualController, "_avatarPartsModel", partsModel);
            visualController.UpdateAvatarVisual(data);

            Plugin.Logger.Debug("Successfully set all avatar sprites and meshes!");

            visualController.UpdateAvatarVisual(data);
            AvatarPrefab.GetComponent<AvatarTweenController>().PresentAvatar();

            Plugin.Logger.Debug("Successfully set all avatar colors!");
        }
    }
}
