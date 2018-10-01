using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DataBindingFromScratch
{
    public class BindExtension : MarkupExtension
    {
        public string Path { get; set; }

        public BindExtension(string path)
        {
            Path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            var targetObject = service.TargetObject as DependencyObject;
            var targetProperty = service.TargetProperty as DependencyProperty;

            var dataContext = typeof(FrameworkElement)
                .GetProperty("DataContext")
                .GetValue(targetObject);

            var context = dataContext as INotifyPropertyChanged;
            context.PropertyChanged += (s, e) =>
            {
                if (Path != e.PropertyName) return;

                var updatedValue = dataContext.GetType()
                    .GetProperty(Path)
                    .GetValue(dataContext);
                targetObject.Dispatcher.Invoke(() =>
                {
                    targetObject.SetValue(targetProperty, $"{updatedValue}");
                });
            };

            DependencyPropertyDescriptor
                .FromProperty(targetProperty, targetObject.GetType())
                .AddValueChanged(targetObject, (sender, args) =>
                {
                    if (sender.GetType() != typeof(TextBox)) return;

                    var val = targetObject.GetValue(targetProperty);
                    dataContext.GetType()
                        .GetProperty(Path)
                        .SetValue(dataContext, val);
                });

            var currentValue = dataContext.GetType()
                .GetProperty(Path)
                .GetValue(dataContext);

            return $"{currentValue}";
        }
    }
}
