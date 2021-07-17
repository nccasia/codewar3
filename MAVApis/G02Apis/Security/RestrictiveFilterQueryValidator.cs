using Microsoft.Data.OData;
using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Query.Validators;

namespace Hvit.Security
{
    public class RestrictiveFilterQueryValidator : FilterQueryValidator
    {
        static readonly string[] allowedProperties = { "ReleaseYear", "Title" };

        public override void ValidateCollectionPropertyAccessNode(
            CollectionPropertyAccessNode collectionPropertyAccessNode,
            ODataValidationSettings settings)
        {
            string propertyName = null;
            if (collectionPropertyAccessNode != null)
            {
                propertyName = collectionPropertyAccessNode.Property.Name;
            }

            if (propertyName != null && !allowedProperties.Contains(propertyName))
            {
                throw new ODataException(
                    String.Format("Filter on {0} not allowed", propertyName));
            }
            base.ValidateCollectionPropertyAccessNode(collectionPropertyAccessNode, settings);
        }
        public override void ValidateAllNode(AllNode allNode,
            ODataValidationSettings settings)
        {
            base.ValidateAllNode(allNode, settings);
        }
    }
}