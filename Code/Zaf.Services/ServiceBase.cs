using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zaf.Services
{
    public class ServiceBase : IDisposable
    {
        private bool isDisposed;

        public ServiceBase()
        {
            this.isDisposed = false;
        }

        /// <summary>
        ///     IDisposable interface required method.
        ///     This function should explicitly free all unmanaged resources and call
        ///     Dispose() on any encapsulated objects that also implement the IDisposable
        ///     interface. In this way, the Dispose() method provides precise control
        ///     over when unmanaged resources are freed.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // Use SuppressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     This function is called by both the destructor and by the 
        ///     IDisposable.Dispose(). The point of this approach is to ensure
        ///     that all cleanup code is in one place.
        /// </summary>
        /// <param name="disposing">Indicates that objects should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these
            // operations, as well as in your methods that use the resource.
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.DisposeInternal();
                }

                this.isDisposed = true;
            }
        }

        /// <summary>
        ///     This should only be called when intending to completely
        ///     destroy the object.
        /// </summary>
        protected virtual void DisposeInternal()
        {
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
