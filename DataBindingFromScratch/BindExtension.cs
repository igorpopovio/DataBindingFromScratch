using System;
using System.Windows.Markup;

namespace DataBindingFromScratch
{
    public class BindExtension : MarkupExtension
    {
        public string Message { get; set; }

        public BindExtension(string message)
        {
            Message = message;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Message;
        }
    }
}
