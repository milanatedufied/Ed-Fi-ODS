﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.LoadTools.ApiClient;
using EdFi.LoadTools.Engine.Factories;
using EdFi.LoadTools.Engine.Mapping;
using EdFi.LoadTools.Engine.MappingFactories;
using NUnit.Framework;

namespace EdFi.LoadTools.Test.MappingFactories
{
    [TestFixture]
    public class LookupToIdentityMetadataMappingFactoryTests
    {
        private MetadataMapping[] _mappings;
        private List<XmlModelMetadata> _xmlMetadata;

        [OneTimeSetUp]
        public void Setup()
        {
            var ssbuilder = new SchemaSetFactory(new XsdStreamsRetriever(JsonMetadataTests.XmlMetadataConfiguration));
            var xfactory = new XsdMetadataFactory(ssbuilder.GetSchemaSet());

            _xmlMetadata = xfactory.GetMetadata().ToList();

            var factory =
                new LookupToIdentityMetadataMappingFactory(_xmlMetadata, new NameMatchingMetadataMapper());

            _mappings = factory.GetMetadataMappings().ToArray();
        }

        [Test]
        [Category("Run Manually")]
        public void Should_map_lookup_to_identity()
        {
            foreach (var mapping in _mappings.OrderBy(x => x.SourceName))
            {
                var sourceModels = new List<ModelMetadata>();
                MetadataMappingFactoryBase.PopulateModelMetadata(_xmlMetadata, sourceModels, mapping.SourceName);

                var targetModels = new List<ModelMetadata>();
                MetadataMappingFactoryBase.PopulateModelMetadata(_xmlMetadata, targetModels, mapping.TargetName);

                //var unmappedSourceProperties =
                //    sourceModels.Where(m => m.IsSimpleType && mapping.Properties.All(p => p.SourceName != m.PropertyPath))
                //        .ToList();

                //var unmappedTargetProperties =
                //    targetModels.Where(m => m.IsSimpleType && mapping.Properties.All(p => p.TargetName != m.PropertyPath))
                //    .ToList();

                // Uncomment to only see resources with missing mappings
                //if (!unmappedXmlProperties.Any() && !unmappedJsonProperties.Any())
                //    continue;

                Console.WriteLine($"{mapping.SourceName}, {mapping.TargetName}");

                foreach (var p in mapping.Properties.OrderBy(x => x.SourceName))
                {
                    Console.WriteLine(
                        p.IsArray
                            ? $"\t{p.SourceName} ({p.SourceType}[]), {p.TargetName} ({p.TargetType}[])\t{p.MappingStrategy.GetType().Name}"
                            : $"\t{p.SourceName} ({p.SourceType}), {p.TargetName} ({p.TargetType})\t{p.MappingStrategy.GetType().Name}");
                }

                //if (unmappedSourceProperties.Any())
                //{
                //    Console.WriteLine($"\tUnmapped Source Properties:");
                //    foreach (var unmappedProperty in unmappedSourceProperties/*.Select(up => up.PropertyPath).Distinct()*/)
                //    {
                //        Console.WriteLine($"\t\t{unmappedProperty.PropertyPath} ({unmappedProperty.Type}, {unmappedProperty.IsRequired})");
                //    }
                //}

                //if (unmappedTargetProperties.Any())
                //{
                //    Console.WriteLine($"\tUnmapped Target Properties:");
                //    foreach (var unmappedProperty in unmappedTargetProperties/*.Select(up => up.PropertyPath).Distinct()*/)
                //    {
                //        Console.WriteLine($"\t\t{unmappedProperty.PropertyPath} ({unmappedProperty.Type}, {unmappedProperty.IsRequired})");
                //    }
                //}

                Console.WriteLine();
            }
        }
    }
}
