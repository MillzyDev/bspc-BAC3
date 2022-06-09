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

            // match visuals
            AvatarData data = trs.First((x) => x.avatarData != null).avatarData;
            AvatarPartsModel partsModel = ReflectionUtil.GetField<AvatarPartsModel, AvatarDataModel>(trs[0], "_avatarPartsModel");

            Color color = partsModel.GetSkinColorById(data.skinColorId).Color;
            Color color1 = data.handsColor;

            AvatarMeshPartSO avatarMeshPartSO = partsModel.headTopCollection.GetById(data.headTopId) ?? partsModel.headTopCollection.GetDefault();
            MeshFilter headMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_headTopMeshFilter");
            headMeshFilter.mesh = avatarMeshPartSO.mesh;
            headMeshFilter.GetComponent<MeshRenderer>().material.color = color;
            
            AvatarMeshPartSO avatarMeshPartSO1 = partsModel.glassesCollection.GetById(data.glassesId) ?? partsModel.glassesCollection.GetDefault();
            MeshFilter glassesMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_glassesMeshFilter");
            glassesMeshFilter.mesh = avatarMeshPartSO1.mesh;
            
            AvatarMeshPartSO avatarMeshPartSO2 = partsModel.facialHairCollection.GetById(data.facialHairId) ?? partsModel.facialHairCollection.GetDefault();
            MeshFilter facialHairMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_facialHairMeshFilter");
            facialHairMeshFilter.mesh = avatarMeshPartSO2.mesh;
            
            AvatarMeshPartSO avatarMeshPartSO3 = partsModel.handsCollection.GetById(data.handsId) ?? partsModel.handsCollection.GetDefault();
            MeshFilter leftHandMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_leftHandsHairMeshFilter");
            leftHandMeshFilter.mesh = avatarMeshPartSO3.mesh;
            MeshFilter rightHandMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_rightHandsHairMeshFilter");
            rightHandMeshFilter.mesh = avatarMeshPartSO2.mesh;

            AvatarMeshPartSO avatarMeshPartSO4 = partsModel.clothesCollection.GetById(data.clothesId) ?? partsModel.clothesCollection.GetDefault();
            MeshFilter bodyMeshFilter = visualController.GetField<MeshFilter, AvatarVisualController>("_bodyMeshFilter");
            bodyMeshFilter.mesh = avatarMeshPartSO4.mesh;

            AvatarSpritePartSO avatarSpritePartSO = partsModel.eyesCollection.GetById(data.eyesId) ?? partsModel.eyesCollection.GetDefault();
            SpriteRenderer eyesRenderer = visualController.GetField<SpriteRenderer, AvatarVisualController>("_eyesSprite");
            eyesRenderer.sprite = avatarSpritePartSO.sprite;

            AvatarSpritePartSO avatarSpritePartSO1 = partsModel.mouthCollection.GetById(data.eyesId) ?? partsModel.mouthCollection.GetDefault();
            SpriteRenderer mouthRenderer = visualController.GetField<SpriteRenderer, AvatarVisualController>("_mouthSprite");
            mouthRenderer.sprite = avatarSpritePartSO1.sprite;

            Plugin.Logger.Debug("Successfully set all avatar sprites and meshes!");

            visualController.GetField<MulticolorAvatarPartPropertyBlockSetter, AvatarVisualController>("_clothesPropertyBlockSetter").SetColors(
                data.clothesPrimaryColor,
                data.clothesSecondaryColor,
                data.clothesDetailColor
                );

            visualController.GetField<MulticolorAvatarPartPropertyBlockSetter, AvatarVisualController>("_leftHandPropertyBlockSetter").SetColors(
                color,
                color1
                );

            visualController.GetField<MulticolorAvatarPartPropertyBlockSetter, AvatarVisualController>("_rightHandPropertyBlockSetter").SetColors(
                color,
                color1
                );

            visualController.GetField<AvatarPropertyBlockColorSetter, AvatarVisualController>("_glassesPropertyBlockColorSetter").SetColor(data.glassesColor);

            visualController.GetField<AvatarPropertyBlockColorSetter, AvatarVisualController>("_facialHairPropertyBlockColorSetter").SetColor(color);

            visualController.GetField<AvatarPropertyBlockColorSetter, AvatarVisualController>("_skinPropertyBlockColorSetter")
                .GetField<MaterialPropertyBlock, AvatarPropertyBlockColorSetter>("_materialPropertyBlock").SetColor(data.skinColorId, )

            Plugin.Logger.Debug("Successfully set all avatar colors!");
        }
    }
}
