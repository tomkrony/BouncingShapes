using Shapes.Extensions;
using Shapes.Helpers;
using Shapes.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Shapes.ViewModels
{
    public enum ShapeType { None, Circle, Rectangle }
    public class ShapesVM : ViewModelBase
    {
        #region Constructor & ItemsSource
        public ShapesVM()
        {
            ShapesItems.Add(new Shape(50, 50, Brushes.Blue, ShapeType.Circle, TimeSpan.FromMilliseconds(100), this));
        }

        public ObservableCollection<Shape> ShapesItems { get; set; } = new ObservableCollection<Shape>();
        #endregion

        #region FillColor and Geometry Edit
        private ICommand _CmdChangeFillColor = null;
        public ICommand CmdChangeFillColor
        {
            get
            {
                if (_CmdChangeFillColor == null)
                {
                    _CmdChangeFillColor = new RelayCommand(
                        (param) =>
                        {
                            if (!(param is Shape shape))
                                return;

                            SelectedShape = shape;
                            shape.UpdateFillColor();
                            shape.UpdateType();
                        },
                        (param) => true
                        );
                }
                return _CmdChangeFillColor;
            }
        }
        private ICommand _CmdCenterItem = null;
        public ICommand CmdCenterItem
        {
            get
            {
                if (_CmdCenterItem == null)
                {
                    _CmdCenterItem = new RelayCommand(
                        (param) =>
                        {
                            var firstItem = ShapesItems.First();
                            firstItem.PlaceItemInCenter();
                        },
                        (param) => true
                        );
                }
                return _CmdCenterItem;
            }
        }

        private Shape _SelectedShape = null;
        public Shape SelectedShape
        {
            get => _SelectedShape;
            set
            {
                if (_SelectedShape != value)
                {
                    _SelectedShape = value;
                    OnPropertyChanged(nameof(SelectedShape));
                }
            }
        }
        #endregion

        #region Animation

        private ICommand _CmdAnimation = null;
        public ICommand CmdAnimation
        {
            get
            {
                if (_CmdAnimation == null)
                {
                    _CmdAnimation = new RelayCommand(
                        (param) =>
                        {
                            if (!(param is Shape shape))
                                return;

                            SelectedShape = shape;
                            shape.Animation();
                        },
                        (param) => true
                        );
                }
                return _CmdAnimation;
            }
        }

        public double CanvasHeight { get; set; }
        public double CanvasWidth { get; set; }
        #endregion

        #region Settings
        private ICommand _CmdEditSettings = null;
        public ICommand CmdEditSettings
        {
            get
            {
                if (_CmdEditSettings == null)
                {
                    _CmdEditSettings = new RelayCommand(
                        (param) =>
                        {
                            var view = new SettingsDlg();
                            var viewModel = new SettingsDlgVM(SelectedShape);
                            view.DataContext = viewModel;
                            view.ShowWindow();

                            if (viewModel.Result == ReturnCode.Ok)
                                UpdateShapeProperties(viewModel);
                        },
                        (param) =>  true
                        );
                }
                return _CmdEditSettings;

            }
        }

        private void UpdateShapeProperties(SettingsDlgVM settingsData)
        {
            if (SelectedShape is null)
            {
                //If no selected shape available then change All shapes properties
                foreach (var shape in ShapesItems)
                {
                    shape.ChangeSpeed(settingsData.MovementSpeed);
                    shape.ChangeSize(settingsData.ShapeSize);
                }
            }
            else
            {
                SelectedShape.ChangeSpeed(settingsData.MovementSpeed);
                SelectedShape.ChangeSize(settingsData.ShapeSize);
            }
        }
        #endregion

        #region Add shapes
        private ICommand _CmdAddShape = null;
        public ICommand CmdAddShape
        {
            get
            {
                if (_CmdAddShape == null)
                {
                    _CmdAddShape = new RelayCommand(
                        (param) =>
                        {
                            if (!(param is ShapeType shapeType))
                                return;

                            switch (shapeType)
                            {
                                case ShapeType.None:

                                    break;
                                case ShapeType.Circle:
                                    ShapesItems.Add(new Shape(50, 50, Brushes.Blue, ShapeType.Circle, TimeSpan.FromMilliseconds(100), this));
                                    break;
                                case ShapeType.Rectangle:
                                    ShapesItems.Add(new Shape(50, 50, Brushes.Blue, ShapeType.Rectangle, TimeSpan.FromMilliseconds(100), this));
                                    break;
                            }
                        },
                        (param) => true
                        );
                }
                return _CmdAddShape;

            }
        }
        #endregion
    }

    public class Shape : ViewModelBase
    {
        #region Private fields
        private bool _MovingRight = true;
        private bool _MovingDown = true;
        private DispatcherTimer _AnimationTimer = new DispatcherTimer();
        #endregion

        #region Constructor and Properties
        public Shape(double height , double width , Brush fillColor , ShapeType type, TimeSpan animationSpeed, ShapesVM mainVM)
        {
            Height = height;
            Width = width;
            FillColor = fillColor;
            Type = type;
            ParentVM = mainVM;
            AnimationInterval = animationSpeed;
            _AnimationTimer.Tick += Timer_Tick;
        }

        public double _Height;
        public double Height 
        {
            get => _Height;
            internal set
            {
                if (_Height != value)
                {
                    _Height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public double _Width;
        public double Width 
        {
            get => _Width;
            internal set
            {
                if (_Width != value)
                {
                    _Width = value;
                    OnPropertyChanged(nameof(Width));
                }
            } 
        }
        public Brush FillColor { get; private set; } = Brushes.Red;

        public double _Top; 
        public double Top 
        {
            get => _Top; 
            private set
            {
                if (_Top != value)
                {
                    _Top = value;
                    OnPropertyChanged(nameof(Top));
                }
            }
        }

        public double _Left; 
        public double Left 
        {
            get => _Left;
            private set
            {
                if (_Left != value)
                {
                    _Left = value;
                    OnPropertyChanged(nameof(Left));
                }
            }
        } 
        public ShapeType Type { get; private set; } = ShapeType.None;
        public ShapesVM ParentVM { get; }

        TimeSpan _AnimationInterval = TimeSpan.Zero;
        public TimeSpan AnimationInterval
        {
            get => _AnimationInterval;
            private set
            {
                if (_AnimationInterval != value)
                {
                    _AnimationInterval = value;
                    _AnimationTimer.Interval = _AnimationInterval;
                    OnPropertyChanged(nameof(AnimationInterval));
                }
            }
        }

        #endregion

        #region Methods
        internal void ChangeSize(double size)
        {
            Height = size;
            Width = size;
        }
        internal void ChangeSpeed(TimeSpan timeSpan)
        {
            AnimationInterval = timeSpan;
        }
        internal void PlaceItemInCenter()
        {
            Top = (ParentVM.CanvasHeight - Height) / 2;
            Left = (ParentVM.CanvasWidth - Width) / 2;
        }

        /// <summary>
        /// Function to update the shape.
        /// </summary>
        internal void UpdateType()
        {
            Type = Type is ShapeType.Circle ? ShapeType.Rectangle :
                    Type is ShapeType.Rectangle ? ShapeType.Circle :
                    ShapeType.None;

            OnPropertyChanged(nameof(Type));
        }

        /// <summary>
        /// Function to update the Fillcolor of Shapes
        /// </summary>
        internal void UpdateFillColor()
        {
            FillColor = FillColor == Brushes.Red ? Brushes.Green :
                        FillColor == Brushes.Green ? Brushes.Blue :
                        Brushes.Red;
            OnPropertyChanged(nameof(FillColor));
        }

        internal void Animation()
        {
            if (_AnimationTimer.IsEnabled)
                _AnimationTimer.Stop();
            else
                _AnimationTimer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Left = _MovingRight ? Left + 5 : Left - 5;
            if (Left >= ParentVM.CanvasWidth - Width)
                _MovingRight = false;
            else if (Left <= 0)
                _MovingRight = true;

            Top = _MovingDown ? Top + 5 : Top - 5;
            if (Top >= ParentVM.CanvasHeight - Height)
                _MovingDown = false;
            else if (Top <= 0)
                _MovingDown = true;
        }
        #endregion
    }
}