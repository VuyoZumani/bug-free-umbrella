﻿#pragma checksum "..\..\AddNewPlayerToScore.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F1F85C07F2F8EE64D4A44DDEC95BAF8F0B0E7DF9B895639E10527B72710E786"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OutOfYourLeague;
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


namespace OutOfYourLeague {
    
    
    /// <summary>
    /// AddNewPlayerToScore
    /// </summary>
    public partial class AddNewPlayerToScore : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 83 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addplayer;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox goals;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox player;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox teamofplayer;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button main;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button topgoalscorers;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\AddNewPlayerToScore.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoff;
        
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
            System.Uri resourceLocater = new System.Uri("/OutOfYourLeague;component/addnewplayertoscore.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddNewPlayerToScore.xaml"
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
            this.addplayer = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\AddNewPlayerToScore.xaml"
            this.addplayer.Click += new System.Windows.RoutedEventHandler(this.addplayer_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.goals = ((System.Windows.Controls.TextBox)(target));
            
            #line 85 "..\..\AddNewPlayerToScore.xaml"
            this.goals.GotFocus += new System.Windows.RoutedEventHandler(this.goals_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.player = ((System.Windows.Controls.TextBox)(target));
            
            #line 86 "..\..\AddNewPlayerToScore.xaml"
            this.player.GotFocus += new System.Windows.RoutedEventHandler(this.player_GotFocus);
            
            #line default
            #line hidden
            
            #line 86 "..\..\AddNewPlayerToScore.xaml"
            this.player.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.teamofplayer = ((System.Windows.Controls.ComboBox)(target));
            
            #line 87 "..\..\AddNewPlayerToScore.xaml"
            this.teamofplayer.GotFocus += new System.Windows.RoutedEventHandler(this.teamofplayer_GotFocus);
            
            #line default
            #line hidden
            
            #line 87 "..\..\AddNewPlayerToScore.xaml"
            this.teamofplayer.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.teamofplayer_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.main = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\AddNewPlayerToScore.xaml"
            this.main.Click += new System.Windows.RoutedEventHandler(this.main_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.topgoalscorers = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\AddNewPlayerToScore.xaml"
            this.topgoalscorers.Click += new System.Windows.RoutedEventHandler(this.topgoalscorers_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.logoff = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\AddNewPlayerToScore.xaml"
            this.logoff.Click += new System.Windows.RoutedEventHandler(this.logoff_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

