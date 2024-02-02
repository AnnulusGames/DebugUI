using UnityEngine;
using UnityEngine.UIElements;

namespace DebugUI
{
    internal sealed class DebugWindowDragManipulator : MouseManipulator
    {
        readonly VisualElement moveTarget;
        readonly VisualElement rectTarget;
        readonly Toggle toggle;
        Vector2 targetStartPosition;
        Vector3 pointerStartPosition;
        bool enabled;

        public DebugWindowDragManipulator(VisualElement moveTarget, VisualElement rectTarget, Toggle toggle)
        {
            this.moveTarget = moveTarget;
            this.rectTarget = rectTarget;
            this.toggle = toggle;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler, TrickleDown.TrickleDown);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler, TrickleDown.TrickleDown);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler, TrickleDown.TrickleDown);
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler, TrickleDown.TrickleDown);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler, TrickleDown.TrickleDown);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler, TrickleDown.TrickleDown);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler, TrickleDown.TrickleDown);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler, TrickleDown.TrickleDown);
        }

        void PointerDownHandler(PointerDownEvent e)
        {
            targetStartPosition = moveTarget.transform.position;
            pointerStartPosition = e.position;
            target.CapturePointer(e.pointerId);
            enabled = true;
        }

        void PointerMoveHandler(PointerMoveEvent e)
        {
            if (enabled && target.HasPointerCapture(e.pointerId))
            {
                var pointerDelta = e.position - pointerStartPosition;
                moveTarget.transform.position = new Vector2(
                    Mathf.Clamp(targetStartPosition.x + pointerDelta.x, moveTarget.parent.contentRect.xMin, moveTarget.parent.contentRect.xMax - rectTarget.contentRect.width),
                    Mathf.Clamp(targetStartPosition.y + pointerDelta.y, moveTarget.parent.contentRect.yMin, moveTarget.parent.contentRect.yMax - rectTarget.contentRect.height)
                );
                toggle.SetEnabled(false);
            }
        }

        void PointerUpHandler(PointerUpEvent e)
        {
            if (enabled && target.HasPointerCapture(e.pointerId))
            {
                target.ReleasePointer(e.pointerId);
                target.schedule.Execute(() =>
                {
                    toggle.SetEnabled(true);
                });
            }
        }

        void PointerCaptureOutHandler(PointerCaptureOutEvent e)
        {
            enabled = false;
        }
    }
}