using System.Security.Claims;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ntech.DisableIntrospection.Data;
using Ntech.DisableIntrospection.Types;
using Ntech.DisableIntrospection.Validations;

namespace Ntech.DisableIntrospection
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CharacterRepository>();
            services.AddSingleton<ReviewRepository>();

            // If you want to disable introspection graphQL
            services.AddQueryExecutor(builder => builder.AddValidationRule<DisableIntrospectionValidationRule>());

            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddServices(sp)

                .AddAuthorizeDirectiveType()

                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddSubscriptionType<SubscriptionType>()
                .AddType<HumanType>()
                .AddType<DroidType>()
                .AddType<EpisodeType>()
                .Create());

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasCountry", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == ClaimTypes.Country))));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets()
               .UseGraphQL("/graphql")
               .UseGraphiQL("/graphql")
               .UsePlayground("/graphql")
               .UseVoyager("/graphql");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Please access endpoint: /graphql?query={__schema{types{name,fields{name}}}}");
                });
            });
        }
    }
}
