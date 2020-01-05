using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Farba.ViewModel.Base;
using Farba.Common.Clusters;
using Farba.Common.ColorDifference;
using Farba.Common.ColorDifference.Base;
using Farba.Enum;
using Farba.Extension;

namespace Farba.ViewModel
{
    class PaletteViewModel : BaseViewModel
    {
        #region CommandFields

        private RelayCommand sortColorCommand;

        #endregion

        #region ViewModelFields

        private bool isProcess;

        private bool isSort;

        private bool paletteListIsEmpty;

        private string colorCombination;

        private ColorSpaceType colorSpaceType;

        private ColorDifferenceType colorDifferenceType;

        private ColorCombinationType colorCombinationType;

        private List<ClusterColor> cluster;

        private List<ColorCombinationViewModel> colorCombinatinList;

        #endregion

        #region Constructor

        public PaletteViewModel(string fileName, BitmapImage image)
        {
            isProcess = true;
            isSort = true;
            paletteListIsEmpty = false;
            FileName = fileName;
            ColorSpaceType = ColorSpaceType.HEX;
            ColorDifferenceType = ColorDifferenceType.CIE76;
            colorCombinationType = ColorCombinationType.Square;
            Id = Guid.NewGuid();
            Image = image;
            cluster = null;
            colorCombinatinList = null;
        }

        #endregion

        #region CommandProperties

        public ICommand SortColorCommand => RelayCommand.Register(ref sortColorCommand, OnSortColor, CanSortColor);

        #endregion

        #region ViewModelProperties

        public bool IsSort
        {
            get => isSort;
            set => SetValue(ref isSort, value);
        }

        public bool IsProcess
        {
            get => isProcess;
            set => SetValue(ref isProcess, value);
        }

        public bool PaletteListIsEmpty
        {
            get => paletteListIsEmpty;
            set => SetValue(ref paletteListIsEmpty, value);
        }

        public string FileName { get; }

        public string CombinationCount
        {
            get => colorCombination;
            set => SetValue(ref colorCombination, value);
        }

        public ColorSpaceType ColorSpaceType
        {
            get => colorSpaceType;
            set
            {
                if (SetValue(ref colorSpaceType, value)
                    && ColorCombinationList != null)
                {
                    SetColorSpaceValue();
                }
            }
        }

        public ColorDifferenceType ColorDifferenceType
        {
            get => colorDifferenceType;
            set
            {
                if (SetValue(ref colorDifferenceType, value)
                    && ColorCombinationList != null)
                {
                    SetColorDifferenceValue();
                }
            }
        }

        public Guid Id { get; }

        public ColorCombinationType ColorCombinationType
        {
            get => colorCombinationType;
            set => SetValue(ref colorCombinationType, value);
        }

        public BitmapImage Image { get; }

        public IEnumerable<(System.Enum, string)> ColorSpaceTypeCollections => ColorSpaceType.GetAllValuesAndDescriptions();

        public IEnumerable<(System.Enum, string)> ColorDifferenceTypeCollections => ColorDifferenceType.GetAllValuesAndDescriptions();

        public List<ClusterColor> Cluster
        {
            get => cluster;
            set => SetValue(ref cluster, value);
        }

        public List<ColorCombinationViewModel> ColorCombinationList
        {
            get => colorCombinatinList;
            set
            {
                if (SetValue(ref colorCombinatinList, value))
                {
                    SetColorDifferenceValue();
                }
            }
        }

        #endregion

        #region CommandExecuteMethod

        private void OnSortColor(object param)
        {
            ColorCombinationList.Reverse();
            ColorCombinationList = ColorCombinationList.OrderByDescending(
                combinationItem => ColorCombinationList.Count(
                    _ => IsSort ? _.BrushTwo == combinationItem.BrushTwo 
                                : _.BrushOne == combinationItem.BrushOne)).ToList();

            IsSort = !IsSort;
        }

        #endregion

        #region CommandCanExecuteMethods

        private bool CanSortColor(object param)
        {
            return IsProcess
                   && ColorCombinationList != null;
        }

        #endregion

        #region

        public void StartProcess()
        {
            IsProcess = false;
            PaletteListIsEmpty = false;
            Clear();
        }

        #endregion

        #region Private

        private void Clear()
        {
            Cluster = null;
            ColorCombinationList = null;
        }

        private BaseColorDifference GetColorDiffAlgorithm(Color colorOne, Color colorTwo)
        {
            switch (colorDifferenceType)
            {
                case ColorDifferenceType.CIE76:
                    return new CIE76(colorOne, colorTwo);
                case ColorDifferenceType.CIE94:
                    return new CIE94(colorOne, colorTwo);
            }

            return default;
        }

        private void SetColorSpaceValue()
        {
            foreach (var colorComb in ColorCombinationList)
            {
                colorComb.SetColorSpaceText(ColorSpaceType);
            }
        }

        private void SetColorDifferenceValue()
        {
            if (ColorCombinationList != null)
            {
                foreach (var colorComb in ColorCombinationList)
                {
                    var colorDifferenceAlgorithm = GetColorDiffAlgorithm(colorComb.ColorOne, colorComb.ColorTwo);
                    colorComb.Difference = colorDifferenceAlgorithm.CalculateTheDifference();
                }
            }
        }

        #endregion
    }
}
