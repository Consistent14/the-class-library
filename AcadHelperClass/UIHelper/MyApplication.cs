//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.Ribbon;
//using Autodesk.Windows;
//using Autodesk.AutoCAD.Runtime;

//[assembly: ExtensionApplication(typeof(MyExtension.MyApplication))]

//namespace MyExtension
//{
//    // IExtensionApplication showing example
//    // usage of the RibbonHelper class:

//    public class MyApplication : IExtensionApplication
//    {
//        public void Initialize()
//        {
//            try
//            {
//                /// When the ribbon is available, do
//                /// initialization for it:
//                RibbonHelper.OnRibbonFound(this.SetupRibbon);

//                /// TODO: Initialize your app
//            }
//            catch (System.Exception ex)
//            {
//                Document doc = Application.DocumentManager.MdiActiveDocument;
//                if (doc != null)
//                    doc.Editor.WriteMessage(ex.ToString());
//            }
//        }

//        public void Terminate()
//        {
//        }

//        /// A method that performs one-time setup
//        /// of Ribbon components:
//        void SetupRibbon(RibbonControl ribbon)
//        {
//            /// TODO: Place ribbon initialization code here

//            /// Example only: Verify our method was called
//            Application.ShowAlertDialog("SetupRibbon() called");
//        }
//    }
//}

///// RibbonHelper class
//namespace Autodesk.AutoCAD.Ribbon
//{
//    public class RibbonHelper
//    {
//        Action<RibbonControl> action = null;
//        bool idleHandled = false;
//        bool created = false;

//        RibbonHelper(Action<RibbonControl> action)
//        {
//            if (action == null)
//                throw new ArgumentNullException("initializer");
//            this.action = action;
//            SetIdle(true);
//        }

//        /// <summary>
//        /// 
//        /// Pass a delegate that takes the RibbonControl
//        /// as its only argument, and it will be invoked
//        /// when the RibbonControl is available. 
//        /// 
//        /// If the RibbonControl exists when the constructor
//        /// is called, the delegate will be invoked on the
//        /// next idle event. Otherwise, the delegate will be
//        /// invoked on the next idle event following the 
//        /// creation of the RibbonControl.
//        /// 
//        /// </summary>

//        public static void OnRibbonFound(Action<RibbonControl> action)
//        {
//            new RibbonHelper(action);
//        }

//        void SetIdle(bool value)
//        {
//            if (value ^ idleHandled)
//            {
//                if (value)
//                    Application.Idle += idle;
//                else
//                    Application.Idle -= idle;
//                idleHandled = value;
//            }
//        }

//        void idle(object sender, EventArgs e)
//        {
//            SetIdle(false);
//            if (action != null)
//            {
//                var ps = RibbonServices.RibbonPaletteSet;
//                if (ps != null)
//                {
//                    var ribbon = ps.RibbonControl;
//                    if (ribbon != null)
//                    {
//                        action(ribbon);
//                        action = null;
//                    }
//                }
//                else if (!created)
//                {
//                    created = true;
//                    RibbonServices.RibbonPaletteSetCreated +=
//                       ribbonPaletteSetCreated;
//                }
//            }
//        }

//        void ribbonPaletteSetCreated(object sender, EventArgs e)
//        {
//            RibbonServices.RibbonPaletteSetCreated
//               -= ribbonPaletteSetCreated;
//            SetIdle(true);
//        }
//    }
//}

