﻿using System.Net;
using System.Threading.Tasks;
using DynamicTranslator.Model;
using DynamicTranslator.Requests;
using DynamicTranslator.SesliSozluk.Configuration;
using DynamicTranslator.SesliSozluk.Orchestration;
using DynamicTranslator.TestBase;
using NSubstitute;
using RestSharp;
using Shouldly;
using Xunit;

namespace DynamicTranslator.Tests.SesliSozlukTests
{
    public class SesliSozulukMeanFinderTests : FinderTestBase<SesliSozlukMeanFinder, ISesliSozlukTranslatorConfiguration, SesliSozlukTranslatorConfiguration, SesliSozlukMeanOrganizer>
    {
        [Fact]
        public async void Finder_Should_Work()
        {
            TranslatorConfiguration.CanSupport().Returns(true);
            TranslatorConfiguration.IsActive().Returns(true);

            MeanOrganizer.OrganizeMean(Arg.Any<string>()).Returns(Task.FromResult(new Maybe<string>("selam")));

            RestClient.ExecutePostTaskAsync(Arg.Any<RestRequest>()).Returns(Task.FromResult<IRestResponse>(new RestResponse { StatusCode = HttpStatusCode.OK }));

            SesliSozlukMeanFinder sut = ResolveSut();

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

            SesliSozlukMeanFinder sut = ResolveSut();

            var translateRequest = new TranslateRequest("hi", "en");
            TranslateResult response = await sut.FindMean(translateRequest);
            response.IsSuccess.ShouldBe(false);
            response.ResultMessage.ShouldBe(new Maybe<string>());
        }
    }
}
