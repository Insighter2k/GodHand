﻿#pragma checksum "..\..\..\Views\FileView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E24FD715934632BDF4A02AFE336AAEB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace GodHand.Client.Views {
    
    
    /// <summary>
    /// FileView
    /// </summary>
    public partial class FileView : MahApps.Metro.Controls.MetroContentControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSelectFile;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnOpenFile;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSaveFile;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblSelectedFile;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbEncoderTable;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxStartOffset;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbxOffsetLength;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\FileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Collection;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GodHand.Client;component/views/fileview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\FileView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BtnSelectFile = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.BtnOpenFile = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.BtnSaveFile = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.LblSelectedFile = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.CmbEncoderTable = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.TbxStartOffset = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TbxOffsetLength = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Collection = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

