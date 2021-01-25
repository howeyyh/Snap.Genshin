using System;
using System.Windows;
using System.Windows.Threading;

namespace DGP.Snap.Framework.Extensions
{
    public static class DispatcherObjectExtensions
    {
        /// <summary>
        /// 在与 Func<TResult> 关联的线程上同步执行指定的 Dispatcher。
        /// </summary>
        /// <typeparam name="T">指定委托的返回值类型。</typeparam>
        /// <param name="dispatcherObject"></param>
        /// <param name="func">要通过调度程序调用的委托。</param>
        /// <returns></returns>
        public static T Invoke<T>(this DispatcherObject dispatcherObject, Func<T> func)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                return func();
            }
            else
            {
                return dispatcherObject.Dispatcher.Invoke(func);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcherObject"></param>
        /// <param name="invokeAction"></param>
        public static void Invoke(this DispatcherObject dispatcherObject, Action invokeAction)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (invokeAction == null)
            {
                throw new ArgumentNullException(nameof(invokeAction));
            }

            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                invokeAction();
            }
            else
            {
                dispatcherObject.Dispatcher.Invoke(invokeAction);
            }
        }

        /// <summary> 
        ///   Executes the specified action asynchronously with the DispatcherPriority.Background on the thread that the Dispatcher was created on.
        /// </summary>
        /// <param name="dispatcherObject">The dispatcher object where the action runs.</param>
        /// <param name="invokeAction">An action that takes no parameters.</param>
        /// <param name="priority">The dispatcher priority.</param> 
        public static void BeginInvoke(this DispatcherObject dispatcherObject, Action invokeAction, DispatcherPriority priority = DispatcherPriority.Background)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (invokeAction == null)
            {
                throw new ArgumentNullException(nameof(invokeAction));
            }

            dispatcherObject.Dispatcher.BeginInvoke(priority, invokeAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dispatcherObject"></param>
        /// <param name="invokeAction"></param>
        /// <param name="priority"></param>
        public static void BeginInvoke<T>(this T dispatcherObject, Action<T> invokeAction, DispatcherPriority priority = DispatcherPriority.Background)
            where T : DispatcherObject
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }

            if (invokeAction == null)
            {
                throw new ArgumentNullException(nameof(invokeAction));
            }

            dispatcherObject.Dispatcher?.BeginInvoke(priority, new Action(() => invokeAction(dispatcherObject)));
        }

        /// <summary> 
        ///   Executes the specified action if the element is loaded or at the loaded event if it's not loaded.
        /// </summary>
        /// <param name="element">The element where the action should be run.</param>
        /// <param name="invokeAction">An action that takes no parameters.</param>
        public static void ExecuteWhenLoaded(this FrameworkElement element, Action invokeAction)
        {
            if (element.IsLoaded)
            {
                element.Invoke(invokeAction);
            }
            else
            {
                void ElementLoaded(object o, RoutedEventArgs a)
                {
                    element.Loaded -= ElementLoaded;
                    element.Invoke(invokeAction);
                }

                element.Loaded += ElementLoaded;
            }
        }
    }
}
