using System;
using System.Collections.Generic;

namespace DebugUI
{
    internal static class MinimalRx
    {
        public static IObservable<TValue> EveryValueChanged<TValue, TObject>(TObject target, Func<TObject, TValue> propertySelector)
            where TObject : class
        {
            var source = new EveryValueChanged<TValue, TObject>(target, propertySelector);
            UpdateDispatcher.Register(source);
            return source;
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
        {
            return Subscribe(source, onNext, null, null);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted, Action<Exception> onError)
        {
            return source.Subscribe(new AnonymousObserver<T>(onNext, onCompleted, onError));
        }

        public static IDisposable AddTo(this IDisposable disposable, ICollection<IDisposable> disposables)
        {
            disposables.Add(disposable);
            return disposable;
        }
    }

    internal sealed class AnonymousObserver<T> : IObserver<T>
    {
        public AnonymousObserver(Action<T> onNext, Action onCompleted, Action<Exception> onError)
        {
            this.onNext = onNext;
            this.onCompleted = onCompleted;
            this.onError = onError;
        }

        readonly Action<T> onNext;
        readonly Action onCompleted;
        readonly Action<Exception> onError;

        public void OnCompleted()
        {
            onCompleted?.Invoke();
        }

        public void OnError(Exception error)
        {
            onError?.Invoke(error);
        }

        public void OnNext(T value)
        {
            onNext?.Invoke(value);
        }
    }

    internal sealed class EveryValueChanged<TValue, TObject> : IUpdateRunnerItem, IObservable<TValue>
        where TObject : class
    {
        public EveryValueChanged(TObject target, Func<TObject, TValue> propertySelector)
        {
            this.target = target;
            this.propertySelector = propertySelector;

            prevValue = propertySelector(target);
        }

        readonly TObject target;
        readonly Func<TObject, TValue> propertySelector;

        readonly List<IObserver<TValue>> observers = new();

        TValue prevValue;

        public IDisposable Subscribe(IObserver<TValue> observer)
        {
            observers.Add(observer);
            return new Subscription(this, observer);
        }

        public bool TryUpdate()
        {
            try
            {
                var value = propertySelector(target); 
                if (!EqualityComparer<TValue>.Default.Equals(prevValue, value))
                {
                    foreach (var observer in observers) observer.OnNext(value);
                    prevValue = value;
                }
            }
            catch (Exception ex)
            {
                foreach (var observer in observers) observer.OnError(ex);
                observers.Clear();
                return false;
            }

            return true;
        }

        sealed class Subscription : IDisposable
        {
            public Subscription(EveryValueChanged<TValue, TObject> source, IObserver<TValue> observer)
            {
                this.source = source;
                this.observer = observer;
            }

            readonly IObserver<TValue> observer;
            readonly EveryValueChanged<TValue, TObject> source;

            public void Dispose()
            {
                source.observers.Remove(observer);
            }
        }
    }
}