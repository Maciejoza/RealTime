﻿// <copyright file="DayTimeCalculator.cs" company="dymanoid">
// Copyright (c) dymanoid. All rights reserved.
// </copyright>

namespace RealTime.Simulation
{
    using System;

    /// <summary>
    /// Calculates the sunrise and sunset time based on the map latitude and current date.
    /// </summary>
    internal sealed class DayTimeCalculator
    {
        private readonly float phase;
        private readonly float halfAmplitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="DayTimeCalculator"/> class.
        /// </summary>
        ///
        /// <param name="latitude">The latitude coordinate to assume for the city.
        /// Valid values are -80° to 80°.</param>
        public DayTimeCalculator(float latitude)
        {
            bool southSemisphere = latitude < 0;

            latitude = Math.Abs(latitude);
            if (latitude > 80f)
            {
                latitude = 80f;
            }

            halfAmplitude = (0.5f + (latitude / 15f)) / 2f;
            phase = southSemisphere ? 0f : (float)Math.PI;
        }

        /// <summary>
        /// Calculates the sunrise and sunset hours for the provided <paramref name="date"/>.
        /// If this object is not properly set up yet (so <see cref="IsReady"/> returns false),
        /// then the out values will be initialized with default empty <see cref="TimeSpan"/>s.
        /// </summary>
        ///
        /// <param name="date">The game date to calculate the sunrise and sunset times for.</param>
        /// <param name="sunriseHour">The calculated sunrise hour (relative to the midnight).</param>
        /// <param name="sunsetHour">The calculated sunset hour (relative to the midnight).</param>
        public void Calculate(DateTime date, out float sunriseHour, out float sunsetHour)
        {
            float modifier = (float)Math.Cos((2 * Math.PI * (date.DayOfYear + 10) / 365.25) + phase);
            sunriseHour = 6f - (halfAmplitude * modifier);
            sunsetHour = 18f + (halfAmplitude * modifier);
        }
    }
}