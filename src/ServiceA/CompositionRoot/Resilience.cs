using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using Serilog;

namespace ServiceA.CompositionRoot;

public static class Resilience
{
    public static IServiceCollection AddResilience(this IServiceCollection services) =>
        services
           .AddResiliencePipeline(
                "default-resilience-pipeline",
                x => x.AddRetry(
                    new ()
                    {
                        Name = "3-retries-with-jitter",
                        Delay = TimeSpan.FromSeconds(1),
                        BackoffType = DelayBackoffType.Linear,
                        MaxRetryAttempts = 3,
                        UseJitter = true,
                        OnRetry = arguments =>
                        {
                            if (arguments.Outcome.Exception is not null)
                            {
                                Log.Warning(
                                    arguments.Outcome.Exception,
                                    "Request failed on attempt {Attempt}, retrying...",
                                    arguments.AttemptNumber
                                );
                            }
                            else
                            {
                                Log.Warning(
                                    "Request failed on attempt {Attempt}, retrying...",
                                    arguments.AttemptNumber
                                );
                            }

                            return ValueTask.CompletedTask;
                        }
                    }
                )
            )
           .AddTransient<ResiliencePipeline>(
                sp => sp.GetRequiredService<ResiliencePipelineProvider<string>>()
                   .GetPipeline("default-resilience-pipeline")
            );
}
