﻿#pragma checksum "..\..\BlackListUI.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49A2147005DDF9AB609D57FF5A77B3DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
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


namespace WpfApplication4 {
    
    
    /// <summary>
    /// BlackListUI
    /// </summary>
    public partial class BlackListUI : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl TC;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MinimumamountTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Okay;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MinimumTextBox;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton1;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView BorderListView;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\BlackListUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkayButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication4;component/blacklistui.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BlackListUI.xaml"
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
            this.TC = ((System.Windows.Controls.TabControl)(target));
            
            #line 6 "..\..\BlackListUI.xaml"
            this.TC.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TC_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MinimumamountTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\BlackListUI.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Okay = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\BlackListUI.xaml"
            this.Okay.Click += new System.Windows.RoutedEventHandler(this.Okay_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MinimumTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.SubmitButton1 = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\BlackListUI.xaml"
            this.SubmitButton1.Click += new System.Windows.RoutedEventHandler(this.SubmitButton1_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BorderListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.OkayButton = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\BlackListUI.xaml"
            this.OkayButton.Click += new System.Windows.RoutedEventHandler(this.OkayButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

