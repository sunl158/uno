﻿// <auto-generated/>
#pragma warning disable
#nullable enable
namespace TestRepro
{
    /// <inheritdoc/>
    partial class MyViewModel
    {
        /// <inheritdoc cref="m_name"/>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string Name
        {
            get => m_name;
            [global::System.Diagnostics.CodeAnalysis.MemberNotNull("m_name")]
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<string>.Default.Equals(m_name, value))
                {
                    OnNameChanging(value);
                    OnNameChanging(default, value);
                    OnPropertyChanging(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangingArgs.Name);
                    m_name = value;
                    OnNameChanged(value);
                    OnNameChanged(default, value);
                    OnPropertyChanged(global::CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Name);
                }
            }
        }

        /// <summary>Executes the logic for when <see cref="Name"/> is changing.</summary>
        /// <param name="value">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Name"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnNameChanging(string value);
        /// <summary>Executes the logic for when <see cref="Name"/> is changing.</summary>
        /// <param name="oldValue">The previous property value that is being replaced.</param>
        /// <param name="newValue">The new property value being set.</param>
        /// <remarks>This method is invoked right before the value of <see cref="Name"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnNameChanging(string? oldValue, string newValue);
        /// <summary>Executes the logic for when <see cref="Name"/> just changed.</summary>
        /// <param name="value">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Name"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnNameChanged(string value);
        /// <summary>Executes the logic for when <see cref="Name"/> just changed.</summary>
        /// <param name="oldValue">The previous property value that was replaced.</param>
        /// <param name="newValue">The new property value that was set.</param>
        /// <remarks>This method is invoked right after the value of <see cref="Name"/> is changed.</remarks>
        [global::System.CodeDom.Compiler.GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.2.0.0")]
        partial void OnNameChanged(string? oldValue, string newValue);
    }
}