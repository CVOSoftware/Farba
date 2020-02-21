using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Farba.ViewModel;
using Farba.Common.Export.Base;
using Farba.Enum;
using Farba.Extension;

namespace Farba.Common.Export
{
    class ExportToHtml : IExport
    {
        #region Field

        private string Filename;

        private string exportData;

        private PaletteViewModel Palette;

        #endregion

        #region Constructor

        public ExportToHtml(string filePath, PaletteViewModel palette)
        {
            Filename = filePath;
            Palette = palette;
        }

        #endregion

        #region Implemented IExport

        public void Export()
        {
            PrepareExportData();
            WriteToFile();
        }

        #endregion

        #region Private

        private void PrepareExportData()
        {
            var data = Palette.Image.UriSource.AbsolutePath;
            exportData = CreateDocument(Filename);
        }

        private void WriteToFile()
        {
            using (var fileStream = new FileStream(Filename, FileMode.Create, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(exportData);
                }
            }
        }

        #endregion

        #region HTML Template

        private string CreateDocument(string Title)
        {
            return $@"
            <!DOCTYPE html>
            <html>
                <head>
                    <Title>{Title}</Title>
                    <Style>{GenerateCSS()}</Style>
                </head>
                <body>
                    <div class=""wrapper"">
                        <div class=""palette-info"">
                            <div class=""palette-info__palette"">
                                {CreatePaletteInfo()}
                            </div>
                            <div class=""palette-info__image"">
                                {CreateImage()}
                            </div>                         
                        </div>
                        {CreateSetting()}
                        <div class=""color-combination"">
                            {CreateColorCombinationList()}
                        </div>
                    </div>
                </body>
            </html>";
        }

        private string GenerateCSS()
        {
            return @"
            * {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
            }
            .wrapper {
                width: 960px;
                margin: 20px auto;
                padding: 20px;
                background-image: linear-gradient(to top, #d5d4d0 0%, #d5d4d0 1%, #eeeeec 31%, #efeeec 75%, #e9e9e7 100%);
                border: 1px solid #41adeb;
                color:whitesmoke;
                font-size: 14px;
                font-family:'Courier New', Courier, monospace;
            }
            .palette-info
            {
                display: flex;
                margin-bottom: 20px;
            }
            .palette-info__palette {
                width: 30%;
                padding-right: 20px;
                margin-right: 20px;
                border-right: 1px solid #41adeb;
                color:#41adeb;
            }
            .palette-item
            {
                display: flex;
                border: 1px solid #41adeb;
                padding: 10px;
                background: whitesmoke;
                margin-bottom: 5px;
            }
            .palette-item:last-child
            {
                margin-bottom: 0;
            }
            .palette-item__square
            {
                width: 50px;
                height: 50px;
                border: 1px solid #41adeb;
                margin-right: 5px;
            }
            .palette-info__image {
                width: 70%;
                height: 300px;
                text-align: center;
                margin: auto;
            }
            .image {
                position: relative;
                height: 100%;
                width: auto;
                border: 1px solid #41adeb;
            }
            .setting {
                display: flex;
                border: 1px solid #41adeb;
                background: whitesmoke;
                color: #41adeb;
                margin-bottom: 20px;
            }
            .setting-item {
                text-align: center;
                width: 33.33%;
                padding: 10px;
                border-right: 1px solid #41adeb;
            }
            .setting-item:last-child {
                border-right: none;
            }
            .color-combination__item {
                display: flex;
                flex-wrap: wrap;
                margin-bottom: 10px;
                border: 1px solid #41adeb;
            }
            .color-combination__item:last-child {
                margin-bottom: 0;
            }
            .color-combination__description-line
            {
                display: flex;
                border-top: 1px solid #41adeb;
                width: 100%;
            }
            .color-combination__description-space
            {
                width: 45%;
                text-align: center;
                background: #e9edee;
                color: #41adeb;
                padding: 2px;
            }
            .color-combination__description-diff
            {
                width: 10%;
                text-align: center;
                border-left: 1px solid #41adeb;
                border-right: 1px solid #41adeb;
                background: #41adeb;
                padding: 2px;
            }
            .color-combination__square
            {
                width: 50%;
                padding: 10px;
            }
            .color-combination__circle
            {
                display: flex;
                flex-direction: row;
                justify-content: center;
                width: 50%;
                padding: 10px;
            }
            .square
            {
                height: 30px;
            }
            .circle {
                width: 30px;
                height: 30px;
                border-radius: 15px;
                margin:0 10px;
            }
            ";
        }

        private string CreatePaletteInfo()
        {
            var paletteCollection = string.Empty;

            for (var i = 0; i < Palette.Cluster.Count; i++)
            {
                var percent = Palette.Cluster[i].Percent.ToString();
                var hex = Palette.Cluster[i].Hex;
                var rgb = Palette.Cluster[i].Rgb;

                paletteCollection += PaletteInfoItem(percent, hex, rgb);
            }

            return paletteCollection;
        }

        private string PaletteInfoItem(string percent, string hex, string rgb)
        {
            return $@"
            <div class=""palette-item"">
                <div class=""palette-item__square"" style=""background: {hex};""></div>
                <div>
                    <div>Процент: {percent}%</div>
                    <div>HEX: {hex}</div>
                    <div>RGB: {rgb}</div>
                </div>
            </div>
            ";
        }

        private string CreateImage()
        {
            using (var memoryStream = new MemoryStream())
            {
                var pngEncoder = new PngBitmapEncoder();

                pngEncoder.Frames.Add(BitmapFrame.Create(Palette.Image));
                pngEncoder.Save(memoryStream);

                var data = Convert.ToBase64String(memoryStream.GetBuffer());

                return $@"<img class=""image"" src=""data:image/png;base64,{data}""/>";
            }
        }

        private string CreateSetting()
        {
            return $@"
            <div class=""setting"">
                <div class=""setting-item"">Цветовое пространство: {Palette.ColorSpaceType.ToString()}</div>
                <div class=""setting-item"">Цветовая разница: {Palette.ColorDifferenceType.ToString()}</div>
                <div class=""setting-item"">Количество комбинаций: {Palette.CombinationCount}</div>
            </div>";
        }

        private string CreateColorCombinationList()
        {
            var combinatinList = string.Empty;

            for (var i = 0; i < Palette.ColorCombinationList.Count; i++)
            {
                var item = string.Empty;
                var diff = Palette.ColorCombinationList[i].Difference.ToString();
                var spaceOne = Palette.ColorCombinationList[i].ColorSpaceOne;
                var spaceTwo = Palette.ColorCombinationList[i].ColorSpaceOne;
                var colorOne = Palette.ColorCombinationList[i].ColorOne.HexFormat();
                var colorTwo = Palette.ColorCombinationList[i].ColorTwo.HexFormat();

                switch (Palette.ColorCombinationType)
                {
                    case ColorCombinationType.Square:
                        item += CreateSquareCombinatinItem(diff, spaceOne, spaceTwo, colorOne, colorTwo);
                        break;
                    case ColorCombinationType.Circle:
                        item += CreateCircleCombinatinItem(diff, spaceOne, spaceTwo, colorOne, colorTwo);
                        break;
                }

                combinatinList += item;
            }

            return combinatinList;
        }

        private string CreateSquareCombinatinItem(string diff, string spaceOne, string spaceTwo, string colorOne, string colorTwo)
        {
            return $@"
            <div class=""color-combination__item""> 
                {CreateCombinationSquare(colorOne, colorTwo)}
                {CreateCombinationSquare(colorTwo, colorOne)}
                {CreateDescriptionLine(diff, spaceOne, spaceTwo)}
            </div>";
        }

        private string CreateCircleCombinatinItem(string diff, string spaceOne, string spaceTwo, string colorOne, string colorTwo)
        {
            return $@"
            <div class=""color-combination__item""> 
                {CreateCombinationCircle(colorOne, colorTwo)}
                {CreateCombinationCircle(colorTwo, colorOne)}
                {CreateDescriptionLine(diff, spaceOne, spaceTwo)}
            </div>";
        }

        public string CreateCombinationSquare(string colorOne, string colorTwo)
        {
            return $@"
            <div class=""color-combination__square"" style=""background: {colorOne}"">
                {CreateSquare(colorTwo)}
            </div>";
        }

        public string CreateCombinationCircle(string colorOne, string colorTwo)
        {
            var circle = CreateCircle(colorTwo);

            return $@"
            <div class=""color-combination__circle"" style=""background: {colorOne}"">
                {circle}
                {circle}
                {circle}
                {circle}
            </div>";
        }

        public string CreateSquare(string color)
        {
            return $@"<div class=""square"" style=""background: {color};""></div>";
        }

        public string CreateCircle(string color)
        {
            return $@"<div class=""circle"" style=""background: {color};""></div>";
        }

        private string CreateDescriptionLine(string diff, string spaceOne, string spaceTwo)
        {
            return $@"
            <div class=""color-combination__description-line"">
                {CreateDescriptionSpace(spaceOne)}
                {CreateDescriptionDiff(diff)}
                {CreateDescriptionSpace(spaceTwo)}
            </div>
            ";
        }

        private string CreateDescriptionSpace(string space)
        {
            return $@"<div class=""color-combination__description-space"">{space}</div>";
        }

        private string CreateDescriptionDiff(string diff)
        {
            return $@"<div class=""color-combination__description-diff"">{diff}</div>";
        }

        #endregion
    }
}
