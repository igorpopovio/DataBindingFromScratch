using System;
using System.Windows;
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

            var value = dataContext.GetType()
                .GetProperty(Path)
                .GetValue(dataContext);

            return $"{value}";
        }
    }
}
