﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FonTech.Application.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessage {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessage() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("FonTech.Application.Resources.ErrorMessage", typeof(ErrorMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string InternalServerError {
            get {
                return ResourceManager.GetString("InternalServerError", resourceCulture);
            }
        }
        
        internal static string ReportsNotFound {
            get {
                return ResourceManager.GetString("ReportsNotFound", resourceCulture);
            }
        }
        
        internal static string ReportNotFound {
            get {
                return ResourceManager.GetString("ReportNotFound", resourceCulture);
            }
        }
        
        internal static string ReportAlreadyExists {
            get {
                return ResourceManager.GetString("ReportAlreadyExists", resourceCulture);
            }
        }
        
        internal static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
