﻿#pragma checksum "..\..\..\MainForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "32D534C7E3E0011D16F28B1E49A2E7F1D8DF8B86"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace OOP_3 {
    
    
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ShapesCb;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CursorBtn;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ColorCb;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border CanvasRow;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Canvas;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OOP_4;component/mainform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\MainForm.xaml"
            ((OOP_3.MainForm)(target)).SourceInitialized += new System.EventHandler(this.Window_SourceInitialized);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenJSON_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 21 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenBinary_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 22 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenXML_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 25 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveToJSON_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 26 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveToBinary_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 27 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveToXML_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 30 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.LoadModule_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 31 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AboutDeveloper_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 32 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Help_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ShapesCb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\..\MainForm.xaml"
            this.ShapesCb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ShapeCb_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.CursorBtn = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\MainForm.xaml"
            this.CursorBtn.Click += new System.Windows.RoutedEventHandler(this.CursorBtn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 59 "..\..\..\MainForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClearBtn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ColorCb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 64 "..\..\..\MainForm.xaml"
            this.ColorCb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ColorCb_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.CanvasRow = ((System.Windows.Controls.Border)(target));
            return;
            case 16:
            this.Canvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 87 "..\..\..\MainForm.xaml"
            this.Canvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 88 "..\..\..\MainForm.xaml"
            this.Canvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.Canvas_MouseMove);
            
            #line default
            #line hidden
            
            #line 89 "..\..\..\MainForm.xaml"
            this.Canvas.KeyUp += new System.Windows.Input.KeyEventHandler(this.Canvas_KeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

