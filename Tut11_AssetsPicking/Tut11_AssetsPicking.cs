using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
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
    [FuseeApplication(Name = "Tut11_AssetsPicking", Description = "Yet another FUSEE App.")]
    public class Tut11_AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform _backLeftTransform;
        private Transform _backRightTransform ;
        private Transform _frontLeftTransform; 
    
        private Transform _frontRightTransform;

        private ScenePicker _scenePicker;

        private PickResult  _currentPick;

        private float4 _oldColor;

        private float _winkelX = 1.7f;
       


        // Init is called on startup. 
        public override void Init()
        {
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);


        _scene = AssetStorage.Get<SceneContainer>("tank.fus");

        _backLeftTransform =   _scene.Children.FindNodes(node => node.Name == "backwheelLeft")?.FirstOrDefault()?.GetComponent<Transform>();
        _backRightTransform =  _scene.Children.FindNodes(node => node.Name == "backwheelRight")?.FirstOrDefault()?.GetComponent<Transform>() ;
        _frontLeftTransform =  _scene.Children.FindNodes(node => node.Name == "frontwheelLeft")?.FirstOrDefault()?.GetComponent<Transform>() ;
        _frontRightTransform = _scene.Children.FindNodes(node => node.Name == "frontwheelRight")?.FirstOrDefault()?.GetComponent<Transform>();
            //_scene = CreateScene();
        
            // Create a scene renderer holding the scene above
            
            _sceneRenderer = new SceneRendererForward(_scene);
            _scenePicker = new ScenePicker(_scene);
        }


        // RenderAFrame is called once a frame
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();
            RC.View = float4x4.CreateTranslation(0, 0, 4) * float4x4.CreateRotationX(-(float) Math.Atan(30.0 / 40.0));
            //_baseTransform.Rotation = new float3(0, M.MinAngle(TimeSinceStart), 0);
           
            _backRightTransform.Rotation = _backLeftTransform.Rotation;
            _frontLeftTransform.Rotation=  _backLeftTransform.Rotation;
            _frontRightTransform.Rotation= _backLeftTransform.Rotation;
            // Clear the backbuffer
           RC.Clear(ClearFlags.Color | ClearFlags.Depth);
           
          if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
               
                PickResult newPick = _scenePicker.Pick(RC, pickPosClip).OrderBy(pr => pr.ClipPos.z).FirstOrDefault();

                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        var ef = _currentPick.Node.GetComponent<DefaultSurfaceEffect>();
                        ef.SurfaceInput.Albedo = _oldColor;

                     


                    }
                    if (newPick != null)
                    {
                        var ef = newPick.Node.GetComponent<SurfaceEffect>();
                        _oldColor = ef.SurfaceInput.Albedo;
                        ef.SurfaceInput.Albedo = (float4) ColorUint.LimeGreen;
                    }
                    _currentPick = newPick;
                     
                }




               
            }
          
            if (_currentPick != null){
                Transform currentTransform = _currentPick.Node.GetTransform();
                    switch (_currentPick.Node.Name)
                    {
                        case "kuppel":
                        currentTransform.Rotation =  currentTransform.Rotation + new float3(0,Keyboard.ADAxis * DeltaTime * 5,0);
                        break;
                        case "canon":
                        _winkelX += Keyboard.WSAxis * DeltaTime ;
                        Diagnostics.Debug(_winkelX);
                        if(_winkelX>1f && _winkelX < 1.7f){
                             currentTransform.Rotation =   new float3(_winkelX,0,0);
                        }else if(_winkelX<=1f){
                            _winkelX = 1f;
                        }else if(_winkelX >=1.7f){
                             _winkelX = 1.7f;
                        }
                       
                        break;
    	                case "chassis":
                        break;

                        default:
                        _backLeftTransform.Rotation = currentTransform.Rotation + new float3(Keyboard.WSAxis * DeltaTime * 3f,0,0);
                        break;
                    }
                    

            }
                    
                
         
          
            

            // Setup the camera 
         

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
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