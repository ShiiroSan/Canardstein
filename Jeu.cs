using IrrlichtLime;
using IrrlichtLime.Core;
using IrrlichtLime.Scene;
using IrrlichtLime.Video;

namespace Canardstein
{
    public class Jeu
    {
        private static void Main(string[] args) { Jeu jeu = new Jeu(); }

        private IrrlichtDevice Device;
        private uint DerniereFrame = 0;
        private bool K_Avant, K_Arriere, K_Gauche, K_Droite;

        public Jeu()
        {
            Device = IrrlichtDevice.CreateDevice(
                DriverType.Direct3D9,
                new Dimension2Di(800, 600),
                32, false, false, true);

            Device.SetWindowCaption("Canardstein 3D");
            Device.OnEvent += Evenement;
            SceneNode cube = Device.SceneManager.AddCubeSceneNode(1, null, 0, new Vector3Df(2, 0, 0), new Vector3Df(0, 45, 0));
            cube.SetMaterialFlag(MaterialFlag.Lighting, false);
            CameraSceneNode camera = Device.SceneManager.AddCameraSceneNode(null, new Vector3Df(0, 0, 0), new Vector3Df(2, 0, 0));
            while (Device.Run())
            {

                Device.VideoDriver.BeginScene(ClearBufferFlag.Color | ClearBufferFlag.Depth, Color.OpaqueMagenta);
                Device.SceneManager.DrawAll();
                Device.VideoDriver.EndScene();
            }
        }

        private bool Evenement(Event e)
        {
            if (e.Type == EventType.Key)
            {
                switch (e.Key.Key)
                {
                    case KeyCode.KeyZ: K_Avant = e.Key.PressedDown; break;
                    case KeyCode.KeyS: K_Arriere = e.Key.PressedDown; break;
                    case KeyCode.KeyQ: K_Gauche = e.Key.PressedDown; break;
                    case KeyCode.KeyD: K_Droite = e.Key.PressedDown; break;
                }
            }
            return false;
        }
    }
}

