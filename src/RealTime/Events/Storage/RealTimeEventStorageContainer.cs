﻿// <copyright file="RealTimeEventStorageContainer.cs" company="dymanoid">
// Copyright (c) dymanoid. All rights reserved.
// </copyright>

namespace RealTime.Events.Storage
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("RealTimeEventStorage")]
    public sealed class RealTimeEventStorageContainer
    {
        [XmlAttribute]
        public int Version { get; set; } = 1;

        [XmlElement]
        public long EarliestEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "XML serialization")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "XML serialization")]
        [XmlArray("Events")]
        [XmlArrayItem("RealTimeEventStorage")]
        public List<RealTimeEventStorage> Events { get; set; } = new List<RealTimeEventStorage>();
    }
}
