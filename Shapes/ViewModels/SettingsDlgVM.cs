using Shapes.Helpers;
using Shapes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shapes.ViewModels
{
    public enum ReturnCode { Cancel, Ok}
    public class SettingsDlgVM : ViewModelBase
    {
        public SettingsDlgVM(Shape model)
        {
            ShapeModel = model;
            if (ShapeModel != null)
            {
                _MovementSpeed = ShapeModel.AnimationInterval;
                _ShapeSize = ShapeModel.Height;
            }
        }

        internal Shape ShapeModel { get; }
        public Window Dialogue { get; }
        internal ReturnCode Result { get; private set; }

        private ICommand _CmdClick = null;
        public ICommand CmdClick
        {
            get
            {
                if (_CmdClick == null)
                {
                    _CmdClick = new RelayCommand(
                        (param) =>
                        {
                            if (!(param is ReturnCode returnCode))
                                return;

                            Result = returnCode;
                        },
                        (param) =>
                        {
                            if (param is ReturnCode returnCode && returnCode is ReturnCode.Cancel)
                                return true;

                            return !HasValidationErrors;
                        }
                        );
                }
                return _CmdClick;

            }
        }

        private TimeSpan _MovementSpeed = TimeSpan.Zero;
        public TimeSpan MovementSpeed
        {
            get => _MovementSpeed ;
            set
            {
                if (_MovementSpeed != value)
                {
                    _MovementSpeed = value;
                    OnPropertyChanged(nameof(MovementSpeed));
                }
            }
        }

        private double _ShapeSize = 0.0;
        public double ShapeSize
        {
            get => _ShapeSize;
            set
            {
                if (_ShapeSize != value)
                {
                    _ShapeSize = value;
                    OnPropertyChanged(nameof(ShapeSize));
                }
            }
        }

    }
}
