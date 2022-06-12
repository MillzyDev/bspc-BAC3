using BAC3.Managers;
using BAC3.UI.FlowCoordinators;
using BAC3.UI.ViewControllers;
using BeatSaberMarkupLanguage;
using Zenject;

namespace BAC3.Installers
{
    internal class MenuInstaller : Installer
    {
        private readonly EditorFlowCoordinator _editorFlowCoordinator;
        private readonly EditorMainViewController _editorMainViewController;

        public MenuInstaller()
        {
            _editorFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<EditorFlowCoordinator>();
            _editorMainViewController = BeatSaberUI.CreateViewController<EditorMainViewController>();
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_editorFlowCoordinator);
            Container.BindInstance(_editorMainViewController);
            Container.BindInterfacesAndSelfTo<AvatarManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EditorUIManager>().AsSingle();
        }
    }
}
