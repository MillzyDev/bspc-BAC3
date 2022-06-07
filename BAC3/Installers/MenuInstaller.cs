using BAC3.Managers;
using Zenject;

namespace BAC3.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AvatarManager>().AsSingle();
        }
    }
}
