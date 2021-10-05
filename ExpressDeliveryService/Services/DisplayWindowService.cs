using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ExpressDeliveryService.Services
{
    public sealed class DisplayWindowService
    {
        private readonly Dictionary<Type, Type> _vmToWindowMapping = new Dictionary<Type, Type>();

        private readonly Dictionary<object, Window> _openWindows = new Dictionary<object, Window>();

        /// <summary> Регистрация связи ViewModel и View.</summary>

        public void RegisterWindow<TVm, TWin>() where TWin : Window, new() where TVm : class
        {
            var vmType = typeof(TVm);

            if (vmType.IsInterface)
            {
                throw new ArgumentException("Cannot register interfaces");
            }

            if (_vmToWindowMapping.ContainsKey(vmType))
            {
                throw new InvalidOperationException(
                    $"Type {vmType.FullName} is already registered");
            }
                
            _vmToWindowMapping[vmType] = typeof(TWin);
        }

        /// <summary> Открывает окно View переданного в параметр ViewModel.
        /// Если есть соответствие.</summary>
        
        public void Show(object vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }

            if (_openWindows.ContainsKey(vm))
            {
                throw new InvalidOperationException(
                    "UI for this VM is already displayed");
            }
                
            var window = CreateWindowInstanceWithVm(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            _openWindows[vm] = window;
        }

        /// <summary> Открывает View переданного в параметр ViewModel, как диалоговое.
        /// Если есть соответствие.</summary>
        
        public async Task ShowDialog(object vm)
        {
            var window = CreateWindowInstanceWithVm(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }

        /// <summary> Создание связи View и ViewModel.</summary>
        
        private Window CreateWindowInstanceWithVm(object vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
                
            Type windowType = null;

            var vmType = vm.GetType();

            while (vmType != null && !_vmToWindowMapping.TryGetValue(vmType, out windowType))
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
