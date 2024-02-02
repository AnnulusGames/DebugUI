using System;

namespace DebugUI
{
    internal interface IUpdateRunnerItem
    {
        bool TryUpdate();
    }

    internal sealed class UpdateRunner
    {
        const int InitialSize = 16;
        readonly Action<Exception> unhandledExceptionCallback;

        int tail = 0;
        IUpdateRunnerItem[] items = new IUpdateRunnerItem[InitialSize];

        public UpdateRunner(Action<Exception> unhandledExceptionCallback)
        {
            this.unhandledExceptionCallback = unhandledExceptionCallback;
        }

        public void Add(IUpdateRunnerItem enumerator)
        {
            if (items.Length == tail)
            {
                Array.Resize(ref items, checked(tail * 2));
            }
            items[tail++] = enumerator;
        }

        public void Run()
        {
            var j = tail - 1;
            for (int i = 0; i < items.Length; i++)
            {
                var coroutine = items[i];
                if (coroutine != null)
                {
                    try
                    {
                        if (!coroutine.TryUpdate())
                        {
                            items[i] = null;
                        }
                        else
                        {
                            continue; // next i 
                        }
                    }
                    catch (Exception ex)
                    {
                        items[i] = null;
                        try
                        {
                            unhandledExceptionCallback(ex);
                        }
                        catch { }
                    }
                }

                // find null, loop from tail
                while (i < j)
                {
                    var fromTail = items[j];
                    if (fromTail != null)
                    {
                        try
                        {
                            if (!fromTail.TryUpdate())
                            {
                                items[j] = null;
                                j--;
                                continue; // next j
                            }
                            else
                            {
                                // swap
                                items[i] = fromTail;
                                items[j] = null;
                                j--;
                                goto NEXT_LOOP; // next i
                            }
                        }
                        catch (Exception ex)
                        {
                            items[j] = null;
                            j--;
                            try
                            {
                                unhandledExceptionCallback(ex);
                            }
                            catch { }
                            continue; // next j
                        }
                    }
                    else
                    {
                        j--;
                    }
                }

                tail = i;
                break;

            NEXT_LOOP:
                continue;
            }
        }
    }
}