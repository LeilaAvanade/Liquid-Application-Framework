// Copyright (c) Avanade Inc. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Liquid.Base;
using Liquid.Interfaces;
using Liquid.Repository;
using Liquid.Tests;
using NSubstitute;
using Xunit;
using FluentValidation;
using Liquid.Repository;
using System;


namespace Liquid.OnAzure.Tests
{
    public class LightRepositoryTests : IDisposable
    {
        private const string ContentType = "text/plain";
        private const string DefaultConnectionString = "UseDevelopmentStorage=true";
        private const string DefaultContainerName = "removecontainer";
        private static readonly IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        private static readonly ILightRepository _fakeLightRepository = Substitute.For<ILightRepository>();

      
        public LightRepositoryTests()
        {
            Workbench.Instance.Reset();

            Workbench.Instance.AddToCache(WorkbenchServiceType.Repository, _fakeLightRepository);
            _fakeLightRepository.Initialize();
            //_fakeLightRepository.


        }

        [Fact]
        public async Task CtorWhenConfigurationIsNullThrows()
        {
            var _entity = new InheritsLightModel
            {
                id = Guid.NewGuid().ToString(),
                TestList = new List<InheritsLightModel> { new InheritsLightModel { id = Guid.NewGuid().ToString(), TestList = new List<InheritsLightModel>() } }
        };
            var result = await _fakeLightRepository.AddOrUpdateAsync<InheritsLightModel>(_entity);

            Assert.NotNull(result);

        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                //_stream?.Dispose();
                //_lightAttachment?.Dispose();
            }
        }


    }
    public class InheritsLightModel : LightModel<InheritsLightModel>
    {
        //this property will not be validated
        public List<InheritsLightModel> TestList { get; set; }

        public override void Validate()
        {
            RuleFor(i => i.TestList.Count).LessThanOrEqualTo(0).WithErrorCode("not_null");
        }
    }
}


