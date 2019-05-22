using IrrlichtLime;
using IrrlichtLime.Core;
using IrrlichtLime.Scene;
using IrrlichtLime.Video;
using System;

namespace Canardstein
{
    public class Jeu
    {
        private static void Main(string[] args) { Jeu jeu = new Jeu(); }

        private IrrlichtDevice Device;
        private uint DerniereFrame = 0;
        private bool K_Avant, K_Arriere, K_Gauche, K_Droite;
        private double Rotation = 0;
        private Vector3Df VecteurAvant = new Vector3Df(1, 0, 0);
        private Vector3Df VecteurDroite = new Vector3Df(0, 0, -1);

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

            Device.CursorControl.Position = new Vector2Di(400, 300);
            Device.CursorControl.Visible = false;

            while (Device.Run())
            {
                float tempsEcoule = (Device.Timer.Time - DerniereFrame) / 1000f;
                DerniereFrame = Device.Timer.Time;

                if (Device.CursorControl.Position.X != 400)
                {
                    Rotation += (Device.CursorControl.Position.X - 400) * 0.0025;
                    Device.CursorControl.Position = new Vector2Di(400, 300);
                    VecteurAvant = new Vector3Df(
                        x: (float)Math.Cos(Rotation),
                        y: 0,
                        z: -(float)Math.Sin(Rotation));
                    VecteurDroite = VecteurAvant;
                    VecteurDroite.RotateXZby(-90);
                }
                Vector3Df vitesse = new Vector3Df();
                if (K_Avant) vitesse += VecteurAvant;
                else if (K_Arriere) vitesse -= VecteurAvant;
                if (K_Gauche) vitesse -= VecteurDroite;
                else if (K_Droite) vitesse += VecteurDroite;

                vitesse = vitesse.Normalize() * tempsEcoule * 2;

                camera.Position += vitesse;
                camera.Target = camera.Position + VecteurAvant;

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

