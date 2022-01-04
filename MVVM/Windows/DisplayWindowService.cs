using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM.Windows
{
    public sealed class DisplayWindowService
    {
        private readonly Dictionary<Type, Type> _vmToWindowMapping = new Dictionary<Type, Type>();

        private readonly Dictionary<object, Window> _openWindows = new Dictionary<object, Window>();

        public void RegisterWindow<TVm, TWin>()
            where TWin : Window, new()
            where TVm : class
        {
            var vmType = typeof(TVm).IsInterface
                ? throw new ArgumentException("Cannot register interfaces")
                : typeof(TVm);

            _vmToWindowMapping[vmType] = _vmToWindowMapping.ContainsKey(vmType)
                ? throw new InvalidOperationException(
                    $"Type {vmType.FullName} is already registered")
                : typeof(TWin);
        }

        public void Show(object viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException("vm");
            }

            var window = CreateWindowInstanceWithVm(viewModel);

            _openWindows[viewModel] = _openWindows.ContainsKey(viewModel)
                ? throw new InvalidOperationException("UI for this VM is already displayed")
                : window;

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Show();
        }

        public void Show(object viewModel, object closableView)
        {
            Show(viewModel);

            (closableView as Window).Close();
        }

        public async Task ShowDialog(object viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException("ViewModel is null!");
            }

            var window = CreateWindowInstanceWithVm(viewModel);

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            await window.Dispatcher.InvokeAsync(() =>
                window.ShowDialog());
        }

        private Window CreateWindowInstanceWithVm(object viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException("ViewModel is null!");
            }

            Type windowType = null;

            var vmType = viewModel.GetType();

            while (vmType != null && !_vmToWindowMapping.
                TryGetValue(vmType, out windowType))
            {
                vmType = vmType.BaseType;
            }

            if (windowType is null)
            {
                throw new ArgumentException(
                    $"No registered window type for argument type {viewModel.GetType().FullName}");
            }

            var window = (Window)Activator.CreateInstance(windowType);

            window.DataContext = viewModel;

            return window;
        }
    }
}
