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
            var visualController = AvatarPrefab.GetComponent<AvatarVisualController>();
            LoadAvatarData(ref visualController);
            s_instance = this;
        }

        public void LoadAvatarData(ref AvatarVisualController visualController)
        {
            Plugin.Logger.Debug("Setting visual avatar data");
            AvatarDataModel[] trs = Resources.FindObjectsOfTypeAll<AvatarDataModel>();
            Plugin.Logger.Debug($"There are {trs.Length} AvatarDataModels");

            // match visuals
            AvatarData data = trs[0].avatarData;
            AvatarPartsModel partsModel = ReflectionUtil.GetField<AvatarPartsModel, AvatarDataModel>(trs[0], "_avatarPartsModel");

            AvatarMeshPartSO avatarMeshPartSO = partsModel.headTopCollection.GetById(data.headTopId) ?? partsModel.headTopCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_headTopMeshFilter").mesh = avatarMeshPartSO.mesh;
            
            AvatarMeshPartSO avatarMeshPartSO1 = partsModel.glassesCollection.GetById(data.glassesId) ?? partsModel.glassesCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_glassesMeshFilter").mesh = avatarMeshPartSO1.mesh;
            
            AvatarMeshPartSO avatarMeshPartSO2 = partsModel.facialHairCollection.GetById(data.facialHairId) ?? partsModel.facialHairCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_facialHairMeshFilter").mesh = avatarMeshPartSO2.mesh;
            
            AvatarMeshPartSO avatarMeshPartSO3 = partsModel.handsCollection.GetById(data.handsId) ?? partsModel.handsCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_leftHandsHairMeshFilter").mesh = avatarMeshPartSO3.mesh;
            visualController.GetField<MeshFilter, AvatarVisualController>("_rightHandsHairMeshFilter").mesh = avatarMeshPartSO3.mesh;

            AvatarMeshPartSO avatarMeshPartSO4 = partsModel.clothesCollection.GetById(data.clothesId) ?? partsModel.clothesCollection.GetDefault();
            visualController.GetField<MeshFilter, AvatarVisualController>("_bodyMeshFilter").mesh = avatarMeshPartSO4.mesh;

            AvatarSpritePartSO avatarSpritePartSO = partsModel.eyesCollection.GetById(data.eyesId) ?? partsModel.eyesCollection.GetDefault();
            visualController.GetField<SpriteRenderer, AvatarVisualController>("_eyesSprite").sprite = avatarSpritePartSO.sprite;

            AvatarSpritePartSO avatarSpritePartSO1 = partsModel.mouthCollection.GetById(data.eyesId) ?? partsModel.mouthCollection.GetDefault();
            visualController.GetField<SpriteRenderer, AvatarVisualController>("_mouthSprite").sprite = avatarSpritePartSO1.sprite;

            Plugin.Logger.Debug("Successfully set all avatar sprites and meshes!");


        }
    }
}
