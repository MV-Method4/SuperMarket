using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Extensions
{
    /// <summary>
    /// This class allows us to add OData to our endpoints in swagger.
    /// </summary>
    public class ODataParametersSwaggerDefinition : IOperationFilter 
    {
        private static readonly Type QueryableType = typeof(IQueryable);

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var responseTypes = context.ApiDescription.SupportedResponseTypes;
            foreach (var responseType in responseTypes)
            {
                // identify if any of the response types of the end point
                // return a queryable type.
                if (responseType.Type.GetInterfaces().Any(i => i == QueryableType))
                {
                    // the schema of our new parameter (string)
                    var schema = new OpenApiSchema { Type = "string" };

                    // $filter
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$filter",
                        Description = "Filter the results using OData syntax.",
                        Required = false, // these are all optional filters, so false.
                        In = ParameterLocation.Query, //specify to pass the parameter in the query
                        Schema = schema
                    });

                    // $orderby
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$orderby",
                        Description = "Order the results using OData syntax",
                        Required = false,
                        In = ParameterLocation.Query,
                        Schema = schema
                    });

                    // $skip
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$skip",
                        Description = "Skip the specified number of entries",
                        Required = false,
                        In = ParameterLocation.Query,
                        Schema = schema
                    });

                    // $top
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$top",
                        Description = "Get the top x number of records",
                        Required = false,
                        In = ParameterLocation.Query,
                        Schema = schema
                    });
                }
            }
        }
    }
}
