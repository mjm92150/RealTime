﻿// <copyright file="UnitySliderItem.cs" company="dymanoid">
// Copyright (c) dymanoid. All rights reserved.
// </copyright>

namespace RealTime.UI
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using ColossalFramework.UI;
    using ICities;
    using RealTime.Localization;

    internal sealed class UnitySliderItem : UnityViewItem<UISlider, float>
    {
        private const string LabelName = "Label";
        private const int SliderValueLabelPadding = 20;

        private readonly UILabel valueLabel;
        private readonly SliderValueType valueType;
        private CultureInfo currentCulture;

        public UnitySliderItem(
            UIHelperBase uiHelper,
            string id,
            PropertyInfo property,
            object config,
            float min,
            float max,
            float step,
            SliderValueType valueType)
            : base(uiHelper, id, property, config)
        {
            UIComponent.minValue = min;
            UIComponent.maxValue = max;
            UIComponent.stepSize = step;
            this.valueType = valueType;

            var parentPanel = UIComponent.parent as UIPanel;
            if (parentPanel != null)
            {
                parentPanel.autoLayoutDirection = LayoutDirection.Horizontal;
                parentPanel.autoSize = true;
            }

            if (UIComponent.parent != null)
            {
                valueLabel = UIComponent.parent.AddUIComponent<UILabel>();
                valueLabel.padding.left = SliderValueLabelPadding;
                valueLabel.name = id + LabelName;
                UpdateValueLabel(Value);
            }
        }

        public override void Translate(LocalizationProvider localizationProvider)
        {
            if (localizationProvider == null)
            {
                throw new ArgumentNullException(nameof(localizationProvider));
            }

            UIComponent panel = UIComponent.parent;
            if (panel == null)
            {
                return;
            }

            panel.tooltip = localizationProvider.Translate(UIComponent.name + Constants.Tooltip);

            UILabel label = panel.Find<UILabel>(LabelName);
            if (label != null)
            {
                label.text = localizationProvider.Translate(UIComponent.name);
            }

            currentCulture = localizationProvider.CurrentCulture;
            UpdateValueLabel(Value);
        }

        protected override UISlider CreateItem(UIHelperBase uiHelper, float defaultValue)
        {
            if (uiHelper == null)
            {
                throw new ArgumentNullException(nameof(uiHelper));
            }

            return (UISlider)uiHelper.AddSlider(Constants.Placeholder, defaultValue, defaultValue + 1, 1, defaultValue, ValueChanged);
        }

        protected override void ValueChanged(float newValue)
        {
            base.ValueChanged(newValue);
            if (valueLabel == null)
            {
                return;
            }

            UpdateValueLabel(newValue);
        }

        private void UpdateValueLabel(float value)
        {
            string valueString;
            switch (valueType)
            {
                case SliderValueType.Percentage:
                    valueString = (value / 100).ToString("P0", currentCulture ?? CultureInfo.CurrentCulture);
                    break;

                case SliderValueType.Time:
                    valueString = default(DateTime).AddHours(value).ToString("t", currentCulture ?? CultureInfo.CurrentCulture);
                    break;

                case SliderValueType.Duration:
                    TimeSpan ts = TimeSpan.FromHours(value);
                    valueString = $"{ts.Hours}:{ts.Minutes:00}";
                    break;

                default:
                    return;
            }

            valueLabel.text = valueString;
        }
    }
}
