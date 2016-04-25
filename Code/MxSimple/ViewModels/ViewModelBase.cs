namespace MxSimple.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Windows.Controls;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ContentControl view;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ContentControl View
        {
            get
            {
                return this.view;
            }

            set
            {
                if (this.view != value)
                {
                    this.view = value;
                    this.OnPropertyChanged("View");
                }
            }
        }

        /// <summary>
        ///     Helper function to raise synchronous events.
        /// </summary>
        /// <typeparam name="T">Event handler type</typeparam>
        /// <param name="handler">The event handler</param>
        /// <param name="e">The event args</param>
        protected void RaiseEvent<T>(EventHandler<T> handler, T e) where T : EventArgs
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        ///     Helper function to raise synchronous events.
        /// </summary>
        /// <param name="handler">The event handler</param>
        /// <param name="e">The event arguments</param>
        protected void RaiseEvent(EventHandler handler, EventArgs e)
        {
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        ///     Helper function to raise asynchronous events.
        /// </summary>
        /// <typeparam name="T">The event handler type</typeparam>
        /// <param name="handler">The event handler</param>
        /// <param name="e">The event arguments</param>
        protected void RaiseEventAsync<T>(EventHandler<T> handler, T e) where T : EventArgs
        {
            if (handler != null)
            {
                Delegate[] delegates = handler.GetInvocationList();
                foreach (Delegate del in delegates)
                {
                    EventHandler<T> sink = (EventHandler<T>)del;
                    sink.BeginInvoke(this, e, null, null);
                }
            }
        }

        /// <summary>
        ///     Helper function to raise asynchronous events.
        /// </summary>
        /// <param name="handler">The event handler</param>
        /// <param name="e">The event arguments</param>
        protected void RaiseEventAsync(EventHandler handler, EventArgs e)
        {
            if (handler != null)
            {
                Delegate[] delegates = handler.GetInvocationList();
                foreach (Delegate del in delegates)
                {
                    EventHandler sink = (EventHandler)del;
                    sink.BeginInvoke(this, e, null, null);
                }
            }
        }
    }
}
