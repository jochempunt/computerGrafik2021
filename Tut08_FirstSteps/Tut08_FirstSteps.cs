using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Engine.Core.Effects;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.GUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        // Init is called on startup. 
        private SceneContainer _scene;// haupt komponente scenegraph
        private SceneRendererForward _sceneRenderer;

        private Transform _cubeTransform1;

        private Transform _cubeTransform2;

        private Transform _cubeTransform3;

        private Transform _cubeTransform4;
    
        private DefaultSurfaceEffect _cubeshader;

        private DefaultSurfaceEffect _cubeshader2;

        private Mesh _cubeMesh;


        private SceneNode node;

        public override void Init()
        {
            // Set the clear color for the backbuffer to "greenery"
            RC.ClearColor =  (float4) (ColorUint.Black);
            // Create a scene with a cube
        // The three components: one Transform, one ShaderEffect (blue material) and the Mesh
           /* _cubeTransform1 = new Transform {
                Translation = new float3(-10, 0, 40),
                Rotation = new float3(0,0.3f,0),
                };
            _cubeshader = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Cyan, float4.Zero);
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
        

            _cubeTransform2 = new Transform{
                Translation = new float3(10,0,40),
                Rotation = new float3(0,0.3f,0),
            };


        // Assemble the cube node containing the three components

            var Cubenode2 = new SceneNode();
            Cubenode2.Components.Add(_cubeTransform2);
            Cubenode2.Components.Add(_cubeshader);
            Cubenode2.Components.Add(cubeMesh);
            
            var cubeNode = new SceneNode(); //Hauptnode
            cubeNode.Components.Add(_cubeTransform1);// kinderKomponenten werden gesetzt
            cubeNode.Components.Add(_cubeshader);
            cubeNode.Components.Add(cubeMesh);

        // Create the scene containing the cube as the only object
            _scene = new SceneContainer();
            _scene.Children.Add(cubeNode);
           _scene.Children.Add(Cubenode2);                                // in childrenliste der szene einreihen
*/
        // Create a scene renderer holding the scene above
            
            
            _cubeTransform1 = new Transform {
                Translation = new float3(-10, 0, 70),
                Rotation = new float3(0,0.3f,0),
                };
            _cubeshader = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Cyan, float4.Zero);
            _cubeshader2 = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Cyan, float4.Zero);
            
            _cubeTransform2 = new Transform{
                Translation = new float3(10,0,70),
                Rotation = new float3(0,0.3f,0),
            };

            _cubeTransform3 = new Transform{
                Translation = new float3(0,0,70),
                Rotation = new float3(0,0.3f,0),
            };

            _cubeTransform4 = new Transform {
                Translation = new float3(0,0,70),
                Rotation = new float3(0,0.3f,0),
            };

            _cubeMesh = SimpleMeshes.CreateCuboid(new float3(6,6,6));


            node = new SceneNode{
                Components =
                {
                     _cubeTransform4,
                     _cubeshader2,
                     _cubeMesh,
                    
                }
            };

            _scene = new SceneContainer
            { 
                Children = 
                {
                    new SceneNode 
                    {
                        Components = 
                        {
                            _cubeTransform1,
                            _cubeshader,
                            SimpleMeshes.CreateCuboid(new float3(3,3,3))
                        }
                    },
                    new SceneNode
                    {
                        Components = 
                        {
                            _cubeTransform2,
                            _cubeshader,
                            SimpleMeshes.CreateCuboid(new float3(3,3,3))
                        }
                    },
                    new SceneNode
                    {
                        Components =
                        {
                            _cubeTransform3,
                            _cubeshader,
                            SimpleMeshes.CreateCuboid(new float3(3,3,3))
                        }
                    },
                    node
                }

            };
            

            _sceneRenderer = new SceneRendererForward(_scene);//renderer der einzelne pixel einfärben kann 
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth); // depth buffer, tiefe


            _cubeTransform1.Rotation = _cubeTransform1.Rotation + new float3(0,90 *(M.Pi /180.0f) * Time.DeltaTime,0); // umrechnung von pro fram in pro sekunde
            _cubeTransform1.Translation.x = 20* M.Sin(2* Time.TimeSinceStart);
            _cubeTransform1.Translation.y = 20*M.Cos(2* Time.TimeSinceStart);
            _cubeTransform2.Rotation = _cubeTransform2.Rotation - new float3(0,90 *(M.Pi /180.0f) * Time.DeltaTime ,0);
            Diagnostics.Debug("Keyboard <-> Axis: " + Keyboard.LeftRightAxis);
           

           _cubeTransform2.Translation.x = 20* M.Sin(2* Time.TimeSinceStart);
           _cubeTransform2.Translation.z = 20*M.Cos(2* Time.TimeSinceStart)+70;


           _cubeTransform3.Rotation = _cubeTransform2.Rotation;
            _cubeTransform4.Rotation =  _cubeTransform2.Rotation;


           _cubeTransform3.Translation.y =  20*M.Sin(2* Time.TimeSinceStart);
           _cubeTransform3.Translation.z =  20*M.Cos(-2* Time.TimeSinceStart)+70;
           
           _cubeTransform4.Scale.x = (0.3f * M.Sin(2*Time.TimeSinceStart)) + 1;
           _cubeTransform4.Scale.y = (0.3f * M.Sin(2*Time.TimeSinceStart)) + 1;
           _cubeTransform4.Scale.z = (0.3f * M.Sin(2*Time.TimeSinceStart)) +1+ (0.5f * Keyboard.LeftRightAxis);
           
           
           if( Mouse.LeftButton ){
                 _cubeshader2.SurfaceInput.Albedo = new float4(0.5f + 0.5f *M.Sin(2*Time.TimeSinceStart),0,0.5f,1);
           }else{
               _cubeshader.SurfaceInput.Albedo =  new float4(0.5f +0.5f * M.Cos(2 * Time.TimeSinceStart), 0.5f +0.5f * M.Sin(4 * Time.TimeSinceStart),0.5f +0.5f * M.Cos(2 * Time.TimeSinceStart),1);
           }

         
           
           
           
           
            _sceneRenderer.Render(RC);

            //hier spielt die musik

           // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
            // Set the rendering area to the entire window size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }        

    }
}