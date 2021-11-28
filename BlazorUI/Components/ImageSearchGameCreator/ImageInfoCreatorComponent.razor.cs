using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorUI;
using BlazorUI.Shared;
using BlazorUI.Models;
using BlazorUI.Services;
using System.Text.Json;

namespace BlazorUI.Components.ImageSearchGameCreator
{
    public partial class ImageInfoCreatorComponent
    {
        // TODO: remove default example?
        string _rawImageUrl = "/imgs/2.jpg"; //string.Empty;
        string _currentImageUrl = string.Empty;
        ImageSize? _currentSrcImageSize = null;
        string _textForUser = string.Empty;
        string _errorTextForUser = "Неправильно! Попробуй ещё раз";
        List<EditingSpot> _points = new();
        EditingSpot? _selectedPoint;
        bool _pointCreationMode = false;
        decimal _defaultAccuracy = 0.05m;
        int _currentAccuracyPercent
        {
            get => (int)(_selectedPoint?.Accuracy * 100 ?? 0);
            set
            {
                if (_selectedPoint != null)
                {
                    _selectedPoint.Accuracy = value / 100m;
                }
            }
        }

        int getTargetImageHeight => 500;
        int getTargetImageWidth => _currentSrcImageSize == null ? 0 : getTargetImageHeight * _currentSrcImageSize.Width / _currentSrcImageSize.Height;
        void OnImageClicked(MouseEventArgs e) => Call.AsSafe(() =>
        {
            if (string.IsNullOrEmpty(_currentImageUrl))
                return;
            if (_pointCreationMode && _currentSrcImageSize != null)
            {
                var srcCoordinateX = (decimal)e.OffsetX;
                var srcCoordinateY = (decimal)e.OffsetY;
                var creatingPoint = new EditingSpot(srcCoordinateX / getTargetImageWidth, srcCoordinateY / getTargetImageHeight, _defaultAccuracy);
                _points.Add(creatingPoint);
                _pointCreationMode = false;
                _selectedPoint = creatingPoint;
            }
        });
        void OnNewImageLoadClicked() => Call.AsSafe(() =>
        {
            OnResetClicked();
            _pointCreationMode = false;
            _currentImageUrl = _rawImageUrl;
        });
        void OnStartAddingButtonClicked() => Call.AsSafe(() =>
        {
            _selectedPoint = null;
            _pointCreationMode = true;
        });
        void OnSavePointButtonClicked() => Call.AsSafe(() =>
        {
            _selectedPoint = null;
            _pointCreationMode = false;
        });
        void OnResetClicked() => Call.AsSafe(() =>
        {
            _selectedPoint = null;
            _points.Clear();
        });
        void OnPointClick(EditingSpot point) => Call.AsSafe(() =>
        {
            if (_pointCreationMode)
            {
                _pointCreationMode = false;
            }

            if (_selectedPoint == point)
            {
                _selectedPoint = null;
                return;
            }

            _selectedPoint = point;
        });
        void OnSaveClicked() => Call.AsSafe(async () =>
        {
            if (!string.IsNullOrEmpty(_currentImageUrl) && _currentSrcImageSize != null)
            {
                var imageInfo = new ImageSpotInfo(_currentImageUrl, _points.Select(p => p.BuildSpot()).ToList(), _textForUser, _errorTextForUser, _currentSrcImageSize);
                var text = JsonSerializer.Serialize(imageInfo, options: new JsonSerializerOptions{WriteIndented = true});
                await _jsFunctions.SaveAs("ImageInfo.json", text);
            }
        });
        void OnImageLoaded() => Call.AsSafe(async () =>
        {
            _currentSrcImageSize = await _jsFunctions.GetImageSize("img_info_creator_img_element");
            await InvokeAsync(StateHasChanged);
        });
        void OnCancelCreateClicked() => Call.AsSafe(() =>
        {
            _pointCreationMode = false;
        });
        void OnRemovePointClicked() => Call.AsSafe(() =>
        {
            if (_selectedPoint != null)
            {
                _points.Remove(_selectedPoint);
                _selectedPoint = null;
            }
        });
        void OnNewFileLoadClicked() => Call.AsSafe(() =>
        {
        });
    }
}