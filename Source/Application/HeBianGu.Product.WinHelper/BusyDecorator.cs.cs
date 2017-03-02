using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HebianGu.Product.WinHelper
{
    class BusyDecorator
    {
        #region 成员属性
        /// <summary>
        /// 条的数量
        /// </summary>
        int _elementCount;

        /// <summary>
        /// 圆的半径
        /// </summary>
        double _radious = 20;

        /// <summary>
        /// 执行动画的DispatcherTimer
        /// </summary>
        DispatcherTimer _animationTimer;

        /// <summary>
        /// 当前条的索引位置
        /// </summary>
        int _currentElementIndex = 0;

        /// <summary>
        /// 需要变换的透明度个数
        /// </summary>
        int _opacityCount;

        /// <summary>
        /// 透明度间的间隔
        /// </summary>
        double _opacityInterval;

        /// <summary>
        /// 透明度
        /// </summary>
        double _opacity;

        /// <summary>
        /// 最小透明度
        /// </summary>
        double _minOpacity;

        /// <summary>
        /// 条的数组
        /// </summary>
        object[] _elements;

        /// <summary>
        /// 画布
        /// </summary>
        private Canvas _canvas;
        #endregion

        private void CreateElements(Canvas canvas, double Left, double Top)
        {
            _elementCount = 12;
            _opacity = 1;
            _minOpacity = 0.3;
            double surplusOpacity = _opacity - _minOpacity;
            _opacityCount = (int)(_elementCount * 0.5);
            _opacityInterval = surplusOpacity / _opacityCount;

            _elements = new object[_elementCount];

            for (int i = 0; i < _elementCount; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(Colors.AliceBlue);
                rect.Width = 15;
                rect.Height = 5;
                rect.RadiusX = 2;
                rect.RadiusY = 2;
                if (i < _opacityCount)
                {
                    rect.Opacity = _opacity - i * _opacityInterval;
                }
                else
                {
                    rect.Opacity = _minOpacity;
                }
                rect.SetValue(Canvas.LeftProperty, Left + _radious * Math.Cos(360 / _elementCount * i * Math.PI / 180));
                rect.SetValue(Canvas.TopProperty, Top - 2.5 - _radious * Math.Sin(360 / _elementCount * i * Math.PI / 180));

                rect.RenderTransform = new RotateTransform(360 - 360 / _elementCount * i, 0, 2.5);
                canvas.Children.Add(rect);

                _elements[i] = rect;
            }

            _currentElementIndex = 0;

        }

        private void _animationTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _currentElementIndex--;
                _currentElementIndex = _currentElementIndex < 0 ? _elements.Length - 1 : _currentElementIndex;
                int opacitiedCount = 0;
                for (int i = _currentElementIndex; i < _currentElementIndex + _elementCount; i++)
                {
                    int j = i > _elements.Length - 1 ? i - _elements.Length : i;

                    if (opacitiedCount < _opacityCount)
                    {
                        ((Rectangle)_elements[j]).Opacity = _opacity - opacitiedCount * _opacityInterval;
                        opacitiedCount++;
                    }
                    else
                    {
                        ((Rectangle)_elements[j]).Opacity = _minOpacity;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        public BusyDecorator(Canvas canvas)
        {
            this._canvas = canvas;
            _animationTimer = new DispatcherTimer();
            _animationTimer.Interval = TimeSpan.FromMilliseconds(40);
            _animationTimer.Tick += new EventHandler(_animationTimer_Tick);

            CreateElements(canvas, canvas.Width / 2, canvas.Height / 2);
        }

        public void StartDecorator()
        {
            _canvas.Visibility = Visibility.Visible;
            _animationTimer.Start();
        }

        public void StopDecorator()
        {
            _canvas.Visibility = Visibility.Hidden;
            _animationTimer.Stop();
        }
    }
}
