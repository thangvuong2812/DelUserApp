using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using static DelUserApp.View01;

namespace DelUserApp
{
    public class BindableStringBuilder : INotifyPropertyChanged
    {
        private readonly StringBuilder _builder = new StringBuilder();

        private EventHandler<EventArgs> TextChanged;

        public string Text
        {
            get { return _builder.ToString(); }
        }


        public int Count
        {
            get { return _builder.Length; }
        }

        public void Append(string text)
        {
            _builder.Append(text);
            if (TextChanged != null)
                TextChanged(this, null);
            RaisePropertyChanged(() => Text);
        }

        public void AppendLine(string text, TypeLog typeLog)
        {
            switch (typeLog)
            {
                case TypeLog.Error:
                    _builder.AppendLine(new Run(text) { Foreground = Brushes.Red }.Text);
                    break;
                case TypeLog.Warning:
                    _builder.AppendLine(new Run(text) { Foreground = Brushes.Yellow }.Text);

                    break;
                case TypeLog.Info:
                    _builder.AppendLine(new Run(text) { Foreground = Brushes.BlueViolet }.Text);

                    break;
                case TypeLog.Success:
                    _builder.AppendLine(new Run(text) { Foreground = Brushes.Green }.Text);
                    break;
                default: break;

            }
            
            if (TextChanged != null)
                TextChanged(this, null);
            RaisePropertyChanged(() => Text);
        }

        public void Clear()
        {
            _builder.Clear();
            if (TextChanged != null)
                TextChanged(this, null);
            RaisePropertyChanged(() => Text);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                return;
            }

            var handler = PropertyChanged;

            if (handler != null)
            {
                var body = propertyExpression.Body as MemberExpression;
                if (body != null)
                    handler(this, new PropertyChangedEventArgs(body.Member.Name));
            }
        }

        #endregion

    }
}
