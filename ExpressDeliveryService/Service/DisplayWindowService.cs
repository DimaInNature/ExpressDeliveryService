﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ExpressDeliveryService.Service
{
    public class DisplayWindowService
    {
        Dictionary<Type, Type> vmToWindowMapping = new Dictionary<Type, Type>();

        /// <summary> Регистрация связи ViewModel и View.</summary>
        
        public void RegisterWindow<VM, Win>() where Win : Window, new() where VM : class
        {
            var vmType = typeof(VM);

            if (vmType.IsInterface)
            {
                throw new ArgumentException("Cannot register interfaces");
            }

            if (vmToWindowMapping.ContainsKey(vmType))
            {
                throw new InvalidOperationException(
                    $"Type {vmType.FullName} is already registered");
            }
                
            vmToWindowMapping[vmType] = typeof(Win);
        }

        Dictionary<object, Window> openWindows = new Dictionary<object, Window>();

        /// <summary> Открывает окно View переданного в параметр ViewModel.
        /// Если есть соответствие.</summary>
        
        public void Show(object vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }

            if (openWindows.ContainsKey(vm))
            {
                throw new InvalidOperationException(
                    "UI for this VM is already displayed");
            }
                
            var window = CreateWindowInstanceWithVM(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            openWindows[vm] = window;
        }

        /// <summary> Открывает View переданного в параметр ViewModel, как диалоговое.
        /// Если есть соответствие.</summary>
        
        public async Task ShowDialog(object vm)
        {
            var window = CreateWindowInstanceWithVM(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }

        /// <summary> Создание связи View и ViewModel.</summary>
        
        private Window CreateWindowInstanceWithVM(object vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
                
            Type windowType = null;

            var vmType = vm.GetType();

            while (vmType != null && !vmToWindowMapping.TryGetValue(vmType, out windowType))
            {
                vmType = vmType.BaseType;
            }

            if (windowType == null)
            {
                throw new ArgumentException(
                    $"No registered window type for argument type {vm.GetType().FullName}");
            }
            
            var window = (Window)Activator.CreateInstance(windowType);
            window.DataContext = vm;
            return window;
        }
    }
}
