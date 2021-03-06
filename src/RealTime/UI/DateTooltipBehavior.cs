﻿// <copyright file="DateTooltipBehavior.cs" company="dymanoid">
// Copyright (c) dymanoid. All rights reserved.
// </copyright>

namespace RealTime.Tools
{
    using System;
    using System.Globalization;
    using ColossalFramework.UI;
    using UnityEngine;

    /// <summary>
    /// A script that can be attached to any <see cref="UIComponent"/>.
    /// Observes the <see cref="SimulationManager.m_currentGameTime"/> value and sets the tooltip
    /// of the <see cref="UIComponent"/> to the date part of that value. The current
    /// <see cref="CultureInfo"/> is used for string conversion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instantiated by Unity Engine")]
    internal sealed class DateTooltipBehavior : MonoBehaviour
    {
        private UIComponent target;
        private DateTime lastValue;
        private string tooltip;
        private CultureInfo currentCulture = CultureInfo.CurrentCulture;

        public string IgnoredComponentNamePrefix { get; set; }

        public void Translate(CultureInfo cultureInfo)
        {
            currentCulture = cultureInfo ?? throw new ArgumentNullException(nameof(cultureInfo));
            UpdateTooltip(true);
        }

        /// <summary>
        /// <see cref="Start"/> is called on the frame when a script is enabled
        /// just before any of the <see cref="Update"/> methods are called the first time.
        /// </summary>
        public void Start()
        {
            target = gameObject.GetComponent<UIComponent>();
        }

        /// <summary>
        /// <see cref="Update"/> is called every frame, if the <see cref="MonoBehaviour"/> is enabled.
        /// </summary>
        public void Update()
        {
            UpdateTooltip();
        }

        private void UpdateTooltip(bool force = false)
        {
            if (target == null)
            {
                return;
            }

            DateTime newValue = SimulationManager.instance.m_currentGameTime;
            if (lastValue.Date != newValue.Date || force)
            {
                tooltip = newValue.ToString("d", currentCulture);
            }

            lastValue = newValue;

            if (!target.containsMouse)
            {
                return;
            }

            if (!string.IsNullOrEmpty(IgnoredComponentNamePrefix))
            {
                UIComponent hovered = UIInput.hoveredComponent;
                if (hovered != null && hovered.name != null && hovered.name.StartsWith(IgnoredComponentNamePrefix, StringComparison.Ordinal))
                {
                    return;
                }
            }

            target.tooltip = tooltip;
            target.RefreshTooltip();
        }
    }
}
