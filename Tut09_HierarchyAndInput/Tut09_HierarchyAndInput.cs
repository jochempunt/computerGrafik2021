using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
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
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _baseTransform;
        private Transform _bodyTransform;

        private Transform _upperarmTransform;

        private Transform _forearmTransform;

         private Transform _fingerLeftTransform;

         private Transform _fingerUpTransform;

         private Transform _fingerRightTransform;

         private Transform _fingerBottomTransform;

         private float winkel =0;

         


       SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };


              _bodyTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };

            _upperarmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };

            _forearmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 8, 0)
            };


            _fingerLeftTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 1, 0)
            };

             _fingerRightTransform = new Transform
             {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 1, 0)
             };

            _fingerUpTransform =   new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1, 2)
            };


            _fingerBottomTransform =new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1, -2)
            };

            

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                {
                    // grey Base
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Black, float4.Zero),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },
                        Children =
                        {
                            // red body
                            new SceneNode
                            {
                                Components=
                                {
                                    _bodyTransform,
                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.Red, float4.Zero),
                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                },

                                Children = 
                                {
                                    new SceneNode
                                    {
                                        Components =
                                        {
                                             _upperarmTransform,
                                           
                                        },
                                        Children =
                                        {
                                            new SceneNode
                                            {
                                             Components =
                                             {
                                                new Transform{Translation = new float3(0,4,0)},
                                                MakeEffect.FromDiffuseSpecular((float4) ColorUint.Green, float4.Zero),
                                                SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                             }   
                                           
                                            },// forearm
                                            new SceneNode 
                                            {
                                                Components = 
                                                {
                                                    _forearmTransform,
                                                },

                                                Children = 
                                                { 
                                                   new SceneNode
                                                   {
                                                        Components = 
                                                        {
                                                            new Transform{Translation = new float3(-2,4,0)},
                                                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue, float4.Zero),
                                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                        },

                                                         Children = 
                                                         {
                                                             // base of the hand
                                                            new SceneNode
                                                            {
                                                                Components=
                                                                {
                                                                    new Transform{Translation = new float3(0,5,0)},
                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue, float4.Zero),
                                                                    SimpleMeshes.CreateCuboid(new float3(5, 1, 5))  
                                                                },

                                                                Children = 
                                                                {
                                                                    new SceneNode
                                                                    {
                                                                        Components = {
                                                                            _fingerUpTransform,
                                                                        },
                                                                          Children = 
                                                                          {
                                                                              new SceneNode
                                                                                {
                                                                                    Components = 
                                                                                    {
                                                                                        new Transform{Translation = new float3(0,1.5f,0)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                        SimpleMeshes.CreateCuboid(new float3(1, 4, 1))  
                                                                                    }
                                                                                }   
                                                                    
                                                                          }
                                                                    },
                                                                    new SceneNode
                                                                    {
                                                                        Components = 
                                                                        {
                                                                            _fingerLeftTransform
                                                                        },
                                                                        Children= 
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                        new Transform{Translation = new float3(0,1.5f,0)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                        SimpleMeshes.CreateCuboid(new float3(1, 4, 1))  
                                                                                }
                                                                            }
                                                                        }

                                                                     },
                                                                     new SceneNode
                                                                    {
                                                                        Components = 
                                                                        {
                                                                            _fingerRightTransform,
                                                                        },
                                                                        Children= 
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                        new Transform{Translation = new float3(0,1.5f,0)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                        SimpleMeshes.CreateCuboid(new float3(1, 4, 1))  
                                                                                }
                                                                            }
                                                                        }

                                                                     },
                                                                     new SceneNode
                                                                     {
                                                                         Components=
                                                                         {
                                                                             _fingerBottomTransform,
                                                                         },

                                                                         Children= 
                                                                         {
                                                                             new SceneNode
                                                                             {
                                                                                 Components =
                                                                                 {
                                                                                       new Transform{Translation = new float3(0,1.5f,0)},
                                                                                        MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                        SimpleMeshes.CreateCuboid(new float3(1, 4, 1))  
                                                                                 }
                                                                             }
                                                                         }


                                                                     }
                                                                }
                                                            }
                                                    }, 
                                                   

                                                }
                                            }
                                        }
                                    }

                                }

                            
                            }
                        }

                    }
                    }
            }
            };
        
    }

                
            
        


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor =new float4(((float4)ColorUint.DarkRed));

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();
            
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);


            //------------------------ INFO: -----------------------------
            // hoch - runter : pfeiltasten updown
            // links - rechts : pfeiltasten leftright
            // öffnen schliesen : w - s tasten
            //kamera drehen : mausklick + drag





            // base movement

            if(Mouse.LeftButton){
                _camAngle +=   Mouse.Velocity.x * Time.DeltaTime * 0.003f;
            } 
            


            _bodyTransform.Rotation.y +=    Keyboard.LeftRightAxis * Time.DeltaTime;

            _upperarmTransform.Rotation.x += Keyboard.UpDownAxis * Time.DeltaTime;

             _forearmTransform.Rotation.x += Keyboard.UpDownAxis * Time.DeltaTime;

            

            winkel += (Keyboard.WSAxis * Time.DeltaTime);
            if(winkel >= -0.5f && winkel <=0.7f){
             _fingerUpTransform.Rotation.x = winkel;
             _fingerLeftTransform.Rotation.z = -winkel;
            _fingerRightTransform.Rotation.z = winkel;
            _fingerBottomTransform.Rotation.x = -winkel;
            }else if(winkel < -0.5f) {
                winkel = -0.5f;
            }else if(winkel > 0.7f){
                 winkel = 0.7f;
            }

            


            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

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