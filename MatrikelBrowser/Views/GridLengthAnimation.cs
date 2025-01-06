using System;
using System.Windows;
using System.Windows.Media.Animation;


namespace MatrikelBrowser
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));

        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));

        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(MahApps.Metro.Controls.GridLengthAnimation));
        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = From.Value;
            double toValue = To.Value;

            if (fromValue == toValue) return From;

            double progress = animationClock.CurrentProgress!.Value;

            if (EasingFunction != null) progress = EasingFunction.Ease(progress);
            double currentValue = (1 - progress) * fromValue + progress * toValue;
            return new GridLength(currentValue, From.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
        }        
        public GridLengthAnimation() => Completed += onComplete;

        public event Action? AnimationCompleted;
        protected override Freezable CreateInstanceCore() => new GridLengthAnimation();
        public override Type TargetPropertyType => typeof(GridLength);
        private void onComplete(object? sender, EventArgs e) => AnimationCompleted?.Invoke();
    }
}

