﻿using System.Web.Mvc;
using Castle.DynamicProxy;
using Glimpse.Net.Extensions;

namespace Glimpse.Net.Interceptor
{
    internal class CreateModelInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)

            var controllerContext = (ControllerContext) invocation.Arguments[0];

            var store = controllerContext.BinderStore();
            store.MemberOf = store.CurrentProperty.Name;

            invocation.Proceed();
        }
    }
}
