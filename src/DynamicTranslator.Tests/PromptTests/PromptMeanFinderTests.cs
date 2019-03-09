﻿using System.Net;
using System.Threading.Tasks;
using DynamicTranslator.Model;
using DynamicTranslator.Prompt.Configuration;
using DynamicTranslator.Prompt.Orchestration;
using DynamicTranslator.Requests;
using DynamicTranslator.TestBase;
using NSubstitute;
using RestSharp;
using Shouldly;
using Xunit;

namespace DynamicTranslator.Tests.PromptTests
{
    public class PromptMeanFinderTests : FinderTestBase<PromptMeanFinder, IPromptTranslatorConfiguration, PromptTranslatorConfiguration, PromptMeanOrganizer>
    {
        [Fact]
        public async void Finder_Should_Work()
        {
            TranslatorConfiguration.CanSupport().Returns(true);
            TranslatorConfiguration.IsActive().Returns(true);

            MeanOrganizer.OrganizeMean(Arg.Any<string>()).Returns(Task.FromResult(new Maybe<string>("selam")));

            RestClient.ExecutePostTaskAsync(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse>(new RestResponse { StatusCode = HttpStatusCode.OK }));

            PromptMeanFinder sut = ResolveSut();

            var translateRequest = new TranslateRequest("hi", "en");
            TranslateResult response = await sut.FindMean(translateRequest);
            response.IsSuccess.ShouldBe(true);
            response.ResultMessage.ShouldBe(new Maybe<string>("selam"));
        }

        [Fact]
        public async void Finder_Should_Return_Empty_If_NotEnabled()
        {
            TranslatorConfiguration.CanSupport().Returns(false);
            TranslatorConfiguration.IsActive().Returns(false);

            PromptMeanFinder sut = ResolveSut();

            var translateRequest = new TranslateRequest("hi", "en");
            TranslateResult response = await sut.FindMean(translateRequest);
            response.IsSuccess.ShouldBe(false);
            response.ResultMessage.ShouldBe(new Maybe<string>());
        }
    }
}
